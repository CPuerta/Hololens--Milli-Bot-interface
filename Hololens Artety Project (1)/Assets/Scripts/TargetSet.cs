using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSet : MonoBehaviour {
    public GameObject RingGenerator;
	// Use this for initialization
	void Start () {
		for (int i= 0; i < transform.childCount; i++)
        {
         //   transform.GetChild(i).GetComponent<RingNodes>().target = RingGenerator.GetComponent<RingGenerator>().NodeArray[i];// for each child sets the target to a target node from the ring generator

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
