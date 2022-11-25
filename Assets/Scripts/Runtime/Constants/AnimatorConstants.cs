using UnityEngine;

namespace LittlerUniverse
{
	public class AnimatorConstants : MonoBehaviour
	{
		#region Character

		public static readonly string IdleAnimatorTag = "Idle";
		public static readonly string WalkAnimatorTag = "Walk";
		public static readonly string JumpAnimatorTag = "Jump";

		public static readonly int IdleAnimatorHash = Animator.StringToHash(IdleAnimatorTag);
		public static readonly int WalkAnimatorHash = Animator.StringToHash(WalkAnimatorTag);
		public static readonly int JumpAnimatorHash = Animator.StringToHash(JumpAnimatorTag);

		#endregion
	}
}