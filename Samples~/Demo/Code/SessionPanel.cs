using System;
using TyrAds.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class SessionPanel : MonoBehaviour
    {
        public event Action OffersClicked;
        
        [SerializeField] private InputField apiKeyInput;
        [SerializeField] private InputField apiSecretInput;
        [SerializeField] private InputField encryptionKeyInput;
        [SerializeField] private InputField placementIdInput;
        [SerializeField] private Button showOffersButton;
        [SerializeField] private TestSessionSelectionPanel testSessionSelectionPanel;

        public uint PlacementId { get; private set; }

        private void Awake()
        {
            showOffersButton.onClick.AddListener(OnOffersClicked);
            placementIdInput.onValueChanged.AddListener(OnPlacementIdValueChanged);
            testSessionSelectionPanel.ApplySessionConfig += OnApplySessionSelectionConfig;
        }

        private void OnDestroy()
        {
            showOffersButton.onClick.RemoveListener(OnOffersClicked);
            testSessionSelectionPanel.ApplySessionConfig -= OnApplySessionSelectionConfig;
        }

        public SessionConfig GetSessionConfig()
        {
            return new SessionConfig(apiKeyInput.text.Trim(), apiSecretInput.text.Trim(), encryptionKeyInput.text.Trim());
        }

        public void SwitchButtonInteractability(bool isInitialized)
        {
            showOffersButton.interactable = isInitialized;
        }

        private void OnOffersClicked()
        {
            OffersClicked?.Invoke(); 
        }

        private void OnApplySessionSelectionConfig(SessionConfig config)
        {
            apiKeyInput.text = config.ApiKey;
            apiSecretInput.text = config.ApiSecret;
            encryptionKeyInput.text = config.EncryptionKey;
        }

        private void OnPlacementIdValueChanged(string value)
        {
            PlacementId = uint.TryParse(value, out uint placementId) ? placementId : 0;
        }
    }
}