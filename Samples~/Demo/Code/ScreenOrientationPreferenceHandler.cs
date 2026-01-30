using System;
using System.Collections.Generic;
using System.Linq;
using TyrAds.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class ScreenOrientationPreferenceHandler : MonoBehaviour
    {
        private readonly List<ScreenOrientationPreference> _screenOrientationPreferences = Enum.GetValues(typeof(ScreenOrientationPreference))
            .Cast<ScreenOrientationPreference>()
            .ToList();
        
        [SerializeField] private Dropdown screenOrientationDropdown;
        
        public ScreenOrientationPreference?  OrientationPreference { get; private set; }

        public void SetupScreenOrientationDropdown()
        {
            screenOrientationDropdown.ClearOptions();
            List<string> preferences = _screenOrientationPreferences.Select(value => value.ToString()).ToList();
            screenOrientationDropdown.AddOptions(preferences);
            screenOrientationDropdown.onValueChanged.AddListener(OnScreenOrientationPreferenceChanged);
        }

        private void OnDestroy()
        {
            screenOrientationDropdown.onValueChanged.RemoveListener(OnScreenOrientationPreferenceChanged);
        }

        private void OnScreenOrientationPreferenceChanged(int index)
        {
            if (index < 0 || index >= _screenOrientationPreferences.Count)
            {
                return;
            }

            ScreenOrientationPreference selectedPreference = _screenOrientationPreferences[index];
            
            OrientationPreference = selectedPreference == ScreenOrientationPreference.None 
                ? null 
                : selectedPreference;
        }
    }
}

