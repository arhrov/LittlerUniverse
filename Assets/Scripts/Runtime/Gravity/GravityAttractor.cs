using UnityEngine;

namespace LittlerUniverse
{
	public class GravityAttractor : MonoBehaviour
	{
        #region Fields

        [SerializeField]
		private float gravity = -9.8f;

        #endregion

        #region Attract

        public void Attract(Rigidbody rigidbody)
		{
			Vector3 gravityUp = (rigidbody.position - transform.position).normalized;
			
			Vector3 localUp = rigidbody.transform.up;

			rigidbody.AddForce(gravityUp * gravity);

			rigidbody.rotation = Quaternion.FromToRotation(localUp, gravityUp) * rigidbody.rotation;
		}

        #endregion
    }
}