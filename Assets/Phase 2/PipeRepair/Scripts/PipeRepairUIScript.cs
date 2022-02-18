using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PipeRepairUIScript : MonoBehaviour {

	public GameObject PipePrefab;
	public GameObject PipeOpenAreaPrefab;

	private GameObject	m_PipeGameObj;
	private Pipe		m_PipeObj;

	public GameObject	PipePanel;
	public GameObject	PlaceButtonPanel;

	public GameObject  	PipeHolder;

    private float m_MinScreenX, m_MaxScreenX, m_MinScreenY, m_MaxScreenY;

	void Start()
	{
        //m_MaxScreenX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        //m_MinScreenX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;

        //m_MaxScreenY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        //m_MinScreenY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        float m_XScreenWidth = System.Convert.ToInt32(Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x-Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x);

		PipeHolder.transform.position = new Vector3 (Camera.main.gameObject.transform.position.x - (m_XScreenWidth / 2) + 2, PipeHolder.transform.position.y, PipeHolder.transform.position.z);

		int m_RandomNo = Random.Range(0, 10000);


        if (m_RandomNo < 3000)
		{
			InitiatePipeObject(1, false, new Vector3(9, 3, 0), "Untagged");
			PlaceButtonClicked();
			InitiatePipeObject(3, false, new Vector3(7, 6, 0), "Untagged");
			PlaceButtonClicked();
			InitiatePipeObject(1, false, new Vector3(3, 7, 0), "Untagged");
			PlaceButtonClicked();
            InitiatePipeObject(3, false, new Vector3(7, 8, 0), "Untagged");
            PlaceButtonClicked();
            InitiatePipeObject(2, false, new Vector3(4, 4, 0), "Untagged");
            PlaceButtonClicked();
        }
		else if(m_RandomNo < 6000)
		{
			InitiatePipeObject(2, false, new Vector3(3, 9, 0), "Untagged");
			m_PipeGameObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
			PlaceButtonClicked();

			InitiatePipeObject(2, false, new Vector3(5, 8, 0), "Untagged");
			m_PipeGameObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
			PlaceButtonClicked();

            InitiatePipeObject(1, false, new Vector3(7, 4, 0), "Untagged");
            //m_PipeGameObj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            PlaceButtonClicked();

            InitiatePipeObject(3, false, new Vector3(9, 8, 0), "Untagged");
            PlaceButtonClicked();
        }
		else
		{
			InitiatePipeObject(3, false, new Vector3(2, 7, 0), "Untagged");
			PlaceButtonClicked();
			InitiatePipeObject(3, false, new Vector3(5, 6, 0), "Untagged");
			PlaceButtonClicked();
            InitiatePipeObject(3, false, new Vector3(8, 7, 0), "Untagged");
            PlaceButtonClicked();
        }

		RepairPipeController.GetInstance().PipeAudioSource.clip = RepairPipeController.GetInstance().mmmAudioClip;
		RepairPipeController.GetInstance().PipeAudioSource.Play();
	}

	private void InitiatePipeObject(int index, bool _shouldMove, Vector3 _pos, string _tag = "Pipe")
	{
		m_PipeGameObj = (GameObject)Instantiate (PipePrefab);//, _pos, Quaternion.identity);
		m_PipeGameObj.tag = _tag;

		if (_shouldMove) {
			m_PipeGameObj.transform.parent = PipeHolder.transform;
			m_PipeGameObj.transform.localPosition = new Vector3 (0, -0.1f, -1.5f);
			m_PipeGameObj.transform.parent = null;
			m_PipeGameObj.transform.position = new Vector3 ((int)m_PipeGameObj.transform.position.x, (int)m_PipeGameObj.transform.position.y, -1f);
		} else {
			m_PipeGameObj.transform.position = _pos;
		}
		m_PipeObj = m_PipeGameObj.GetComponent<Pipe>();
		m_PipeObj.SetTexture(index, _shouldMove);

		GameObject m_TempOpenAreaObj;

		if(index == 0)
		{
			for(int i = -1; i < 2; i+=2)
			{
				m_TempOpenAreaObj = (GameObject)Instantiate(PipeOpenAreaPrefab);
				m_TempOpenAreaObj.name = "OpenArea";
				m_TempOpenAreaObj.transform.parent = m_PipeGameObj.transform;
				m_TempOpenAreaObj.transform.localPosition = new Vector3(-0.5f*i, 0, 0);
			}
		}
		else if(index == 1)
		{
			for(int i = -1; i < 2; i+=2)
			{
				m_TempOpenAreaObj = (GameObject)Instantiate(PipeOpenAreaPrefab);
				m_TempOpenAreaObj.name = "OpenArea";
				m_TempOpenAreaObj.transform.parent = m_PipeGameObj.transform;
				m_TempOpenAreaObj.transform.localPosition = new Vector3(-0.5f*i, 0, 0);
			}

			m_TempOpenAreaObj = (GameObject)Instantiate(PipeOpenAreaPrefab);
			m_TempOpenAreaObj.name = "OpenArea";
			m_TempOpenAreaObj.transform.parent = m_PipeGameObj.transform;
			m_TempOpenAreaObj.transform.localPosition = new Vector3(0, 0.5f, 0);
		}
		else if(index == 2)
		{
			m_TempOpenAreaObj = (GameObject)Instantiate(PipeOpenAreaPrefab);
			m_TempOpenAreaObj.name = "OpenArea";
			m_TempOpenAreaObj.transform.parent = m_PipeGameObj.transform;
			m_TempOpenAreaObj.transform.localPosition = new Vector3(0.5f, 0, 0);

			m_TempOpenAreaObj = (GameObject)Instantiate(PipeOpenAreaPrefab);
			m_TempOpenAreaObj.name = "OpenArea";
			m_TempOpenAreaObj.transform.parent = m_PipeGameObj.transform;
			m_TempOpenAreaObj.transform.localPosition = new Vector3(0, -0.5f, 0);
		}
		else
		{
			for(int i = -1; i < 2; i+=2)
			{
				m_TempOpenAreaObj = (GameObject)Instantiate(PipeOpenAreaPrefab);
				m_TempOpenAreaObj.name = "OpenArea";
				m_TempOpenAreaObj.transform.parent = m_PipeGameObj.transform;
				m_TempOpenAreaObj.transform.localPosition = new Vector3(-0.5f*i, 0, 0);
			}

			for(int i = -1; i < 2; i+=2)
			{
				m_TempOpenAreaObj = (GameObject)Instantiate(PipeOpenAreaPrefab);
				m_TempOpenAreaObj.name = "OpenArea";
				m_TempOpenAreaObj.transform.parent = m_PipeGameObj.transform;
				m_TempOpenAreaObj.transform.localPosition = new Vector3(0, -0.5f*i, 0);
			}
		}
	}

	public void OnPipeButtonsClicked(int index)
	{
		if(RepairPipeController.GetInstance().PipeInstantiated) return;

		RepairPipeController.GetInstance().PipeInstantiated = true;

		InitiatePipeObject(index, true, new Vector3(1, 4, -1));

		PlaceButtonPanel.SetActive(true);
		PipePanel.SetActive(false);

		m_PipeObj.IsObjectMoving = true;
	}

	public void PlaceButtonClicked()
	{
        if (m_PipeObj.transform.localPosition.y < 3)
            return;

        if (!RepairPipeController.GetInstance().IsPositionAvailable((int)m_PipeGameObj.transform.position.x, (int)m_PipeGameObj.transform.position.y))
            return;

        if (m_PipeGameObj.transform.position.x < -1 || m_PipeGameObj.transform.position.x > 13 || m_PipeGameObj.transform.position.y < 3 || m_PipeGameObj.transform.position.y > 9)
            return;

        RepairPipeController.GetInstance().SetGridPosition((int)m_PipeGameObj.transform.position.x, (int)m_PipeGameObj.transform.position.y);

		RepairPipeController.GetInstance().PipeInstantiated = false;
        if(m_PipeGameObj.tag == "Pipe")
		    m_PipeGameObj.tag = "EditPipe";
		m_PipeGameObj.transform.position = new Vector3(m_PipeGameObj.transform.position.x, m_PipeGameObj.transform.position.y, 0);
		m_PipeObj.IsObjectMoving = false;
		m_PipeObj.PipeBack.SetActive(false);
		PlaceButtonPanel.SetActive(false);
		PipePanel.SetActive(true);



        for (int i = 0; i < m_PipeGameObj.transform.childCount; i++)
        {
            if (m_PipeGameObj.transform.GetChild(i).gameObject.GetComponent<PipeOpenArea>() == null) continue;

            foreach (GameObject go in RepairPipeController.GetInstance().PipeObjectList)
            {
                for(int r = 0; r < go.transform.childCount; r++)
                {
                    if (go.transform.GetChild(r).gameObject.GetComponent<PipeOpenArea>() == null) continue;

                    if(Vector2.Distance(go.transform.GetChild(r).position, m_PipeGameObj.transform.GetChild(i).position) < 0.2f)
                    {
                        go.transform.GetChild(r).gameObject.GetComponent<PipeOpenArea>().ConnectedOpenArea = m_PipeGameObj.transform.GetChild(i).gameObject;
                        m_PipeGameObj.transform.GetChild(i).gameObject.GetComponent<PipeOpenArea>().ConnectedOpenArea = go.transform.GetChild(r).gameObject;

                    }
                }
            }
        }
		RepairPipeController.GetInstance().PipeObjectList.Add(m_PipeGameObj);

		m_PipeObj = null;
		m_PipeGameObj = null;

		bool m_IsAllConnected = true;



        foreach (GameObject go in RepairPipeController.GetInstance().PipeObjectList)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                if (go.transform.GetChild(i).gameObject.GetComponent<PipeOpenArea>() == null) continue;

                if (go.transform.GetChild(i).gameObject.GetComponent<PipeOpenArea>().ConnectedOpenArea == null)
                {
                    m_IsAllConnected = false;
                    break;
                }
            }
        }

		if(m_IsAllConnected)
		{
			// Call Game Over Win
			Debug.Log("Won");

			StopAllCoroutines();

			SaveDataStatic.StorySequence = "PRepEnd";

            RepairPipeController.GetInstance().LoadTheTownScene();
		}
	}

    public void DeleteButtonClicked()
	{
        RepairPipeController.GetInstance().PipeInstantiated = false;
		RepairPipeController.GetInstance().PipeObjectList.Remove(m_PipeGameObj);
        if (RepairPipeController.GetInstance().IsPositionAvailable((int)m_PipeGameObj.transform.position.x, (int)m_PipeGameObj.transform.position.y))
            RepairPipeController.GetInstance().ResetGridPosition((int)m_PipeGameObj.transform.position.x, (int)m_PipeGameObj.transform.position.y);
		Destroy(m_PipeGameObj);
		m_PipeGameObj = null;
		m_PipeObj = null;
		PlaceButtonPanel.SetActive(false);
		PipePanel.SetActive(true);
        bool m_IsAllConnected = true;
        foreach (GameObject go in RepairPipeController.GetInstance().PipeObjectList)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                if (go.transform.GetChild(i).gameObject.GetComponent<PipeOpenArea>() == null) continue;

                if (go.transform.GetChild(i).gameObject.GetComponent<PipeOpenArea>().ConnectedOpenArea == null)
                {
                    m_IsAllConnected = false;
                    break;
                }
            }
        }

        if (m_IsAllConnected)
        {
            // Call Game Over Win
            Debug.Log("Won");

            StopAllCoroutines();

            SaveDataStatic.StorySequence = "PRepEnd";

            RepairPipeController.GetInstance().LoadTheTownScene();
        }
    }

	public void RotateButtonClicked()
	{
		m_PipeGameObj.transform.Rotate(0, 0, 90);
	}

	public void SetEditPipe(GameObject _pipe)
	{
		m_PipeGameObj = _pipe;
		m_PipeObj = m_PipeGameObj.GetComponent<Pipe>();

		RepairPipeController.GetInstance().PipeAudioSource.clip = RepairPipeController.GetInstance().HeaveAudioClip;
		RepairPipeController.GetInstance().PipeAudioSource.Play();
	}
}
