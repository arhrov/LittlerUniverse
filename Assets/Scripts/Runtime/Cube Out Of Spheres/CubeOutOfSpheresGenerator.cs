using System.Collections.Generic;
using UnityEngine;

namespace LittlerUniverse
{
    public class CubeOutOfSpheresGenerator : MonoBehaviour
    {
        [SerializeField]
        private int cubeSize = 15;

        [SerializeField]
        private float cubeDensity = 1.5f;

        [SerializeField]
        private Material sphereMaterial = null;

        [SerializeField]
        private ColorAnimationSettingConfig colorAnimationSettingConfig = null;

        #region Init

        private void Start()
        {
            List<GameObject> sphereGameObjects = new List<GameObject>();
            List<GameObject> lineGameObjects = new List<GameObject>();

            GameObject tempParentGameObject = new GameObject("TempParent");

            Transform tempParentTransform = tempParentGameObject.transform;

            for (int i = 0; i < cubeSize; i++)
            {
                GameObject sphereGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                Transform sphereTransform = sphereGameObject.transform;

                sphereTransform.position += Vector3.right * cubeDensity * i;

                sphereTransform.parent = tempParentTransform;

                sphereGameObjects.Add(sphereGameObject);
            }

            GameObject lineOfSpheresGameObject = new GameObject("LineOfSpheresGameObject");

            MeshFilter lineOfSpheresMeshFilter = lineOfSpheresGameObject.AddComponent<MeshFilter>();
            lineOfSpheresGameObject.AddComponent<MeshRenderer>();

            lineOfSpheresGameObject.transform.parent = tempParentTransform;

            CreateCombinedMesh(sphereGameObjects, lineOfSpheresMeshFilter);

            for (int i = 0; i < cubeSize; i++)
            {
                GameObject lineGameObject = Instantiate(lineOfSpheresGameObject);

                Transform lineTransform = lineGameObject.transform;

                lineTransform.position += Vector3.up * cubeDensity * i;

                lineTransform.parent = tempParentTransform;

                lineGameObjects.Add(lineGameObject);
            }

            GameObject planeOfSpheresGameObject = new GameObject("planeOfSpheresGameObject");

            MeshFilter planeOfSpheresMeshFilter = planeOfSpheresGameObject.AddComponent<MeshFilter>();
            MeshRenderer planeOfSpheresMeshRenderer = planeOfSpheresGameObject.AddComponent<MeshRenderer>();

            planeOfSpheresMeshRenderer.transform.parent = tempParentTransform;

            CreateCombinedMesh(lineGameObjects, planeOfSpheresMeshFilter);

            planeOfSpheresMeshRenderer.material = sphereMaterial;

            for (int i = 0; i < cubeSize; i++)
            {
                GameObject planeGameObject = Instantiate(planeOfSpheresGameObject);

                Transform planeTransform = planeGameObject.transform;

                planeTransform.position += Vector3.forward * cubeDensity * i;

                planeTransform.parent = transform;

                PlaneOutOfSpheresColorController planeOutOfSpheresColorController = planeGameObject.AddComponent<PlaneOutOfSpheresColorController>();

                planeOutOfSpheresColorController.ColorAnimationSettingConfig = colorAnimationSettingConfig;

                planeOutOfSpheresColorController.ColorTweenDelay = colorAnimationSettingConfig.DelayBetweenPlanes * i;
            }

            Destroy(tempParentGameObject);
        }

        #endregion

        #region Mesh Combine

        private void CreateCombinedMesh(List<GameObject> gameObjects, MeshFilter meshFilter)
        {
            List<MeshFilter> meshFilters = new List<MeshFilter>();

            foreach (var gameObject in gameObjects)
            {
                meshFilters.Add(gameObject.GetComponent<MeshFilter>());
            }

            CombineInstance[] combineInstanceArray = CreateCombineInstanceArray(meshFilters);

            Mesh combinedMesh = new()
            {
                indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
            };

            combinedMesh.CombineMeshes(combineInstanceArray);

            meshFilter.mesh = combinedMesh;
        }

        private CombineInstance[] CreateCombineInstanceArray(List<MeshFilter> meshFilters)
        {
            var combineInstances = new CombineInstance[meshFilters.Count];

            var index = 0;

            foreach (MeshFilter meshFilter in meshFilters)
            {
                meshFilter.gameObject.GetComponent<MeshRenderer>().enabled = false;

                var combineInstance = new CombineInstance
                {
                    mesh = meshFilter.mesh,
                    transform = meshFilter.transform.localToWorldMatrix
                };

                combineInstances[index++] = combineInstance;
            }

            return combineInstances;
        }

        #endregion
    }
}