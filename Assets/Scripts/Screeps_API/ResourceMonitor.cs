﻿using Assets.Scripts.Common.SettingsManagement;
using Common;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Screeps_API
{
    public class ResourceMonitor : MonoBehaviour
    {
        public Action OnResources;

        public float Credits { get; private set; }
        public int CPUUnlocks { get; private set; }
        public int Pixels { get; private set; }
        public int Keys { get; private set; }

        private Queue<JSONObject> queue = new Queue<JSONObject>();

        private void Start()
        {
            Debug.Log("Resource Monitor started");
            ScreepsAPI.OnConnectionStatusChange += SubscribeResources;
        }

        private void SubscribeResources(bool connected)
        {
            Debug.Log("connected: " + connected);
            if (connected)
            {
                Debug.Log("subscribing to resources");
                ScreepsAPI.Socket.Subscribe(string.Format("user:{0}/resources", ScreepsAPI.Me.UserId), RecieveData);
            }
        }

        private void RecieveData(JSONObject data)
        {
            
            queue.Enqueue(data);
        }

        private void Update()
        {
            if (queue.Count == 0)
                return;
            UnpackResources(queue.Dequeue());

        }

        private void UnpackResources(JSONObject data)
        {
            Debug.Log(data.ToString());
            // pixels
            // credits

            // cpu-unlock
            // keys
            var creditsData = data["credits"];
            if (creditsData != null)
            {
                Credits = creditsData.n;
            }

            var cpuUnlockData = data["cpu-unlock"];
            if (cpuUnlockData != null)
            {
                CPUUnlocks = (int)cpuUnlockData.n;
            }

            var pixelData = data["pixel"];
            if (pixelData != null)
            {
                Pixels = (int)pixelData.n;
            }

            var keysData = data["keys"];
            if (keysData != null)
            {
                Keys = (int)keysData.n;
            }

            if (OnResources != null)
                OnResources();
        }
    }
}