using DG.Tweening;
using UnityEngine;

namespace GameObjects.Character
{
    public class CharacterAnimator : MonoBehaviour
    {
        private static readonly int DamageHash = Animator.StringToHash("Damage");
        private static readonly int SlashAttack = Animator.StringToHash("SlashAttack");
        
        private static readonly int Alpha = Shader.PropertyToID("_Alpha");

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;

        private Sequence _attackSequence;
        private Tweener _deathTween;
        
        // TODO: Переделать на DeathAnimation.anim
        public void PlayDeathAnimation(TweenCallback onComplete)
        {
            Material material = spriteRenderer.material;
            _deathTween = DOTween
                .To(
                    () => material.GetFloat(Alpha), 
                    a => material.SetFloat(Alpha, a), 0, 2f)
                .OnComplete(onComplete);
        }

        public void PlayAttackAnimation(Vector3 direction, TweenCallback onComplete = null, TweenCallback callback = null)
        {
            if (_attackSequence != null && _attackSequence.IsActive())
            {
                return;
            }

            Vector3 defaultPosition = transform.position;
            Vector3 targetPosition = defaultPosition;
            targetPosition.x += direction.x;;

            _attackSequence = DOTween.Sequence()
                .AppendCallback(callback)
                .Append(transform.DOMove(targetPosition, .15f))
                .Append(transform.DOMove(defaultPosition, .15f))
                .SetEase(Ease.OutBack)
                .OnComplete(onComplete);
        }

        public void PlaySplashAttackAnimation()
        {
            animator.SetTrigger(SlashAttack);
        }
        
        public void PlayDamageAnimation()
        {
            animator.SetTrigger(DamageHash);
        }

        private void OnDestroy()
        {
            _deathTween?.Kill();
            _deathTween = null;
            
            _attackSequence?.Kill();
            _attackSequence = null;
        }
    }
}