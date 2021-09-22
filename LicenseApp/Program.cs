using DeviceId;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LicenseApp
{
	internal class Program
	{
		private static async Task Main(string[] args)
		{
			var deviceKey = new DeviceIdBuilder()
				.AddOsVersion()
				.OnWindows(windows => windows
					.AddProcessorId()
					.AddMotherboardSerialNumber()
					.AddSystemDriveSerialNumber())
				.ToString();

			try
			{
				if (!await CheckLicense(deviceKey))
				{
					Console.WriteLine("Need to register this key");
					Console.WriteLine($"[{deviceKey}]");
					Console.WriteLine("Send his to author");
					Console.ReadKey();
					return;
				}

				Console.WriteLine("App license is active");
				Console.WriteLine("Lets go!");
			}
			catch (HttpRequestException)
			{
				Console.WriteLine("Failed to connect to remote server. Please try again later.");
			}
			finally
			{
				Console.ReadKey();
			}
		}

		private static async Task<bool> CheckLicense(string key)
		{
			using var http = new HttpClient();

			var url = $"http://localhost:5000/api/license/is-active/{key}";
			var json = await http.GetStringAsync(url);

			return JsonConvert.DeserializeObject<bool>(json);
		}
	}
}