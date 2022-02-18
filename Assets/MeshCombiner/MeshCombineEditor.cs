using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;


public class MeshCombineEditor : EditorWindow {

	#region Editor Script
	[MenuItem ("Window/MeshCombiner")]
	static void Init ()
	{
		MeshCombineEditor window = EditorWindow.GetWindow (typeof (MeshCombineEditor)) as MeshCombineEditor;
		window.Show ();
	}

	public MeshCombiner MeC;

	void OnGUI ()
	{
		// 2
		if (GUILayout.Button ("Basic Combine")) {
			MeshCombiner mc = GameObject.FindWithTag("meshcombiner").GetComponent<MeshCombiner>();
			mc.MergeChildObjects ();
		}

		// Or this is done
		if (GUILayout.Button ("BasicMerge")) {
			MeshCombiner mc = GameObject.FindWithTag("meshcombiner").GetComponent<MeshCombiner>();
			mc.BasicMerge ();
		}

		// 1
		if (GUILayout.Button ("Group GameObjects")) {
			MeshCombiner mc = GameObject.FindWithTag("meshcombiner").GetComponent<MeshCombiner>();
			mc.GroupObjectsWithSameMaterial ();
		}

		//3
		if (GUILayout.Button ("DeleteChildObjects")) {
			MeshCombiner mc = GameObject.FindWithTag("meshcombiner").GetComponent<MeshCombiner>();
			mc.DeleteChildObjects ();
		}

		//4
		if (GUILayout.Button ("CombineWithDifferentMaterial")) {
			MeshCombiner mc = GameObject.FindWithTag("meshcombiner").GetComponent<MeshCombiner>();
			mc.MergeWithDiffMaterials ();
		}

        //5 Add Colliders
        if (GUILayout.Button("Add Colliders"))
        {
            AddColliders mc = GameObject.FindWithTag("meshcombiner").GetComponent<AddColliders>();
            mc.AddCapsuleCollider();
        }
    }
	#endregion
}
#endif
