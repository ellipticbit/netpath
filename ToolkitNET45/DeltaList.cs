﻿using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;

namespace System.Collections.Generic
{
	[Serializable]
	public class DeltaList<T> : IList<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		public delegate void AddRemoveClearedEventHandler(IEnumerable<T> Values);
		public event AddRemoveClearedEventHandler Added;
		public event AddRemoveClearedEventHandler Removed;
		public event AddRemoveClearedEventHandler Cleared;
		public delegate void InsertRemoveAtEventHandler(int Index, IEnumerable<T> Values);
		public event InsertRemoveAtEventHandler RemovedAt;
		public event InsertRemoveAtEventHandler Inserted;
		public delegate void MovedEventHandler(int OldIndex, int NewIndex, T Value);
		public event MovedEventHandler Moved;
		public delegate void ReplacedEventHandler(int Index, T OldValue, T NewValue);
		public event ReplacedEventHandler Replaced;

		private List<T> il;
		[NonSerialized] private readonly ReaderWriterLockSlim ocl;
		[NonSerialized] private ConcurrentQueue<ChangeListItem<T>> dl;

		~DeltaList()
		{
			try
			{
				ocl.Dispose();
			}
			catch { }
		}

		public DeltaList()
		{
			il = new List<T>();
			ocl = new ReaderWriterLockSlim();
			dl = new ConcurrentQueue<ChangeListItem<T>>();
		}

		public DeltaList(int Capacity)
		{
			il = new List<T>(Capacity);
			ocl = new ReaderWriterLockSlim();
			dl = new ConcurrentQueue<ChangeListItem<T>>();
		}

		public DeltaList(IEnumerable<T> Items)
		{
			il = new List<T>(Items);
			ocl = new ReaderWriterLockSlim();
			dl = new ConcurrentQueue<ChangeListItem<T>>();
		}

		public IEnumerable<ChangeListItem<T>> PeekDelta()
		{
			return dl.ToArray();
		}

		public IEnumerable<ChangeListItem<T>> GetDelta()
		{
			var tdl = new List<ChangeListItem<T>>();
			ChangeListItem<T> td;
			while (dl.TryDequeue(out td))
				tdl.Add(td);
			return tdl;
		}

		public void ClearDelta()
		{
			Threading.Interlocked.Exchange(ref dl, new ConcurrentQueue<ChangeListItem<T>>());
		}

