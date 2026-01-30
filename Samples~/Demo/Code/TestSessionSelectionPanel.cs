using System;
using System.Collections.Generic;
using TyrAds.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class TestSessionSelectionPanel : MonoBehaviour
    {
        public event Action<SessionConfig> ApplySessionConfig;
        
        [SerializeField] private Dropdown sessionSelection;
        [SerializeField] private Button applyButton;
        
        private readonly TestSessionsProvider _testSessionsProvider = new();
        private TestSessionConfig _selectedSessionConfig;

        private void Awake()
        {
            bool isTestSessionAvailable = _testSessionsProvider.TestSessions.Count > 0;
            gameObject.SetActive(isTestSessionAvailable);

            applyButton.onClick.AddListener(OnApplyButtonClicked);
            sessionSelection.onValueChanged.AddListener(OnSessionSelected);
            
            InitializeDropdown(_testSessionsProvider.TestSessions);
        }

        private void OnDestroy()
        {
            applyButton.onClick.RemoveListener(OnApplyButtonClicked);
            sessionSelection.onValueChanged.RemoveListener(OnSessionSelected);
        }

        private void InitializeDropdown(IReadOnlyDictionary<string, TestSessionConfig> testSessions)
        {
            sessionSelection.ClearOptions();

            foreach (KeyValuePair<string, TestSessionConfig> config in testSessions)
            {
                sessionSelection.options.Add(new Dropdown.OptionData(config.Key));
            }
            
            sessionSelection.value = 0;
            OnSessionSelected(0);
        }

        private void OnSessionSelected(int index)
        {
            Dropdown.OptionData config = sessionSelection.options[index];

            if (!_testSessionsProvider.TestSessions.TryGetValue(config.text, out TestSessionConfig sessionConfig))
            {
                 Debug.LogError($"Failed to find session config for {config.text}");
                 return;
            }
            
            _selectedSessionConfig = sessionConfig;
        }

        private void OnApplyButtonClicked()
        {
            ApplySessionConfig?.Invoke(_selectedSessionConfig.Config);
        }
    }
}