using UnityEngine;
using System.Collections;

public class TrafficCrossings : MonoBehaviour {

	public TrafficCar[] TrafficCars;

	private bool m_IsCollided = false;

	void OnTriggerEnter(Collider other)
	{
		if(m_IsCollided == false)
		{
			m_IsCollided = true;
			foreach(TrafficCar tc in TrafficCars)
				tc.IsCharacterInCrossing = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(m_IsCollided)
		{
			m_IsCollided = false;
			foreach(TrafficCar tc in TrafficCars)
				tc.IsCharacterInCrossing = false;
		}
	}
}
