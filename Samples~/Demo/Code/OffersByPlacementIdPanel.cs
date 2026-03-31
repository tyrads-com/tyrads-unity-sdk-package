using System;
using TyrAds.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class OffersByPlacementIdPanel : MonoBehaviour
    {
        [SerializeField] private PlacementIdData placementIdCoins;
        [SerializeField] private PlacementIdData placementIdGems;
        [SerializeField] private PlacementIdData placementIdCash;
        
        [SerializeField] private Button coinsOffersButton;
        [SerializeField] private Button gemsOffersButton;
        [SerializeField] private Button cashOffersButton;

        private void Start()
        {
            coinsOffersButton.onClick.AddListener(OnShowCoins);   
            gemsOffersButton.onClick.AddListener(OnShowGems);   
            cashOffersButton.onClick.AddListener(OnShowCash);   
        }
        
        private void OnDestroy()
        {
            coinsOffersButton.onClick.RemoveListener(OnShowCoins);   
            gemsOffersButton.onClick.RemoveListener(OnShowGems);   
            cashOffersButton.onClick.RemoveListener(OnShowCash);   
        }
        
        public void SwitchButtonInteractability(bool isInteractable)
        {
            coinsOffersButton.interactable = isInteractable;
            gemsOffersButton.interactable = isInteractable;
            cashOffersButton.interactable = isInteractable;
        }

        private void ShowOffersByPlacementId(PlacementIdData data)
        {
#if UNITY_ANDROID
            OnOfferClicked(data.Android);
            return;
#elif UNITY_IPHONE
            OnOfferClicked(data.iOS);
            return;
#endif 
        }
        
        private void OnOfferClicked(uint placementId)
        {
            if (placementId == 0)
            {
                Debug.LogWarning("Placement ID is not set.");
                return;
            }
            
            TyrSDKPlugin.Instance.ShowOffers(new OffersRoutingData(placementId));
        }

        private void OnShowCoins()
        {
            ShowOffersByPlacementId(placementIdCoins);
        }

        private void OnShowGems()
        {
            ShowOffersByPlacementId(placementIdGems);
        }

        private void OnShowCash()
        {
            ShowOffersByPlacementId(placementIdCash);
        }
    }

    [Serializable]
    internal struct PlacementIdData
    {
        public uint Android;
        public uint iOS;
    } 
}