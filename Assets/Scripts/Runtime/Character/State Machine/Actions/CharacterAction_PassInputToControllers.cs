using StateMachine;
using UnityEngine;

namespace LittlerUniverse
{
    [CreateAssetMenu(fileName = "CharacterAction_PassInputToControllers",
                     menuName = "State Machine/Character/Actions/Pass Input To Controllers")]
    public class CharacterAction_PassInputToControllers : FSMAction
    {
		public override void Perform(FSMController stateController)
		{
			var stateControllerData = stateController.GetStateControllerData<CharacterFSMControllerData>();

			Vector2 joystickDirection = stateControllerData.Joystick.Direction;

			stateControllerData.CharacterMovementController.Input = joystickDirection;
			stateControllerData.CharacterRotationController.Input = joystickDirection;
		}
	}
}