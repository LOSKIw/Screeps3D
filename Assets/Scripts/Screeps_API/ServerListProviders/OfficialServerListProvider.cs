﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Screeps_API;

namespace Assets.Scripts.Screeps_API.ServerListProviders
{
    public class OfficialServerListProvider : IServerListProvider
    {
        public bool MergeWithCache
        {
            get { return true; }
        }

        public void Load(Action<IEnumerable<ServerCache>> callback)
        {
            var serverList = new List<ServerCache>();

            var publicServer = new ServerCache
            {
                Type = SourceProviderType.Official,
                Name = "Screeps.com",
                Address = {HostName = "screeps.com", Path = "/", Port = "443", Ssl = true}
            };
            serverList.Add(publicServer);

            var ptr = new ServerCache
            {
                Type = SourceProviderType.Official,
                Name = "PTR Screeps.com",
                Address = { HostName = "screeps.com", Path = "/ptr", Port = "443", Ssl = true }
            };

            serverList.Add(ptr);

            var season = new ServerCache
            {
                Type = SourceProviderType.Official,
                Name = "SEASONAL Screeps.com",
                Address = { HostName = "screeps.com", Path = "/season", Port = "443", Ssl = true },
            };

            serverList.Add(season);

            callback(serverList);
        }
    }
}