using DG.Tweening;
using UnityEngine;

namespace Ui
{
    public class ScaleButton : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private float scaleX = 1.05f;
        [SerializeField] private float scaleY = 1.05f;
        
        private Tweener _scalePositiveTween;
        private Tweener _scaleNegativeTween;
        
        public void ScalePositive()
        {
            if (_scaleNegativeTween != null || _scaleNegativeTween.IsActive())
            {
                _scaleNegativeTween.Kill();
            }
            
            if (_scalePositiveTween == null || !_scalePositiveTween.IsActive())
            {
                _scalePositiveTween = transform.DOScale(new Vector3(scaleX, scaleY, 1), duration).
                    OnKill(() => transform.localScale = new Vector3(scaleX, scaleY, 1));   
            }
        }

        public void ScaleNegative()
        {
            if (_scalePositiveTween != null || _scalePositiveTween.IsActive())
            {
                _scalePositiveTween.Kill();
            }
            
            if (_scaleNegativeTween == null || !_scaleNegativeTween.IsActive())
            {
                _scaleNegativeTween = transform.DOScale(Vector3.one, duration).
                    OnKill(() => transform.localScale = Vector3.one);   
            }
        }
        
        private void OnDestroy()
        {
            _scaleNegativeTween.Kill();
            _scalePositiveTween.Kill();
        }
    }
}