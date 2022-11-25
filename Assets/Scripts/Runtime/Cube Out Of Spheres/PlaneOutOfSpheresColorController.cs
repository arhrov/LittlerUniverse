using DG.Tweening;
using UnityEngine;

namespace LittlerUniverse
{
    public class PlaneOutOfSpheresColorController : MonoBehaviour
    {
        #region Properties

        public float ColorTweenDelay { get; set; }

        public ColorAnimationSettingConfig ColorAnimationSettingConfig { get; set; }

        #endregion

        #region Fields

        private static MaterialPropertyBlock materialPropertyBlock;

        private MeshRenderer meshRenderer = null;

        #endregion

        #region Init

        private void Start()
        {
            meshRenderer = GetComponent<MeshRenderer>();

            if(materialPropertyBlock == null)
            {
                materialPropertyBlock = new MaterialPropertyBlock();
            }

            PlayColorTween();
        }

        #endregion

        #region Color

        private void PlayColorTween()
        {
            Color currentColor = ColorAnimationSettingConfig.StartColor;

            SetColor(currentColor);

            Tween colorTween = DOTween.To(() => currentColor, x => currentColor = x, ColorAnimationSettingConfig.EndColor, 
                ColorAnimationSettingConfig.ColorTweenDuration);
            colorTween.SetEase(ColorAnimationSettingConfig.ColorTweenEase);
            colorTween.SetDelay(ColorTweenDelay);
            colorTween.OnUpdate(() => SetColor(currentColor));
            colorTween.SetLoops(-1, LoopType.Yoyo);
        }

        private void SetColor(Color color)
        {
            meshRenderer.GetPropertyBlock(materialPropertyBlock);

            materialPropertyBlock.SetColor(ShaderConstants.Color, color);

            meshRenderer.SetPropertyBlock(materialPropertyBlock);
        }

        #endregion
    }
}