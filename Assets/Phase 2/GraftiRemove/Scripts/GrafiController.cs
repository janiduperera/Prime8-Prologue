using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GrafiController : MonoBehaviour {

    public int maxSize;
    public int currentSize;
    public int xBound;
    public int yBound;
    public GameObject GraftiPrefab;
    public GameObject CurrentGrafti;
    public GameObject TailPrefab;
    public Caterpillar CaterpillarHead;
    public Caterpillar CaterpillerTail;
    public int NorthEastSouthWest;
    public Vector3 NextPos;
    public float InvokeRepeatTime;

   // public Text ScoreTxt;
   // private int m_Score;

    private float m_CamMinX;
    private float m_CamMaxX;
    private float m_CamMinY;
    private float m_CamMaxY;

    private AudioSource m_GrafContAudioSource;
    public AudioClip ThereYouGoMrPostManAudio;
    public AudioClip OhDearWhatDoweHaveHearAudio;
    public AudioClip GameOverAudio;
    public AudioClip[] GamePlayAudios;
    public AudioSource CaterpillarFootStepAudioSource;

    private static GrafiController m_Instance;

    public void SetMaxSize()
    {
        maxSize++;

       // InvokeRepeatTime = InvokeRepeatTime - (0.8f / 84f);
        InvokeRepeatTime = InvokeRepeatTime - (0.8f / 42f);
       // Debug.Log("Invoke time :  " + InvokeRepeatTime);
        if (InvokeRepeatTime < 0.2f)
        {
            InvokeRepeatTime = 0.2f;
        }
    }

    private GrafiController() { }

    public static GrafiController GetInstance()
    {
        return m_Instance;
    }

    void Awake()
    {
        m_Instance = this;

        m_GrafContAudioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start() {

        m_StartGame = false;

#if !(UNITY_IOS || UNITY_ANDROID)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ScreenArrows.SetActive(false);
#else
        // ScreenArrows.SetActive(true);
#endif

        m_CamMinX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        m_CamMaxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        m_CamMinY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        m_CamMaxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;


        ActivateStartMenuPanel("Start");

        dragDistance = Screen.height * 10 / 100; //dragDistance is 10% height of the screen
    }

    private bool m_StartGame = false;
    IEnumerator PlayWhatDoWeHaveHereAudio()
    {
        m_GrafContAudioSource.clip = OhDearWhatDoweHaveHearAudio;
        m_GrafContAudioSource.Play();

        yield return new WaitForSeconds(OhDearWhatDoweHaveHearAudio.length);

 //       InvokeRepeating("InvokeTimer", 0, InvokeRepeatTime);

        m_GrafContAudioSource.clip = GamePlayAudios[0];
        m_GrafContAudioSource.Play();

        InvokeRepeating("PlayGamePlaySound", 10, 10);

     //   m_StartTime = System.DateTime.Now;

        InvokeRepeatTime = 1;
        m_StartGame = true;
        m_GameTimerCoroutine = StartCoroutine(GameTimer());


    }

    //private System.DateTime m_StartTime;
    //private System.DateTime m_EndTime; 

    void PlayGamePlaySound()
    {
        int m_RandomNo = Random.Range(0, GamePlayAudios.Length);
        m_GrafContAudioSource.clip = GamePlayAudios[m_RandomNo];
        m_GrafContAudioSource.Play();
    }

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    // Update is called once per frame
    void Update() {

        if (!m_StartGame) return;
#if UNITY_EDITOR
        Swipe();
#else
        TouchSwipe();
#endif

        if (NorthEastSouthWest != 2 && m_Direction == 0) // North
        {
            NorthEastSouthWest = 0;
        }
        if (NorthEastSouthWest != 0 && m_Direction == 1) // South
        {
            NorthEastSouthWest = 2;
        }
        if (NorthEastSouthWest != 1 && m_Direction == 2) // Left 
        {
            NorthEastSouthWest = 3;
        }
        if (NorthEastSouthWest != 3 && m_Direction == 3) // Right
        {
            NorthEastSouthWest = 1;
        }

        //#if !(UNITY_IOS || UNITY_ANDROID)

        //if (NorthEastSouthWest != 2 && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        //{
        //    NorthEastSouthWest = 0;
        //}
        //if (NorthEastSouthWest != 0 && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)))
        //{
        //    NorthEastSouthWest = 2;
        //}
        //if (NorthEastSouthWest != 1 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)))
        //{
        //    NorthEastSouthWest = 3;
        //}
        //if (NorthEastSouthWest != 3 && (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)))
        //{
        //    NorthEastSouthWest = 1;
        //}
        //#else
        //if (NorthEastSouthWest != 2 && m_Direction == 0) // North
        //{
        //    NorthEastSouthWest = 0;
        //}
        //if (NorthEastSouthWest != 0 && m_Direction == 1) // South
        //{
        //    NorthEastSouthWest = 2;
        //}
        //if (NorthEastSouthWest != 1 && m_Direction == 2) // Left 
        //{
        //    NorthEastSouthWest = 3;
        //}
        //if (NorthEastSouthWest != 3 && m_Direction == 3) // Right
        //{
        //    NorthEastSouthWest = 1;
        //}


        //#endif

        m_TempTime += Time.deltaTime;
        if(m_TempTime > InvokeRepeatTime)
        {
            m_TempTime = 0;
            Movement();
            if (CaterpillarHead.transform.position.x > m_CamMaxX || CaterpillarHead.transform.position.x < m_CamMinX)
            {
                Wrap();
            }
            else if (CaterpillarHead.transform.position.y > m_CamMaxY || CaterpillarHead.transform.position.y < m_CamMinY)
            {
                Wrap();
            }
            if (currentSize >= maxSize)
            {
                TailFunction();
            }
            else
            {
                currentSize++;
            }
        }

    }

    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    void Swipe()
    {

        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

            //normalize the 2d vector
            currentSwipe.Normalize();

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("up swipe");
                m_Direction = 0;
            }
            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("down swipe");
                m_Direction = 1;
            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("left swipe");
                m_Direction = 2;
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("right swipe");
                m_Direction = 3;
            }
        }
    }

    void TouchSwipe()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            Debug.Log("Right Swipe");
                            m_Direction = 3;
                        }
                        else
                        {   //Left swipe
                            Debug.Log("Left Swipe");
                            m_Direction = 2;
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            //Debug.Log("Up Swipe");
                            m_Direction = 0;
                        }
                        else
                        {   //Down swipe
                            //Debug.Log("Down Swipe");
                            m_Direction = 1;
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
    }


    float m_TempTime = 0;

    public GameObject ScreenArrows;
    int m_Direction = 1; // 0 = North, 1 south, 2 left, 3 right
    public void KeyBoardArrowPress(int _direction)
    {
        m_Direction = _direction;
    }

 //   void InvokeTimer()
	//{
	//	Movement();
	//	StartCoroutine(CheckVisibility());
	//	if(currentSize >= maxSize)
	//	{
	//		TailFunction();
	//	}
	//	else
	//	{
	//		currentSize++;
	//	}
	//}

	void Movement()
	{
		GameObject temp;
		NextPos = CaterpillarHead.transform.position;

		switch(NorthEastSouthWest)
		{
		case 0: NextPos = new Vector3(NextPos.x, NextPos.y+1, -1);
			break;
		case 1:NextPos = new Vector3(NextPos.x+1, NextPos.y, -1);
			break;
		case 2:NextPos = new Vector3(NextPos.x, NextPos.y-1, -1);
			break;
		case 3:NextPos = new Vector3(NextPos.x-1, NextPos.y, -1);
			break;
		}

		CaterpillarHead.SetClothTexture();
		temp = (GameObject)Instantiate(TailPrefab, NextPos, transform.rotation);
		CaterpillarHead.SetNext(temp.GetComponent<Caterpillar>());
		CaterpillarHead = temp.GetComponent<Caterpillar>();
		CaterpillarHead.SetCaterpillarTexture();

        CaterpillarFootStepAudioSource.Play();

        switch (NorthEastSouthWest)
		{
		case 0: temp.transform.rotation = Quaternion.Euler(Vector3.zero);
			break;
		case 1:temp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
			break;
		case 2:temp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
			break;
		case 3:temp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
			break;
		}
		return;
	}

	void TailFunction()
	{
		Caterpillar tempCaterpillar = CaterpillerTail;
		CaterpillerTail = CaterpillerTail.GetNext();
		tempCaterpillar.RemoveTail();
	}

	void Wrap()
	{
		if(NorthEastSouthWest == 0)
		{
			CaterpillarHead.gameObject.transform.position = 
				new Vector2(CaterpillarHead.transform.position.x, -(CaterpillarHead.transform.position.y/*-1*/));
		}
		else if(NorthEastSouthWest == 1)
		{
			CaterpillarHead.gameObject.transform.position = 
				new Vector2(-(CaterpillarHead.transform.position.x/*-1*/), CaterpillarHead.transform.position.y);
		}
		else if(NorthEastSouthWest == 2)
		{
			CaterpillarHead.gameObject.transform.position = 
				new Vector2(CaterpillarHead.transform.position.x, -(CaterpillarHead.transform.position.y/*+1*/));
		}
		else if(NorthEastSouthWest == 3)
		{
			CaterpillarHead.gameObject.transform.position = 
				new Vector2(-(CaterpillarHead.transform.position.x/*+1*/), CaterpillarHead.transform.position.y);
		}
	}

	//IEnumerator CheckVisibility()
	//{
	//	yield return new WaitForEndOfFrame();

	//	if(CaterpillarHead.transform.position.x > m_CamMaxX || CaterpillarHead.transform.position.x < m_CamMinX)
	//	{
	//		Wrap();
	//	}
	//	else if(CaterpillarHead.transform.position.y > m_CamMaxY || CaterpillarHead.transform.position.y < m_CamMinY)
	//	{
	//		Wrap();
	//	}
	//}

