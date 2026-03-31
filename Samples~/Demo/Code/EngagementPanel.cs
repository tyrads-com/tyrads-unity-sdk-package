using TyrAds.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class EngagementPanel : MonoBehaviour
    {
        
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
    }
}