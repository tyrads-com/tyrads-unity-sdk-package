using System;
using System.Collections.Generic;
using System.Linq;
using TyrAds.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class UserInfoPanel : MonoBehaviour
    {
        private readonly List<UserGender> _availableGenders = Enum.GetValues(typeof(UserGender))
            .Cast<UserGender>()
            .ToList();
        
        [SerializeField] private Toggle useUserInfoToggle;
        [SerializeField] private InputField phoneNumberInput;
        [SerializeField] private InputField userEmailInput;
        [SerializeField] private InputField userGroupInput;
        [SerializeField] private InputField userAgeInput;
        [SerializeField] private Dropdown userGenderDropdown;
        
        private void Awake()
        {
            userGenderDropdown.ClearOptions();
            List<string> types = _availableGenders.Select(value => value.ToString()).ToList();
            userGenderDropdown.AddOptions(types);
        }

        public TyradsUserInfo GetTyradsUserInfo()
        {
            if (!useUserInfoToggle.isOn)
            {
                return null;
            }

            return new TyradsUserInfo(
                userPhoneNumber: GetInput(phoneNumberInput),
                userEmail: GetInput(userEmailInput),
                userGroup: GetInput(userGroupInput),
                userAge: GetAge(),
                userGender: GetGender()
            );
        }

        private string GetInput(InputField input)
        {
            return string.IsNullOrWhiteSpace(input.text) ? null : input.text;
        }

        private uint GetAge()
        {
            return uint.TryParse(userAgeInput.text, out uint age) ? age : 0;
        }

        private UserGender GetGender()
        {
            int dropdownIndex = userGenderDropdown.value;
            
            if (dropdownIndex < 0 || dropdownIndex >= _availableGenders.Count)
            {
                return UserGender.Undefined;
            }

            return _availableGenders[dropdownIndex];
        }
    }
}
