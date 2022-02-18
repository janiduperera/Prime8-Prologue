using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  Piliwela thamai,
/// 
///  Group Gameobjects, Basic Combine, Delete Child objects, Combine with different material
/// </summary>

public class MeshCombiner : MonoBehaviour {

	protected Mesh mesh;
	
	public void AdvancedMerge()
	{
		// All our children (and us)
		MeshFilter[] filters = GetComponentsInChildren<MeshFilter> (false);

		// All the meshes in our children (just a big list)
		List<Material> materials = new List<Material>();
		MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer> (false); // <-- you can optimize this
		foreach (MeshRenderer renderer in renderers)
		{
			if (renderer.transform == transform)
				continue;
			Material[] localMats = renderer.sharedMaterials;
			foreach (Material localMat in localMats)
				if (!materials.Contains (localMat))
					materials.Add (localMat);
		}

		// Each material will have a mesh for it.
		List<Mesh> submeshes = new List<Mesh>();
		foreach (Material material in materials)
		{
			// Make a combiner for each (sub)mesh that is mapped to the right material.
			List<CombineInstance> combiners = new List<CombineInstance> ();
			foreach (MeshFilter filter in filters)
			{
				if (filter.transform == transform) continue;
				// The filter doesn't know what materials are involved, get the renderer.
				MeshRenderer renderer = filter.GetComponent<MeshRenderer> ();  // <-- (Easy optimization is possible here, give it a try!)
				if (renderer == null)
				{
					Debug.LogError (filter.name + " has no MeshRenderer");
					continue;
				}

				// Let's see if their materials are the one we want right now.
				Material[] localMaterials = renderer.sharedMaterials;
				for (int materialIndex = 0; materialIndex < localMaterials.Length; materialIndex++)
				{
					if (localMaterials [materialIndex] != material)
						continue;
					// This submesh is the material we're looking for right now.
					CombineInstance ci = new CombineInstance();
					ci.mesh = filter.sharedMesh;
					ci.subMeshIndex = materialIndex;
					ci.transform = Matrix4x4.identity;
					combiners.Add (ci);
				}
			}
			// Flatten into a single mesh.
			Mesh mesh = new Mesh ();
			mesh.CombineMeshes (combiners.ToArray(), true);
			submeshes.Add (mesh);
		}

		// The final mesh: combine all the material-specific meshes as independent submeshes.
		List<CombineInstance> finalCombiners = new List<CombineInstance> ();
		foreach (Mesh mesh in submeshes)
		{
			CombineInstance ci = new CombineInstance ();
			ci.mesh = mesh;
			ci.subMeshIndex = 0;
			ci.transform = Matrix4x4.identity;
			finalCombiners.Add (ci);
		}
		Mesh finalMesh = new Mesh();
		finalMesh.CombineMeshes (finalCombiners.ToArray(), false);
		GetComponent<MeshFilter> ().sharedMesh = finalMesh;
		//myMeshFilter.sharedMesh = finalMesh;
		Debug.Log ("Final mesh has " + submeshes.Count + " materials.");
	}

	public void BasicMerge()
	{
//		mesh = GetComponent<MeshFilter> ().sharedMesh;
//		if (mesh == null) {
//			mesh = new Mesh ();
//			GetComponent<MeshFilter> ().sharedMesh = mesh;
//		} else
//			mesh.Clear ();
//
//		MeshFilter[] filters = GetComponentsInChildren<MeshFilter> (false);
//		Debug.Log ("Merging  " + (filters.Length - 1) + " meshes...");
//
//		List<CombineInstance> combiners = new List<CombineInstance> ();
//
//		foreach (MeshFilter filter in filters) {
//
//			//Don't combine us with ourself
//			if (filter == GetComponent<MeshFilter> ()) {
//				continue;
//			}
//
//			CombineInstance ci = new CombineInstance ();
//			ci.mesh = filter.sharedMesh;
//			ci.subMeshIndex = 0;
//			ci.transform = Matrix4x4.identity;
//			combiners.Add (ci);
//		}
//
//		mesh.CombineMeshes (combiners.ToArray (), true);

		Quaternion oldRot = transform.rotation;
		Vector3 oldPos = transform.position;

		transform.rotation = Quaternion.identity;
		transform.position = Vector3.zero;

		MeshFilter[] filters = GetComponentsInChildren<MeshFilter> ();
		Debug.Log (name + " is combining " + (filters.Length - 1) + " meshes");

		Mesh finalMesh = new Mesh ();
		CombineInstance[] combiners = new CombineInstance[filters.Length];

		for (int a = 0; a < filters.Length; a++) {
			if (filters [a].transform == transform)
				continue;

			combiners [a].subMeshIndex = 0;
			combiners [a].mesh = filters [a].sharedMesh;
			combiners [a].transform = filters [a].transform.localToWorldMatrix;
		}

		finalMesh.CombineMeshes (combiners);

		GetComponent<MeshFilter> ().sharedMesh = finalMesh;

		transform.rotation = oldRot;
		transform.position = oldPos;

		for (int a = 0; a < transform.childCount; a++) {
			transform.GetChild (a).gameObject.SetActive (false);
		}
	}

