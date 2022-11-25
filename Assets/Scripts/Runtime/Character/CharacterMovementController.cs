using UnityEngine;

namespace LittlerUniverse
{
    public class CharacterMovementController : MonoBehaviour
    {
        #region Properties

        public bool MovementAllowed { get; set; }

        public Vector2 Input { get; set; }

        #endregion

        #region Fields

        [SerializeField]
        private float moveSpeed = 10.0f;

        private Vector3 moveAmount = Vector3.zero;

        Rigidbody characterRigidbody = null;

        private Vector3 currentVelocity = Vector3.zero;

        #endregion

        #region Constants

        private const float moveSmoothTime = 0.15f;

        #endregion

        #region Init

        private void Awake()
        {
            characterRigidbody = GetComponent<Rigidbody>();
        }

        #endregion

        #region Move

        private void Update()
        {
            if (!MovementAllowed)
            {
                return;
            }

            Vector3 characterMoveDirection = new Vector3(Input.x, 0.0f, Input.y).normalized;

            Vector3 targetMoveAmount = characterMoveDirection * moveSpeed;

            moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref currentVelocity, moveSmoothTime);
        }

        private void FixedUpdate()
        {
            if (!MovementAllowed)
            {
                return;
            }

            Move();
        }

        private void Move()
        {
            Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
            characterRigidbody.MovePosition(characterRigidbody.position + localMove);
        }

        #endregion
    }
}