using TyrAds.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class MediaSourcePanel : MonoBehaviour
    {
        [SerializeField] private Toggle useMediaSourceInfoToggle;
        [SerializeField] private InputField sourceNameInput;
        [SerializeField] private InputField campaignNameInput;
        [SerializeField] private InputField sourceIdInput;
        [SerializeField] private InputField subSourceIdInput;
        [SerializeField] private Toggle incentivizedToogle;
        [SerializeField] private InputField adsetNameInput;
        [SerializeField] private InputField adsetIdInput;
        [SerializeField] private InputField creativeNameInput;
        [SerializeField] private InputField creativeIdInput;
        [SerializeField] private InputField sub1Input;
        [SerializeField] private InputField sub2Input;
        [SerializeField] private InputField sub3Input;
        [SerializeField] private InputField sub4Input;
        [SerializeField] private InputField sub5Input;

        public TyradsMediaSourceInfo GetTyradsMediaSourceInfo()
        {
            if (!useMediaSourceInfoToggle.isOn || string.IsNullOrWhiteSpace(sourceNameInput.text))
            {
                return null;
            }

            return new TyradsMediaSourceInfo(
                mediaSourceName: sourceNameInput.text,
                mediaCampaignName: GetInput(campaignNameInput),
                mediaSourceId: GetInput(sourceIdInput),
                mediaSubSourceId: GetInput(subSourceIdInput),
                incentivized: incentivizedToogle.isOn,
                mediaAdsetName: GetInput(adsetNameInput),
                mediaAdsetId: GetInput(adsetIdInput),
                mediaCreativeName: GetInput(creativeNameInput),
                mediaCreativeId: GetInput(creativeIdInput),
                sub1: GetInput(sub1Input),
                sub2: GetInput(sub2Input),
                sub3: GetInput(sub3Input),
                sub4: GetInput(sub4Input),
                sub5: GetInput(sub5Input)
            );
        }

        private string GetInput(InputField input)
        {
            return string.IsNullOrWhiteSpace(input.text) ? null : input.text;
        }
    }
}