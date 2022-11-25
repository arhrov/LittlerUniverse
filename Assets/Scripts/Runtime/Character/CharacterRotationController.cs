using UnityEngine;

namespace LittlerUniverse
{
    public class CharacterRotationController : MonoBehaviour
    {
        #region Properties

        public bool RotationAllowed { get; set; }

        public Vector2 Input { get; set; }

        #endregion

        #region Fields

        [SerializeField]
        private Transform transformToRatate = null;

        [SerializeField]
        private float rotateSpeed = 10.0f;

        #endregion

		#region Rotate 

		private void Update()
		{
            if (!RotationAllowed)
            {
                return;
            }

            Vector3 characterDirection = new Vector3(Input.x, 0.0f, Input.y).normalized;

            float characterAngle = Vector3.SignedAngle(Vector3.forward, characterDirection, Vector3.up);
            
            Vector3 targetDirection = transformToRatate.localEulerAngles;
            targetDirection.y = characterAngle;

            transformToRatate.localRotation = Quaternion.RotateTowards(transformToRatate.localRotation, Quaternion.Euler(targetDirection), Time.deltaTime * rotateSpeed);
        }

		#endregion
	}
}