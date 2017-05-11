using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using HoloLensXboxController;

public class RingGenerator : MonoBehaviour {

	public bool ShowGizmos = true;
	public float gizmoSize = 2.0f;
	public float RayLifeInSecond = 2.0f;
	public float ringLineLifeInSecond = 20.0f;
	public float EmitorDistance = 5.0f;
	public int RingNodeNumber = 12;
	public PrimitiveType HitNodeObjectType = PrimitiveType.Cube;
	public Color EmitterLineColor = Color.green;
	public Color RayLineColor = Color.red;
	public float rayLenghth = 2.0f;
	public Color hitNodeObjectColor = Color.yellow;
	public Color smallestRingColor = Color.gray;
	public GameObject emissionPoint;
	public int targetObjectLayer = 9;
	int index = 0;
    public GameObject RingSystem;
    [HideInInspector]
    public GameObject[] NodeArray;
    private ControllerInput controllerInput;


    class Ring {
		public List<RaycastHit> ringRayCastHitList = new List<RaycastHit> ();
		public List<GameObject> ringHitObjectList = new List<GameObject> ();
		public float ringAvgDisrance;
	}

	 List<Ring> _RingList = new List<Ring>();

	// Use this for initialization
	void Start () {
        controllerInput = new ControllerInput(0, 0.19f);
        //emissionPoint =  createEmissionPoint ();
        //createRing();
        //drawRing ();
    }
	
	// Update is called once per frame
	void Update () {
		if (transform.hasChanged) {
			
		}
        if (controllerInput.GetAxisLeftTrigger() == 1.0f)
        {
            emissionPoint = createEmissionPoint();
            createRing();
            drawRing();
        }


    }


	void createRing (){
		float _rayEmittionDegree = 360 / RingNodeNumber;
		int layerMask = 1 << targetObjectLayer;
		int hitObjCounter = 0;
		int ringCounter=0;
		List<Ring> ringList = new List<Ring> ();
		for (float i = 0.0f; i <= 180 -_rayEmittionDegree; i+=_rayEmittionDegree) {
			emissionPoint.transform.Rotate (new Vector3 (_rayEmittionDegree, 0, 0));
			for (float j = 0.0f; j <= 180 - _rayEmittionDegree; j+=_rayEmittionDegree) {
				Ring newRing = new Ring ();
				List<RaycastHit> _ringRayCastHitList = new List<RaycastHit> ();
				List<GameObject> _ringHitObjectList = new List<GameObject> ();
				float averageDistance = 0.0f;
				emissionPoint.transform.Rotate (new Vector3 (0, _rayEmittionDegree, 0));
				for (float k = 0.0f; k <= 360 - _rayEmittionDegree; k+=_rayEmittionDegree) {
					RaycastHit hit;
					float zValue = Mathf.Cos ((k * Mathf.PI) / 180);
					float yValue = Mathf.Sin ((k * Mathf.PI) / 180);
					Vector3 rayTarget = emissionPoint.transform.TransformDirection(new Vector3(0,yValue,zValue))*rayLenghth;
					Debug.DrawRay(emissionPoint.transform.position,rayTarget,RayLineColor,RayLifeInSecond);
					if (Physics.Raycast(emissionPoint.transform.position,rayTarget, out hit,Mathf.Infinity,layerMask)) {
						if (ShowGizmos) {
							GameObject hitNode = GameObject.CreatePrimitive (HitNodeObjectType);
							hitNode.transform.localScale = new Vector3 (gizmoSize/10, gizmoSize/10, gizmoSize/10);
							hitNode.transform.position = hit.point;
							hitNode.name = "hit_node_" + hitObjCounter;
							hitNode.GetComponent<MeshRenderer> ().material.color = hitNodeObjectColor;
							_ringHitObjectList.Add (hitNode);
						}
						averageDistance += hit.distance;
						_ringRayCastHitList.Add (hit);
						hitObjCounter++;
					}
				}
				if (ShowGizmos) {
					newRing.ringHitObjectList = _ringHitObjectList;
				}
				newRing.ringRayCastHitList = _ringRayCastHitList;
				newRing.ringAvgDisrance = (averageDistance / RingNodeNumber);
				ringList.Add (newRing);
				ringCounter++;
			}
		}

		if (ShowGizmos) {
			for (int i = 0; i < ringList.Count; i++) {
				for (int j = 0; j < ringList[i].ringHitObjectList.Count; j++) {
					ringList [i].ringHitObjectList[j].transform.parent = emissionPoint.transform;
				}
			}
		}
		//find the smallest ring
		//ringList = ringList.OrderBy (x => x.ringAvgDisrance).ToList();
		float minimumDistance = ringList[0].ringAvgDisrance;
        print(ringList.Count);
		for (int i = 0; i < ringList.Count; i++) {
			if (ringList[i].ringAvgDisrance<minimumDistance) {
				minimumDistance = ringList [i].ringAvgDisrance;
				index = i;
			}
			_RingList = ringList;
			print ("List was coppied");
			print ("Ring number " + index + " was found and has distance "+minimumDistance);
		}
	}

	void drawRing (){
        print("entered draw ring");
        //GameObject ringSystem = (GameObject)Instantiate(RingSystem, transform.position, transform.rotation);// spawn the ring system 
		for (int i = 0; i < _RingList[index].ringRayCastHitList.Count-1; i++) {
			Debug.DrawLine (_RingList [index].ringRayCastHitList [i].point, _RingList [index].ringRayCastHitList [i + 1].point,Color.green,ringLineLifeInSecond);
            //NodeArray[i] = _RingList[index].ringHitObjectList[i];

		}
        print(_RingList[index].ringRayCastHitList.Count);
		Debug.DrawLine (_RingList [index].ringRayCastHitList [_RingList[index].ringRayCastHitList.Count-1].point, _RingList [index].ringRayCastHitList [0].point,Color.green,10.0f);
	}




	GameObject createEmissionPoint (){
		GameObject _emissionPoint = (GameObject)Instantiate(RingSystem, transform.TransformPoint(Vector3.right * EmitorDistance), transform.rotation);// GameObject.CreatePrimitive (PrimitiveType.Cube);
        _emissionPoint.GetComponent<TargetSet>().RingGenerator = this.gameObject;
        //_emissionPoint.name= "Emission_Point";
		//_emissionPoint.transform.position = transform.TransformPoint(Vector3.right* EmitorDistance);
		//_emissionPoint.transform.rotation = transform.rotation;
		
		_emissionPoint.transform.localScale = new Vector3 (gizmoSize/10, gizmoSize/10, gizmoSize/10);
		_emissionPoint.transform.parent = this.transform;
		Debug.DrawRay (transform.position, _emissionPoint.transform.position - transform.position, EmitterLineColor, RayLifeInSecond);
		checkShowGizmo (ref _emissionPoint);
		return _emissionPoint;

	}

	void checkShowGizmo(ref GameObject _gameObject) {
		if (ShowGizmos) {
			_gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}
}

