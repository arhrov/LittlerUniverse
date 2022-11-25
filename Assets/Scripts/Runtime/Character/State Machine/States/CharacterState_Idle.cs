using StateMachine;
using UnityEngine;

namespace LittlerUniverse
{
	[CreateAssetMenu(fileName = "CharacterState_Idle", menuName = "State Machine/Character/States/Idle")]
	public class CharacterState_Idle : FSMState
	{
		public override void OnStateEntered(FSMController stateController, bool enteredViaPreviousStateDummy)
		{
			base.OnStateEntered(stateController, enteredViaPreviousStateDummy);

			var stateControllerData = stateController.GetStateControllerData<CharacterFSMControllerData>();

			stateControllerData.CharacterMovementController.MovementAllowed = false;
			stateControllerData.CharacterRotationController.RotationAllowed = false;

			stateControllerData.CharacterAnimationController.PlayIdleAnimation();
		}
	}
}