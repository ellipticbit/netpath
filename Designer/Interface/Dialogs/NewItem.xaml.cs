﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WCFArchitect.Interface.Project;

namespace WCFArchitect.Interface.Dialogs
{
	public partial class NewItem : Grid
	{
		private bool IsNamespaceListUpdating { get; set; }

		private Projects.Project ActiveProject { get; set; }
		private Action<Projects.OpenableDocument> OpenProjectItem { get; set; }

		public NewItem(Projects.Project Project, Action<Projects.OpenableDocument> OpenItemAction)
		{
			InitializeComponent();

			this.ActiveProject = Project;
			this.OpenProjectItem = OpenItemAction;
		}

		private void NewItemTypesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (NewItemTypesList.SelectedItem == null)
			{
				NewItemProjectNamespaces.Visibility = System.Windows.Visibility.Collapsed;
				NewItemBindingTypesList.Visibility = System.Windows.Visibility.Collapsed;
				NewItemSecurityTypesList.Visibility = System.Windows.Visibility.Collapsed;
			}

			NewItemAdd.IsEnabled = false;
			NewItemProjectNamespaceList.ItemsSource = null;
			NewItemBindingTypesList.SelectedItem = null;
			NewItemSecurityTypesList.SelectedItem = null;
			NewItemProjectNamespaces.Visibility = System.Windows.Visibility.Collapsed;
			NewItemBindingTypesList.Visibility = System.Windows.Visibility.Collapsed;
			NewItemSecurityTypesList.Visibility = System.Windows.Visibility.Collapsed;
			
			IsNamespaceListUpdating = true;
			NewItemProjectNamespaceRoot.IsChecked = true;
			if (NewItemTypesList.SelectedItem == null) return;

			NewItemType NIT = NewItemTypesList.SelectedItem as NewItemType;
			if (NIT.DataType == 1 || NIT.DataType == 2 || NIT.DataType == 3 || NIT.DataType == 4)
			{
				NewItemProjectNamespaceList.ItemsSource = ActiveProject.Namespace.Children;
				NewItemProjectNamespaceRoot.IsChecked = true;
				NewItemProjectNamespaceRoot.Content = ActiveProject.Namespace.Name;
				NewItemProjectNamespaces.Visibility = System.Windows.Visibility.Visible;
			}
			if (NIT.DataType == 5)
			{
				NewItemBindingTypesList.Visibility = System.Windows.Visibility.Visible;
				NewItemBindingTypesList.Focus();
			}
			if (NIT.DataType == 6)
			{
				NewItemSecurityTypesList.Visibility = System.Windows.Visibility.Visible;
				NewItemSecurityTypesList.Focus();
			}
			if (NIT.DataType == 7)
			{
				NewItemName.Focus();
			}
			IsNamespaceListUpdating = false;
		}

		private void NewItemProjectNamespaceRoot_Checked(object sender, RoutedEventArgs e)
		{
			if (IsNamespaceListUpdating == true) return;
			NewItemProjectNamespaceList.ItemsSource = null;
		}