	public void GroupObjectsWithSameMaterial()
	{
		Dictionary<Material, List<GameObject>> matDic = new Dictionary<Material, List<GameObject>> ();
		List<Material> materials = new List<Material>();
		MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer> (false); // <-- you can optimize this
		foreach (MeshRenderer renderer in renderers)
		{
			if (renderer.transform == transform)
				continue;
			Material[] localMats = renderer.sharedMaterials;
			foreach (Material localMat in localMats) {
//				if (!materials.Contains (localMat)) {
//					materials.Add (localMat);
//				}
				List<GameObject> li;
				if (matDic.TryGetValue (localMat, out li)) {
					li.Add (renderer.gameObject);
					matDic [localMat] = li;
				} else {
					li = new List<GameObject> ();
					li.Add (renderer.gameObject);
					matDic.Add (localMat, li);
				}
			}
		}

		List<int> _grouppedObj = new List<int> ();

		int r = 0;
		foreach(List<GameObject> prop in matDic.Values)
		{ 
			GameObject go = new GameObject (r + "");
			go.AddComponent<MeshFilter> ();
			go.AddComponent<MeshRenderer> ();
			go.transform.localScale = Vector3.one;
			go.transform.SetParent (transform);
			go.transform.localPosition = Vector3.zero;

			foreach (GameObject g in prop) {
				g.transform.SetParent (go.transform);
			}
			_grouppedObj.Add (go.GetInstanceID());
			r++;
//			Vector3 _oldScale = go.transform.localScale;
//			go.transform.SetParent (transform);
//			go.transform.localScale = Vector3.one;
//			go.transform.localScale = 
//			go.transform.localPosition = Vector3.zero;
		}
		Debug.Log ("Differetn Materials count  : " + matDic.Count);

		List<GameObject> _toDestroy = new List<GameObject> ();
		for (int i = 0; i < transform.childCount; i++) {
			if (!_grouppedObj.Contains (transform.GetChild (i).gameObject.GetInstanceID())) {
				_toDestroy.Add(transform.GetChild (i).gameObject);
			}
		}

		foreach (GameObject _g in _toDestroy)
			DestroyImmediate (_g);
	}

	public void MergeChildObjects()
	{
		for (int i = 0; i < transform.childCount; i++) {
			Quaternion oldRot = transform.GetChild(i).rotation;
			Vector3 oldPos = transform.GetChild(i).position;

			transform.GetChild(i).rotation = Quaternion.identity;
			transform.GetChild(i).position = Vector3.zero;

			MeshFilter[] filters = transform.GetChild(i).GetComponentsInChildren<MeshFilter> (true);
			Debug.Log (name + " is combining " + (filters.Length - 1) + " meshes");

			Mesh finalMesh = new Mesh ();
			CombineInstance[] combiners = new CombineInstance[filters.Length];
			List<Material> materials = new List<Material>();
			for (int a = 0; a < filters.Length; a++) {
				if (filters [a].transform == transform)
					continue;

				combiners [a].subMeshIndex = 0;
				combiners [a].mesh = filters [a].sharedMesh;
				combiners [a].transform = filters [a].transform.localToWorldMatrix;

				if(materials.Count == 0)
					materials.Add(filters [a].GetComponent<MeshRenderer> ().sharedMaterials[0]);
			}

			finalMesh.CombineMeshes (combiners);

			transform.GetChild(i).GetComponent<MeshFilter> ().sharedMesh = finalMesh;

			transform.GetChild (i).GetComponent<Renderer>().sharedMaterial = transform.GetChild(i).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;


			transform.GetChild(i).rotation = oldRot;
			transform.GetChild(i).position = oldPos;

			for (int a = 0; a < transform.GetChild(i).childCount; a++) {
				transform.GetChild(i).GetChild (a).gameObject.SetActive (false);
			}
		}
	}

