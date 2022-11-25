using UnityEngine;

namespace LittlerUniverse
{
    public class CharacterJumpController : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private Transform transformToRaycastFrom = null;

        [SerializeField]
        private float jumpDuration = 0.6f;

        [SerializeField]
        private AnimationCurve jumpCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);

        [SerializeField]
        private float jumpOvershootUp = 0.1f;

        [SerializeField]
        float rayForwardLength = 0.55f;

        [SerializeField]
        float rayForwardOffsetY = 0.3f;

        [SerializeField]
        private LayerMask groundLayerMask = default;

        private float jumpTimer = 0.0f;

        private Vector3 jumpStartPosition = Vector3.zero;

        private Vector3 jumpEndPosition = Vector3.zero;

        private float jumpHeight = 0.0f;

        private bool jumping = false;

        private GravityBody gravityBody = null;

        private new Rigidbody rigidbody = null;

        #endregion

        #region Init


        private void Start()
        {
            gravityBody = GetComponent<GravityBody>();
            rigidbody = GetComponent<Rigidbody>();
        }

        #endregion

        #region Jump

        private void FixedUpdate()
        {
            if (jumping)
            {
                Jump();
                return;
            }

            JumpCheck();
        }

        private void JumpCheck()
        {
            Vector3 rayForwardStartPosition = transformToRaycastFrom.position + transformToRaycastFrom.up * rayForwardOffsetY;

            Ray rayForward = new Ray(rayForwardStartPosition, transformToRaycastFrom.forward);

            RaycastHit hitInFront;
       
            bool hitGroundCellInFront = Physics.Raycast(rayForward, out hitInFront, rayForwardLength, groundLayerMask);

            if (!hitGroundCellInFront)
            {
                return;
            }

            Transform groundCellInFrontTransform = hitInFront.transform;
            
            if(groundCellInFrontTransform.parent == null)
            {
                return;
            }

            jumpStartPosition = transform.position;

            Vector3 groundCellDirectionUp = (groundCellInFrontTransform.position - groundCellInFrontTransform.parent.position).normalized;

            float characterYOffsetFromPlanetCenter = Mathf.Abs(Vector3.Distance(transform.position, Vector3.zero));

            float targetGroundCellTopYOffsetFromPlanetCenter = Mathf.Abs(Vector3.Distance(groundCellInFrontTransform.position, Vector3.zero));

            jumpHeight = targetGroundCellTopYOffsetFromPlanetCenter - characterYOffsetFromPlanetCenter;

            const float jumpOvershootForward = 1.5f;

            jumpEndPosition = transform.position + groundCellDirectionUp * (jumpHeight + jumpOvershootUp) + transformToRaycastFrom.forward * jumpOvershootForward;

            jumpTimer = 0.0f;

            gravityBody.enabled = false;

            rigidbody.isKinematic = true;

            jumping = true;
        }

        private void Jump()
        {
            jumpTimer += Time.fixedDeltaTime;

            float spawnCompletion = Mathf.Clamp01(jumpTimer / jumpDuration);
            float spawnPositionLerpValue = jumpCurve.Evaluate(spawnCompletion);

            transform.position = CalculateSymmetricalArcMovementPoint(spawnPositionLerpValue, jumpStartPosition, jumpEndPosition, jumpHeight, transform.up);

            if (!Mathf.Approximately(spawnPositionLerpValue, 1.0f))
            {
                return;
            }

            gravityBody.enabled = true;

            rigidbody.isKinematic = false;

            jumping = false;
        }

        public Vector3 CalculateSymmetricalArcMovementPoint(float progress, Vector3 startPosition, Vector3 endPosition, float arcHeight, Vector3 upDirection)
        {
            Vector3 newPosition = Vector3.Lerp(startPosition, endPosition, progress);

            newPosition += arcHeight * Mathf.Sin(progress * Mathf.PI) * upDirection;

            return newPosition;
        }

        #endregion
    }
}