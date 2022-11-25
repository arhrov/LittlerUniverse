using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LittlerUniverse
{
    public class ResourceSpawner : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private ResourceConfigDatabase resourceConfigDatabase = null;

        [SerializeField]
        private int maxNumberOfResourcesToSpawn = 30;

        [SerializeField]
        private ResourceSpawnSettingConfig resourceSpawnSettingConfig = null;

        [SerializeField]
        private int poolSizeForEachResource = 200;

        [SerializeField]
        private Transform pooledObjectHolder = null;

        [SerializeField]
        private Transform spawnedObjectHolder = null;

        private Sequence spawnSequence = null;

        Dictionary<ResourceType, Transform> resourceTypeToParentDictionary = new Dictionary<ResourceType, Transform>();


        #endregion

        #region Init

        private void Start()
        {
            for (int i = 0; i < resourceConfigDatabase.ResourceConfigs.Count; i++)
            {
                ResourceConfig resourceConfig = resourceConfigDatabase.ResourceConfigs[i];

                GameObject parentGameObject = new GameObject(resourceConfig.ResourceType.ToString());

                Transform pooledResourceParentTransform = parentGameObject.transform;
                
                pooledResourceParentTransform.parent = pooledObjectHolder;

                resourceTypeToParentDictionary.Add(resourceConfig.ResourceType, pooledResourceParentTransform);

                for (int j = 0; j < poolSizeForEachResource; j++)
                {
                    Resource resource = Instantiate(resourceConfig.Resource, pooledResourceParentTransform);

                    resource.PooledParentTransform = pooledResourceParentTransform;

                    resource.gameObject.SetActive(false);
                }
            }
        }

        #endregion

        #region Spawn

        public void PlaySpawnResourcesSequence(Transform transformToSpawnFrom, Vector3 resourceSpawnEndPosition, ResourceExchageDataEntry[] resourceExchageDataEntries,
            Action<ResourceType, int> resourceOfTypeSpawnedCallback)
        {
            spawnSequence = DOTween.Sequence();

            foreach (var resourceExchageDataEntry in resourceExchageDataEntries)
            {
                Sequence singleResourceSpawnSequence = DOTween.Sequence();

                int singleSpawnedResourceValue = 1;

                int numberOfResourcesToSpawn = resourceExchageDataEntry.ResourceToExchangeAmount;

                int remainder = 0;

                if (numberOfResourcesToSpawn > maxNumberOfResourcesToSpawn)
                {
                    singleSpawnedResourceValue = numberOfResourcesToSpawn / maxNumberOfResourcesToSpawn;

                    remainder = numberOfResourcesToSpawn - (singleSpawnedResourceValue * maxNumberOfResourcesToSpawn);

                    numberOfResourcesToSpawn = maxNumberOfResourcesToSpawn;
                }

                for (int i = 0; i < numberOfResourcesToSpawn; i++)
                {
                    Tween spawnTween = CreateDelayedSpawnTween(transformToSpawnFrom, resourceSpawnEndPosition, resourceExchageDataEntry.ResourceType, resourceOfTypeSpawnedCallback,
                        singleSpawnedResourceValue);

                    singleResourceSpawnSequence.Append(spawnTween);
                }

                if(remainder != 0)
                {
                    Tween spawnTween = CreateDelayedSpawnTween(transformToSpawnFrom, resourceSpawnEndPosition, resourceExchageDataEntry.ResourceType, resourceOfTypeSpawnedCallback,
                        remainder);

                    singleResourceSpawnSequence.Append(spawnTween);
                }

                spawnSequence.Insert(0.0f, singleResourceSpawnSequence);
            }
        }

        private Tween CreateDelayedSpawnTween(Transform transformToSpawnFrom, Vector3 resourceSpawnEndPosition, ResourceType resourceType, Action<ResourceType, int> resourceOfTypeSpawnedCallback, 
            int resourceValue)
        {
            float spawnTimer = 0.0f;
            Tween spawnTween = DOTween.To(() => spawnTimer, x => spawnTimer = x, 0.0f, resourceSpawnSettingConfig.SpawnBurstDelay).SetEase(Ease.Linear).
                OnComplete(() => SpawnResource(transformToSpawnFrom, resourceSpawnEndPosition, resourceType, resourceOfTypeSpawnedCallback, resourceValue));
            
            return spawnTween;
        }

        private void SpawnResource(Transform transformToSpawnFrom, Vector3 resourceSpawnEndPosition, ResourceType resourceType, Action<ResourceType, int> resourceOfTypeSpawnedCallback,
            int resourceValue)
        {

            resourceTypeToParentDictionary.TryGetValue(resourceType, out Transform pooledResourceParentTransform);

            Resource resource;

            if (pooledResourceParentTransform.childCount == 0)
            {
                Resource resourceToSpawn = resourceConfigDatabase.GetResourceOfType(resourceType);
                
                resource = Instantiate(resourceToSpawn, pooledResourceParentTransform);

                resource.PooledParentTransform = pooledResourceParentTransform;
            }

            resource = pooledResourceParentTransform.GetChild(0).GetComponent<Resource>();

            resource.transform.parent = spawnedObjectHolder;

            resource.gameObject.SetActive(true);

            resource.Spawn(transformToSpawnFrom.position, resourceSpawnEndPosition, transformToSpawnFrom.up);

            resourceOfTypeSpawnedCallback?.Invoke(resourceType, resourceValue);
        }
        
        public void StopSpawnSequence()
        {
            spawnSequence?.Kill();
        }

        #endregion
    }
}