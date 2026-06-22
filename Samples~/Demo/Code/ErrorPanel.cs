using System;
using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class ErrorPanel : MonoBehaviour
    {
        [SerializeField] private Text errorText;
        [SerializeField] private Button closeButton;

        private void OnDestroy()
        {
            closeButton.onClick.RemoveListener(OnClose);
        }

        public void SetError(string error)
        {
            errorText.text = error;
            gameObject.SetActive(true);
            closeButton.onClick.AddListener(OnClose);
        }

        private void OnClose()
        {
            gameObject.SetActive(false);
        }
    }
}