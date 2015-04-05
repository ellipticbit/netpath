﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using NETPath.Generators.Interfaces;
using NETPath.Projects;
using NETPath.Projects.Helpers;
using NETPath.Projects.WebApi;

namespace NETPath.Generators.CS.WebApi
{
	public class Generator : IGenerator
	{
		public Action<string> NewOutput { get; private set; }
		public Action<CompileMessage> NewMessage { get; private set; }
		public ObservableCollection<CompileMessage> Messages { get; private set; }
		public CompileMessageSeverity HighestSeverity { get; private set; }
		public string Name { get; private set; }
		public GenerationLanguage Language { get; private set; }
		public GenerationModule Module { get; private set; }
		public bool IsInitialized { get; private set; }

		private WebApiProject Data;
		private string ProjectPath;

		public Generator()
		{
			Messages = new ObservableCollection<CompileMessage>();
			Name = "NETPath .NET CSharp Generator";
			Language = GenerationLanguage.CSharp;
			Module = GenerationModule.WindowsRuntime;
		}

		public void Initialize(Project Data, string ProjectPath, Action<string> OutputHandler, Action<CompileMessage> CompileMessageHandler)
		{
			this.Data = Data as WebApiProject;
			if (this.Data == null) throw new Exception("Project is null!");
			this.ProjectPath = ProjectPath;
			NewOutput = OutputHandler;
			NewMessage = CompileMessageHandler;
			IsInitialized = true;
		}

		public void Build(bool ClientOnly = false)
		{
			HighestSeverity = CompileMessageSeverity.INFO;
			Messages.Clear();
			NewOutput(Globals.ApplicationTitle);
			NewOutput(string.Format("Version: {0}", Globals.ApplicationVersion));
			NewOutput("Copyright © 2012-2015 Dr. Honda Inc.");

			Verify();

			//If the verification produced errors exit with an error code, we cannot proceed.
			if (HighestSeverity == CompileMessageSeverity.ERROR)
				return;

			Console.WriteLine("Project Path: {0}", ProjectPath);
			string projdir = System.IO.Path.GetDirectoryName(ProjectPath);
			Console.WriteLine("Project Directory: {0}", projdir);

			foreach (ProjectGenerationTarget t in Data.ServerGenerationTargets)
			{
				Console.WriteLine("Output Relative Path: {0}", t.Path);
				string op = System.IO.Path.Combine(projdir, t.Path.Replace("/", "\\"));
				Console.WriteLine("Output Absolute Path: {0}", op);
				NewOutput(string.Format("Writing Server Output: {0}", op));
				System.IO.File.WriteAllText(op, Generate(t, true));
			}

			foreach (ProjectGenerationTarget t in Data.ClientGenerationTargets)
			{
				Console.WriteLine("Output Relative Path: {0}", t.Path);
				string op = System.IO.Path.Combine(projdir, t.Path.Replace("/", "\\"));
				Console.WriteLine("Output Absolute Path: {0}", op);
				op = Uri.UnescapeDataString(op);
				NewOutput(string.Format("Writing Client Output: {0}", op));
				System.IO.File.WriteAllText(op, Generate(t, false));
			}
		}

		public Task BuildAsync(bool ClientOnly = false)
		{
			return System.Windows.Application.Current == null ? null : Task.Run(() => System.Windows.Application.Current.Dispatcher.Invoke(() => Build(ClientOnly), DispatcherPriority.Normal));
		}

		public void Verify()
		{
			NamespaceGenerator.VerifyCode(Data.Namespace, AddMessage);
		}

		public Task VerifyAsync()
		{
			return System.Windows.Application.Current == null ? null : Task.Run(() => System.Windows.Application.Current.Dispatcher.Invoke(Verify, DispatcherPriority.Normal));
		}

