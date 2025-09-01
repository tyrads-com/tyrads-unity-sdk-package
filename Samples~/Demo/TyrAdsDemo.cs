using System.Collections.Generic;
using System.Linq;
using TyrAds.SDK.Data;
using TyrAds.Internal.Infrastructure.Services.Localization.Data;
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

        [SerializeField] private InputField apiKeyInput;
        [SerializeField] private InputField apiSecretInput;
        [SerializeField] private InputField encryptionKeyInput;
        [SerializeField] private InputField userIdInput;
        [SerializeField] private Button initializeButton;
        [SerializeField] private Button clearUserButton;
        [SerializeField] private Button showButton;
        [SerializeField] private Dropdown languageSelectionDropdown;
        [SerializeField] private Toggle useUserInfoToggle;
        [SerializeField] private Toggle useMediaSourceInfoToggle;

        private void Start()
        {
            clearUserButton.onClick.AddListener(ClearUser);
            initializeButton.onClick.AddListener(Initialize);
            showButton.onClick.AddListener(ShowOffers);
            SetupDropdown();

            TyrSDKPlugin.Instance.InitializationCompleted += OnSdkInitializationCompleted;

            string userId = TyrSDKPlugin.Instance.GetUserId();
            userIdInput.text = userId;
            initializeButton.interactable = false;

            var userInfo = GetTyradsUserInfo();
            var mediaInfo = GetTyradsMediaSourceInfo();
            TyrSDKPlugin.Instance.LoginUser(userId, userInfo, mediaInfo);
        }

        private TyradsMediaSourceInfo GetTyradsMediaSourceInfo()
        {
            if (!useMediaSourceInfoToggle.isOn)
            {
                return null;
            }

            return new TyradsMediaSourceInfo(
                sub1: "campaign_source",
                sub2: "ad_group",
                sub3: "creative_type",
                sub4: "placement",
                sub5: "custom_param",
                userGroup: "target_audience",
                mediaSourceName: "Facebook",
                mediaSourceId: "fb_123",
                mediaSubSourceId: "fb_sub_456",
                incentivized: true,
                mediaAdsetName: "Summer Sale Adset",
                mediaAdsetId: "adset_789",
                mediaCreativeName: "Summer Sale Creative",
                mediaCreativeId: "creative_101",
                mediaCampaignName: "Summer Sale Campaign"
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
            
            var userInfo = GetTyradsUserInfo();
            var mediaInfo = GetTyradsMediaSourceInfo();
            TyrSDKPlugin.Instance.LoginUser(userId, userInfo, mediaInfo);
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
            languageSelectionDropdown.ClearOptions();
            List<string> languages = _availableLanguages.Keys.ToList();
            languageSelectionDropdown.AddOptions(languages);

            languageSelectionDropdown.onValueChanged.AddListener(OnSelectLanguage);
        }

        private void OnSdkInitializationCompleted(bool tyrAdsInitialized)
        {
            showButton.interactable = tyrAdsInitialized;
            initializeButton.interactable = true;
        }

        private void OnSelectLanguage(int index)
        {
            Dropdown.OptionData selectedLanguage = languageSelectionDropdown.options[index];

            if (_availableLanguages.TryGetValue(selectedLanguage.text, out string languageCode))
            {
                TyrSDKPlugin.Instance.SetLanguage(languageCode);
            }
        }
    }
}