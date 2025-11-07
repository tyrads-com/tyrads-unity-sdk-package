using System;
using System.Collections.Generic;
using System.Linq;
using TyrAds.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class TyrAdsDemo : MonoBehaviour
    {
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
        private readonly List<string> _availableDeepLinkRoutes = new()
        {
            TyradsDeepRoutes.Offers,
            TyradsDeepRoutes.ActiveOffers,
            TyradsDeepRoutes.Offer,
            TyradsDeepRoutes.Support
        };

        [SerializeField] private InputField apiKeyInput;
        [SerializeField] private InputField apiSecretInput;
        [SerializeField] private InputField encryptionKeyInput;
        [SerializeField] private InputField userIdInput;
        [SerializeField] private InputField engagementIdInput;
        [SerializeField] private Button initializeButton;
        [SerializeField] private Button clearUserButton;
        [SerializeField] private Button showButton;
        [SerializeField] private Dropdown languageSelectionDropdown;
        [SerializeField] private Dropdown premiumWidgetVisualTypeDropdown;
        [SerializeField] private Toggle useUserInfoToggle;
        [SerializeField] private Toggle useMediaSourceInfoToggle;
        [SerializeField] private Toggle engagementInfoToggle;
        [SerializeField] private Dropdown deepLinkRouteDropdown;
        [SerializeField] private InputField campaignIdInput;
        [SerializeField] private Button deepLinkButton;

        private void Start()
        {
            clearUserButton.onClick.AddListener(ClearUser);
            initializeButton.onClick.AddListener(Initialize);
            showButton.onClick.AddListener(ShowOffers);
            deepLinkButton.onClick.AddListener(ShowOffersWithDeepLink);
            SetupDropdown();

            TyrSDKPlugin.Instance.InitializationCompleted += OnSdkInitializationCompleted;

            string userId = TyrSDKPlugin.Instance.GetUserId();
            userIdInput.text = userId;
            initializeButton.interactable = false;

            TyradsUserInfo userInfo = GetTyradsUserInfo();
            TyradsMediaSourceInfo mediaInfo = GetTyradsMediaSourceInfo();
            TyradsEngagementInfo engagementInfo = GetTyradsEngagementInfo();
            TyrSDKPlugin.Instance.LoginUser(userId, userInfo, mediaInfo, engagementInfo);
        }

        private TyradsEngagementInfo GetTyradsEngagementInfo()
        {
            if (!engagementInfoToggle.isOn)
            {
                return null;
            }

            if (string.IsNullOrEmpty(engagementIdInput.text))
            {
                return null;
            }
            
            if (int.TryParse(engagementIdInput.text, out int engagementId))
            {
                return new TyradsEngagementInfo(engagementId: engagementId);
            }
            
            Debug.LogWarning($"Invalid engagement ID: '{engagementIdInput.text}'. Must be a valid integer.");
            return null;
        }

        private TyradsMediaSourceInfo GetTyradsMediaSourceInfo()
        {
            if (!useMediaSourceInfoToggle.isOn)
            {
                return null;
            }

            return new TyradsMediaSourceInfo(
                mediaSourceName: "Facebook",
                mediaCampaignName: "Summer Sale Campaign",
                mediaSourceId: "fb_123",
                mediaSubSourceId: "fb_sub_456",
                incentivized: true,
                mediaAdsetName: "Summer Sale Adset",
                mediaAdsetId: "adset_789",
                mediaCreativeName: "Summer Sale Creative",
                mediaCreativeId: "creative_101",
                sub1: "campaign_source",
                sub2: "ad_group",
                sub3: "creative_type",
                sub4: "placement",
                sub5: "custom_param"
            );
        }

        private TyradsUserInfo GetTyradsUserInfo()
        {
            if (!useUserInfoToggle.isOn)
            {
                return null;
            }

            return new TyradsUserInfo(
                userPhoneNumber: "+1234567890",
                userEmail: "demo@example.com",
                userGroup: "premium_users"
            );
        }

        private void Initialize()
        {
            if (TyrSDKPlugin.Instance.IsInitialized)
            {
                TyrSDKPlugin.Instance.ClearUserData();
            }

            string apiKey = apiKeyInput.text.Trim();
            string apiSecret = apiSecretInput.text.Trim();
            string encryptionKey = encryptionKeyInput.text.Trim();

            if (!string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(apiSecret))
            {
                TyrSDKPlugin.Instance.Init(apiKey, apiSecret, encryptionKey);
            }

            string userId = userIdInput.text.Trim();
            initializeButton.interactable = false;
            
            TyradsUserInfo userInfo = GetTyradsUserInfo();
            TyradsMediaSourceInfo mediaInfo = GetTyradsMediaSourceInfo();
            TyradsEngagementInfo engagementInfo = GetTyradsEngagementInfo();
            TyrSDKPlugin.Instance.LoginUser(userId, userInfo, mediaInfo, engagementInfo);
        }

        private void ShowOffers()
        {
            TyrSDKPlugin.Instance.ShowOffers();
        }

        private void ClearUser()
        {
            TyrSDKPlugin.Instance.ClearUserData();
        }

        private void SetupDropdown()
        {
            SetupLanguagesDropdown();
            SetupVisualStyleDropdown();
            SetupDeepLinkRouteDropdown();
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

        private void SetupDeepLinkRouteDropdown()
        {
            if (deepLinkRouteDropdown == null)
            {
                Debug.LogWarning("Deep Link Route Dropdown is not assigned in the inspector");
                return;
            }

            deepLinkRouteDropdown.ClearOptions();
            deepLinkRouteDropdown.AddOptions(_availableDeepLinkRoutes);
            deepLinkRouteDropdown.onValueChanged.AddListener(OnSelectDeepLinkRoute);
            
            if (campaignIdInput != null)
            {
                campaignIdInput.gameObject.SetActive(false);
            }
        }

        private void OnSdkInitializationCompleted(bool tyrAdsInitialized)
        {
            showButton.interactable = tyrAdsInitialized;
            deepLinkButton.interactable = tyrAdsInitialized;
            initializeButton.interactable = true;
        }

        private void ShowOffersWithDeepLink()
        {
            if (deepLinkRouteDropdown == null)
            {
                return;
            }

            int selectedIndex = deepLinkRouteDropdown.value;
            string selectedRoute = _availableDeepLinkRoutes[selectedIndex];
            string campaignId = campaignIdInput != null ? campaignIdInput.text.Trim() : string.Empty;

            if ((selectedRoute == TyradsDeepRoutes.Offer || selectedRoute == TyradsDeepRoutes.Support)
                && !string.IsNullOrEmpty(campaignId))
            {
                TyrSDKPlugin.Instance.ShowOffers(selectedRoute, campaignId);
            }
            else
            {
                TyrSDKPlugin.Instance.ShowOffers(selectedRoute);
            }
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

        private void OnSelectDeepLinkRoute(int index)
        {
            if (campaignIdInput == null)
            {
                return;
            }

            string selectedRoute = _availableDeepLinkRoutes[index];
            
            if (selectedRoute == TyradsDeepRoutes.Offer || selectedRoute == TyradsDeepRoutes.Support)
            {
                campaignIdInput.gameObject.SetActive(true);
            }
            else
            {
                campaignIdInput.gameObject.SetActive(false);
            }
        }
    }
}