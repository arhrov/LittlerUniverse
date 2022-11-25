using UnityEngine;

namespace LittlerUniverse
{
    public class CameraFacer : MonoBehaviour
    {
        #region Fields

        private static Camera mainCamera = null;

		#endregion
		
		#region Init

		private void Awake()
		{
			if (mainCamera != null)
			{
				return;
			}

			mainCamera = GameObject.FindWithTag(Tags.MainCamera).GetComponent<Camera>();
		}

		#endregion

		#region Rotate

		private void LateUpdate()
		{
			transform.rotation = mainCamera.transform.rotation;
		}

		#endregion
	}
}