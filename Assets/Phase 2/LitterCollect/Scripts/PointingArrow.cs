using UnityEngine;
using System.Collections;

public class PointingArrow : MonoBehaviour {

	private Transform m_Transform;

	private bool m_ShouldAnimate = false;

	private int m_Direction = -1;
	private bool m_IsDirectionChangeCalled = false;

	// Use this for initialization
	void Awake () {

		m_Transform = transform;

		GetComponent<SpriteRenderer>().enabled = false;
	
	}

	public void StartPointingArrow()
	{
		m_ShouldAnimate = true;

		GetComponent<SpriteRenderer>().enabled = true;
	}

	void OnDisable()
	{
		m_ShouldAnimate = false;

		GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(!m_ShouldAnimate)
		{
			return;
		}

		m_Transform.Translate(Vector3.up * 3f * Time.deltaTime * m_Direction);

		if(Camera.main != null)
		{
			transform.LookAt(Camera.main.gameObject.transform);
		}

		if(!m_IsDirectionChangeCalled)
		{
			m_IsDirectionChangeCalled = true;
			StartCoroutine(WaitToChangeDirection());
		}
	}

	IEnumerator WaitToChangeDirection()
	{
		yield return new WaitForSeconds(0.5f);

		m_Direction = m_Direction * -1;

		m_IsDirectionChangeCalled = false;
	}
}
