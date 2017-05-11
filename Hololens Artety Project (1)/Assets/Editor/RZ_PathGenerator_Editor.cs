using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RZ_PathGenerator))]
public class RZ_PathGenerator_Editor : Editor {

	public override void OnInspectorGUI ()
	{
		RZ_PathGenerator pathGenerator = (RZ_PathGenerator)target;
		base.OnInspectorGUI ();
		if(GUILayout.Button("Draw Line")){
			pathGenerator.createCenterPoint(ref pathGenerator.verticesList,pathGenerator.pointSize,pathGenerator.targetObject, ref pathGenerator.pointList);
			//pathGenerator.DrawLineBetweenPoints (pathGenerator.pointList);
		}
	}
}
