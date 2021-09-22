using System;
using System.Collections.Generic;

namespace WebAPI.Services
{
	public class LicenseService
	{
		private readonly Dictionary<string, DateTime> _store;
		private readonly TimeSpan _keyLifeTime = TimeSpan.FromDays(30);

		public LicenseService()
		{
			_store = new Dictionary<string, DateTime>();
		}

		public void RegisterKey(string key)
		{
			var time = DateTime.Now.Add(_keyLifeTime);
			_store[key] = time;
		}

		public bool KeyIsActive(string key)
		{
			if (!_store.TryGetValue(key, out var time))
			{
				return false;
			}

			return DateTime.Now < time;
		}
	}
}