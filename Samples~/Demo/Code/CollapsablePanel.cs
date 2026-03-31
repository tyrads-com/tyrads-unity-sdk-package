using UnityEngine;
using UnityEngine.UI;

namespace TyrAds.Demo
{
    public class CollapsablePanel : MonoBehaviour
    {
        [SerializeField] private Button panelVisibilityToggle;
        [SerializeField] private GameObject container;
        [SerializeField] private RectTransform arrowRect;

        private bool _isExpanded;
        
        private void Awake()
        {
            panelVisibilityToggle.onClick.AddListener(OnToggleUserInfo);
        }

        private void OnDestroy()
        {
            panelVisibilityToggle.onClick.RemoveListener(OnToggleUserInfo);
        }
        
        private void OnToggleUserInfo()
        {
            _isExpanded = !_isExpanded;
            container.SetActive(_isExpanded);
            
            if (arrowRect == null)
            {
                return;
            }

            Vector3 scale = arrowRect.localScale;
            scale.y = Mathf.Abs(scale.y) * (_isExpanded ? -1f : 1f);
            arrowRect.localScale = scale;
        }
    }
}