		private void NewItemProjectNamespaceRoot_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (NewItemProjectNamespaceList.SelectedItem != null) NewItemProjectNamespaceRoot.IsChecked = false;
			NewItemName.Focus();
		}

		private void NewItemBindingTypesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			NewItemName.Focus();
		}

		private void NewItemSecurityTypesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			NewItemName.Focus();
		}

		private void NewItemName_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
				NewItemAdd_Click(null, null);
		}

		private void NewItemName_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (NewItemName.Text == "") NewItemAdd.IsEnabled = false;
			else NewItemAdd.IsEnabled = true;
		}

		private void NewItemAdd_Click(object sender, RoutedEventArgs e)
		{
			if (NewItemTypesList.SelectedItem == null) return;
			Globals.IsLoading = true;

			try
			{
				NewItemType NIT = NewItemTypesList.SelectedItem as NewItemType;

				if (NIT.DataType == 1)
				{
					if (NewItemProjectNamespaceRoot.IsChecked == true)
					{
						Projects.Namespace NI = new Projects.Namespace(NewItemName.Text, ActiveProject.Namespace, ActiveProject);
						if (NewItemProjectNamespaceList.SelectedItem == null) ActiveProject.Namespace.Children.Add(NI);
						else ActiveProject.Namespace.Children.Add(NI);
						Globals.IsLoading = false;
						OpenProjectItem(NI);
					}
					else
					{
						Projects.Namespace NIN = NewItemProjectNamespaceList.SelectedItem as Projects.Namespace;
						Projects.Namespace NI = new Projects.Namespace(NewItemName.Text, NIN, ActiveProject);
						if (NewItemProjectNamespaceList.SelectedItem == null) ActiveProject.Namespace.Children.Add(NI);
						else NIN.Children.Add(NI);
						Globals.IsLoading = false;
						OpenProjectItem(NI);
					}
				}
				else if (NIT.DataType == 2)
				{
					if (NewItemProjectNamespaceRoot.IsChecked == true)
					{
						Projects.Service NI = new Projects.Service(NewItemName.Text, ActiveProject.Namespace);
						ActiveProject.Namespace.Services.Add(NI);
						Globals.IsLoading = false;
						OpenProjectItem(NI);
					}
					else
					{
						Projects.Namespace NIN = NewItemProjectNamespaceList.SelectedItem as Projects.Namespace;
						if (NIN == null)
						{
							if (Prospective.Controls.MessageBox.Show("You must select a select a Namespace from the list or use the Root Namespace to create a new Service. Would you like to use the Root Namespace?", "No Namespace Selected", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
								NewItemProjectNamespaceRoot.IsChecked = true;
							return;
						}

						Projects.Service NI = new Projects.Service(NewItemName.Text, NIN);
						NIN.Services.Add(NI);
						Globals.IsLoading = false;
						OpenProjectItem(NI);
					}
				}
				else if (NIT.DataType == 3)
				{
					if (NewItemProjectNamespaceRoot.IsChecked == true)
					{
						Projects.Data NI = new Projects.Data(NewItemName.Text, ActiveProject.Namespace);
						ActiveProject.Namespace.Data.Add(NI);
						Globals.IsLoading = false;
						OpenProjectItem(NI);
					}
					else
					{
						Projects.Namespace NIN = NewItemProjectNamespaceList.SelectedItem as Projects.Namespace;
						if (NIN == null)
						{
							if (Prospective.Controls.MessageBox.Show("You must select a select a Namespace from the list or use the Root Namespace to create a new Data Object. Would you like to use the Root Namespace?", "No Namespace Selected", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
								NewItemProjectNamespaceRoot.IsChecked = true;
							return;
						}

						Projects.Data NI = new Projects.Data(NewItemName.Text, NIN);
						NIN.Data.Add(NI);
						Globals.IsLoading = false;
						OpenProjectItem(NI);
					}
				}
				else if (NIT.DataType == 4)
				{
					if (NewItemProjectNamespaceRoot.IsChecked == true)
					{
						Projects.Enum NI = new Projects.Enum(NewItemName.Text, ActiveProject.Namespace);
						ActiveProject.Namespace.Enums.Add(NI);
						Globals.IsLoading = false;
						OpenProjectItem(NI);
					}
					else
					{
						Projects.Namespace NIN = NewItemProjectNamespaceList.SelectedItem as Projects.Namespace;
						if (NIN == null)
						{
							if (Prospective.Controls.MessageBox.Show("You must select a select a Namespace from the list or use the Root Namespace to create a new Enum. Would you like to use the Root Namespace?", "No Namespace Selected", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
								NewItemProjectNamespaceRoot.IsChecked = true;
							return;
						}

						Projects.Enum NI = new Projects.Enum(NewItemName.Text, NIN);
						NIN.Enums.Add(NI);
						Globals.IsLoading = false;
						OpenProjectItem(NI);
					}
				}
				else if (NIT.DataType == 5)
				{
					NewItemType NBT = NewItemBindingTypesList.SelectedItem as NewItemType;
					if (NBT == null)
					{
						Prospective.Controls.MessageBox.Show("You must select a Binding Type for this Binding!", "No Binding Type Selected", MessageBoxButton.OK, MessageBoxImage.Error);
						return;
					}

					Projects.Namespace NIP = null;
					if (NewItemProjectNamespaceRoot.IsChecked == true) NIP = ActiveProject.Namespace;
					else NIP = NewItemProjectNamespaceList.SelectedItem as Projects.Namespace; 

					Projects.ServiceBinding NI = null;
					if (NBT.DataType == 1) NI = new Projects.ServiceBindingBasicHTTP(NewItemName.Text, NIP);
					if (NBT.DataType == 2) NI = new Projects.ServiceBindingWSHTTP(NewItemName.Text, NIP);
					if (NBT.DataType == 3) NI = new Projects.ServiceBindingWS2007HTTP(NewItemName.Text, NIP);
					if (NBT.DataType == 4) NI = new Projects.ServiceBindingWSDualHTTP(NewItemName.Text, NIP);
					if (NBT.DataType == 5) NI = new Projects.ServiceBindingWSFederationHTTP(NewItemName.Text, NIP);
					if (NBT.DataType == 6) NI = new Projects.ServiceBindingWS2007FederationHTTP(NewItemName.Text, NIP);
					if (NBT.DataType == 7) NI = new Projects.ServiceBindingTCP(NewItemName.Text, NIP);
					if (NBT.DataType == 8) NI = new Projects.ServiceBindingNamedPipe(NewItemName.Text, NIP);
					if (NBT.DataType == 9) NI = new Projects.ServiceBindingMSMQ(NewItemName.Text, NIP);
					if (NBT.DataType == 10) NI = new Projects.ServiceBindingPeerTCP(NewItemName.Text, NIP);
					if (NBT.DataType == 11) NI = new Projects.ServiceBindingWebHTTP(NewItemName.Text, NIP);
					if (NBT.DataType == 12) NI = new Projects.ServiceBindingMSMQIntegration(NewItemName.Text, NIP);

					ActiveProject.Namespace.Bindings.Add(NI);
					Globals.IsLoading = false;
					OpenProjectItem(NI);
				}
				else if (NIT.DataType == 6)
				{
					NewItemType NBT = NewItemSecurityTypesList.SelectedItem as NewItemType;
					if (NBT == null)
					{
						Prospective.Controls.MessageBox.Show("You must select a Security Type for this Binding!", "No Security Type Selected", MessageBoxButton.OK, MessageBoxImage.Error);
						return;
					}

					Projects.Namespace NIP = null;
					if (NewItemProjectNamespaceRoot.IsChecked == true) NIP = ActiveProject.Namespace;
					else NIP = NewItemProjectNamespaceList.SelectedItem as Projects.Namespace; 

					Projects.BindingSecurity NI = null;
					if (NBT.DataType == 1) NI = new Projects.BindingSecurityBasicHTTP(NewItemName.Text, NIP);
					if (NBT.DataType == 2) NI = new Projects.BindingSecurityWSHTTP(NewItemName.Text, NIP);
					if (NBT.DataType == 3) NI = new Projects.BindingSecurityWSDualHTTP(NewItemName.Text, NIP);
					if (NBT.DataType == 4) NI = new Projects.BindingSecurityWSFederationHTTP(NewItemName.Text, NIP);
					if (NBT.DataType == 5) NI = new Projects.BindingSecurityTCP(NewItemName.Text, NIP);
					if (NBT.DataType == 6) NI = new Projects.BindingSecurityNamedPipe(NewItemName.Text, NIP);
					if (NBT.DataType == 7) NI = new Projects.BindingSecurityMSMQ(NewItemName.Text, NIP);
					if (NBT.DataType == 8) NI = new Projects.BindingSecurityPeerTCP(NewItemName.Text, NIP);
					if (NBT.DataType == 9) NI = new Projects.BindingSecurityWebHTTP(NewItemName.Text, NIP);
					if (NBT.DataType == 10) NI = new Projects.BindingSecurityMSMQIntegration(NewItemName.Text, NIP);

					NIP.Security.Add(NI);
					Globals.IsLoading = false;
					OpenProjectItem(NI);
				}
				else if (NIT.DataType == 7)
				{
					Projects.Namespace NIP = null;
					if (NewItemProjectNamespaceRoot.IsChecked == true) NIP = ActiveProject.Namespace;
					else NIP = NewItemProjectNamespaceList.SelectedItem as Projects.Namespace;
					
					Projects.Host NI = new Projects.Host(NewItemName.Text, NIP);
					NIP.Hosts.Add(NI);
					Globals.IsLoading = false;
					OpenProjectItem(NI);
				}
			}
			finally
			{
				Globals.IsLoading = false;
			}
			NewItemCancel_Click(null, null);
		}

		private void NewItemCancel_Click(object sender, RoutedEventArgs e)
		{
			Globals.MainScreen.CloseActiveMessageBox();
		}
	}
}
