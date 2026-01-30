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
        //TODO: Premium Widget feature is disabled temporarily
        /*private readonly List<string> _availablePremiumWidgetStyles = Enum.GetValues(typeof(PremiumWidgetVisualizationType))
            .Cast<PremiumWidgetVisualizationType>()
            .Where(value => value != PremiumWidgetVisualizationType.Undefined)
            .Select(value => value.ToString())
            .ToList();*/

        [SerializeField] private InputField userIdInput;
        [SerializeField] private SessionPanel sessionPanel;
        [SerializeField] private Button initializeButton;
        [SerializeField] private OffersByPlacementIdPanel offersByPlacementIdPanel;
        [SerializeField] private RoutingPanel routingPanel;
        [SerializeField] private Dropdown languageSelectionDropdown;
        //Premium Widget feature is disabled temporarily
        // [SerializeField] private Dropdown premiumWidgetVisualTypeDropdown;
        [SerializeField] private AdvancedOptionsPanel advancedOptionsPanel;
        [SerializeField] private ScreenOrientationPreferenceHandler screenOrientationPreferenceHandler;

        private void Start()
        {
            initializeButton.onClick.AddListener(OnInitialize);
            routingPanel.RouteClicked += OnShowOffersWithDeepLink;
            sessionPanel.OffersClicked += OnShowOffers;
            
            DisableButtons();
            SetupDropdowns();
            userIdInput.text = TyrSDKPlugin.Instance.GetUserId();
            _ = LoginUserAsync();
        }

        private void OnDestroy()
        {
            initializeButton.onClick.RemoveListener(OnInitialize);
            routingPanel.RouteClicked -= OnShowOffersWithDeepLink;
            sessionPanel.OffersClicked -= OnShowOffers;
        }

        private void SetupDropdowns()
        {
            SetupLanguagesDropdown();
            //Premium Widget feature is disabled temporarily
            // SetupVisualStyleDropdown();
            screenOrientationPreferenceHandler.SetupScreenOrientationDropdown();
        }

        private void SetupLanguagesDropdown()
        {
            languageSelectionDropdown.ClearOptions();
            List<string> languages = _availableLanguages.Keys.ToList();
            languageSelectionDropdown.AddOptions(languages);
            languageSelectionDropdown.onValueChanged.AddListener(OnSelectLanguage);
        }

        //TODO: Premium Widget feature is disabled temporarily
        /*private void SetupVisualStyleDropdown()
        {
            premiumWidgetVisualTypeDropdown.ClearOptions();
            premiumWidgetVisualTypeDropdown.AddOptions(_availablePremiumWidgetStyles);
            premiumWidgetVisualTypeDropdown.onValueChanged.AddListener(OnSelectVisualStyle);
        }*/

        private void DisableButtons()
        {
            sessionPanel.SwitchButtonInteractability(false);
            routingPanel.SwitchButtonInteractability(false);
            offersByPlacementIdPanel.SwitchButtonInteractability(false);
            initializeButton.interactable = false;
        }

        private async Task LoginUserAsync()
        {
            LoginData loginData = GetLoginData();
            LoginResult result = await TyrSDKPlugin.Instance.LoginUserAsync(loginData);

            if (result.IsSuccessful)
            {
                string userId = TyrSDKPlugin.Instance.GetUserId();
                userIdInput.text = userId;
            }
            
            sessionPanel.SwitchButtonInteractability(result.IsSuccessful);
            routingPanel.SwitchButtonInteractability( result.IsSuccessful);
            offersByPlacementIdPanel.SwitchButtonInteractability( result.IsSuccessful);
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

            if (sessionPanel.GetSessionConfig().IsValid())
            {
                Debug.Log($"{DebugTag} Initializing TyrAds with session");
                TyrSDKPlugin.Instance.Init(new InitConfig(sessionPanel.GetSessionConfig(), screenOrientationPreferenceHandler.OrientationPreference));
            }

            DisableButtons();
            _ = LoginUserAsync();
        }

        private void OnShowOffers()
        {
            OffersRoutingData offersRoutingData = new (sessionPanel.PlacementId);
            TyrSDKPlugin.Instance.ShowOffers(offersRoutingData);
        }

        private void OnShowOffersWithDeepLink((string Route, string CampaignId) routingData)
        {
            if (!TyrSDKPlugin.Instance.IsInitialized)
            {
                Debug.LogError($"{DebugTag} No registered sessions found");
                return;
            }

            OffersRoutingData offersRoutingData = new (sessionPanel.PlacementId, routingData.Route, routingData.CampaignId);
            TyrSDKPlugin.Instance.ShowOffers(offersRoutingData);
        }

        private void OnSelectLanguage(int index)
        {
            Dropdown.OptionData selectedLanguage = languageSelectionDropdown.options[index];

            if (_availableLanguages.TryGetValue(selectedLanguage.text, out string languageCode))
            {
                TyrSDKPlugin.Instance.SetLanguage(languageCode);
            }
        }

        //TODO: Premium Widget feature is disabled temporarily
        /*private void OnSelectVisualStyle(int index)
        {
            Dropdown.OptionData selectedStyle = premiumWidgetVisualTypeDropdown.options[index];

            if (Enum.TryParse(selectedStyle.text, out PremiumWidgetVisualizationType visualStyle))
            {
                TyrSDKPlugin.Instance.SetPremiumWidgetStyle(visualStyle);
            }
        }*/
    }
}