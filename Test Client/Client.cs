//---------------------------------------------------------------------------
// This code was generated by a tool. Changes to this file may cause 
// incorrect behavior and will be lost if the code is regenerated.
//
// WCF Architect Version:	2.0.2000.0
// .NET Framework Version:	4.5
//---------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Windows;

namespace Test1
{
	/**************************************************************************
	*	Data Contracts
	**************************************************************************/

	[KnownType(typeof(Guid[]))]
	[System.Diagnostics.DebuggerStepThroughAttribute]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("WCF Architect Service Compiler", "2.0.2000.0")]
	[DataContract(Name = "TestData1", Namespace = "http://tempuri.org/Test1/")]
	public partial class TestData1 : System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged
	{
		public System.Runtime.Serialization.ExtensionDataObject ExtensionData { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		private void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberNameAttribute] string propertyName = "")
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		private string sdlfjField;
		[DataMember(Name = "sdlfj")] public string sdlfj { get { return sdlfjField; } set { if (value != sdlfjField) { sdlfjField = value; NotifyPropertyChanged(); } } }
		private Guid IDField;
		[DataMember(Name = "ID")] public Guid ID { get { return IDField; } set { IDField = value; } }
		private Test1.TestData2 linktestField;
		[DataMember(Name = "linktest")] public Test1.TestData2 linktest { get { return linktestField; } set { linktestField = value; } }
		private ObservableCollection<string> collectiontestField;
		[DataMember(Name = "collectiontest")] public ObservableCollection<string> collectiontest { get { return collectiontestField; } set { if (value != collectiontestField) { collectiontestField = value; NotifyPropertyChanged(); } } }
		private Dictionary<int, Guid> sdfField;
		[DataMember(Name = "sdf")] public Dictionary<int, Guid> sdf { get { return sdfField; } set { sdfField = value; } }
		private SortedDictionary<int, string> sdfggField;
		[DataMember(Name = "sdfgg")] public SortedDictionary<int, string> sdfgg { get { return sdfggField; } set { sdfggField = value; } }
		private List<string> askdjsadjField;
		[DataMember(Name = "askdjsadj")] public List<string> askdjsadj { get { return askdjsadjField; } set { askdjsadjField = value; } }
		private Dictionary<int, string> sdkjfsldjfField;
		[DataMember(Name = "sdkjfsldjf")] public Dictionary<int, string> sdkjfsldjf { get { return sdkjfsldjfField; } set { sdkjfsldjfField = value; } }
		private Guid[] askdjalsdField;
		[DataMember(Name = "askdjalsd")] public Guid[] askdjalsd { get { return askdjalsdField; } set { if (value != askdjalsdField) { askdjalsdField = value; NotifyPropertyChanged(); } } }
	}

	//XAML Integration Object for the TestData1 DTO
	[KnownType(typeof(Guid[]))]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("WCF Architect Service Compiler", "2.0.2000.0")]
	public partial class TestData1XAML : System.Windows.DependencyObject
	{
		//Properties
		public string sdlfj { get { return (string)GetValue(sdlfjProperty); } set { SetValue(sdlfjProperty, value); } }
		public static readonly DependencyProperty sdlfjProperty = DependencyProperty.Register("sdlfj", typeof(string), typeof(Test1.TestData1));
		public Guid ID { get { return (Guid)GetValue(IDProperty); } set { SetValue(IDProperty, value); } }
		public static readonly DependencyProperty IDProperty = DependencyProperty.Register("ID", typeof(Guid), typeof(Test1.TestData1));
		public Test1.TestData2 linktest { get { return (Test1.TestData2)GetValue(linktestProperty); } set { SetValue(linktestProperty, value); } }
		public static readonly DependencyProperty linktestProperty = DependencyProperty.Register("linktest", typeof(Test1.TestData2), typeof(Test1.TestData1));
		public ObservableCollection<string> collectiontest { get { return (ObservableCollection<string>)GetValue(collectiontestProperty); } set { SetValue(collectiontestProperty, value); } }
		public static readonly DependencyProperty collectiontestProperty = DependencyProperty.Register("collectiontest", typeof(ObservableCollection<string>), typeof(Test1.TestData1));
		public Dictionary<int, Guid> sdf { get { return (Dictionary<int, Guid>)GetValue(sdfProperty); } set { SetValue(sdfProperty, value); } }
		public static readonly DependencyProperty sdfProperty = DependencyProperty.Register("sdf", typeof(Dictionary<int, Guid>), typeof(Test1.TestData1));
		public SortedDictionary<int, string> sdfgg { get { return (SortedDictionary<int, string>)GetValue(sdfggProperty); } set { SetValue(sdfggProperty, value); } }
		public static readonly DependencyProperty sdfggProperty = DependencyProperty.Register("sdfgg", typeof(SortedDictionary<int, string>), typeof(Test1.TestData1));
		public List<string> askdjsadj { get { return (List<string>)GetValue(askdjsadjProperty); } set { SetValue(askdjsadjProperty, value); } }
		public static readonly DependencyProperty askdjsadjProperty = DependencyProperty.Register("askdjsadj", typeof(List<string>), typeof(Test1.TestData1));
		public Dictionary<int, string> sdkjfsldjf { get { return (Dictionary<int, string>)GetValue(sdkjfsldjfProperty); } set { SetValue(sdkjfsldjfProperty, value); } }
		public static readonly DependencyProperty sdkjfsldjfProperty = DependencyProperty.Register("sdkjfsldjf", typeof(Dictionary<int, string>), typeof(Test1.TestData1));
		public Guid[] askdjalsd { get { return (Guid[])GetValue(askdjalsdProperty); } set { SetValue(askdjalsdProperty, value); } }
		public static readonly DependencyProperty askdjalsdProperty = DependencyProperty.Register("askdjalsd", typeof(Guid[]), typeof(Test1.TestData1));

		//Implicit Conversion
		public static implicit operator Test1.TestData1(Test1.TestData1XAML Data)
		{
			if (Data == null) return null;
			if (Application.Current.Dispatcher.CheckAccess())
				return TestData1XAML.ConvertFromXAMLObject(Data);
			else
				return (Test1.TestData1)Application.Current.Dispatcher.Invoke(new Func<Test1.TestData1>(() => Test1.TestData1XAML.ConvertFromXAMLObject(Data)), System.Windows.Threading.DispatcherPriority.Normal);
		}
		public static implicit operator Test1.TestData1XAML(Test1.TestData1 Data)
		{
			if (Data == null) return null;
			if (Application.Current.Dispatcher.CheckAccess())
				return TestData1XAML.ConvertToXAMLObject(Data);
			else
				return (Test1.TestData1XAML)Application.Current.Dispatcher.Invoke(new Func<TestData1>(() => Test1.TestData1XAML.ConvertToXAMLObject(Data)), System.Windows.Threading.DispatcherPriority.Normal);
		}

		//Constructors
		public TestData1XAML(Test1.TestData1 Data)
		{
			Type t_DT = Data.GetType();
			FieldInfo fi_sdlfj = t_DT.GetField("sdlfjField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdlfj != null)
				sdlfj = (string)fi_sdlfj.GetValue(Data);
			FieldInfo fi_ID = t_DT.GetField("IDField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_ID != null)
				ID = (Guid)fi_ID.GetValue(Data);
			FieldInfo fi_linktest = t_DT.GetField("linktestField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_linktest != null)
				linktest = (Test1.TestData2)fi_linktest.GetValue(Data);
			FieldInfo fi_collectiontest = t_DT.GetField("collectiontestField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_collectiontest != null)
			{
				ObservableCollection<string> v_collectiontest = fi_collectiontest.GetValue(Data) as ObservableCollection<string>;
				if(v_collectiontest != null)
				{
					ObservableCollection<string> tv = new ObservableCollection<string>();
					foreach(string a in v_collectiontest) { tv.Add(a); }
				}
			}
			FieldInfo fi_sdf = t_DT.GetField("sdfField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdf != null)
			{
				Dictionary<int, Guid> tv = new Dictionary<int, Guid>();
				Dictionary<int, Guid> v_sdf = fi_sdf.GetValue(Data) as Dictionary<int, Guid>;
				if(v_sdf != null)
					foreach(KeyValuePair<int, Guid> a in v_sdf) { tv.Add(a.Key, a.Value); }
			}
			FieldInfo fi_sdfgg = t_DT.GetField("sdfggField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdfgg != null)
			{
				SortedDictionary<int, string> tv = new SortedDictionary<int, string>();
				Dictionary<int, string> v_sdfgg = fi_sdfgg.GetValue(Data) as Dictionary<int, string>;
				if(v_sdfgg != null)
					foreach(KeyValuePair<int, string> a in v_sdfgg) { tv.Add(a.Key, a.Value); }
			}
			FieldInfo fi_askdjsadj = t_DT.GetField("askdjsadjField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_askdjsadj != null)
			{
				List<string> v_askdjsadj = fi_askdjsadj.GetValue(Data) as List<string>;
				if(v_askdjsadj != null)
				{
					List<string> tv = new List<string>();
					foreach(string a in v_askdjsadj) { tv.Add(a); }
				}
			}
			FieldInfo fi_sdkjfsldjf = t_DT.GetField("sdkjfsldjfField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdkjfsldjf != null)
			{
				Dictionary<int, string> tv = new Dictionary<int, string>();
				Dictionary<int, string> v_sdkjfsldjf = fi_sdkjfsldjf.GetValue(Data) as Dictionary<int, string>;
				if(v_sdkjfsldjf != null)
					foreach(KeyValuePair<int, string> a in v_sdkjfsldjf) { tv.Add(a.Key, a.Value); }
			}
			FieldInfo fi_askdjalsd = t_DT.GetField("askdjalsdField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_askdjalsd != null)
			{
				Guid[] v_askdjalsd = fi_askdjalsd.GetValue(Data) as Guid[];
				if(v_askdjalsd != null)
				{
					Guid[] tv = new Guid[v_askdjalsd.GetLength(0)];
					for(int i = 0; i < v_askdjalsd.GetLength(0); i++) { try { tv[i] = v_askdjalsd[i]; } catch { continue; } }
				}
			}
		}

		public TestData1XAML()
		{
		}

		//XAML/DTO Conversion Functions
		public static TestData1 ConvertFromXAMLObject(Test1.TestData1XAML Data)
		{
			Test1.TestData1 DTO = new Test1.TestData1();
			Type t_XAML = Data.GetType();
			Type t_DTO = DTO.GetType();
			PropertyInfo pi_sdlfj = t_XAML.GetProperty("sdlfj", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_sdlfj = t_DTO.GetField("sdlfjField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdlfj != null && pi_sdlfj != null) fi_sdlfj.SetValue(DTO, (string)pi_sdlfj.GetValue(Data, null));
			PropertyInfo pi_ID = t_XAML.GetProperty("ID", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_ID = t_DTO.GetField("IDField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_ID != null && pi_ID != null) fi_ID.SetValue(DTO, (Guid)pi_ID.GetValue(Data, null));
			PropertyInfo pi_linktest = t_XAML.GetProperty("linktest", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_linktest = t_DTO.GetField("linktestField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_linktest != null && pi_linktest != null) fi_linktest.SetValue(DTO, (Test1.TestData2)pi_linktest.GetValue(Data, null));
			PropertyInfo pi_collectiontest = t_XAML.GetProperty("collectiontest", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_collectiontest = t_DTO.GetField("collectiontestField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_collectiontest != null && pi_collectiontest != null)
			{
				ObservableCollection<string> v_collectiontest = new ObservableCollection<string>();
				fi_collectiontest.SetValue(DTO, v_collectiontest);
				ObservableCollection<string> XAML_collectiontest = pi_collectiontest.GetValue(Data, null) as ObservableCollection<string>;
				foreach(string a in XAML_collectiontest) { v_collectiontest.Add(a); }
			}
			PropertyInfo pi_sdf = t_XAML.GetProperty("sdf", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_sdf = t_DTO.GetField("sdfField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdf != null && pi_sdf != null)
			{
				Dictionary<int, Guid> v_sdf = new Dictionary<int, Guid>();
				fi_sdf.SetValue(DTO, v_sdf);
				Dictionary<int, Guid> XAML_sdf = pi_sdf.GetValue(Data, null) as Dictionary<int, Guid>;
				foreach(KeyValuePair<int, Guid> a in XAML_sdf) { v_sdf.Add(a.Key, a.Value); }
			}
			PropertyInfo pi_sdfgg = t_XAML.GetProperty("sdfgg", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_sdfgg = t_DTO.GetField("sdfggField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdfgg != null && pi_sdfgg != null)
			{
				SortedDictionary<int, string> v_sdfgg = new SortedDictionary<int, string>();
				fi_sdfgg.SetValue(DTO, v_sdfgg);
				SortedDictionary<int, string> XAML_sdfgg = pi_sdfgg.GetValue(Data, null) as SortedDictionary<int, string>;
				foreach(KeyValuePair<int, string> a in XAML_sdfgg) { v_sdfgg.Add(a.Key, a.Value); }
			}
			PropertyInfo pi_askdjsadj = t_XAML.GetProperty("askdjsadj", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_askdjsadj = t_DTO.GetField("askdjsadjField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_askdjsadj != null && pi_askdjsadj != null)
			{
				List<string> v_askdjsadj = new List<string>();
				fi_askdjsadj.SetValue(DTO, v_askdjsadj);
				List<string> XAML_askdjsadj = pi_askdjsadj.GetValue(Data, null) as List<string>;
				foreach(string a in XAML_askdjsadj) { v_askdjsadj.Add(a); }
			}
			PropertyInfo pi_sdkjfsldjf = t_XAML.GetProperty("sdkjfsldjf", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_sdkjfsldjf = t_DTO.GetField("sdkjfsldjfField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdkjfsldjf != null && pi_sdkjfsldjf != null)
			{
				Dictionary<int, string> v_sdkjfsldjf = new Dictionary<int, string>();
				fi_sdkjfsldjf.SetValue(DTO, v_sdkjfsldjf);
				Dictionary<int, string> XAML_sdkjfsldjf = pi_sdkjfsldjf.GetValue(Data, null) as Dictionary<int, string>;
				foreach(KeyValuePair<int, string> a in XAML_sdkjfsldjf) { v_sdkjfsldjf.Add(a.Key, a.Value); }
			}
			PropertyInfo pi_askdjalsd = t_XAML.GetProperty("askdjalsd", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_askdjalsd = t_DTO.GetField("askdjalsdField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_askdjalsd != null && pi_askdjalsd != null)
			{
				Guid[] XAML_askdjalsd = pi_askdjalsd.GetValue(Data, null) as Guid[];
				Guid[] v_askdjalsd = new Guid[XAML_askdjalsd.GetLength(0)];
				for(int i = 0; i < XAML_askdjalsd.GetLength(0); i++) { v_askdjalsd[i] = Data.askdjalsd[i]; }
				fi_askdjalsd.SetValue(DTO, v_askdjalsd);
			}
			return DTO;
		}

		public static TestData1XAML ConvertToXAMLObject(Test1.TestData1 Data)
		{
			Test1.TestData1XAML XAML = new Test1.TestData1XAML();
			Type t_DTO = Data.GetType();
			Type t_XAML = XAML.GetType();
			PropertyInfo pi_sdlfj = t_XAML.GetProperty("sdlfj", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_sdlfj = t_DTO.GetField("sdlfjField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdlfj != null && pi_sdlfj != null)
				pi_sdlfj.SetValue(XAML, (string)fi_sdlfj.GetValue(Data), null);
			PropertyInfo pi_ID = t_XAML.GetProperty("ID", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_ID = t_DTO.GetField("IDField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_ID != null && pi_ID != null)
				pi_ID.SetValue(XAML, (Guid)fi_ID.GetValue(Data), null);
			PropertyInfo pi_linktest = t_XAML.GetProperty("linktest", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_linktest = t_DTO.GetField("linktestField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_linktest != null && pi_linktest != null)
				pi_linktest.SetValue(XAML, (Test1.TestData2)fi_linktest.GetValue(Data), null);
			PropertyInfo pi_collectiontest = t_XAML.GetProperty("collectiontest", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_collectiontest = t_DTO.GetField("collectiontestField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_collectiontest != null && pi_collectiontest != null)
			{
				ObservableCollection<string> v_collectiontest = fi_collectiontest.GetValue(Data) as ObservableCollection<string>;
				if(v_collectiontest != null)
				{
					ObservableCollection<string> XAML_collectiontest = new ObservableCollection<string>();
					pi_collectiontest.SetValue(XAML, XAML_collectiontest, null);
					foreach(string a in v_collectiontest) { XAML_collectiontest.Add(a); }
				}
			}
			PropertyInfo pi_sdf = t_XAML.GetProperty("sdf", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_sdf = t_DTO.GetField("sdfField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdf != null && pi_sdf != null)
			{
				Dictionary<int, Guid> XAML_sdf = new Dictionary<int, Guid>();
				pi_sdf.SetValue(XAML, XAML_sdf, null);
				Dictionary<int, Guid> v_sdf = fi_sdf.GetValue(Data) as Dictionary<int, Guid>;
				if(v_sdf != null) foreach(KeyValuePair<int, Guid> a in v_sdf) { XAML_sdf.Add(a.Key, a.Value); }
			}
			PropertyInfo pi_sdfgg = t_XAML.GetProperty("sdfgg", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_sdfgg = t_DTO.GetField("sdfggField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdfgg != null && pi_sdfgg != null)
			{
				SortedDictionary<int, string> XAML_sdfgg = new SortedDictionary<int, string>();
				pi_sdfgg.SetValue(XAML, XAML_sdfgg, null);
				SortedDictionary<int, string> v_sdfgg = fi_sdfgg.GetValue(Data) as SortedDictionary<int, string>;
				if(v_sdfgg != null) foreach(KeyValuePair<int, string> a in v_sdfgg) { XAML_sdfgg.Add(a.Key, a.Value); }
			}
			PropertyInfo pi_askdjsadj = t_XAML.GetProperty("askdjsadj", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_askdjsadj = t_DTO.GetField("askdjsadjField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_askdjsadj != null && pi_askdjsadj != null)
			{
				List<string> v_askdjsadj = fi_askdjsadj.GetValue(Data) as List<string>;
				if(v_askdjsadj != null)
				{
					List<string> XAML_askdjsadj = new List<string>();
					pi_askdjsadj.SetValue(XAML, XAML_askdjsadj, null);
					foreach(string a in v_askdjsadj) { XAML_askdjsadj.Add(a); }
				}
			}
			PropertyInfo pi_sdkjfsldjf = t_XAML.GetProperty("sdkjfsldjf", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_sdkjfsldjf = t_DTO.GetField("sdkjfsldjfField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdkjfsldjf != null && pi_sdkjfsldjf != null)
			{
				Dictionary<int, string> XAML_sdkjfsldjf = new Dictionary<int, string>();
				pi_sdkjfsldjf.SetValue(XAML, XAML_sdkjfsldjf, null);
				Dictionary<int, string> v_sdkjfsldjf = fi_sdkjfsldjf.GetValue(Data) as Dictionary<int, string>;
				if(v_sdkjfsldjf != null) foreach(KeyValuePair<int, string> a in v_sdkjfsldjf) { XAML_sdkjfsldjf.Add(a.Key, a.Value); }
			}
			PropertyInfo pi_askdjalsd = t_XAML.GetProperty("askdjalsd", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_askdjalsd = t_DTO.GetField("askdjalsdField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_askdjalsd != null && pi_askdjalsd != null)
			{
				Guid[] v_askdjalsd = fi_askdjalsd.GetValue(Data) as Guid[];
				if(v_askdjalsd != null)
				{
					Guid[] XAML_askdjalsd = new Guid[v_askdjalsd.GetLength(0)];
					pi_askdjalsd.SetValue(XAML, XAML_askdjalsd, null);
					for(int i = 0; i < v_askdjalsd.GetLength(0); i++) { XAML_askdjalsd[i] = v_askdjalsd[i]; }
				}
			}
			return XAML;
		}
	}

	[System.Diagnostics.DebuggerStepThroughAttribute]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("WCF Architect Service Compiler", "2.0.2000.0")]
	[DataContract(Name = "TestData2", Namespace = "http://tempuri.org/Test1/")]
	public partial class TestData2 : System.Runtime.Serialization.IExtensibleDataObject
	{
		public System.Runtime.Serialization.ExtensionDataObject ExtensionData { get; set; }

		private string sdfField;
		[DataMember(Name = "sdf")] public string sdf { get { return sdfField; } set { sdfField = value; } }
	}

	//XAML Integration Object for the TestData2 DTO
	[System.CodeDom.Compiler.GeneratedCodeAttribute("WCF Architect Service Compiler", "2.0.2000.0")]
	public partial class TestData2XAML : System.Windows.DependencyObject
	{
		//Properties
		public string sdf { get { return (string)GetValue(sdfProperty); } set { SetValue(sdfProperty, value); } }
		public static readonly DependencyProperty sdfProperty = DependencyProperty.Register("sdf", typeof(string), typeof(Test1.TestData2));

		//Implicit Conversion
		public static implicit operator Test1.TestData2(Test1.TestData2XAML Data)
		{
			if (Data == null) return null;
			if (Application.Current.Dispatcher.CheckAccess())
				return TestData2XAML.ConvertFromXAMLObject(Data);
			else
				return (Test1.TestData2)Application.Current.Dispatcher.Invoke(new Func<Test1.TestData2>(() => Test1.TestData2XAML.ConvertFromXAMLObject(Data)), System.Windows.Threading.DispatcherPriority.Normal);
		}
		public static implicit operator Test1.TestData2XAML(Test1.TestData2 Data)
		{
			if (Data == null) return null;
			if (Application.Current.Dispatcher.CheckAccess())
				return TestData2XAML.ConvertToXAMLObject(Data);
			else
				return (Test1.TestData2XAML)Application.Current.Dispatcher.Invoke(new Func<TestData2>(() => Test1.TestData2XAML.ConvertToXAMLObject(Data)), System.Windows.Threading.DispatcherPriority.Normal);
		}

		//Constructors
		public TestData2XAML(Test1.TestData2 Data)
		{
			Type t_DT = Data.GetType();
			FieldInfo fi_sdf = t_DT.GetField("sdfField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdf != null)
				sdf = (string)fi_sdf.GetValue(Data);
		}

		public TestData2XAML()
		{
		}

		//XAML/DTO Conversion Functions
		public static TestData2 ConvertFromXAMLObject(Test1.TestData2XAML Data)
		{
			Test1.TestData2 DTO = new Test1.TestData2();
			Type t_XAML = Data.GetType();
			Type t_DTO = DTO.GetType();
			PropertyInfo pi_sdf = t_XAML.GetProperty("sdf", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_sdf = t_DTO.GetField("sdfField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdf != null && pi_sdf != null) fi_sdf.SetValue(DTO, (string)pi_sdf.GetValue(Data, null));
			return DTO;
		}

		public static TestData2XAML ConvertToXAMLObject(Test1.TestData2 Data)
		{
			Test1.TestData2XAML XAML = new Test1.TestData2XAML();
			Type t_DTO = Data.GetType();
			Type t_XAML = XAML.GetType();
			PropertyInfo pi_sdf = t_XAML.GetProperty("sdf", BindingFlags.Public | BindingFlags.Instance);
			FieldInfo fi_sdf = t_DTO.GetField("sdfField", BindingFlags.NonPublic | BindingFlags.Instance);
			if(fi_sdf != null && pi_sdf != null)
				pi_sdf.SetValue(XAML, (string)fi_sdf.GetValue(Data), null);
			return XAML;
		}
	}


}

