//---------------------------------------------------------------------------
// This code was generated by a tool. Changes to this file may cause 
// incorrect behavior and will be lost if the code is regenerated.
//
// WCF Architect Version:	2.0.0.12501
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

[assembly: System.Runtime.Serialization.ContractNamespaceAttribute("http://tempuri.org/WCFArchitect/SampleServer/BasicNET/", ClrNamespace="WCFArchitect.SampleServer.BasicNET")]

#pragma warning disable 1591
	/**************************************************************************
	*	Dependency Types
	**************************************************************************/

namespace WCFArchitect.SampleServer.BasicWinRT
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute("WCF Architect .NET CSharp Generator - BETA", "2.0.0.12501")]
	[DataContract(Namespace = "http://tempuri.org/WCFArchitect/SampleServer/BasicWinRT/")]
	public enum Colors : long
	{
		[EnumMember()] Blue,
		[EnumMember()] Green,
		[EnumMember()] Red,
		[EnumMember()] Yellow,
		[EnumMember()] Orange,
		[EnumMember()] Gray,
		[EnumMember()] Teal,
		[EnumMember()] Black,
	}

}

namespace WCFArchitect.SampleServer.BasicWinRT
{
	[System.Diagnostics.DebuggerStepThroughAttribute]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("WCF Architect .NET CSharp Generator - BETA", "2.0.0.12501")]
	[DataContract(Name = "Customer", Namespace = "http://tempuri.org/")]
	public partial class Customer : System.Runtime.Serialization.IExtensibleDataObject
	{
		//Automatic Data Update Support

		public System.Runtime.Serialization.ExtensionDataObject ExtensionData { get; set; }

		private Guid IDField;
		[DataMember(Name = "ID")] public Guid ID { get { return IDField; } set { IDField = value; } }
		private string NameField;
		[DataMember(Name = "Name")] public string Name { get { return NameField; } set { NameField = value; } }
		private string AddressLine1Field;
		[DataMember(Name = "AddressLine1")] public string AddressLine1 { get { return AddressLine1Field; } set { AddressLine1Field = value; } }
		private string AddressLine2Field;
		[DataMember(Name = "AddressLine2")] public string AddressLine2 { get { return AddressLine2Field; } set { AddressLine2Field = value; } }
		private string CityField;
		[DataMember(Name = "City")] public string City { get { return CityField; } set { CityField = value; } }
		private string StateField;
		[DataMember(Name = "State")] public string State { get { return StateField; } set { StateField = value; } }
		private int ZipCodeField;
		[DataMember(Name = "ZipCode")] public int ZipCode { get { return ZipCodeField; } set { ZipCodeField = value; } }
		private WCFArchitect.SampleServer.BasicWinRT.Colors ColorField;
		[DataMember(Name = "Color")] public WCFArchitect.SampleServer.BasicWinRT.Colors Color { get { return ColorField; } set { ColorField = value; } }
		private List<WCFArchitect.SampleServer.BasicWinRT.Customer> TestXAMLField;
		[DataMember(Name = "TestXAML")] public List<WCFArchitect.SampleServer.BasicWinRT.Customer> TestXAML { get { return TestXAMLField; } set { TestXAMLField = value; } }
	}


	//XAML Integration Object for the Customer DTO
	[System.CodeDom.Compiler.GeneratedCodeAttribute("WCF Architect .NET CSharp Generator - BETA", "2.0.0.12501")]
	public partial class CustomerXAML : DependencyObject
	{
		//Properties
		public Guid ID { get { return (Guid)GetValue(IDProperty); } set { SetValue(IDProperty, value); } }
		public static readonly DependencyProperty IDProperty = DependencyProperty.Register("ID", typeof(Guid), typeof(WCFArchitect.SampleServer.BasicWinRT.CustomerXAML));
		public string Name { get { return (string)GetValue(NameProperty); } set { SetValue(NameProperty, value); } }
		public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(WCFArchitect.SampleServer.BasicWinRT.CustomerXAML));
		public string AddressLine1 { get { return (string)GetValue(AddressLine1Property); } set { SetValue(AddressLine1Property, value); } }
		public static readonly DependencyProperty AddressLine1Property = DependencyProperty.Register("AddressLine1", typeof(string), typeof(WCFArchitect.SampleServer.BasicWinRT.CustomerXAML));
		public string AddressLine2 { get { return (string)GetValue(AddressLine2Property); } set { SetValue(AddressLine2Property, value); } }
		public static readonly DependencyProperty AddressLine2Property = DependencyProperty.Register("AddressLine2", typeof(string), typeof(WCFArchitect.SampleServer.BasicWinRT.CustomerXAML));
		public string City { get { return (string)GetValue(CityProperty); } set { SetValue(CityProperty, value); } }
		public static readonly DependencyProperty CityProperty = DependencyProperty.Register("City", typeof(string), typeof(WCFArchitect.SampleServer.BasicWinRT.CustomerXAML));
		public string State { get { return (string)GetValue(StateProperty); } set { SetValue(StateProperty, value); } }
		public static readonly DependencyProperty StateProperty = DependencyProperty.Register("State", typeof(string), typeof(WCFArchitect.SampleServer.BasicWinRT.CustomerXAML));
		public int ZipCode { get { return (int)GetValue(ZipCodeProperty); } set { SetValue(ZipCodeProperty, value); } }
		public static readonly DependencyProperty ZipCodeProperty = DependencyProperty.Register("ZipCode", typeof(int), typeof(WCFArchitect.SampleServer.BasicWinRT.CustomerXAML));
		public WCFArchitect.SampleServer.BasicWinRT.Colors Color { get { return (WCFArchitect.SampleServer.BasicWinRT.Colors)GetValue(ColorProperty); } set { SetValue(ColorProperty, value); } }
		public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(WCFArchitect.SampleServer.BasicWinRT.Colors), typeof(WCFArchitect.SampleServer.BasicWinRT.CustomerXAML));
		public List<WCFArchitect.SampleServer.BasicWinRT.CustomerXAML> TestXAML { get { return (List<WCFArchitect.SampleServer.BasicWinRT.CustomerXAML>)GetValue(TestXAMLProperty); } set { SetValue(TestXAMLProperty, value); } }
		public static readonly DependencyProperty TestXAMLProperty = DependencyProperty.Register("TestXAML", typeof(List<WCFArchitect.SampleServer.BasicWinRT.CustomerXAML>), typeof(WCFArchitect.SampleServer.BasicWinRT.CustomerXAML));

		//Implicit Conversion
		public static implicit operator WCFArchitect.SampleServer.BasicWinRT.Customer(WCFArchitect.SampleServer.BasicWinRT.CustomerXAML Data)
		{
			if (Data == null) return null;
			Customer v = null;
			if (Application.Current.Dispatcher.CheckAccess()) v = ConvertFromXAMLObject(Data);
			else Application.Current.Dispatcher.Invoke(() => { v = ConvertFromXAMLObject(Data); }, System.Windows.Threading.DispatcherPriority.Normal);
			return v;
		}
		public static implicit operator WCFArchitect.SampleServer.BasicWinRT.CustomerXAML(WCFArchitect.SampleServer.BasicWinRT.Customer Data)
		{
			if (Data == null) return null;
			CustomerXAML v = null;
			if (Application.Current.Dispatcher.CheckAccess()) v = ConvertToXAMLObject(Data);
			else Application.Current.Dispatcher.Invoke(() => { v = ConvertToXAMLObject(Data); }, System.Windows.Threading.DispatcherPriority.Normal);
			return v;
		}

		//Constructors
		public CustomerXAML()
		{
		}

		public CustomerXAML(WCFArchitect.SampleServer.BasicWinRT.Customer Data)
		{
			ID = Data.ID;
			Name = Data.Name;
			AddressLine1 = Data.AddressLine1;
			AddressLine2 = Data.AddressLine2;
			City = Data.City;
			State = Data.State;
			ZipCode = Data.ZipCode;
			Color = Data.Color;
			List<WCFArchitect.SampleServer.BasicWinRT.CustomerXAML> vTestXAML = new List<WCFArchitect.SampleServer.BasicWinRT.CustomerXAML>();
			foreach(WCFArchitect.SampleServer.BasicWinRT.CustomerXAML a in Data.TestXAML) { vTestXAML.Add(a); }
			TestXAML = vTestXAML;
		}

		//XAML/DTO Conversion Functions
		public static Customer ConvertFromXAMLObject(WCFArchitect.SampleServer.BasicWinRT.CustomerXAML Data)
		{
			WCFArchitect.SampleServer.BasicWinRT.Customer DTO = new WCFArchitect.SampleServer.BasicWinRT.Customer();
			DTO.ID = Data.ID;
			DTO.Name = Data.Name;
			DTO.AddressLine1 = Data.AddressLine1;
			DTO.AddressLine2 = Data.AddressLine2;
			DTO.City = Data.City;
			DTO.State = Data.State;
			DTO.ZipCode = Data.ZipCode;
			DTO.Color = Data.Color;
			List<WCFArchitect.SampleServer.BasicWinRT.Customer> vTestXAML = new List<WCFArchitect.SampleServer.BasicWinRT.Customer>();
			foreach(WCFArchitect.SampleServer.BasicWinRT.Customer a in Data.TestXAML) { vTestXAML.Add(a); }
			DTO.TestXAML = vTestXAML;
			return DTO;
		}

		public static CustomerXAML ConvertToXAMLObject(WCFArchitect.SampleServer.BasicWinRT.Customer Data)
		{
			WCFArchitect.SampleServer.BasicWinRT.CustomerXAML XAML = new WCFArchitect.SampleServer.BasicWinRT.CustomerXAML();
			XAML.ID = Data.ID;
			XAML.Name = Data.Name;
			XAML.AddressLine1 = Data.AddressLine1;
			XAML.AddressLine2 = Data.AddressLine2;
			XAML.City = Data.City;
			XAML.State = Data.State;
			XAML.ZipCode = Data.ZipCode;
			XAML.Color = Data.Color;
			List<WCFArchitect.SampleServer.BasicWinRT.CustomerXAML> vTestXAML = new List<WCFArchitect.SampleServer.BasicWinRT.CustomerXAML>();
			foreach(WCFArchitect.SampleServer.BasicWinRT.CustomerXAML a in Data.TestXAML) { vTestXAML.Add(a); }
			XAML.TestXAML = vTestXAML;
			return XAML;
		}
	}

}

