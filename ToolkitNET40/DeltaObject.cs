﻿using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace System
{
	[Serializable]
	public abstract class DeltaObject
	{
		[NonSerialized] private readonly ConcurrentDictionary<HashID, object> values;
		[NonSerialized] private readonly ConcurrentQueue<KeyValuePair<HashID, object>> modifications;
		[NonSerialized] private long ChangeCount;
		[XmlIgnore] public long BatchInterval { get; private set; }

		protected DeltaObject()
		{
			modifications = new ConcurrentQueue<KeyValuePair<HashID, object>>();
			values = new ConcurrentDictionary<HashID, object>();
			BatchInterval = -1;
		}

		protected DeltaObject(long BatchInterval)
		{
			modifications = new ConcurrentQueue<KeyValuePair<HashID, object>>();
			values = new ConcurrentDictionary<HashID, object>();
			this.BatchInterval = BatchInterval;
		}

		public T GetValue<T>(DeltaProperty<T> de)
		{
			object value;
			return values.TryGetValue(de.ID, out value) == false ? de.DefaultValue : (T)value;
		}

		internal object GetValue(DeltaPropertyBase de)
		{
			object value;
			return values.TryGetValue(de.ID, out value) == false ? de.defaultValue : value;
		}

		public void SetValue<T>(DeltaProperty<T> de, T value)
		{
			//Call the validator to see if this value is acceptable
			if (de.DeltaValidateValueCallback != null && !de.DeltaValidateValueCallback(this, value)) return;

			//If the new value is the default value remove this from the modified values list, otherwise add/update it.
			if (EqualityComparer<T>.Default.Equals(value, de.DefaultValue))
			{
				//Remove the value from the list, which sets it to the default value.
				object temp;
				values.TryRemove(de.ID, out temp);
				modifications.Enqueue(new KeyValuePair<HashID, object>(de.ID, de.defaultValue));
				IncrementChangeCount();

				//Clear the changed event handlers
				var tt = value as DeltaCollectionBase;
				if (tt != null) tt.ClearChangedHandlers();
				
				//Call the property changed callback
				if (de.DeltaPropertyChangedCallback != null) de.DeltaPropertyChangedCallback(this, (T)temp, de.DefaultValue);
			}
			else
			{
				//Setup the change event handler
				var tt = value as DeltaCollectionBase;
				if (tt != null) tt.Changed += (Sender, Args) => IncrementChangeCount();

				//Update the value
				object temp = values.AddOrUpdate(de.ID, value, (p, v) => value);
				modifications.Enqueue(new KeyValuePair<HashID, object>(de.ID, value));
				IncrementChangeCount();

				//Call the property changed callback
				if (de.DeltaPropertyChangedCallback != null) de.DeltaPropertyChangedCallback(this, (T)temp, value);
			}
		}

		public void UpdateValue<T>(DeltaProperty<T> de, T value)
		{
			//Call the validator to see if this value is acceptable
			if (de.DeltaValidateValueCallback != null && !de.DeltaValidateValueCallback(this, value)) return;

			//If the new value is the default value remove this from the modified values list, otherwise add/update it.
			if (EqualityComparer<T>.Default.Equals(value, de.DefaultValue))
			{
				//Remove the value from the list, which sets it to the default value.
				object temp;
				values.TryRemove(de.ID, out temp);

				//Clear the changed event handlers
				var tt = value as DeltaCollectionBase;
				if (tt != null) tt.ClearChangedHandlers();
			}
			else
			{
				//Setup the change event handler
				var tt = value as DeltaCollectionBase;
				if (tt != null) tt.Changed += (Sender, Args) => IncrementChangeCount();

				//Update the values
				values.AddOrUpdate(de.ID, value, (p, v) => value);
			}
		}

		public void ClearValue<T>(DeltaProperty<T> de)
		{
			object temp;
			if (!values.TryRemove(de.ID, out temp))
			{
				modifications.Enqueue(new KeyValuePair<HashID, object>(de.ID, de.defaultValue));
				IncrementChangeCount();
				var tt = temp as DeltaCollectionBase;
				if (tt != null) tt.ClearChangedHandlers();
			}
			if (de.DeltaPropertyChangedCallback != null)
				de.DeltaPropertyChangedCallback(this, (T) temp, de.DefaultValue);
		}


		public Dictionary<HashID, object> GetNonDefaultValues()
		{
			return values.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
		}

		public Dictionary<HashID, object> GetDeltaValues()
		{
			var dl = new Dictionary<HashID, object>();
			KeyValuePair<HashID, object> tmp;
			while (modifications.TryDequeue(out tmp))
				if (dl.ContainsKey(tmp.Key)) dl[tmp.Key] = tmp.Value;
				else dl.Add(tmp.Key, tmp.Value);
			return dl;
		}

		private void IncrementChangeCount()
		{
			//If the change notification interval is less than zero, do nothing.
			if (BatchInterval < 0) return;
			Threading.Interlocked.Increment(ref ChangeCount);

			//If the change count is greater than the interval run the batch updates.
			//Note that we don't need to use CompareExchange here because we only care if the value is greater-than-or-equal-to the batch interval, not what the exact overage is.
			if (ChangeCount < BatchInterval) return;
			Threading.Interlocked.Exchange(ref ChangeCount, 0);
			BatchUpdates();
		}

		protected abstract void BatchUpdates();
	}
}