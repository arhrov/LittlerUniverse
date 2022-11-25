using System.Collections.Generic;
using UnityEngine;

namespace LittlerUniverse
{
    [CreateAssetMenu(fileName = "ResourceConfigDatabase", menuName = "Resource/Resource Config Database")]
    public class ResourceConfigDatabase : ScriptableObject
    {
        #region Properties

        public List<ResourceConfig> ResourceConfigs => resourceConfigs;

        #endregion

        #region Fields
        
        [SerializeField]
        private List<ResourceConfig> resourceConfigs = new List<ResourceConfig>();

        #endregion

        #region Get

        public Resource GetResourceOfType(ResourceType resourceType)
        {
            ResourceConfig resourceConfigOfType = null;

            foreach (ResourceConfig resourceConfig in resourceConfigs)
            {
                if (resourceConfig.ResourceType != resourceType)
                {
                    continue;
                }

                resourceConfigOfType = resourceConfig;
                break;
            }


            return resourceConfigOfType.Resource;
        }

        public string GetResourceIconStingOfType(ResourceType resourceType)
        {
            string resourceIconStringOfType = "";
            foreach (ResourceConfig resourceConfig in resourceConfigs)
            {
                if (resourceConfig.ResourceType != resourceType)
                {
                    continue;
                }

                resourceIconStringOfType = resourceConfig.IconString;
                break;
            }


            return resourceIconStringOfType;
        }

        #endregion
    }
}