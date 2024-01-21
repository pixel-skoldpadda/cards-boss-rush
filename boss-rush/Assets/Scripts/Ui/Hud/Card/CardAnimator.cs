using DG.Tweening;
using UnityEngine;

namespace Ui.Hud.Card
{
    public class CardAnimator : MonoBehaviour
    {
        private const int MAX_SORTING_ORDER = 5;
        
        [SerializeField] private int moveOffset;
        [SerializeField] private Canvas canvas;
        
        private Vector3 _endPosition;

        private Vector3 _defaultPosition;
        private Vector3 _defaultRotation;
        private int _defaultSortingOrder;
        
        private Sequence _pickUpCardSequence;
        private Sequence _pickDownCardSequence;
        
        private void Awake()
        {
            Transform cardTransform = transform;

            _defaultPosition = cardTransform.position;
            _endPosition = _defaultPosition;
            _endPosition.y += moveOffset;
            
            _defaultRotation = cardTransform.rotation.eulerAngles;
            _defaultSortingOrder = canvas.sortingOrder;
        }
        
        public void PickUp()
        {
            canvas.sortingOrder = MAX_SORTING_ORDER;
            
            _pickUpCardSequence = DOTween.Sequence()
                .Append(transform.DOMove(_endPosition, .2f))
                .Join(transform.DOScale(1.5f, .2f))
                .Join(transform.DORotate(Vector3.zero, .2f));
        }
        
        public void PickDown()
        {
            canvas.sortingOrder = _defaultSortingOrder;
            
            _pickDownCardSequence = DOTween.Sequence()
                .Append(transform.DOMove(_defaultPosition, .2f))
                .Join(transform.DOScale(1f, .2f))
                .Join(transform.DORotate(_defaultRotation, .2f));
        }

        public void MoveToLeft()
        {
            Vector3 leftPosition = _defaultPosition;
            leftPosition.x -= 75;
            transform.DOMove(leftPosition, .2f);
        }

        public void MoveToRight()
        {
            Vector3 rightPosition = _defaultPosition;
            rightPosition.x += 75;
            transform.DOMove(rightPosition, .2f);
        }

        public void ResetPosition()
        {
            transform.DOMove(_defaultPosition, .2f);
        }
    }
}