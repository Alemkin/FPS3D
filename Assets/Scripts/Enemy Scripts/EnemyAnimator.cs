using UnityEngine;

namespace Assets.Scripts.Enemy_Scripts
{
    public class EnemyAnimator : MonoBehaviour
    {

        private Animator _animator;
        void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Walk(bool walk)
        {
            _animator.SetBool(AnimationTags.WALK_PARAMETER, walk);
        }

        public void Run(bool run)
        {
            _animator.SetBool(AnimationTags.RUN_PARAMETER, run);
        }

        public void Attack()
        {
            _animator.SetTrigger(AnimationTags.ATTACK_TRIGGER);
        }

        public void Dead()
        {
            _animator.SetTrigger(AnimationTags.DEAD_TRIGGER);
        }
    }
}
