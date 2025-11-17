using System;
using System.Collections.Generic;
using System.Linq;
using TyrAds.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class RoutingPanel : MonoBehaviour
    {
        public event Action<(string Route, string CampaignId)> RouteClicked;
        
        private readonly Dictionary<string, string> _availableDeepLinkRoutes = new()
        {
            { nameof(TyradsDeepRoutes.Offers), TyradsDeepRoutes.Offers },
            { nameof(TyradsDeepRoutes.ActiveOffers), TyradsDeepRoutes.ActiveOffers },
            { nameof(TyradsDeepRoutes.Offer), TyradsDeepRoutes.Offer },
            { nameof(TyradsDeepRoutes.Support), TyradsDeepRoutes.Support }
        };
        
        [SerializeField] private Dropdown routeDropdown;
        [SerializeField] private InputField routeCampaignIdInput;
        [SerializeField] private Button routeButton;
        
        private void Awake()
        {
            if (!ReferenceEquals(routeButton, null))
            {
                routeButton.onClick.AddListener(OnShowOffersWithDeepLink);
            }
            
            SetupRouteDropdown();
            SwitchButtonInteractability(false);
        }

        private void OnDestroy()
        {
            routeButton.onClick.RemoveListener(OnShowOffersWithDeepLink);

            if (!ReferenceEquals(routeDropdown, null))
            {
                routeDropdown.onValueChanged.RemoveListener(OnSelectDeepLinkRoute);
            }
        }

        public void SwitchButtonInteractability(bool isInteractable)
        {
            routeButton.interactable = isInteractable;
        }

        private void SetupRouteDropdown()
        {
            if (routeDropdown == null)
            {
                Debug.LogWarning("Deep Link Route Dropdown is not assigned in the inspector");
                return;
            }

            routeDropdown.ClearOptions();
            List<string> routes = _availableDeepLinkRoutes.Keys.ToList();
            routeDropdown.AddOptions(routes);
            routeDropdown.onValueChanged.AddListener(OnSelectDeepLinkRoute);
            
            if (routeCampaignIdInput != null)
            {
                routeCampaignIdInput.gameObject.SetActive(false);
            }
        }

        private string GetCampaignId(string selectedRoute)
        {
            return selectedRoute is TyradsDeepRoutes.Offer or TyradsDeepRoutes.Support && !ReferenceEquals(routeCampaignIdInput, null) 
                ? routeCampaignIdInput.text.Trim() 
                : string.Empty;
        }

        private void OnSelectDeepLinkRoute(int index)
        {
            if (ReferenceEquals(routeCampaignIdInput, null) || ReferenceEquals(routeDropdown, null))
            {
                return;
            }

            string routeKey = routeDropdown.options[index].text;
            bool isCampaignIdVisible = false;
            
            if (_availableDeepLinkRoutes.TryGetValue(routeKey, out string selectedRoute))
            {
                isCampaignIdVisible = selectedRoute is TyradsDeepRoutes.Offer or TyradsDeepRoutes.Support;
            }

            routeCampaignIdInput.gameObject.SetActive(isCampaignIdVisible);
        }

        private void OnShowOffersWithDeepLink()
        {
            if (routeDropdown == null)
            {
                return;
            }

            int selectedIndex = routeDropdown.value;
            string routeKey = routeDropdown.options[selectedIndex].text;
            
            if (!_availableDeepLinkRoutes.TryGetValue(routeKey, out string selectedRoute))
            {
                Debug.LogWarning($"Routing interrupted. Route '{routeKey}' is not find.");
                return;
            }
            
            string campaignId = GetCampaignId(selectedRoute);
            RouteClicked?.Invoke((selectedRoute, campaignId));
        }
    }
}