using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class RZ_PathGenerator : MonoBehaviour {


	//PARAMETERS
	public class TargetObjectVertexStruct
	{
		public int vertexID;
		public Vector3 vertexPosition;
		public GameObject vertexObject;
		public bool selectedVertex;
	}

	int pointCounter;
	public List<GameObject> pointList= new List<GameObject>();
	public List<TargetObjectVertexStruct> verticesList = new List<TargetObjectVertexStruct>();
	public float vertexSize = 0.05f;
	public float pointSize=0.08f;
	public GameObject targetObject;









	//BODY
	void Start () {

		getVerticesPosition(ref verticesList, targetObject);
		createTargetObjectVerticesObject (ref verticesList, targetObject,vertexSize);
	}
		
	void Update () {

		selectInBoundVertices(ref verticesList);
		DrawLineBetweenPoints (pointList);
	}










	//METHODS
	public void getVerticesPosition (ref List<TargetObjectVertexStruct> _ref_VerticesList,GameObject _targetObject){
		Vector3[] _targetObjectVerticesList = _targetObject.GetComponent<MeshFilter> ().mesh.vertices;
		for(int _index=0; _index<_targetObjectVerticesList.Length;_index++){
			TargetObjectVertexStruct _temp_TargetObjectVertexStruct = new TargetObjectVertexStruct ();
			_temp_TargetObjectVertexStruct.vertexID = _index;
			_temp_TargetObjectVertexStruct.selectedVertex = false;
			_temp_TargetObjectVertexStruct.vertexObject = null;
			_temp_TargetObjectVertexStruct.vertexPosition = _targetObjectVerticesList[_index] +_targetObject.transform.position;
			_ref_VerticesList.Add(_temp_TargetObjectVertexStruct);
		}
	}

	public void createTargetObjectVerticesObject(ref List<TargetObjectVertexStruct> _ref_verticesList, GameObject _targetObject,float _vectorSize){
		for (int _index = 0;_index < _ref_verticesList.Count; _index++) {
			GameObject _vertexSphereObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			_vertexSphereObject.name = "Vertex_" + _index;
			_vertexSphereObject.GetComponent<Renderer> ().material.color = Color.red;
			_vertexSphereObject.transform.localScale = new Vector3 (_vectorSize, _vectorSize, _vectorSize);
			_vertexSphereObject.transform.position = _ref_verticesList[_index].vertexPosition;
			_vertexSphereObject.transform.parent = _targetObject.transform;
			TargetObjectVertexStruct _temp_TargetObjectVertexStruct = new TargetObjectVertexStruct ();
			_temp_TargetObjectVertexStruct.vertexID = _ref_verticesList[_index].vertexID;
			_temp_TargetObjectVertexStruct.selectedVertex = _ref_verticesList[_index].selectedVertex;
			_temp_TargetObjectVertexStruct.vertexObject = _vertexSphereObject;
			_temp_TargetObjectVertexStruct.vertexPosition = _ref_verticesList[_index].vertexPosition;
			_ref_verticesList [_index] = _temp_TargetObjectVertexStruct;
		}
	}

	public void selectInBoundVertices (ref List<TargetObjectVertexStruct> __ref_verticesList){

		if (gameObject.transform.hasChanged) {
			gameObject.transform.hasChanged = false;
			foreach (var vertext in __ref_verticesList) {
				if (gameObject.GetComponent<Renderer> ().bounds.Contains (vertext.vertexPosition)) {
					vertext.selectedVertex = true;
					vertext.vertexObject.GetComponent<Renderer> ().material.color = Color.green;
				} else {
					vertext.selectedVertex = false;
					vertext.vertexObject.GetComponent<Renderer> ().material.color = Color.blue;
				}
			}
		}

	}

	public void createCenterPoint(ref List<TargetObjectVertexStruct> __ref_verticesList, float _pointSize, GameObject _targetObject, ref List<GameObject> _pointList){
		Vector3 _sumVertices = new Vector3 (0,0,0);
		int counter = 0;
		foreach (var vertex in __ref_verticesList) {
			if (vertex.selectedVertex) {
				counter++;
				_sumVertices += vertex.vertexPosition;
			}
		}

		if (counter > 0) {
			pointCounter++;
			GameObject _newCenterPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			_newCenterPoint.name = "Point_"+pointCounter;
			_newCenterPoint.GetComponent<Renderer> ().material.color = Color.yellow;
			_newCenterPoint.transform.localScale = new Vector3 (_pointSize, _pointSize, _pointSize);
			_newCenterPoint.transform.position = _sumVertices/counter;
			_newCenterPoint.transform.parent = _targetObject.transform;
			_pointList.Add (_newCenterPoint);
		}
	}

	public void DrawLineBetweenPoints(List<GameObject> _pointList){
		if (_pointList.Count > 1) {
			for (int _index = 0; _index < _pointList.Count-1; _index++) {
				Debug.DrawLine (_pointList [_index].transform.position, _pointList [_index + 1].transform.position, Color.green);
			}
		}
	}
}






