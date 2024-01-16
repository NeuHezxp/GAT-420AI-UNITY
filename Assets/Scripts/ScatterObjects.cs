using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterObjects : MonoBehaviour
{
    public GameObject[] prefabs;         // Array of prefabs to scatter
    public GameObject targetObject;      // The object to scatter prefabs onto
    public int numberOfInstances;        // Number of prefab instances to scatter
    public Vector2 rotationRange;        // Range for random rotation
    public Vector3 scatterAreaSize = new Vector3(20, 20, 20); // Size of the area to scatter over

    void Start()
    {
        ScatterObject();
    }

    void ScatterObject()
    {
        if (prefabs == null || prefabs.Length == 0 || targetObject == null)
        {
            Debug.LogError("Prefabs and target object must be set!");
            return;
        }

        Bounds bounds = new Bounds(targetObject.transform.position, scatterAreaSize);

        for (int i = 0; i < numberOfInstances; i++)
        {
            // Select a random prefab from the array
            GameObject prefabToInstantiate = prefabs[Random.Range(0, prefabs.Length)];

            // Random position within the bounds
            Vector3 randomPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                bounds.max.y + 5, // Start above the object
                Random.Range(bounds.min.z, bounds.max.z)
            );

            // Raycast down to find the surface of the target object
            if (Physics.Raycast(randomPosition, Vector3.down, out RaycastHit hit, 100))
            {
                // Random rotation
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(rotationRange.x, rotationRange.y), 0);

                // Instantiate the selected prefab at the hit point
                Instantiate(prefabToInstantiate, hit.point, randomRotation);
            }
        }
    }
}
