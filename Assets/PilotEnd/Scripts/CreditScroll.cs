using UnityEngine;
using System.Collections;

public class CreditScroll : MonoBehaviour {

	public GameObject PilotEndObj;

    private Transform m_Transform;
    private void Start()
    {
        m_Transform = transform;
    }

    private Coroutine m_ScrollCoroutine;
    public void StartScroll()
    {
        m_Transform.localPosition = new Vector3(0, -2025, 0);
        m_ScrollCoroutine = StartCoroutine(ScrollCredit());
    }

    public void StopScroll()
    {
       if(m_ScrollCoroutine != null)
        {
            StopCoroutine(m_ScrollCoroutine);
        }
    }

    IEnumerator ScrollCredit()
    {
        while(true)
        {
            m_Transform.Translate(Vector3.up * 75 * Time.deltaTime);

            if(m_Transform.localPosition.y > 6735)
            {
                if(PilotEndObj)
                {
                    PilotEndObj.GetComponent<PilotEnd>().CreditSkipButtonPressed();
                    yield break;
                }
                m_Transform.localPosition = new Vector3(0, -2025, 0);
            }
            yield return null;
        }
    }
}
