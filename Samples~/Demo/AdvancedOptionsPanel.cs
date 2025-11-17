using TyrAds.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class AdvancedOptionsPanel : MonoBehaviour
    {
        [SerializeField] private Toggle useUserInfoToggle;
        [SerializeField] private Toggle useMediaSourceInfoToggle;
        [SerializeField] private Toggle engagementInfoToggle;
        [SerializeField] private InputField engagementIdInput;

        public TyradsEngagementInfo GetTyradsEngagementInfo()
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

        public TyradsMediaSourceInfo GetTyradsMediaSourceInfo()
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

        public TyradsUserInfo GetTyradsUserInfo()
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
    }
}