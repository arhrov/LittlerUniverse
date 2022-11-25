using DG.Tweening;
using UnityEngine;

namespace LittlerUniverse
{
    [CreateAssetMenu(fileName = "ColorAnimationSettingConfig", menuName = "Cube Out Of Spheres/Color Animation Setting Config")]
    public class ColorAnimationSettingConfig : ScriptableObject
    {
        #region Properties

        public float ColorTweenDuration => colorTweenDuration;

        public float DelayBetweenPlanes => delayBetweenPlanes;

        public Ease ColorTweenEase => colorTweenEase;

        public Color StartColor => startColor;

        public Color EndColor => endColor;
        
        #endregion

        #region Fields

        [SerializeField]
        private float colorTweenDuration = 2.0f;

        [SerializeField]
        private float delayBetweenPlanes = 0.1f;

        [SerializeField]
        private Ease colorTweenEase = Ease.Linear;

        [SerializeField]
        private Color startColor = Color.red;

        [SerializeField]
        private Color endColor = Color.blue;

        #endregion
    }
}