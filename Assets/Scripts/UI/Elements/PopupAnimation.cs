using DG.Tweening;
using UnityEngine;

namespace UI.Elements
{
    public class PopupAnimation : MonoBehaviour
    {
        [SerializeField] private float duration = 0.5f;
        private RectTransform windowPanel;
        private Tween currentTween;
        private CanvasGroup canvasGroup;
        [SerializeField] private Ease easeInType = Ease.OutBack;
        [SerializeField] private Ease easeOutType = Ease.InBack;

        private void Awake()
        {
            windowPanel = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            windowPanel.localScale = Vector3.zero;
            currentTween?.Kill();
            currentTween = windowPanel
                .DOScale(1f, duration)
                .SetEase(easeInType)
                .OnComplete(() =>
                {
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    currentTween = null;
                });
        }

        public void Close()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            currentTween?.Kill();
            currentTween = windowPanel
                .DOScale(0f, duration)
                .SetEase(easeOutType)
                .OnComplete(() =>
                {
                    currentTween = null;
                    gameObject.SetActive(false);
                });
        }

        private void OnDisable()
        {
            currentTween?.Kill();
            currentTween = null;
        }
    }
}