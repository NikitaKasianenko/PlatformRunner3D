using UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button CloseButton;
        public Button OpenButton;

        private PopupAnimation popupAnimation;

        private void Awake()
        {
            popupAnimation = GetComponent<PopupAnimation>();
            OnAwake();
        }

        protected virtual void OnAwake()
        {
                OpenButton?.onClick.AddListener(Open);
                CloseButton.onClick.AddListener(Close);
        }

        public virtual void Open()
        {
            if (OpenButton != null)
            {
                gameObject.SetActive(true);
            }
        }

        public virtual void Close()
        {
            if (popupAnimation != null)
            {
                popupAnimation.Close();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}