#region UI Events
	public GameObject StartMenuPanel;
    public void ActivateStartMenuPanel(string _what)
    {
        StartMenuPanel.SetActive(true);
        if (_what == "Start")
        {
            StartMenuPanel.transform.GetChild(0).Find("StartPanel").gameObject.SetActive(true);
            StartMenuPanel.transform.GetChild(0).Find("LostTxt").gameObject.SetActive(false);
            StartMenuPanel.transform.GetChild(0).Find("WinTxt").gameObject.SetActive(false);
        }
        else if(_what == "Lost")
        {
            StartMenuPanel.transform.GetChild(0).Find("StartPanel").gameObject.SetActive(false);
            StartMenuPanel.transform.GetChild(0).Find("LostTxt").gameObject.SetActive(true);
            StartMenuPanel.transform.GetChild(0).Find("WinTxt").gameObject.SetActive(false);
        }
        else
        {
            StartMenuPanel.transform.GetChild(0).Find("StartPanel").gameObject.SetActive(false);
            StartMenuPanel.transform.GetChild(0).Find("LostTxt").gameObject.SetActive(false);
            StartMenuPanel.transform.GetChild(0).Find("WinTxt").gameObject.SetActive(true);
        }
    }
    public void OnPlayButtonClicked()
	{
        StartMenuPanel.SetActive(false);
        StartCoroutine(PlayWhatDoWeHaveHereAudio());
    }

    public Text TimerTxt;
    private int m_GameTime = 240;
    public AudioSource CountDownAudio;
    private Coroutine m_GameTimerCoroutine;
    IEnumerator GameTimer()
    {
        //TimerTxt.text = "4.00";
        TimerTxt.text = "00:04:00";

        string m_DisplayTime = "";
        int m_SecondsVal = 0;
        string m_SecondsDisplay = "";

        while (m_GameTime > 0)
        {
            yield return new WaitForSeconds(1);

            m_SecondsVal = (int)((m_GameTime - 1) % 60);

            if (m_SecondsVal < 10) m_SecondsDisplay = "0" + m_SecondsVal;
            else m_SecondsDisplay = m_SecondsVal + "";

            m_DisplayTime = ((int)((m_GameTime - 1) / 60)) + "." + m_SecondsDisplay;

            TimerTxt.text = "00:0" + m_DisplayTime;

            m_GameTime = m_GameTime - 1;

            if (CountDownAudio.isPlaying == false)
                CountDownAudio.Play();

        }

        if (m_GameTime <= 0)
        {
            TimerTxt.text = "00:00:00";


            ShowGameFinishPanel();
        }
    }

    public void ShowGameFinishPanel()
	{
        GameObject[] leftObjs = GameObject.FindGameObjectsWithTag("Grafti");

		//Debug.Log("Left Grafiti count : " + leftObjs.Length);


		if(leftObjs == null || leftObjs.Length == 0)
		{
			Debug.Log("Grafiti Won");
			// Win
			GameWon();

		}
		else
		{
			Debug.Log("Grafiti Lost");
			// Lost

			StartCoroutine(GameOverAudioPlay());

		}
	}

	public IEnumerator GameOverAudioPlay()
	{
        if (m_GameTimerCoroutine != null)
        {
            StopCoroutine(m_GameTimerCoroutine);
        }
        CaterpillarFootStepAudioSource.Stop();
        m_StartGame = false;
        ActivateStartMenuPanel("Lost");
        CancelInvoke();
		m_GrafContAudioSource.clip = GameOverAudio;
		m_GrafContAudioSource.Play();

		yield return new WaitForSeconds(GameOverAudio.length);

		SaveDataStatic.StorySequence = "GrafitiBegin";

        SaveDataStatic.WasPostBoxCleaned = false;
        //TODO : Remove this
        //SaveDataStatic.StorySequence = "GrafitiEnd";
        SceneManager.LoadSceneAsync("Town");
       //SceneManager.LoadSceneAsync("GraftiRemove");

    }

	public void GameWon()
	{
        if(m_GameTimerCoroutine != null)
        {
            StopCoroutine(m_GameTimerCoroutine);
        }
        CaterpillarFootStepAudioSource.Stop();
        m_StartGame = false;
        //m_EndTime = System.DateTime.Now;
        //System.TimeSpan m_TimeDiff = m_EndTime - m_StartTime;
        //SaveDataStatic.GraffityFinishTime = m_TimeDiff.ToString(@"hh\:mm\:ss");
        SetConsumeGameTime();
        CancelInvoke();
        ActivateStartMenuPanel("Win");

        StartCoroutine(ThereYouGoMrPostManAudioPlay());
	}

	IEnumerator ThereYouGoMrPostManAudioPlay()
	{
		m_GrafContAudioSource.Stop();

		m_GrafContAudioSource.clip = ThereYouGoMrPostManAudio;
		m_GrafContAudioSource.Play();

		yield return new WaitForSeconds(ThereYouGoMrPostManAudio.length);

		SaveDataStatic.StorySequence = "GrafitiEnd";

        SaveDataStatic.WasPostBoxCleaned = true;

        SceneManager.LoadSceneAsync("Town");
        //SceneManager.LoadSceneAsync("GraftiRemove");
    }

    void SetConsumeGameTime()
    {
        float m_SecondsVal = (int)(((240 - m_GameTime) - 1) % 60);

        string m_SecondsDisplay;
        if (m_SecondsVal < 10) m_SecondsDisplay = "0" + m_SecondsVal;
        else m_SecondsDisplay = m_SecondsVal + "";

        string m_DisplayTime = ((int)(((240 - m_GameTime) - 1) / 60)) + ":" + m_SecondsDisplay;

        SaveDataStatic.GraffityFinishTime = "00:0" + m_DisplayTime;
    }
    #endregion UI Events
}
