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
        private Vector3 _defaultRotation;
        private int _defaultSortingOrder;
        
        private Sequence _pickUpCardSequence;
        private Sequence _pickDownCardSequence;
        private Sequence _resetSequence;
        private Sequence _positioningSequence;
        
        private Tweener _moveRightTween;
        private Tweener _moveLeftTween;
        private bool _isMoving;
        
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
                    .Join(rectTransform.DOScale(1.2f, .15f))
                    .Join(rectTransform.DORotate(Vector3.zero, .15f))
                    .SetEase(Ease.InCubic);
            }
        }

        public void PickDown()
        {
            if (_pickUpCardSequence != null || _pickUpCardSequence.IsActive())
            {
                _pickUpCardSequence.Kill();
            }

            if (_pickDownCardSequence == null || !_pickDownCardSequence.IsActive())
            {
                _pickDownCardSequence = DOTween.Sequence()
                    .Append(rectTransform.DOMove(_defaultPosition, .2f))
                    .Join(rectTransform.DOScale(1f, .2f))
                    .Join(rectTransform.DORotate(_defaultRotation, .2f))
                    .OnComplete(() => canvas.sortingOrder = _defaultSortingOrder)
                    .SetEase(Ease.InCirc);
            }
        }

        public void PlayPositioningAnimation(Vector2 position, Vector3 rotation, int sortingOrder)
        {
            _isMoving = true;
            
            _defaultSortingOrder = canvas.sortingOrder = sortingOrder;
            _positioningSequence = DOTween.Sequence()
                .Append(rectTransform.DOLocalMove(position, .1f))
                .Join(rectTransform.DORotate(rotation, .1f))
                .OnComplete(() =>
                {
                    _isMoving = false;
                    UpdateDefaultValues();
                });
        }

        private void UpdateDefaultValues()
        {
            _defaultPosition = rectTransform.position;
            _endPosition = _defaultPosition;
            _endPosition.y = 0;
            
            _defaultRotation = rectTransform.rotation.eulerAngles;
            _defaultSortingOrder = canvas.sortingOrder;
        }

        private void OnDestroy()
        { 
            _positioningSequence?.Kill();
            _pickUpCardSequence?.Kill(); 
            _pickDownCardSequence?.Kill();
            _moveRightTween?.Kill();
            _moveLeftTween?.Kill();
            _resetSequence?.Kill();
        }

        public bool IsMoving => _isMoving;
    }
}