using UnityEngine;

namespace LittlerUniverse
{
    public class CharacterAnimationController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Animator animator = null;

        #endregion

        #region Animations

        public void PlayIdleAnimation()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag(AnimatorConstants.IdleAnimatorTag))
            {
                return;
            }

            PlayAnimation(AnimatorConstants.IdleAnimatorHash);
        }

        public void PlayWalkAnimation()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsTag(AnimatorConstants.WalkAnimatorTag))
            {
                return;
            }

            PlayAnimation(AnimatorConstants.WalkAnimatorHash);
        }

        private void PlayAnimation(int animatorHash) => animator.SetTrigger(animatorHash);

        #endregion
    }
}