using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace LittlerUniverse
{
    public class ResourceToCellExchanger : MonoBehaviour
    {
        #region Fields

        private static ResourceSpawner resourceSpawner = null;

		[SerializeField]
		private ResourceExchageDataEntry[] resourceExchageDataEntries = null;

		[SerializeField]
		private Cell rewardedCell = null;

		[Space(10)]
		[SerializeField]
		private ResourceConfigDatabase resourceConfigDatabase = null;
		
		[SerializeField]
		private Transform transformToScale = null;

		[SerializeField]
		private new Collider collider = null;

		[SerializeField]
		private TextMeshProUGUI exchangeProgressText = null;

		[Space(10)]
		[SerializeField]
		private float punchScaleAmount = 1.2f;

		[SerializeField]
		private float punchScaleDuration = 1.0f;

		[SerializeField]
		private Ease punchScaleEase = Ease.InOutQuad;

		[SerializeField]
		private float scaleUpDuration = 1.0f;

		[SerializeField]
		private Ease scaleUpEase = Ease.OutSine;

		[SerializeField]
		private float scaleDownDuration = 0.5f;

		[SerializeField]
		private Ease scaleDownEase = Ease.InSine;

		public LayerMask groundLayerMask = default;

		#endregion

		#region Init

		private void Start()
        {
			if(resourceSpawner == null)
            {
				resourceSpawner = FindObjectOfType<ResourceSpawner>();
            }

			SetResourceExchangeProgressText();
        }

        #endregion

        #region Trigger

        private void OnTriggerEnter(Collider other)
		{
			if (!other.CompareTag(Tags.Player))
			{
				return;
			}

			Tween punchScaleTween = transformToScale.DOPunchScale(Vector3.one * punchScaleAmount, punchScaleDuration, 0, 0.0f).SetEase(punchScaleEase);
	
			resourceSpawner.PlaySpawnResourcesSequence(other.transform, rewardedCell.CenterPosition, resourceExchageDataEntries,
				AddResourceOfTypeToExchangedResources);
		}

        private void OnTriggerExit(Collider other)
        {
			resourceSpawner.StopSpawnSequence();
		}

		#endregion

		#region Exchange

		private void AddResourceOfTypeToExchangedResources(ResourceType resourceType, int resourceAmount)
        {
            for (int i = 0; i < resourceExchageDataEntries.Length; i++)
            {
				if (resourceExchageDataEntries[i].ResourceType != resourceType)
                {
					continue;
                }

				resourceExchageDataEntries[i].AddResourceExchanged(resourceAmount);
				break;
            }

			SetResourceExchangeProgressText();

			CheckIfAllResourcesHaveBeenExchanged();
		}

		private void CheckIfAllResourcesHaveBeenExchanged()
        {
			for (int i = 0; i < resourceExchageDataEntries.Length; i++)
			{
				if (resourceExchageDataEntries[i].IsFull)
				{
					continue;
				}

				return;
			}

			ActivateRewardedCell();
		}

		private void ActivateRewardedCell()
		{
			rewardedCell.gameObject.SetActive(true);

			rewardedCell.PlayShowCellSequence();

			collider.enabled = false;

			transformToScale.DOScale(0.0f, scaleDownDuration).SetEase(scaleDownEase).OnComplete(() => gameObject.SetActive(false));
		}

		#endregion

		#region Text

		private void SetResourceExchangeProgressText()
		{
			exchangeProgressText.text = "";

            foreach (var resourceExchageDataEntry in resourceExchageDataEntries)
            {
				string formatedResourceExchangedAmount = MathUtils.GetFormattedNumberWithUnitPrefix(resourceExchageDataEntry.ResourceExchangedAmount);
				string formatedResourceAmount = MathUtils.GetFormattedNumberWithUnitPrefix(resourceExchageDataEntry.ResourceAmount);

				exchangeProgressText.text += formatedResourceExchangedAmount + "/" + formatedResourceAmount + resourceConfigDatabase.
					GetResourceIconStingOfType(resourceExchageDataEntry.ResourceType) + "\n";
			}
		}

        #endregion

        #region Align

		[Button("Align To Ground")]
		private void AlignToGround()
        {
			Ray ray = new Ray(transform.position, -transform.up);
			RaycastHit hit;

			if (!Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayerMask))
			{
				return;
			}

			transform.position = hit.point;
			transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
		}

		#endregion

		#region Show

		public Tween PlayScaleUpTween()
		{
			Vector3 targetScale = transform.localScale;

			transform.localScale = Vector3.zero;

			Tween scaleUpTween = transform.DOScale(targetScale, scaleUpDuration).SetEase(scaleUpEase);

			return scaleUpTween;
		}

		#endregion
	}
}