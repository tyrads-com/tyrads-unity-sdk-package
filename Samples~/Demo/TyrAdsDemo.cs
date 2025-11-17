using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TyrAds.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class TyrAdsDemo : MonoBehaviour
    {
        private const string DebugTag = "[" + nameof(TyrAdsDemo) + "]";

        private readonly Dictionary<string, string> _availableLanguages = new()
        {
            { nameof(LanguageCode.English), LanguageCode.English },
            { nameof(LanguageCode.Spanish), LanguageCode.Spanish },
            { nameof(LanguageCode.Indonesian), LanguageCode.Indonesian },
            { nameof(LanguageCode.Japanese), LanguageCode.Japanese },
            { nameof(LanguageCode.Korean), LanguageCode.Korean },
            { nameof(LanguageCode.ChineseSimplified), LanguageCode.ChineseSimplified }
        };
        private readonly List<string> _availablePremiumWidgetStyles = Enum.GetValues(typeof(PremiumWidgetVisualizationType))
            .Cast<PremiumWidgetVisualizationType>()
            .Where(value => value != PremiumWidgetVisualizationType.Undefined)
            .Select(value => value.ToString())
            .ToList();
        
        [SerializeField] private InputField userIdInput;
        [SerializeField] private SessionPanel[] sessionPanels;
        [SerializeField] private Button initializeButton;
        [SerializeField] private Button switchToNextSessionButton;
        [SerializeField] private RoutingPanel routingPanel;
        [SerializeField] private Dropdown languageSelectionDropdown;
        [SerializeField] private Dropdown premiumWidgetVisualTypeDropdown;
        [SerializeField] private AdvancedOptionsPanel advancedOptionsPanel;

        private int _sessionId;
        private IReadOnlyList<string> _registeredSessions;

        private void Start()
        {
            initializeButton.onClick.AddListener(OnInitialize);
            switchToNextSessionButton.onClick.AddListener(OnSwitchToNextSession);
            routingPanel.RouteClicked += OnShowOffersWithDeepLink;
                
            foreach (SessionPanel panel in sessionPanels)
            {
                panel.OffersClicked += OnShowOffers;
            }
            
            DisableButtons();
            SetupDropdowns();
            userIdInput.text = TyrSDKPlugin.Instance.GetUserId();
            _ = LoginUserAsync();
        }

        private void OnDestroy()
        {
            initializeButton.onClick.RemoveListener(OnInitialize);
            switchToNextSessionButton.onClick.RemoveListener(OnSwitchToNextSession);
            routingPanel.RouteClicked -= OnShowOffersWithDeepLink;
            
            foreach (SessionPanel panel in sessionPanels)
            {
                panel.OffersClicked -= OnShowOffers;
            }
        }

        private void SetupDropdowns()
        {
            SetupLanguagesDropdown();
            SetupVisualStyleDropdown();
        }

        private void SetupLanguagesDropdown()
        {
            languageSelectionDropdown.ClearOptions();
            List<string> languages = _availableLanguages.Keys.ToList();
            languageSelectionDropdown.AddOptions(languages);
            languageSelectionDropdown.onValueChanged.AddListener(OnSelectLanguage);
        }

        private void SetupVisualStyleDropdown()
        {
            premiumWidgetVisualTypeDropdown.ClearOptions();
            premiumWidgetVisualTypeDropdown.AddOptions(_availablePremiumWidgetStyles);
            premiumWidgetVisualTypeDropdown.onValueChanged.AddListener(OnSelectVisualStyle);
        }

        private void DisableButtons()
        {
            foreach (SessionPanel panel in sessionPanels)
            {
                panel.SwitchButtonInteractability(null);
            }

            switchToNextSessionButton.interactable = false;
            routingPanel.SwitchButtonInteractability(false);
            initializeButton.interactable = false;
        }

        private async Task LoginUserAsync()
        {
            LoginData loginData = GetLoginData();
            LoginResult result = await TyrSDKPlugin.Instance.LoginUserAsync(loginData);
            _registeredSessions = result.InitializedSessions;
            _sessionId = 0;

            if (result.IsSuccessful)
            {
                userIdInput.text = TyrSDKPlugin.Instance.GetUserId();
            }
            
            foreach (SessionPanel panel in sessionPanels)
            {
                panel.SwitchButtonInteractability(_registeredSessions);
            }
            
            switchToNextSessionButton.interactable = _registeredSessions is { Count: > 1 };
            routingPanel.SwitchButtonInteractability( result.IsSuccessful);
            initializeButton.interactable = true;
        }

        private LoginData GetLoginData()
        {
            string userId = userIdInput.text.Trim();
            TyradsUserInfo userInfo = advancedOptionsPanel.GetTyradsUserInfo();
            TyradsMediaSourceInfo mediaInfo = advancedOptionsPanel.GetTyradsMediaSourceInfo();
            TyradsEngagementInfo engagementInfo = advancedOptionsPanel.GetTyradsEngagementInfo();

            return new LoginData(userId, userInfo, mediaInfo, engagementInfo);
        }

        private void OnInitialize()
        {
            if (TyrSDKPlugin.Instance.IsInitialized)
            {
                TyrSDKPlugin.Instance.ClearUserData();
            }

            SessionConfig[] sessions = sessionPanels
                .Select(panel => panel.GetSessionConfig())
                .Where(config => config.IsValid())
                .ToArray();

            if (sessions.Length > 0)
            {
                Debug.Log($"{DebugTag} Initializing TyrAds with sessions: {string.Join(", ", sessions.Select(session => session.Id))}");
                TyrSDKPlugin.Instance.Init(sessions);
            }

            DisableButtons();
            _ = LoginUserAsync();
        }

        private void OnShowOffers(string sessionId)
        {
            TyrSDKPlugin.Instance.ShowOffers(new OffersRoutingData(sessionId));
        }

        private void OnShowOffersWithDeepLink((string Route, string CampaignId) routingData)
        {
            if (_registeredSessions == null)
            {
                Debug.LogWarning($"{DebugTag} No registered sessions found");
                return;
            }
            
            string sessionId = _registeredSessions[_sessionId];
            TyrSDKPlugin.Instance.ShowOffers(new OffersRoutingData(sessionId, routingData.Route, routingData.CampaignId));
        }

        private void OnSelectLanguage(int index)
        {
            Dropdown.OptionData selectedLanguage = languageSelectionDropdown.options[index];

            if (_availableLanguages.TryGetValue(selectedLanguage.text, out string languageCode))
            {
                TyrSDKPlugin.Instance.SetLanguage(languageCode);
            }
        }

        private void OnSelectVisualStyle(int index)
        {
            Dropdown.OptionData selectedStyle = premiumWidgetVisualTypeDropdown.options[index];

            if (Enum.TryParse(selectedStyle.text, out PremiumWidgetVisualizationType visualStyle))
            {
                TyrSDKPlugin.Instance.SetPremiumWidgetStyle(visualStyle);
            }
        }
        
        private void OnSwitchToNextSession()
        {
            if (_registeredSessions == null)
            {
                Debug.LogWarning($"{DebugTag} No registered sessions found");
                return;
            }
            
            _sessionId = (_sessionId + 1) % _registeredSessions.Count;
            string sessionId = _registeredSessions[_sessionId];
            Debug.Log($"{DebugTag} Switching to session: {sessionId}");
            TyrSDKPlugin.Instance.SwitchToSession(sessionId);
        }
    }
}