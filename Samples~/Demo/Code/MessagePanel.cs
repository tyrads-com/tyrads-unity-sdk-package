using System;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class MessagePanel : MonoBehaviour
    {
        [SerializeField] private Text headerText;
        [SerializeField] private Text messageText;
        [SerializeField] private Button closeButton;
        [SerializeField] private Graphic[] graphics = Array.Empty<Graphic>();

        private void OnEnable()
        {
            closeButton.onClick.AddListener(OnClose);
        }
        
        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(OnClose);
        }

        public void SetMessage(string error, string header, Color colorScheme)
        {
            foreach (var graphic in graphics)
            {
                if (graphic != null)
                {
                    graphic.color = colorScheme;
                }
            }
            
            headerText.text = header;
            messageText.text = error;
            gameObject.SetActive(true);
        }

        private void OnClose()
        {
            gameObject.SetActive(false);
        }
    }
}