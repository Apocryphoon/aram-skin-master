﻿using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Management;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;

namespace ARAMSkinBooster
{
    class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
                Console.WriteLine("================================== ARAM BOOSTER ==================================");
				Console.WriteLine("[>] CRÉDITOS AO CRIADOR!");
				Console.WriteLine("[>] O programa estava quebrado eu apenas arrumei.");
				Console.WriteLine("[+] Aguarde até que a habilidade esteja totalmente pronta para uso.");
				Thread.Sleep(1500);
				Console.WriteLine("[+] O programa está pronto para uso.");
				Console.WriteLine(" ");
				Console.WriteLine("[-] PARA USAR DIGITE: aram");
				string input = Console.ReadLine();
				if (input == "aram")
				{
					try
					{
						ValueTuple<string, string> valueTuple = GetInfo();
						PostRequest(valueTuple.Item1, valueTuple.Item2);
						Thread.Sleep(1500);
					}
					catch (Exception)
					{
						Console.WriteLine("Não foi possivel continuar, encontramos um erro.");
					}
				}
			}
		}

		private static ValueTuple<string, string> GetInfo()
		{
			string text = "";
			string text2 = "";
			ManagementClass managementClass = new ManagementClass("Win32_Process");
			foreach (ManagementBaseObject managementBaseObject in managementClass.GetInstances())
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				if (managementObject["Name"].Equals("LeagueClientUx.exe"))
				{
					foreach (object obj in Regex.Matches(managementObject["CommandLine"].ToString(), string_0, regexOptions_0))
					{
						Match match = (Match)obj;
						if (!string.IsNullOrEmpty(match.Groups["port"].ToString()))
						{
							text2 = match.Groups["port"].ToString();
						}
						else if (!string.IsNullOrEmpty(match.Groups["token"].ToString()))
						{
							text = match.Groups["token"].ToString();
						}
					}
				}
			}
			if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
			{
				Console.WriteLine("Error, cliet não está aberto!");
			}
			return new ValueTuple<string, string>(text, text2);
		}

		static void PostRequest(string string_1, string string_2)
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback)Delegate.Combine(ServicePointManager.ServerCertificateValidationCallback, new RemoteCertificateValidationCallback(Class1.NewClass.Main));
			RestClient restClient = new RestClient("https://127.0.0.1:" + string_2)
			{
				Authenticator = new HttpBasicAuthenticator("riot", string_1)
			};
			RestRequest request = new RestRequest("/lol-champ-select/v1/team-boost/purchase", Method.POST);
			restClient.Execute(request);
		}

		private static readonly string string_0 = "\"--remoting-auth-token=(?'token'.*?)\" | \"--app-port=(?'port'|.*?)\"";
		private static readonly RegexOptions regexOptions_0 = RegexOptions.Multiline;

		[CompilerGenerated]
		[Serializable]
		private sealed class Class1
		{
			internal bool Main(object object_0, X509Certificate x509Certificate_0, X509Chain x509Chain_0, SslPolicyErrors sslPolicyErrors_0)
			{
				return true;
			}

			public static readonly Class1 NewClass = new Class1();
            		public static RemoteCertificateValidationCallback callback;
		}
	}
}
