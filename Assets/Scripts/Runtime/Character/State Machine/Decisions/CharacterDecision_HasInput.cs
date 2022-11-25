using StateMachine;
using UnityEngine;

namespace LittlerUniverse
{
	[CreateAssetMenu(fileName = "CharacterDecision_HasInput", menuName = "State Machine/Character/Decisions/HasInput")]
    public class CharacterDecision_HasInput : FSMDecision
    {
        public override bool Decide(FSMController stateController)
        {
            var stateControllerData = stateController.GetStateControllerData<CharacterFSMControllerData>();
            return stateControllerData.Joystick.Direction != Vector2.zero;
        }
    }
}