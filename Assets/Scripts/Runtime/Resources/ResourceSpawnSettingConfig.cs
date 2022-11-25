using DG.Tweening;
using UnityEngine;

namespace LittlerUniverse
{
    [CreateAssetMenu(fileName = "ResourceSpawnSettingConfig", menuName = "Resource/Resource Spawn Setting Config")]
    public class ResourceSpawnSettingConfig : ScriptableObject
    {
        #region Properties

        public float SpawnBurstDelay => spawnBurstDelay;

        public float MoveDuration => moveDuration;

        public float MoveMaxHeight => moveMaxHeight;

        public AnimationCurve MovePositionCurve => movePositionCurve;

        public float ScaleUpDuration => scaleUpDuration;

        public Ease ScaleUpEase => scaleUpEase;

        public float RotationSpeed => rotationSpeed;

        public float StartPositionRandomOffsetMin => startPositionRandomOffsetMin;

        public float StartPositionRandomOffsetMax => startPositionRandomOffsetMax;

        public float EndPositionRandomOffsetMin => endPositionRandomOffsetMin;

        public float EndPositionRandomOffsetMax => endPositionRandomOffsetMax;

        #endregion

        #region Fields

        [SerializeField]
        private float spawnBurstDelay = 0.1f;

        [SerializeField]
        private float moveDuration = 1.0f;

        [SerializeField]
        private float moveMaxHeight = 6.0f;

        [SerializeField]
        private AnimationCurve movePositionCurve = AnimationCurve.EaseInOut(0.0f, 0.0f, 1.0f, 1.0f);

        [SerializeField]
        private float scaleUpDuration = 0.2f;

        [SerializeField]
        private Ease scaleUpEase = Ease.OutQuad;

        [SerializeField]
        private float rotationSpeed = 500.0f;

        [SerializeField]
        private float startPositionRandomOffsetMin = -0.5f;

        [SerializeField]
        private float startPositionRandomOffsetMax = 0.5f;

        [SerializeField]
        private float endPositionRandomOffsetMin = -2.0f;

        [SerializeField]
        private float endPositionRandomOffsetMax = 2.0f;

        #endregion
    }
}