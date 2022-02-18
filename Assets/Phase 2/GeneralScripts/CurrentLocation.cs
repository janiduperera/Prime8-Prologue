using UnityEngine;
using System.Collections;

public class CurrentLocation : MonoBehaviour {

	private Transform m_MyLocation;
	private Transform m_MyTransform;
	public Transform Target;
	public Transform DirectionArrow;

	private float	m_AlphaVal = 1;
	private bool  m_IsDescending = true;

	public Material	 TargetMat;
	// Use this for initialization
	void Awake () {
	
		m_MyTransform = transform;
	}

	public void SetMyLocation(Transform myLocation)
	{
		m_MyLocation = myLocation;
	}

	void OnDisable()
	{
		m_MyLocation = null;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(m_MyLocation)
		{
			m_MyTransform.position = new Vector3(m_MyLocation.position.x, 200, m_MyLocation.position.z);
			//Quaternion.LookRotation ((Target.position - m_MyTransform.position).normalized)
			DirectionArrow.rotation = Quaternion.LookRotation ((Target.position - m_MyTransform.position).normalized);
			//DirectionArrow.rotation = Quaternion.Euler (new Vector3 (90, DirectionArrow.rotation.y, 0));

			TargetMat.SetColor("_Color", new Color(1,1,1,m_AlphaVal));

			if(m_IsDescending)
			{
				m_AlphaVal = m_AlphaVal - 0.2f;

				if(m_AlphaVal <= 0)
				{
					m_AlphaVal = 0;
					m_IsDescending = false;
				}
			}
			else
			{
				m_AlphaVal = m_AlphaVal + 0.2f;

				if(m_AlphaVal >= 1)
				{
					m_AlphaVal = 1;
					m_IsDescending = true;
				}
			}

			// Rain
			if (TownController.GetInstance ().RainBasic.activeSelf) {
				TownController.GetInstance ().RainBasic.transform.position = new Vector3(m_MyLocation.position.x, 15, m_MyLocation.position.z);
			}
		}
	}
}
