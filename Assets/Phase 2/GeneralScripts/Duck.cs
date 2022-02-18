using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour {

	private Vector3[] m_WaterAreaLocations = new Vector3[3];

	private int m_AreaLocationChoosen = 0;
	private Vector3 m_RandomPosition;

	private Transform m_MyTransform;

	private Quaternion m_TargetRotation;
	private bool m_GetARandomPostion = true;

	private AudioSource m_MyAudioSource;
	private float m_DealyQuackSound = 0;

	// Use this for initialization
	void Start () {
		m_MyTransform = transform;
		m_MyAudioSource = GetComponent<AudioSource> ();

		m_WaterAreaLocations [0] = new Vector3 (-1233f, 0, -131.5f);
		m_WaterAreaLocations [1] = new Vector3 (-1245.5f, 0, -121.3f);
		m_WaterAreaLocations [2] = new Vector3 (-1257.7f, 0, -110.3f);
	}
	
	// Update is called once per frame
	void Update () {

		if (m_GetARandomPostion) {
			m_GetARandomPostion = false;
			if (m_AreaLocationChoosen == 0) {
				m_RandomPosition = (Random.insideUnitSphere * 13) + m_WaterAreaLocations [m_AreaLocationChoosen];
			} else if (m_AreaLocationChoosen == 1) {
				m_RandomPosition = (Random.insideUnitSphere * 17) + m_WaterAreaLocations [m_AreaLocationChoosen];
			} else {
				m_RandomPosition = (Random.insideUnitSphere * 13) + m_WaterAreaLocations [m_AreaLocationChoosen];
			}

			m_RandomPosition = new Vector3 (m_RandomPosition.x, 0.77f, m_RandomPosition.z);

			// Smoothly rotates towards target 
			m_TargetRotation = Quaternion.LookRotation(m_RandomPosition - m_MyTransform.position, Vector3.up);
		} 

		m_MyTransform.position = Vector3.MoveTowards(m_MyTransform.position, m_RandomPosition, Time.deltaTime * 2f);

		m_MyTransform.rotation = Quaternion.Slerp(m_MyTransform.rotation, m_TargetRotation, Time.deltaTime * 4.0f); 

		if ((m_MyTransform.position - m_RandomPosition).magnitude < 0.3f) {

			if (m_AreaLocationChoosen + 1 > 2) {
				m_AreaLocationChoosen = 0;
			} else {
				m_AreaLocationChoosen++;
				m_GetARandomPostion = true;
			}
		}

		m_DealyQuackSound += Time.deltaTime;

		if (m_DealyQuackSound > 7) {

			m_DealyQuackSound = 0;
			if (!m_MyAudioSource.isPlaying) {

				if (TownController.GetInstance ().GetActiveISelObj () != null && TownController.GetInstance ().GetActiveISelObj ().Name == "Panda") {

					if ((TownController.GetInstance ().Panda.transform.position - m_MyTransform.position).magnitude < 90) {
						m_MyAudioSource.Play ();
					} 
				}
				else if(TownController.GetInstance ().GetActiveISelObj () != null && TownController.GetInstance ().GetActiveISelObj ().Name == "Gorilla")
				{
					if ((TownController.GetInstance ().Gorilla.transform.position - m_MyTransform.position).magnitude < 70) {
						m_MyAudioSource.Play ();
					} 
				}
				else if(TownController.GetInstance ().GetActiveISelObj () != null && TownController.GetInstance ().GetActiveISelObj ().Name == "Caterpillar")
				{
					if ((TownController.GetInstance ().Caterpillar.transform.position - m_MyTransform.position).magnitude < 70) {
						m_MyAudioSource.Play ();
					} 
				}
				else if(TownController.GetInstance ().GetActiveISelObj () != null && TownController.GetInstance ().GetActiveISelObj ().Name == "Snake")
				{
					if ((TownController.GetInstance ().SamSnake.transform.position - m_MyTransform.position).magnitude < 70) {
						m_MyAudioSource.Play ();
					} 
				}
				else if(TownController.GetInstance ().GetActiveISelObj () != null && TownController.GetInstance ().GetActiveISelObj ().Name == "Rhino")
				{
					if ((TownController.GetInstance ().Rhino.transform.position - m_MyTransform.position).magnitude < 70) {
						m_MyAudioSource.Play ();
					} 
				}
				else if(TownController.GetInstance ().GetActiveISelObj () != null && TownController.GetInstance ().GetActiveISelObj ().Name == "Hippo")
				{
					if ((TownController.GetInstance ().Hippo.transform.position - m_MyTransform.position).magnitude < 70) {
						m_MyAudioSource.Play ();
					} 
				}
			}
		}
	}
}