	public void DeleteChildObjects()
	{
		List<GameObject> li = new List<GameObject> ();
		for (int i = 0; i < transform.childCount; i++) {
			Transform t = transform.GetChild (i);

			li.Clear ();
			int index = t.childCount;
			for(int r = 0; r < index; r++)
			{
				li.Add (t.GetChild (r).gameObject);

			}

			foreach(GameObject g in li)
				DestroyImmediate (g);
		}
	}

	public void MergeWithDiffMaterials()
	{
		Quaternion oldRot = transform.rotation;
		Vector3 oldPos = transform.position;

		Dictionary<string, List<CombineInstance>> dic = new Dictionary<string, List<CombineInstance>> ();

		List<Material> materials = new List<Material>();
		for (int i = 0; i < transform.childCount; i++) {

			materials.Add(transform.GetChild(i).GetComponent<MeshRenderer>().sharedMaterial);

			//Get all meshfilters from this tree, true to also find deactivated children
			MeshFilter[] meshFilters = transform.GetChild(i).GetComponentsInChildren<MeshFilter>(true);

			//Loop through all children
			for (int j = 0; j < meshFilters.Length; j++)
			{
				MeshFilter meshFilter = meshFilters[j];

				CombineInstance combine = new CombineInstance();

				//Is it wood or leaf?
				MeshRenderer meshRender = meshFilter.GetComponent<MeshRenderer>();

				//Modify the material name, because Unity adds (Instance) to the end of the name
				string materialName = meshRender.sharedMaterial.name.Replace(" (Instance)", "");

				combine.mesh = meshFilter.sharedMesh;
				//combine.transform = meshFilter.transform.localToWorldMatrix;
				combine.transform = Matrix4x4.identity;

				List<CombineInstance> cob;
				if (dic.TryGetValue (materialName, out cob)) {
					
					cob.Add (combine);
					dic [materialName] = cob;
				} else {
					cob = new List<CombineInstance> ();
					cob.Add (combine);
					dic.Add (materialName, cob);
				}
			}
		}

		List<Mesh> combindMatMesh = new List<Mesh> ();

		foreach (KeyValuePair<string, List<CombineInstance>> pair in dic) {
			Mesh combinedMesh = new Mesh();
			combinedMesh.CombineMeshes (pair.Value.ToArray ());
			combindMatMesh.Add (combinedMesh);
		}

		//Create the array that will form the combined mesh
		CombineInstance[] totalMesh = new CombineInstance[combindMatMesh.Count];

		for (int h = 0; h < combindMatMesh.Count; h++) {
			totalMesh[h].mesh = combindMatMesh[h];
			//totalMesh[h].transform = transform.localToWorldMatrix;
			totalMesh[h].transform = Matrix4x4.identity;
			//totalMesh[h].transform = GameObject.FindWithTag("tailPoint").transform.localToWorldMatrix;
		}

		GetComponent<MeshRenderer> ().sharedMaterials = materials.ToArray ();

		//Create the final combined mesh
		Mesh combinedAllMesh = new Mesh();

		//Make sure it's set to false to get 2 separate meshes
		combinedAllMesh.CombineMeshes(totalMesh, false);
		GetComponent<MeshFilter>().mesh = combinedAllMesh;

		transform.rotation = oldRot;
		transform.position = oldPos;

		for (int a = 0; a < transform.childCount; a++) {
			transform.GetChild (a).gameObject.SetActive (false);
		}
	}
}
