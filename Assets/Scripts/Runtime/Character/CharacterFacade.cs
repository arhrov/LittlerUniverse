using StateMachine;
using UnityEngine;

namespace LittlerUniverse
{
    public class CharacterFacade : MonoBehaviour
    {

        [SerializeField]
        private FSMController fsmController = null;

        [SerializeField]
        private FSMState startState = null;

        [SerializeField]
        private CharacterMovementController characterMovementController = null;

        [SerializeField]
        private CharacterRotationController characterRotationController = null;

        [SerializeField]
        private CharacterAnimationController characterAnimationController = null;

        private void Start()
        {
            fsmController.Disable();
            StartStateMachine();
        }

        private void StartStateMachine()
        {
            CharacterFSMControllerData stateControllerData = new CharacterFSMControllerData()
            {
                CharacterFacade = this,
                CharacterMovementController = characterMovementController,
                CharacterRotationController = characterRotationController,
                CharacterAnimationController = characterAnimationController,
                Joystick = FindObjectOfType<Joystick>()
            };

            fsmController.StateControllerData = stateControllerData;
            fsmController.SetState(startState);

            fsmController.Enable();
        }
    }
}