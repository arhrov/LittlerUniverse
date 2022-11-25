using UnityEngine;

namespace LittlerUniverse
{
    [CreateAssetMenu(fileName = "ResourceConfig_New", menuName = "Resource/Config")]
    public class ResourceConfig : ScriptableObject
    {
        #region Properties

        public ResourceType ResourceType => resourceType;

        public string IconString => GetIconString();

        public Resource Resource => resource;

        #endregion

        #region Fields

        [SerializeField]
        private ResourceType resourceType = default;

        [SerializeField]
        private Resource resource = null;

        #endregion

        #region Constants

        private const string WoodLogIconString = "<sprite=\"Resource_WoodLog\" name=\"Resource_WoodLog\">";

        private const string StoneCubeIconString = "<sprite=\"Resource_StoneCube\" name=\"Resource_StoneCube\">";

        #endregion

        #region Icon

        private string GetIconString()
        {
            switch (resourceType)
            {
                case ResourceType.WoodLog:
                    return WoodLogIconString;
                case ResourceType.StoneCube:
                    return StoneCubeIconString;
            }

            return null;
        }

        #endregion
    }
}