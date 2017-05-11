using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingNodes : MonoBehaviour {
    // The Ring object spawns 30 or so small boxes 
    RaycastHit hit;
    public GameObject target;// each box is pair to node outside outside the artery
    float distance;

                                                                                                                                                                              public float speed = 0.5f;
    public float threshold = .3f;
	// Update is called once per frame
	void Update () {
        Vector3 rayTarget = target.transform.position - transform.position;// We find the direction of the node
        Debug.DrawRay(transform.position, rayTarget);
        float step = speed * Time.deltaTime;

        if (Physics.Raycast(transform.position,rayTarget, out hit)){
            distance = hit.distance;// the raycast hits artery walls and we obtain the distance from it 
           // print(distance + " from " + hit.collider.gameObject.name);
            if (distance > threshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);// each box then moves into the threshold range
                // a Line renderer script then connects each of them to form a ring that adapts to the current size of them artery 
            }
        
        }

		
	}
}
