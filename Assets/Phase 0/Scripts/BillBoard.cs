using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour {

	public Camera m_Camera;
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (transform.position + m_Camera.transform.rotation * Vector3.back, m_Camera.transform.rotation * Vector3.up);
	}
}
