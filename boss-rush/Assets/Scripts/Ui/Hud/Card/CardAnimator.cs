using DG.Tweening;
using UnityEngine;

namespace Ui.Hud.Card
{
    public class CardAnimator : MonoBehaviour
    {
        private const int MAX_SORTING_ORDER = 5;
        
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Canvas canvas;
        
        private Vector2 _endPosition;

        private Vector2 _defaultPosition;
        private Vector2 _defaultRotation;
        private int _defaultSortingOrder;
        
        private Sequence _pickUpCardSequence;
        private Sequence _pickDownCardSequence;
        private Sequence _resetSequence;

        private Tweener _moveRightTween;
        private Tweener _moveLeftTween;

        private bool _cardMoving;
        
        public void PickUp()
        {
            canvas.sortingOrder = MAX_SORTING_ORDER;
            if (_pickDownCardSequence != null && _pickDownCardSequence.IsActive())
            {
                _pickDownCardSequence.Kill();
            }

            if (_pickUpCardSequence == null || !_pickUpCardSequence.IsActive())
            {
                _pickUpCardSequence = DOTween.Sequence()
                    .Append(rectTransform.DOMove(_endPosition, .15f))
                    .Join(rectTransform.DOScale(1.5f, .15f))
                    .Join(rectTransform.DORotate(Vector3.zero, .15f))
                    .SetEase(Ease.OutQuad);
            }
        }

        public void PickDown()
        {
            canvas.sortingOrder = _defaultSortingOrder;
            if (_pickUpCardSequence != null || _pickUpCardSequence.IsActive())
            {
                _pickUpCardSequence.Kill();
            }

            if (_pickDownCardSequence == null || !_pickDownCardSequence.IsActive())
            {
                _pickDownCardSequence = DOTween.Sequence()
                    .Append(rectTransform.DOMove(_defaultPosition, .15f))
                    .Join(rectTransform.DOScale(1f, .15f))
                    .Join(rectTransform.DORotate(_defaultRotation, .15f))
                    .SetEase(Ease.InQuad);
            }
        }

        public void MoveToPosition(Vector2 position, int cardIndex)
        {
            _cardMoving = true;
            _defaultSortingOrder = cardIndex;
            canvas.sortingOrder = _defaultSortingOrder;
            
            rectTransform
                .DOLocalMove(position, .1f)
                .OnComplete(() =>
                {
                    UpdatePosition();
                    _cardMoving = false;
                });
        }

        public void MoveToLeft()
        {
            Vector3 leftPosition = _defaultPosition;
            leftPosition.x -= 75;
            _moveLeftTween = rectTransform.DOMove(leftPosition, .15f);
        }

        public void MoveToRight()
        {
            Vector3 rightPosition = _defaultPosition;
            rightPosition.x += 75;
            _moveRightTween = rectTransform.DOMove(rightPosition, .15f);
        }

        public void Reset()
        {
            DOTween.Sequence()
                .Append(rectTransform.DOMove(_defaultPosition, .15f))
                .Join(rectTransform.DOScale(1f, .15f))
                .Join(rectTransform.DORotate(_defaultRotation, .15f));
        }

        private void UpdatePosition()
        {
            _defaultPosition = rectTransform.position;
            _endPosition = _defaultPosition;
            _endPosition.y = 0;
            
            _defaultRotation = rectTransform.rotation.eulerAngles;
            _defaultSortingOrder = canvas.sortingOrder;
        }

        private void OnDestroy()
        { 
            _pickUpCardSequence?.Kill(); 
            _pickDownCardSequence?.Kill();
            _moveRightTween?.Kill();
            _moveLeftTween?.Kill();
            _resetSequence?.Kill();
        }

        public bool IsMoving()
        {
            return _cardMoving;
        }
    }
}