using StateMachine;

namespace LittlerUniverse
{
	public class CharacterFSMControllerData : FSMControllerData
    {
		#region Properties

		public CharacterFacade CharacterFacade { get; set; }

		public CharacterMovementController CharacterMovementController { get; set; }
		
		public CharacterRotationController CharacterRotationController { get; set; }

		public CharacterAnimationController CharacterAnimationController { get; set; }

		public Joystick Joystick { get; set; }	

		#endregion
	}
}