		private string Generate(ProjectGenerationTarget Target, bool Server)
		{
			var code = new StringBuilder(1048576);
			code.AppendLine("//---------------------------------------------------------------------------");
			code.AppendLine("// This code was generated by a tool. Changes to this file may cause ");
			code.AppendLine("// incorrect behavior and will be lost if the code is regenerated.");
			code.AppendLine("//");
			code.AppendLine(string.Format("// NETPath Version:\t{0}", Globals.ApplicationVersion));
			if (Target.Framework == ProjectGenerationFramework.NET45) code.AppendLine("// .NET Framework Version:\t4.5");
			if (Target.Framework == ProjectGenerationFramework.WINRT) code.AppendLine("// Windows Runtime Version:\t8.0");
			code.AppendLine("//---------------------------------------------------------------------------");
			code.AppendLine();

			if (!Data.UsingInsideNamespace)
			{
				if (Data.GenerateRegions)
				{
					code.AppendLine("#region Using");
					code.AppendLine();
				}
				// Generate using namespaces
				foreach (ProjectUsingNamespace pun in Data.UsingNamespaces)
				{
					if ((pun.Server && Server) || (pun.Client && !Server && ((pun.RT && Target.Framework == ProjectGenerationFramework.WINRT) || (pun.NET && Target.Framework == ProjectGenerationFramework.NET45))))
						code.AppendLine(string.Format("using {0};", pun.Namespace));
				}
				if (Data.EnableEntityFramework && Server) code.AppendLine("using System.Data.Entity.Core.Objects;");
				if (Target.TargetTypes.OfType<WebApiData>().Any(a => a.HasSql))
				{
					code.AppendLine("using System.Data;");
					code.AppendLine("using System.Data.SqlClient;");
				}
				code.AppendLine();
				if (Data.GenerateRegions)
				{
					code.AppendLine("#endregion");
					code.AppendLine();
				}
			}

			//Generate ContractNamespace Attributes
			if (!Server) code.AppendLine(NamespaceGenerator.GenerateContractNamespaceAttributes(Data.Namespace, Target));

			//Disable XML documentation warnings
			code.AppendLine(string.Format("#pragma warning disable 0649{0}", !Data.EnableDocumentationWarnings ? ", 1591" : ""));

			//Generate globally required code
			if (Data.Namespace.HasServices())
			{
				code.AppendFormat("namespace {0}{1}", Data.Namespace.FullName, Environment.NewLine);
				code.AppendLine("{");
				code.AppendLine("\tpublic class WebAppConfiguration");
				code.AppendLine("\t{");
				code.AppendLine("\t\tpublic void Configuration(IAppBuilder appBuilder)");
				code.AppendLine("\t\t{");
				code.AppendLine("\t\t\tvar config = new HttpConfiguration();");
				code.AppendLine("\t\t\tconfig.MapHttpAttributeRoutes(new InheritedDirectRouteProvider());");
				code.AppendLine("\t\t\tappBuilder.UseWebApi(config);");
				code.AppendLine("\t\t}");
				code.AppendLine("\t}");
				code.AppendLine();
                code.AppendLine("\tpublic class InheritedDirectRouteProvider : DefaultDirectRouteProvider");
				code.AppendLine("\t{");
				code.AppendLine("\t\tprotected override IReadOnlyList<IDirectRouteFactory> GetActionRouteFactories(HttpActionDescriptor actionDescriptor)");
				code.AppendLine("\t\t{");
				code.AppendLine("\t\t\treturn actionDescriptor.GetCustomAttributes<IDirectRouteFactory>(inherit: true);");
				code.AppendLine("\t\t}");
				code.AppendLine("\t}");
				code.AppendLine();
				code.AppendLine("\tpublic static class ServiceController");
				code.AppendLine("\t{");
				code.AppendLine("\t\tprivate static IDisposable _serviceConfig;");
				if (Target.TargetTypes.OfType<WebApiData>().Any(a => a.HasSql))
				{
					code.AppendLine("\t\tprivate static string _sqlConnectionString;");
					code.AppendLine("\t\tprivate static SqlCredential _sqlCredential;");
					code.AppendLine("\t\tpublic static void Start(string baseUri, SqlConnectionStringBuilder sqlBuilder)");
					code.AppendLine("\t\t{");
					code.AppendLine("\t\t\t//Configure SQL Server Connections");
					code.AppendLine("\t\t\tvar secure = new SecureString();");
					code.AppendLine("\t\t\tforeach(var c in sqlBuilder.Password.ToCharArray()) secure.AppendChar(c);");
					code.AppendLine("\t\t\tsecure.MakeReadOnly();");
					code.AppendLine("\t\t\t_sqlCredential = new SqlCredential(sqlBuilder.UserID, secure);");
					code.AppendLine("\t\t\tsqlBuidler.UserID = string.Empty;");
					code.AppendLine("\t\t\tsqlBuidler.Password = string.Empty;");
					code.AppendLine("\t\t\t_sqlConnectionString = sqlBuilder.ToString();");
					code.AppendLine("\t\t\t_serviceConfig = WebApp.Start<WebAppConfiguration>(baseUri);");
					code.AppendLine("\t\t}");
					code.AppendLine();
					code.AppendLine("\t\tpublic static SqlConnection CreateAndOpen()");
					code.AppendLine("\t\t{");
					code.AppendLine("\t\t\tvar sql = new SqlConnection(_sqlConnectionString, _sqlCredential);");
					code.AppendLine("\t\t\tsql.Open();");
					code.AppendLine("\t\t\treturn sql;");
					code.AppendLine("\t\t}");
					code.AppendLine();
					code.AppendLine("\t\tpublic static async Task<SqlConnection> CreateAndOpenAsync()");
					code.AppendLine("\t\t{");
					code.AppendLine("\t\t\tvar sql = new SqlConnection(_sqlConnectionString, _sqlCredential);");
					code.AppendLine("\t\t\tawait sql.OpenAsync();");
					code.AppendLine("\t\t\treturn sql;");
					code.AppendLine("\t\t}");
				}
				else
				{
					code.AppendLine("\t\tpublic static void Start(string baseUri)");
					code.AppendLine("\t\t{");
					code.AppendLine("\t\t\t_serviceConfig = WebApp.Start<WebAppConfiguration>(baseUri);");
					code.AppendLine("\t\t}");
				}
				code.AppendLine();
				code.AppendLine("\t\tpublic static void Stop()");
				code.AppendLine("\t\t{");
				code.AppendLine("\t\t\tif (_serviceConfig != null) _serviceConfig.Dispose();");
				code.AppendLine("\t\t}");
				code.AppendLine("\t}");
				code.AppendLine("}");
			}

			//Generate project
			if (Server)
			{
				if (Target.Framework == ProjectGenerationFramework.NET45 || Target.Framework == ProjectGenerationFramework.WINRT)
					code.AppendLine(NamespaceGenerator.GenerateServerCode45(Data.Namespace, Target));
			}
			else
			{
				if (Target.Framework == ProjectGenerationFramework.NET45) code.AppendLine(NamespaceGenerator.GenerateClientCode45(Data.Namespace, Target));
				if (Target.Framework == ProjectGenerationFramework.WINRT) code.AppendLine(NamespaceGenerator.GenerateClientCodeRT8(Data.Namespace, Target));
			}

			//Reenable XML documentation warnings
			code.AppendLine(string.Format("#pragma warning restore 0649{0}", !Data.EnableDocumentationWarnings ? ", 1591" : ""));

			return code.ToString();
		}

		private void AddMessage(CompileMessage Message)
		{
			Messages.Add(Message);
			if (Message.Severity == CompileMessageSeverity.ERROR && HighestSeverity != CompileMessageSeverity.ERROR) HighestSeverity = CompileMessageSeverity.ERROR;
			if (Message.Severity == CompileMessageSeverity.WARN && HighestSeverity == CompileMessageSeverity.INFO) HighestSeverity = CompileMessageSeverity.WARN;
			NewOutput(string.Format("{0} {1}: {2} Object: {3} Owner: {4}", Message.Severity, Message.Code, Message.Description, Message.ErrorObject, Message.Owner));
			NewMessage(Message);
		}
	}
}