﻿using Common.Utils;
using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    public class TerminalView : MonoBehaviour, IObjectViewComponent
    {
        [SerializeField] private ScaleVisAxes _energyDisplay;
        private Terminal _terminal;

        public void Init()
        {
        }

        public void Load(RoomObject roomObject)
        {
            _terminal = roomObject as Terminal;
            AdjustScale();
        }

        public void Delta(JSONObject data)
        {
            AdjustScale();
        }

        public void Unload(RoomObject roomObject)
        {
        }

        private void AdjustScale()
        {
            _energyDisplay.SetVisibility(_terminal.TotalResources / _terminal.StoreCapacity);
        }
    }
}