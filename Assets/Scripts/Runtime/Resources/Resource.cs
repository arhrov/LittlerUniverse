using DG.Tweening;
using UnityEngine;

namespace LittlerUniverse
{
    public class Resource : MonoBehaviour
    {
		#region Properties

		public ResourceType ResourceType => resourceType;

		public Transform PooledParentTransform { get; set; }

        #endregion

        #region Fields

        [SerializeField]
		private ResourceType resourceType = default;

		[SerializeField]
		private ResourceSpawnSettingConfig resourceSpawnSettingConfig = null;

		private Vector3 spawnStartPosition = Vector3.zero;
		
		private Vector3 spawnEndPosition = Vector3.zero;

		private float spawnTimer = 0.0f;

		private Tween scaleUpTween = null;

		private Vector3 upDirection = Vector3.up;

		private Vector3 rotationDirection = Vector3.forward;

		#endregion

		#region Spawn 

		public void Spawn(Vector3 spawnStartPosition, Vector3 spawnEndPosition, Vector3 upDirection)
        {
			this.spawnStartPosition = RandomizeVector(spawnStartPosition, resourceSpawnSettingConfig.StartPositionRandomOffsetMin,
				resourceSpawnSettingConfig.StartPositionRandomOffsetMax);
			
			this.spawnEndPosition = RandomizeVector(spawnEndPosition, resourceSpawnSettingConfig.EndPositionRandomOffsetMin,
				resourceSpawnSettingConfig.EndPositionRandomOffsetMax);

			this.upDirection = upDirection;

			transform.rotation = Random.rotation;

			Vector3 targetScale = transform.localScale;

			transform.localScale = Vector3.zero;

			scaleUpTween = transform.DOScale(targetScale, resourceSpawnSettingConfig.ScaleUpDuration).SetEase(resourceSpawnSettingConfig.ScaleUpEase);

			rotationDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));

			spawnTimer = 0.0f;

			enabled = true;
		}

		private Vector3 RandomizeVector(Vector3 vector, float offsetMin, float offsetMax)
        {
			Vector3 offset = new Vector3(Random.Range(offsetMin, offsetMax), Random.Range(offsetMin, offsetMax),Random.Range(offsetMin, offsetMax));

			return vector + offset;
		}

		#endregion

		#region Move

		private void Update()
		{
			spawnTimer += Time.deltaTime;

			float spawnCompletion = Mathf.Clamp01(spawnTimer / resourceSpawnSettingConfig.MoveDuration);
			float spawnPositionLerpValue = resourceSpawnSettingConfig.MovePositionCurve.Evaluate(spawnCompletion);

			transform.position = CalculateSymmetricalArcMovementPoint(spawnPositionLerpValue, spawnStartPosition, spawnEndPosition, 
				resourceSpawnSettingConfig.MoveMaxHeight, upDirection);

			transform.Rotate(rotationDirection * (resourceSpawnSettingConfig.RotationSpeed * Time.deltaTime));

			if (!Mathf.Approximately(spawnPositionLerpValue, 1.0f))
			{
				return;
			}

			scaleUpTween.Kill();

			gameObject.SetActive(false);

			transform.parent = PooledParentTransform;

			//Destroy(gameObject);
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