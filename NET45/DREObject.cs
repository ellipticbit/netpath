﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;
using ProtoBuf;

namespace System
{
	[DataContract]
	[ProtoBuf.ProtoContract]
	public abstract class DREObject<T> : DeltaObject where T : DREObject<T>
	{
		[NonSerialized, IgnoreDataMember, XmlIgnore] private static readonly System.Collections.Concurrent.ConcurrentDictionary<Guid, T> __dcm;
		static DREObject()
		{
			__dcm = new System.Collections.Concurrent.ConcurrentDictionary<Guid, T>();
		}

		public static T GetDataFromID(Guid ID)
		{
			T t;
			__dcm.TryGetValue(ID, out t);
			return t;
		}

		public static void UpdateValue<TType>(Guid ID, DeltaProperty<TType> prop, TType value)
		{
			T t;
			__dcm.TryGetValue(ID, out t);
			if (t != null) t.SetValue(prop, value);
		}

		public static bool HasData(Guid DataID)
		{
			return __dcm.ContainsKey(DataID); 
		}

		public static T RegisterData(Guid ClientID, T Data)
		{
			Data.__crl.GetOrAdd(ClientID, Data._DREID);
			return __dcm.GetOrAdd(Data._DREID, Data);
		}

		public static T RegisterData(Guid ClientID, Guid DataID)
		{
			T Data;
			__dcm.TryGetValue(DataID, out Data);
			if (Data == null) return null;
			Data.__crl.GetOrAdd(ClientID, Data._DREID);
			return __dcm.GetOrAdd(Data._DREID, Data);
		}

		public static bool UnregisterData(Guid ClientID, Guid DataID)
		{
			T data;
			__dcm.TryGetValue(DataID, out data);
			if (data == null) return true;
			Guid dreid;
			data.__crl.TryRemove(ClientID, out dreid);
			return !data.__crl.IsEmpty || __dcm.TryRemove(DataID, out data);
		}

		protected void SetDREID(string PrimaryKey) { if (_DREID == Guid.Empty) _DREID = HashID.GenerateHashID(PrimaryKey).ToGUID(); }
		protected void SetDREID(byte PrimaryKey) { if (_DREID == Guid.Empty) _DREID = HashID.GenerateHashID(new byte[] { PrimaryKey }).ToGUID(); }
		protected void SetDREID(sbyte PrimaryKey) { if (_DREID == Guid.Empty) _DREID = HashID.GenerateHashID(BitConverter.GetBytes(PrimaryKey)).ToGUID(); }
		protected void SetDREID(short PrimaryKey) { if (_DREID == Guid.Empty) _DREID = HashID.GenerateHashID(BitConverter.GetBytes(PrimaryKey)).ToGUID(); }
		protected void SetDREID(int PrimaryKey) { if (_DREID == Guid.Empty) _DREID = HashID.GenerateHashID(BitConverter.GetBytes(PrimaryKey)).ToGUID(); }
		protected void SetDREID(long PrimaryKey) { if (_DREID == Guid.Empty) _DREID = HashID.GenerateHashID(BitConverter.GetBytes(PrimaryKey)).ToGUID(); }
		protected void SetDREID(ushort PrimaryKey) { if (_DREID == Guid.Empty) _DREID = HashID.GenerateHashID(BitConverter.GetBytes(PrimaryKey)).ToGUID(); }
		protected void SetDREID(uint PrimaryKey) { if (_DREID == Guid.Empty) _DREID = HashID.GenerateHashID(BitConverter.GetBytes(PrimaryKey)).ToGUID(); }
		protected void SetDREID(ulong PrimaryKey) { if (_DREID == Guid.Empty) _DREID = HashID.GenerateHashID(BitConverter.GetBytes(PrimaryKey)).ToGUID(); }
		protected void SetDREID(Guid PrimaryKey) { if (_DREID == Guid.Empty) _DREID = HashID.GenerateHashID(PrimaryKey.ToByteArray()).ToGUID(); }

		[IgnoreDataMember, XmlIgnore] public IEnumerable<Guid> ClientList { get { return __crl.Keys; } }
		[NonSerialized, IgnoreDataMember, XmlIgnore] private ConcurrentDictionary<Guid, Guid> __crl = new ConcurrentDictionary<Guid, Guid>();

		protected DREObject()
		{
			_DREID = Guid.Empty;
		}

		protected DREObject(DependencyObjectEx baseXAMLObject)
			: base(baseXAMLObject)
		{
			_DREID = Guid.Empty;
		}

		protected DREObject(long BatchInterval)
			: base(BatchInterval)
		{
			_DREID = Guid.Empty;
		}

		protected DREObject(DependencyObjectEx baseXAMLObject, long BatchInterval)
			: base(baseXAMLObject, BatchInterval)
		{
			_DREID = Guid.Empty;
		}

		protected override sealed void OnDeserializingBase(StreamingContext context)
		{
			 base.OnDeserializingBase(context);
			__crl = new ConcurrentDictionary<Guid, Guid>();
		}

		[DataMember][ProtoMember(1)] public Guid _DREID { get; private set; }

		protected abstract void BatchUpdates();

		protected sealed override void IncrementChangeCount()
		{
			if (ChangeCount >= BatchInterval) BatchUpdates();
			base.IncrementChangeCount();
		}
	}
}