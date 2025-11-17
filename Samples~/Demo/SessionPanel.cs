using System;
using System.Collections.Generic;
using System.Linq;
using TyrAds.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class SessionPanel : MonoBehaviour
    {
        public event Action<string> OffersClicked;
        
        [SerializeField] private Text sessionId;
        [SerializeField] private InputField apiKeyInput;
        [SerializeField] private InputField apiSecretInput;
        [SerializeField] private InputField encryptionKeyInput;
        [SerializeField] private Button showOffersButton;

        private string SessionId { get; set; }

        private void Awake()
        {
            SessionId = sessionId.text.Trim();
            showOffersButton.onClick.AddListener(OnOffersClicked);
        }

        private void OnDestroy()
        {
            showOffersButton.onClick.RemoveListener(OnOffersClicked);
        }

        public SessionConfig GetSessionConfig()
        {
            return new SessionConfig(SessionId, apiKeyInput.text.Trim(), apiSecretInput.text.Trim(), encryptionKeyInput.text.Trim());
        }

        public void SwitchButtonInteractability(IReadOnlyList<string> initializedSessions)
        {
            if (initializedSessions == null)
            {
                showOffersButton.interactable = false;
                return;
            }

            bool isInteractable = initializedSessions.Contains(SessionId);
            showOffersButton.interactable = isInteractable;
        }

        private void OnOffersClicked()
        {
            OffersClicked?.Invoke(SessionId); 
        }
    }
}