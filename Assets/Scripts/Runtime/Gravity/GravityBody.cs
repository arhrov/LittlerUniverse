using UnityEngine;

namespace LittlerUniverse
{
	[RequireComponent(typeof(Rigidbody))]
	public class GravityBody : MonoBehaviour
	{
        #region Fields

        private static GravityAttractor gravityAttractor = null;

        private new Rigidbody rigidbody = null;

        #endregion

        #region Init

        private void Awake()
		{
			rigidbody = GetComponent<Rigidbody>();

			rigidbody.useGravity = false;
			rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

			if (gravityAttractor != null)
			{
				return;
			}
			
			gravityAttractor = FindObjectOfType<GravityAttractor>();
		}

        #endregion

        #region Attract

        private void FixedUpdate()
		{
			gravityAttractor.Attract(rigidbody);
		}

        #endregion
    }
}