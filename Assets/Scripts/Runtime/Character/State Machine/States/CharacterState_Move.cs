using StateMachine;
using UnityEngine;

namespace LittlerUniverse
{
	[CreateAssetMenu(fileName = "CharacterState_Move", menuName = "State Machine/Character/States/Move")]
    public class CharacterState_Move : FSMState
    {
		public override void OnStateEntered(FSMController stateController, bool enteredViaPreviousStateDummy)
		{
			base.OnStateEntered(stateController, enteredViaPreviousStateDummy);

			var stateControllerData = stateController.GetStateControllerData<CharacterFSMControllerData>();

			stateControllerData.CharacterMovementController.MovementAllowed = true;
			stateControllerData.CharacterRotationController.RotationAllowed = true;

			stateControllerData.CharacterAnimationController.PlayWalkAnimation();
		}
	}
}