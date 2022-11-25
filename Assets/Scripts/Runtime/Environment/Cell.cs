using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace LittlerUniverse
{
    public class Cell : MonoBehaviour
    {
        #region Properties

        public Vector3 CenterPosition => GetComponent<Renderer>().bounds.center;

        #endregion

        #region Fields

        [SerializeField]
        private bool randomizePositionYAtStart = true;

        [SerializeField]
        private float cellOffsetYMin = -0.5f;

        [SerializeField]
        private float cellOffsetYMax = 0.5f;

        [SerializeField]
        private float scaleUpDuration = 0.5f;

        [SerializeField]
        private Ease scaleUpEase = Ease.OutSine;

        #endregion

        #region Start

        private void Start()
        {
            if (!randomizePositionYAtStart)
            {
                return;
            }

            Vector3 cellDirection = (transform.position - transform.parent.position).normalized;

            transform.position += cellDirection * Random.Range(cellOffsetYMin, cellOffsetYMax);   
        }

        #endregion

        #region Show

        public void PlayShowCellSequence()
        {
            Vector3 targetScale = transform.localScale;

            transform.localScale = Vector3.zero;

            Tween cellScaleUpTween = transform.DOScale(targetScale, scaleUpDuration).SetEase(scaleUpEase);

            ResourceToCellExchanger resourceToCellExchanger = GetComponentInChildren<ResourceToCellExchanger>();

            if (resourceToCellExchanger == null)
            {
                return;
            }

            Tween resourceToCellExchangerScaleUpTween = resourceToCellExchanger.PlayScaleUpTween();

            Sequence showSequence = DOTween.Sequence();

            showSequence.Append(cellScaleUpTween);
            showSequence.Append(resourceToCellExchangerScaleUpTween);
        }



        #endregion

        #region Move

        [Button("Move Up 1 Unit")]
        private void MoveUp()
        {
            Vector3 cellDirection = (transform.position - transform.parent.position).normalized;

            transform.position += cellDirection;
        }

        [Button("Move Down 1 Unit")]
        private void MoveDown()
        {
            Vector3 cellDirection = (transform.position - transform.parent.position).normalized;

            transform.position -= cellDirection;
        }

        #endregion
    }
}