		public void Add(T item)
		{
			ocl.EnterWriteLock();
			try
			{
				il.Add(item);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Add, item));
			Added(new[] { item });
		}

		public void AddRange(IEnumerable<T> items)
		{
			ocl.EnterWriteLock();
			try
			{
				il.AddRange(items);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			foreach (var t in items) dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Add, t));
			Added(items);
		}

		public void AddNoUpdate(T item)
		{
			ocl.EnterWriteLock();
			try
			{
				il.Add(item);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Add, item));
		}

		public void AddRangeNoUpdate(IEnumerable<T> items)
		{
			ocl.EnterWriteLock();
			try
			{
				il.AddRange(items);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			foreach (var t in items) dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Add, t));
		}

		public int BinarySearch(T item)
		{
			ocl.EnterReadLock();
			try
			{
				return il.BinarySearch(item);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int BinarySearch(T item, IComparer<T> comparer)
		{
			ocl.EnterReadLock();
			try
			{
				return il.BinarySearch(item, comparer);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int BinarySearch(int start, int count, T item, IComparer<T> comparer)
		{
			ocl.EnterReadLock();
			try
			{
				return il.BinarySearch(start, count, item, comparer);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public void Clear()
		{
			T[] tl;
			ocl.EnterWriteLock();
			try
			{
				tl = ToArray();
				il.Clear();
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			foreach (var t in tl) dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Remove, t));
			Cleared(tl);
		}

		public void ClearNoUpdate()
		{
			T[] tl;
			ocl.EnterWriteLock();
			try
			{
				tl = ToArray();
				il.Clear();
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			foreach (var t in tl) dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Remove, t));
		}

		public bool Contains(T item)
		{
			ocl.EnterReadLock();
			try
			{
				return il.Contains(item);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public void CopyTo(T[] array)
		{
			ocl.EnterReadLock();
			try
			{
				il.CopyTo(array);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			ocl.EnterReadLock();
			try
			{
				il.CopyTo(array, arrayIndex);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public void CopyTo(int index, T[] array, int arrayIndex, int count)
		{
			ocl.EnterReadLock();
			try
			{
				il.CopyTo(index, array, arrayIndex, count);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int Count
		{
			get
			{
				ocl.EnterReadLock();
				try
				{
					return il.Count;
				}
				finally
				{
					ocl.ExitReadLock();
				}
			}
		}

		public bool Exists(Predicate<T> match)
		{
			ocl.EnterReadLock();
			try
			{
				return il.Exists(match);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public T Find(Predicate<T> match)
		{
			ocl.EnterReadLock();
			try
			{
				return il.Find(match);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public IEnumerable<T> FindAll(Predicate<T> match)
		{
			ocl.EnterReadLock();
			try
			{
				return il.FindAll(match);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public T FindLast(Predicate<T> match)
		{
			ocl.EnterReadLock();
			try
			{
				return il.FindLast(match);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int FindIndex(Predicate<T> match)
		{
			ocl.EnterReadLock();
			try
			{
				return il.FindIndex(match);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int FindIndex(int start, Predicate<T> match)
		{
			ocl.EnterReadLock();
			try
			{
				return il.FindIndex(start, match);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int FindIndex(int start, int count, Predicate<T> match)
		{
			ocl.EnterReadLock();
			try
			{
				return il.FindIndex(start, count, match);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int FindLastIndex(Predicate<T> match)
		{
			ocl.EnterReadLock();
			try
			{
				return il.FindLastIndex(match);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int FindLastIndex(int start, Predicate<T> match)
		{
			ocl.EnterReadLock();
			try
			{
				return il.FindLastIndex(start, match);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int FindLastIndex(int start, int count, Predicate<T> match)
		{
			ocl.EnterReadLock();
			try
			{
				return il.FindLastIndex(start, count, match);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public IEnumerable<T> GetRange(int start, int count)
		{
			ocl.EnterReadLock();
			try
			{
				return il.GetRange(start, count);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int IndexOf(T item)
		{
			ocl.EnterReadLock();
			try
			{
				return il.IndexOf(item);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int IndexOf(T item, int start)
		{
			ocl.EnterReadLock();
			try
			{
				return il.IndexOf(item, start);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int IndexOf(T item, int start, int count)
		{
			ocl.EnterReadLock();
			try
			{
				return il.IndexOf(item, start, count);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public void Insert(int index, T item)
		{
			ocl.EnterWriteLock();
			try
			{
				il.Insert(index, item);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Insert, item, index));
			Inserted(index, new[]{item});
		}

		public void InsertRange(int index, IEnumerable<T> items)
		{
			ocl.EnterWriteLock();
			try
			{
				il.InsertRange(index, items);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			int c = index;
			foreach (var t in items) dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Insert, t, c++));
			Inserted(index, items);
		}

		public void InsertNoUpdate(int index, T item)
		{
			ocl.EnterWriteLock();
			try
			{
				il.Insert(index, item);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Insert, item, index));
		}

		public void InsertRangeNoUpdate(int index, IEnumerable<T> items)
		{
			ocl.EnterWriteLock();
			try
			{
				il.InsertRange(index, items);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			int c = index;
			foreach (var t in items) dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Insert, t, c++));
		}

		public int LastIndexOf(T item)
		{
			ocl.EnterReadLock();
			try
			{
				return il.LastIndexOf(item);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int LastIndexOf(T item, int start)
		{
			ocl.EnterReadLock();
			try
			{
				return il.LastIndexOf(item, start);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public int LastIndexOf(T item, int start, int count)
		{
			ocl.EnterReadLock();
			try
			{
				return il.LastIndexOf(item, start, count);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public void Move(int oldindex, int newindex)
		{
			T ti = default(T);
			ocl.EnterWriteLock();
			try
			{
				ti = this[oldindex];
				il.Insert(newindex, ti);
				il.Remove(ti);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Move, ti, newindex));
			Moved(oldindex, newindex, ti);
		}

		public bool Remove(T item)
		{
			ocl.EnterWriteLock();
			bool rt;
			try
			{
				rt = il.Remove(item);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Remove, item));
			Removed(new[] { item });
			return rt;
		}

		public void RemoveAt(int index)
		{
			T ti = default(T);
			ocl.EnterWriteLock();
			try
			{
				ti = this[index];
				il.RemoveAt(index);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Remove, ti, index));
			RemovedAt(index, new[]{ti});
		}

		public void RemoveRange(int index, int count)
		{
			T[] tl;
			ocl.EnterWriteLock();
			try
			{
				tl = GetRange(index, count).ToArray();
				il.RemoveRange(index, count);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			int c = index;
			foreach(var t in tl) dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Remove, t, c++));
			Removed(tl);
		}

		public void MoveNoUpdate(int oldindex, int newindex)
		{
			T ti = default(T);
			ocl.EnterWriteLock();
			try
			{
				ti = this[oldindex];
				il.Insert(newindex, ti);
				il.Remove(ti);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Move, ti, newindex));
		}

		public bool RemoveNoUpdate(T item)
		{
			ocl.EnterWriteLock();
			bool rt;
			try
			{
				rt = il.Remove(item);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Remove, item));
			return rt;
		}

		public void RemoveAtNoUpdate(int index)
		{
			T ti = default(T);
			ocl.EnterWriteLock();
			try
			{
				ti = this[index];
				il.RemoveAt(index);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Remove, ti, index));
		}

		public void RemoveRangeNoUpdate(int index, int count)
		{
			T[] tl;
			ocl.EnterWriteLock();
			try
			{
				tl = GetRange(index, count).ToArray();
				il.RemoveRange(index, count);
			}
			finally
			{
				ocl.ExitWriteLock();
			}
			int c = index;
			foreach (var t in tl) dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Remove, t, c++));
		}
		public T[] ToArray()
		{
			ocl.EnterReadLock();
			try
			{
				return il.ToArray();
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public void TrimExcess()
		{
			ocl.EnterWriteLock();
			try
			{
				il.TrimExcess();
			}
			finally
			{
				ocl.ExitWriteLock();
			}
		}

		public bool TrueForAll(Predicate<T> match)
		{
			ocl.EnterReadLock();
			try
			{
				return il.TrueForAll(match);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public void FromList(IEnumerable<T> range)
		{
			ocl.EnterWriteLock();
			try
			{
				System.Threading.Interlocked.Exchange(ref il, new List<T>(range));
			}
			finally
			{
				ocl.ExitWriteLock();
			}
		}

		public List<T> ToList()
		{
			ocl.EnterReadLock();
			try
			{
				return new List<T>(il);
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public T this[int index]
		{
			get
			{
				ocl.EnterReadLock();
				try
				{
					return il[index];
				}
				finally
				{
					ocl.ExitReadLock();
				}
			}
			set
			{
				T ti = default(T);
				ocl.EnterWriteLock();
				try
				{
					ti = this[index];
					il[index] = value;
				}
				finally
				{
					ocl.ExitWriteLock();
				}
				dl.Enqueue(new ChangeListItem<T>(ListItemChangeMode.Replace, ti));
				Replaced(index, ti, value);
			}
		}

		public void ClearEventFunctions()
		{
			Added = null;
			Removed = null;
			Cleared = null;
			RemovedAt = null;
			Inserted = null;
			Moved = null;
			Replaced = null;
		}

		#region - Interface Implementations -

		public bool IsReadOnly
		{
			get { return false; }
		}

		public IEnumerator<T> GetEnumerator()
		{
			ocl.EnterReadLock();
			try
			{
				return il.GetEnumerator();
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			ocl.EnterReadLock();
			try
			{
				return ((IEnumerable)il).GetEnumerator();
			}
			finally
			{
				ocl.ExitReadLock();
			}
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = "")
		{
			if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
		}

		#endregion
	}
}