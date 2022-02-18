using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birds : MonoBehaviour {

	private Transform m_MyTransform;

	private Vector3 m_RandomPosition;
	private Quaternion m_TargetRotation;
	private bool m_GetARandomPostion = true;

	private const float m_MINX = -1264f;
	private const float m_MAXX = 164f;
	private const float m_MINZ = -65f;
	private const float m_MAXZ = 622f;

	// Use this for initialization
	void Start () {
		m_MyTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {

		if (m_GetARandomPostion) {
			m_GetARandomPostion = false;

			GetRandomPosition ();

			// Smoothly rotates towards target 
			m_TargetRotation = Quaternion.LookRotation(m_RandomPosition - m_MyTransform.position, Vector3.up);
		}

		m_MyTransform.position = Vector3.MoveTowards(m_MyTransform.position, m_RandomPosition, Time.deltaTime * 10f);

		m_MyTransform.rotation = Quaternion.Slerp(m_MyTransform.rotation, m_TargetRotation, Time.deltaTime * 4.0f); 

		if ((m_MyTransform.position - m_RandomPosition).magnitude < 0.3f) {

			m_GetARandomPostion = true;

		}
	}

	void GetRandomPosition()
	{
		m_RandomPosition = new Vector3 (Random.Range (m_MINX, m_MAXX), 22f, Random.Range (m_MINZ, m_MAXZ));
	}
}
