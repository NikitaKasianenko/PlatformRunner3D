using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Elements
{
    public class SwipeController : MonoBehaviour, IEndDragHandler
    {
        [SerializeField] private int maxPage;
        public int currentPage = 1;
        public Vector3 targetPos;
        public Vector3 pageStep;
        public RectTransform levelPagesRect;
        public Ease tweenEase = Ease.InOutSine;
        public float dragThershould;
        public float tweenTime = 0.5f;

        private void Awake()
        {
            targetPos = levelPagesRect.localPosition;
            dragThershould = Screen.width / 15;
        }

        public void Next()
        {
            if (currentPage < maxPage)
            {
                currentPage++;
                targetPos += pageStep;
                MovePage();
            }
        }
        public void Previous()
        {
            if (currentPage > 1)
            {
                currentPage--;
                targetPos -= pageStep;
                MovePage();
            }
        }

        void MovePage()
        {
            levelPagesRect.DOLocalMoveX(targetPos.x, tweenTime).SetEase(tweenEase);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThershould)
            {
                if (eventData.position.x > eventData.pressPosition.x)
                {
                    Previous();
                }
                else
                {
                    Next();
                }
            }
            else
            {
                MovePage();
            }
            
        }
    }

}