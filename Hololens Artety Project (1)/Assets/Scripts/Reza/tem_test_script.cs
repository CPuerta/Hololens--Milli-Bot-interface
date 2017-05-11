using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tem_test_script : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		for (int i = 0; i < vertices.Length; i++) {
			GameObject vertex = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			vertex.name= "Vertex_"+i;
			vertex.transform.localScale = new Vector3 (0.02f, 0.02f, 0.02f);
			vertex.transform.position = vertices [i]+this.transform.position;
			vertex.GetComponent<MeshRenderer> ().material.color = Color.green;
			vertex.transform.parent = this.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}