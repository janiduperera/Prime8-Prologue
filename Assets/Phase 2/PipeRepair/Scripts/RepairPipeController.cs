using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RepairPipeController : MonoBehaviour {

	private Transform m_Grabbed;
	private float m_GrabbedOffset = 0.2f;

	public PipeRepairUIScript PRepairUIScript;
	public Text				TimerTxt;
    //private int m_TotalTime = 240; // 4 minitues
    //private int				m_GameTime = 240; // 4 minitues
    private int m_TotalTime = 150; // 2 min 30 seconds
    private int m_GameTime = 150; // 2 min 30 seconds

	int[][] myGridArray = new int[20][];

	public bool	PipeInstantiated = false;

	public List<GameObject> PipeObjectList;
	public GameObject		StartPipe;
	public GameObject		EndPipe;
	public Texture2D		HandTexture;

	public AudioSource		PipeAudioSource;
	public AudioClip 		mmmAudioClip;
	public AudioClip		ICanDoThisAudioClip;
	public AudioClip		JustABitMoreAudioClip;
	public AudioClip		NowThatsBetterAudioClip;
	public AudioClip		HeaveAudioClip;
	public AudioClip		BeingJackedOffAudioClip;
	public AudioClip		GameWinAudio;
	public AudioSource		CountDownAudio;
	public AudioSource		BackgroundLeakAudio;


    public Text InfoTxt;
	private static RepairPipeController m_Instance;

	private RepairPipeController(){}

	public static RepairPipeController GetInstance()
	{
		return m_Instance;
	}

	void Awake () 
	{
		m_Instance = this;

//		 m_XScreenWidth = Convert.ToInt32(Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x-Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x);
//		//m_YScreenWidth = Convert.ToInt32(Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y-Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y);
//
//		PipeHolder.transform.position = new Vector3 (Camera.main.gameObject.transform.position.x - (m_XScreenWidth / 2) + 2, PipeHolder.transform.position.y, PipeHolder.transform.position.z);
//
//		Camera.main.gameObject.transform.position = new Vector3(m_XScreenWidth/2, m_YScreenWidth/2, -10);

		for (int i = 0; i < myGridArray.Length; i++ )
		{
			myGridArray[i] = new int[20];
		}

		for ( int c = 0; c < 20 ; c++ )
		{
			for ( int r = 0; r < 20 ; r++ )
			{
				myGridArray[c][r] = -1;
			}
		}

		PipeObjectList = new List<GameObject>();

		PipeObjectList.Add(StartPipe);
		PipeObjectList.Add(EndPipe);

		SetGridPosition((int)StartPipe.transform.position.x, (int)StartPipe.transform.position.y);
		SetGridPosition((int)EndPipe.transform.position.x, (int)EndPipe.transform.position.y);

#if !(UNITY_IOS || UNITY_ANDROID)
        Cursor.lockState = CursorLockMode.None;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
		Cursor.SetCursor(HandTexture, Vector2.zero, CursorMode.Auto);
#endif

  //      if(SaveDataStatic.forTesting)
		//{
			//SaveDataStatic.StorySequence = "PRepEnd";
        //    LoadTheTownScene();
        //}

        //SaveDataStatic.StorySequence = "PRepEnd";
        //SceneManager.LoadSceneAsync("Town");
    }

	public void SetGridPosition (int _x, int _y) 
	{
		myGridArray[_x][_y] = 1;
	}

	public void ResetGridPosition(int _x, int _y) 
	{
		myGridArray[_x][_y] = -1;
	}

	public bool IsPositionAvailable(int _x, int _y)
	{
		try  {
		if(myGridArray[_x][_y] == -1)
			return true;
		else
			return false;
		}
		catch(Exception e) {
			return false;
		}
	}

	void Start()
	{
#if !(UNITY_IOS || UNITY_ANDROID)
        InfoTxt.text = "To edit, please right-click on pipes already used";
#else
        InfoTxt.text = "To edit, please tap on the pipes already used";
#endif
        StartCoroutine(GameTimer());
	}
    
	void Update()
	{
		if(PipeInstantiated)
		{
			UpdateHoldDrag ();
		}
		else
		{
            //#if !(UNITY_IOS || UNITY_ANDROID)
#if UNITY_EDITOR
            if (Input.GetMouseButton(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "EditPipe")
                    {
                        hit.collider.tag = "Pipe";
                        PipeInstantiated = true;
                        PRepairUIScript.PlaceButtonPanel.SetActive(true);
                        PRepairUIScript.PipePanel.SetActive(false);

                        ResetGridPosition((int)hit.collider.gameObject.transform.position.x, (int)hit.collider.gameObject.transform.position.y);

                        hit.collider.gameObject.GetComponent<Pipe>().IsObjectMoving = true;

                        ResetOpenAreas(hit.collider.gameObject);
                        PRepairUIScript.SetEditPipe(hit.collider.gameObject);
                    }
                }
            }
#else
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.tag == "EditPipe")
                        {
                            hit.collider.tag = "Pipe";
                            PipeInstantiated = true;
                            PRepairUIScript.PlaceButtonPanel.SetActive(true);
                            PRepairUIScript.PipePanel.SetActive(false);

                            ResetGridPosition((int)hit.collider.gameObject.transform.position.x, (int)hit.collider.gameObject.transform.position.y);

                            hit.collider.gameObject.GetComponent<Pipe>().IsObjectMoving = true;

                            ResetOpenAreas(hit.collider.gameObject);
                            PRepairUIScript.SetEditPipe(hit.collider.gameObject);
                        }
                    }
                }
            }
