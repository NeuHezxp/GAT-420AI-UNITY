using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAgenSpawner : MonoBehaviour
{
    public AiAgent[] agents;
    int index = 0;
    public LayerMask layerMask;
    // Start is called before the first frame update

    void Update()

    {
        // press tab to cycle through agents to spawn
        if (Input.GetKeyDown(KeyCode.Tab)) index = (++index % agents.Length);
        // click to spawn. ctrl click to spawn multiple
        if (Input.GetMouseButtonDown(0) || (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl)))

        {
            //get ray from camera to screen position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //raycast and see if it hits an object with layer
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, layerMask))

            {
                //spawn agent
                Instantiate(agents[index], hitInfo.point, Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up)); //instaniate=create object of this type of prefab.

            }

        }

    }// Update is called once per frame

}