namespace WCFArchitect.SampleServer.BasicNET
{
	/**************************************************************************
	*	Service Contracts
	**************************************************************************/

	[System.CodeDom.Compiler.GeneratedCodeAttribute("WCF Architect .NET CSharp Generator - BETA", "2.0.0.12501")]
	public interface ITestNET
	{
		[OperationContract(Action = "http://tempuri.org/WCFArchitect/SampleServer/BasicNET/TestNET/RefTestAsync", ReplyAction = "http://tempuri.org/WCFArchitect/SampleServer/BasicNET/TestNET/RefTestAsyncResponse")]
		System.Threading.Tasks.Task<WCFArchitect.SampleServer.BasicWinRT.Customer> RefTestAsync();

	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("WCF Architect .NET CSharp Generator - BETA", "2.0.0.12501")]
	public interface ITestNETCallback
	{
		[OperationContract(Action = "http://tempuri.org/WCFArchitect/SampleServer/BasicNET/TestNET/RetTestCallbackAsync", ReplyAction = "http://tempuri.org/WCFArchitect/SampleServer/BasicNET/TestNET/RetTestCallbackAsyncResponse")]
		System.Threading.Tasks.Task<WCFArchitect.SampleServer.BasicWinRT.Customer> RetTestCallbackAsync();

	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("WCF Architect .NET CSharp Generator - BETA", "2.0.0.12501")]
	public interface ITestNETChannel : ITestNET, System.ServiceModel.IClientChannel
	{
	}

	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("WCF Architect .NET CSharp Generator - BETA", "2.0.0.12501")]
	public partial class TestNETProxy : System.ServiceModel.DuplexClientBase<ITestNET>, ITestNET
	{
		public TestNETProxy(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		public TestNETProxy(System.ServiceModel.InstanceContext callbackInstance) : base(callbackInstance)
		{
		}

		public TestNETProxy(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : base(callbackInstance, endpointConfigurationName)
		{
		}

		public TestNETProxy(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		public TestNETProxy(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		public TestNETProxy(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : base(callbackInstance, binding, remoteAddress)
		{
		}

		public TestNETProxy(System.ServiceModel.Description.ServiceEndpoint endpoint) : base(endpoint)
		{
		}

		public TestNETProxy(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Description.ServiceEndpoint endpoint) : base(callbackInstance, endpoint)
		{
		}

		public System.Threading.Tasks.Task<WCFArchitect.SampleServer.BasicWinRT.Customer> RefTestAsync()
		{
			return base.Channel.RefTestAsync();
		}

	}


}

#pragma warning restore 1591