#endif
        }
    }

    private void ResetOpenAreas(GameObject _GO)
    {
        PipeObjectList.Remove(_GO);
        PipeOpenArea[] openAreaArray = _GO.GetComponentsInChildren<PipeOpenArea>();
        foreach (PipeOpenArea p in openAreaArray)
        {
            if (p.ConnectedOpenArea)
            {
                p.ConnectedOpenArea.GetComponent<PipeOpenArea>().ConnectedOpenArea = null;
                p.ConnectedOpenArea = null;
            }
        }
    }

    void  UpdateHoldDrag ()
	{
		if (Input.GetMouseButton(0)) 
		{
			if (m_Grabbed)
			{
				Drag();
			}
			else 
			{
				Grab();
			}
		}
		else if (Input.GetMouseButtonUp(0) && m_Grabbed)
		{
			m_Grabbed = null;
		}
	} 

	void  Grab ()
	{
		if (m_Grabbed) 
		{
			m_Grabbed = null;
		}
		else 
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))  
			{
				if(hit.collider.tag == "Pipe")
				{
					m_Grabbed = hit.collider.gameObject.transform;
					m_Grabbed.gameObject.GetComponent<Pipe>().MoveObjectInGrid();
				}
			}      
		}

	}

	void Drag ()
	{
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if ( Physics.Raycast(ray, out hit))
		{
			if(hit.collider.tag != "Pipe")
			{
				m_Grabbed.position = new Vector3(hit.point.x - m_GrabbedOffset, hit.point.y + m_GrabbedOffset, m_Grabbed.transform.position.z);
		
			}
		}
	}

	IEnumerator GameTimer()
	{
        //TimerTxt.text = "4.00";
        //TimerTxt.text = "00:04:00";
        TimerTxt.text = "00:02:30";

        string m_DisplayTime = "";
		int m_SecondsVal = 0;
		string m_SecondsDisplay = "";

		while(m_GameTime > 0)
		{
			yield return new WaitForSeconds(1);

			m_SecondsVal = (int)((m_GameTime-1)%60);

			if(m_SecondsVal < 10) m_SecondsDisplay = "0"+m_SecondsVal;
			else m_SecondsDisplay = m_SecondsVal+"";

			m_DisplayTime = ((int)((m_GameTime-1)/60))+"."+ m_SecondsDisplay;

            //TimerTxt.text = m_DisplayTime;
            TimerTxt.text = "00:0" + m_DisplayTime;
            //TimerTxt.text = ((m_GameTime-0.01f)%60f).ToString("0.00");
            m_GameTime = m_GameTime-1;

			if(CountDownAudio.isPlaying == false)
				CountDownAudio.Play();

			if(m_GameTime == 160)
			{
				PipeAudioSource.clip = JustABitMoreAudioClip;
				PipeAudioSource.Play();
			}
			else if(m_GameTime == 120)
			{
				PipeAudioSource.clip = ICanDoThisAudioClip;
				PipeAudioSource.Play();
			}
		}

		if(m_GameTime <= 0)
		{
            TimerTxt.text = "00:00:00";

            bool m_IsAllConnected = true;

			foreach(GameObject go in PipeObjectList)
			{
				foreach(Transform m_ListPipesOpenAreas in go.transform)
				{
					if(m_ListPipesOpenAreas.gameObject.GetComponent<PipeOpenArea>() == null) continue;

					if(m_ListPipesOpenAreas.gameObject.GetComponent<PipeOpenArea>().ConnectedOpenArea == null)
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

				SaveDataStatic.StorySequence = "PRepEnd";

            }
			else
			{
				Debug.Log("Lost");

				SaveDataStatic.StorySequence = "PRepStart";

			}



			LoadTheTownScene();
		}
	}

	public Text GameStateText;

	public void LoadTheTownScene()
	{
//		//TODO : Remove this
//		SaveDataStatic.StorySequence = "PRepEnd";
		StopAllCoroutines();
		StartCoroutine(DisplayGameStateTextBeforeExit());
	}

	IEnumerator DisplayGameStateTextBeforeExit()
	{
		BackgroundLeakAudio.Stop();

		if(SaveDataStatic.StorySequence == "PRepEnd")
		{
			GameStateText.text = "Water leak fixed...";

            SetConsumeGameTime();

            PipeAudioSource.clip = NowThatsBetterAudioClip;
			PipeAudioSource.Play();

			yield return new WaitForSeconds(NowThatsBetterAudioClip.length);

			PipeAudioSource.clip = GameWinAudio;
			PipeAudioSource.Play();

			yield return new WaitForSeconds(GameWinAudio.length);
		}
		else
		{
			GameStateText.text = "Water is still leaking...";

			PipeAudioSource.clip = BeingJackedOffAudioClip;
			PipeAudioSource.Play();

			yield return new WaitForSeconds(BeingJackedOffAudioClip.length);

		}

        SceneManager.LoadSceneAsync("Town");	
	}

    void SetConsumeGameTime()
    {
        float m_SecondsVal = (int)(((m_TotalTime-m_GameTime) - 1) % 60);

        string m_SecondsDisplay;
        if (m_SecondsVal < 10) m_SecondsDisplay = "0" + m_SecondsVal;
        else m_SecondsDisplay = m_SecondsVal + "";

        string m_DisplayTime = ((int)(((m_TotalTime - m_GameTime) - 1) / 60)) + ":" + m_SecondsDisplay;

       SaveDataStatic.PipeRepairFinishTime = "00:0" + m_DisplayTime;
    }
}
