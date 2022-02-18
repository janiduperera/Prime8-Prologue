using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using Narrate;
using UnityEngine.Video;
using System;

public class KitchenGamePlay : MonoBehaviour {

    public GameObject medalImage;
    public GameObject medal;
	public GameObject RecycleMedal;
	public GameObject door2;
	public GameObject radio;
	public GameObject drawerup;
	public GameObject fridge;
	public GameObject drawer;
	public GameObject lights;
	public GameObject tap;
	public GameObject cursor2;
	public GameObject tableGlow;
	public GameObject cubordGlow;
	public GameObject serialBox;
	public GameObject binGlow;
    public GameObject entryFormGlow;
	public GameObject CupbordUpDrowerGlow;
	public GameObject PandaForm;
	public GameObject panda;
	public GameObject display;
	public GameObject pointerDot;
	public GameObject inventory;
	public GameObject inventoryPanel;
	public GameObject frigdeGlow;
	public GameObject StampDrawer;
	public GameObject StampDrawerGlow;
    public GameObject cursor4;
    public GameObject Drawers3Glow;
    public GameObject bateryGlow;
    public GameObject stampGlow;
    public GameObject stamp;
    public GameObject quitMenu;
    public GameObject answers;
    public GameObject[] ticks;
    public GameObject singingBirds;
	public GameObject Door3Glow;

	public AudioClip SwitchSound;
    
	public AudioClip 	ItemPickAudio;
    public AudioClip[] bobSounds;

    //public GameObject fridgeGlow;
    public GameObject[] statsQues;

	public Camera formCam;
    public inventory2 inventory2;
    public InventryOnclick InventryOnclick;

#if UNITY_WEBGL
	//public WebGLMovieTexture[] moviez;
	MonoBehaviour [] behav;
	private WebGLMovieTexture movieTexture;
	
#else
    //public MovieTexture[] moviez;
    public VideoPlayer videoPlayer;
#endif
    //    public MovieManager2 movieManager;
    private bool IsBreakfastMoviePlaying = false;

    public CanvasGroup JoyStick;

    MonoBehaviour[] behav;
    public AudioSource VideoAudioSource;

    public AudioClip[] audioClip;
	public AudioClip   NowToMakeSureIHaveClosedDoorAudio;
	public AudioClip[] NewAudioClips;

	public Material []lightMat;
	public Text subtitleText;
    public SubtitleManager subtitleManager;

    public GameObject cursor;

	public Sprite magnifierTexture;
	public Sprite handTexture;
    public Sprite knob;
    public Texture texform;
	public Texture finaltexForm;
    public Texture2D HandTex;

	public Image[] buttons;

    private bool findStamp = false;
	private bool playedSerial = false;
	private bool gotSerialBox = false;
	private bool binExplored = false;
	private bool isTapOpen = false;
	private bool iscupdownOpen = false;
	private bool isRadiOn = false;
	private bool isDrawerUpOpen = false;
	private bool isFridgeOpen = false;
	private bool isDrawerOpen = false;
	private bool lightsOn = false;
    //private bool toggleCam = false;
    public bool FormCamFocus
    {
        get { return formCamFocused; }
    }
    private bool formCamFocused = false;
	public bool explorebatry = false;
	public bool exploreMagnifire = false;
	public bool exploreSalard = false;
	public bool startGlow = false;
	public bool binDestroy = false;
	public bool keyDestroy = false;
	public bool canGetStamp = false;
	public bool stampDrawerOprn =false;
	public bool stampOk = false;
	public bool exploreRedbin = false;
	public bool exploreyellowbin = false;
	public bool exploregreenbin = false;
	public bool FormOk = false;
    public bool medalFinish =false;
   // public bool cursoractive = true;
    public bool putcans = false;
    public bool setStamp = false;
  //  public bool getSalard = false;

	public  bool putBagNbattery= false;
	public  bool unlockedDoor = false;
	public  bool isDoor2Open = false;
	public  bool putbin = false;
	public  bool putsalardTobin = false;
	public Sprite[] buttonImages;

    public GameObject quizesGO;
    private StatsQuizes quizMB;

	public  bool HasQuizStarted = false;

	public GameObject LoadingTxt;

	private string[] m_HintList = new string[9];
	public AudioClip[] HintAudios;

	private Vector3 midPos = Vector3.zero;

	public GameObject InstructionUI;




    private void OnApplicationPause(bool pause)
    {
        //if(pause)
        //{
        //    SaveGameOnExit();
        //}
    }

 //   public void SaveGameOnExit()
	//{
	//	string m_MedalString = "";
	//	foreach (string st in SaveDataStatic.AwardedMedalList) {
	//		m_MedalString += st + "|";
	//	}
	//	m_MedalString = m_MedalString.Remove (m_MedalString.Length - 1);
	//	PlayerPrefs.SetString ("Medal", m_MedalString);
	//	PlayerPrefs.SetString ("Scene", "Kitchen");
	//}

	// Use this for initialization
	void Start () {
        Time.timeScale = 1; // Because bringing Save message, could pause the game
        m_HintList [0] = "Have you looked carefully inside the fridge?";
		m_HintList [1] = "Is there anything in the kitchen bin that should not be there?";
		m_HintList [2] = "Have you searched in the garden?";
		m_HintList [3] = "Have a look in the hallway";
		m_HintList [4] = "You need to place the items you have collected into the correct bins";
		m_HintList [5] = "Are there doors or cupboards open…or lights switched on?";
		m_HintList [6] = "The front door is located in the hallway";
		m_HintList [7] = "If an item glows or flashes, or has an arrow pointing to it – you should interact with it";
		m_HintList [8] = "Remember you need to investigate the location indicated by the flashing red dot on the map";

        subtitleManager.OnSubtitileDisplayBeginEvent += SubtitleManager_OnSubtitileDisplayBeginEvent;
        subtitleManager.OnSubtitileDisplayEndEvent += SubtitleManager_OnSubtitileDisplayEndEvent;

        videoPlayer.loopPointReached += EndReachedOnVideo;
        behav = panda.GetComponents<MonoBehaviour>();

        quizMB = quizesGO.GetComponent<StatsQuizes>();
		formCam.depth = -1.0F;
		tap.SetActive (false);
		midPos = new Vector3 (Screen.width / 2f, Screen.height / 2f, 0f);
        //		movieManager = new MovieManager2(panda, display, pointerDot, inventory, inventoryPanel);

        Soundmanager2.instance.playSingle(audioClip[22]);
        StartCoroutine(ShowSubtitle("Hmm...breakfast."));
#if !(UNITY_IOS || UNITY_ANDROID)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
#endif

        StartCoroutine(ds());

	}

    private void SubtitleManager_OnSubtitileDisplayBeginEvent()
    {
        JoyStickSettings(false);
    }

    private void SubtitleManager_OnSubtitileDisplayEndEvent()
    {
        JoyStickSettings(true);
    }

    public void JoyStickSettings(bool _activate)
    {
#if UNITY_IOS || UNITY_ANDROID
        if (_activate)
        {
            JoyStick.alpha = 1;
            JoyStick.blocksRaycasts = true;
            JoyStick.interactable = true;
        }
        else
        {
            JoyStick.alpha = 0;
            JoyStick.blocksRaycasts = false;
            JoyStick.interactable = false;
        }
#endif
    }


    string m_ItemSelectStatus = null;

	string ReturnTheHint()
	{
		if (System.String.IsNullOrEmpty (m_ItemSelectStatus)) {
			return m_HintList [7];
		}

		if (m_ItemSelectStatus == "Friedge") {
			return m_HintList [0];
		}

		if (m_ItemSelectStatus == "Bin") {
			return m_HintList [1];
		}

		if (m_ItemSelectStatus == "Battery") {
			return m_HintList [2];
		}

		if (m_ItemSelectStatus == "Stamp") {
			return m_HintList [3];
		}

		if (m_ItemSelectStatus == "GardenBin") {
			return m_HintList [4];
		}

		if (m_ItemSelectStatus == "DoorsOpen") {
			return m_HintList [5];
		}

		if (m_ItemSelectStatus == "DoorsClosed") {
			return m_HintList [6];
		}

		return "";
	}

	bool m_IsHintDisplaying = false;
	public GameObject	HintTextObj;
	private Text		m_HintText;
	IEnumerator ShowHint()
	{
		m_IsHintDisplaying = true;

		HintTextObj.SetActive (true);

		if (m_HintText == null) {
			m_HintText = HintTextObj.GetComponent<Text> ();
		}
		m_HintText.text = ReturnTheHint ();

		switch (m_ItemSelectStatus) {
		case "Friedge":Soundmanager2.instance.playSingle(HintAudios[0]);
			break;
		case "Bin":Soundmanager2.instance.playSingle(HintAudios[1]);
			break;
		case "Battery": Soundmanager2.instance.playSingle(HintAudios[2]);
			break;
		case "Stamp":Soundmanager2.instance.playSingle(HintAudios[3]);
			break;
		case "GardenBin":Soundmanager2.instance.playSingle(HintAudios[4]);
			break;
		case "DoorsOpen":Soundmanager2.instance.playSingle(HintAudios[5]);
			break;
		case "DoorsClosed":Soundmanager2.instance.playSingle(HintAudios[6]);
			break;
		default:Soundmanager2.instance.playSingle(HintAudios[7]);
			break;
		}

		while(Soundmanager2.instance.getEfxSource ().isPlaying)
		{
			yield return null;
		}

		m_HintText.text = "";
		HintTextObj.SetActive (false);
		m_IsHintDisplaying = false;
	}

	IEnumerator ds()
	{
		yield return new WaitForEndOfFrame();
		inventory2.AddItem(buttonImages[4], "ipod");
	}

    void PlayVideo(string clipName)
    {

        JoyStickSettings(false);
        videoPlayer.gameObject.SetActive(true);
        if (clipName == Application.streamingAssetsPath + "/panda_breakfast.mp4")
        {
            IsBreakfastMoviePlaying = true;
        }
#if !(UNITY_IOS || UNITY_ANDROID)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
#endif
        display.SetActive(true);
        display.transform.GetChild(1).gameObject.SetActive(true);
        inventory.GetComponent<inventory2>().enabled = false;
        inventoryPanel.SetActive(false);
        cursor.SetActive(false);
        pointerDot.SetActive(false);
        pointerDot.GetComponent<Image>().enabled = false;
        cursor.GetComponent<Image>().enabled = false;
        foreach (MonoBehaviour b in behav)
        {
            b.enabled = false;
        }

        videoPlayer.gameObject.SetActive(true);
        m_MovieFinishedPlaying = false;
        videoPlayer.url = clipName;

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, VideoAudioSource);
        videoPlayer.controlledAudioTrackCount = 1;
        videoPlayer.Play();
    }

    private bool m_MovieFinishedPlaying = false;
    void EndReachedOnVideo(VideoPlayer _vp)
    {
        foreach (MonoBehaviour b in behav)
        {
            b.enabled = true;
        }
        display.SetActive(false);
        pointerDot.SetActive(true);
        pointerDot.GetComponent<Image>().enabled = true;
        cursor.GetComponent<Image>().enabled = true;
        inventoryPanel.SetActive(false);
        inventory.GetComponent<inventory2>().enabled = true;

        m_MovieFinishedPlaying = true;
        videoPlayer.gameObject.SetActive(false);

        if (IsBreakfastMoviePlaying)
         { IsBreakfastMoviePlaying = false;
            PandaForm.SetActive(true);
        }

        JoyStickSettings(true);
        display.SetActive(false);
        display.transform.GetChild(1).gameObject.SetActive(false);

        if (_vp.url == Application.streamingAssetsPath + "/panda_formfill.mp4")
        {
            ScoreUI.SetActive(true);
            SaveDataStatic.Score = 100;
            ScoreUI.transform.Find("ScoreTxt").gameObject.GetComponent<Text>().text = "" + SaveDataStatic.Score;
            SaveDataStatic.MissionList["Complete recycling competition"] = "100| ";
        }
    }

    public GameObject ScoreUI;

    private int m_SubIndexPlayed = 0;
	public bool IsSubtitleStillDisplaying = false;
	// Update is called once per frame
	void Update () {

#if UNITY_WEBGL
		//if(display.transform.GetChild(0).gameObject.activeSelf)
        if(!m_MovieFinishedPlaying)
		{
			movieTexture.Update();

			if (Mathf.Approximately (movieTexture.time, movieTexture.duration)) {
				foreach(MonoBehaviour b in behav){
					b.enabled = true;
				}
				display.transform.GetChild(0).gameObject.SetActive (false);
				display.transform.GetChild(1).gameObject.SetActive(false);
				//	pointerDot.SetActive (true);
				inventory.GetComponent<inventory2> ().enabled = true;

				//movieManager.SetPlayBackComplete (true);
         m_MovieFinishedPlaying = true;

				if(IsBreakfastMoviePlaying) {IsBreakfastMoviePlaying = false;
        PandaForm.SetActive (true);
        }
			}
		}
#endif

        if (IsSubtitleStillDisplaying || inventoryPanel.activeSelf)
			return;

		if (m_HasStampPuttingCompleted && !Door3Glow.activeSelf) {
			if (isTapOpen || iscupdownOpen || isFridgeOpen || isDrawerOpen || isDrawerUpOpen || isDoor2Open || stampOk == false || stampDrawerOprn || lightsOn) {
			} else {
				Door3Glow.SetActive (true);
				m_ItemSelectStatus = "DoorsClosed";
			}
		}

        SelectMethod();

		if (cursor4.activeSelf == true )
        {
            cursor.SetActive(false);
          
        }
		if (quitMenu.activeSelf)
        {
            panda.GetComponent<MonoBehaviour>().enabled = false;
        }

#if !(UNITY_IOS || UNITY_ANDROID)
        if (Cursor.visible)
        {
           // cursor4.SetActive(false);
            cursor.SetActive(false);
            
        }
#endif
		if (answers.activeSelf)
        {
           // radio.SetActive(false);
            radio.GetComponent<AudioSource>().Pause();
            singingBirds.SetActive(false);
#if !(UNITY_IOS || UNITY_ANDROID)
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
#endif
            panda.GetComponent<MonoBehaviour>().enabled = false;
            inventoryPanel.SetActive(false);
        }
		else if (answers.activeSelf == false)
        {
            singingBirds.SetActive(true);
        }
		if (inventoryPanel.activeSelf) {
            panda.GetComponent<MonoBehaviour>().enabled = false;

         }
        ////if (cursor4.active == false && cursoractive == false)
        ////{
        ////    cursor.SetActive(true);
        ////    cursoractive = true;
        ////}
		if (inventoryPanel.activeSelf == false && quitMenu.activeSelf == false && answers.activeSelf == false && !RecycleMedal.activeSelf/*&& medal.active == false*/)
        {
#if !(UNITY_IOS || UNITY_ANDROID)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
#endif
        }



        if (putcans && putBagNbattery && putsalardTobin && canGetStamp == false && findStamp ==false)
        {
          
            entryFormGlow.SetActive(true);
        }
        //if (medal.active)
		if(RecycleMedal.activeSelf)
        {
#if !(UNITY_IOS || UNITY_ANDROID)
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Cursor.SetCursor(HandTex, Vector2.zero, CursorMode.Auto);
#endif
            panda.GetComponent<MonoBehaviour>().enabled = false;

        }

#if !(UNITY_IOS || UNITY_ANDROID)
		// Hint 
		if (m_SubIndexPlayed > 3 && Input.GetKeyDown (KeyCode.H) && !m_IsHintDisplaying) {

			StartCoroutine (ShowHint ());
		}
		//Look for Instruciton
		else if(/*m_SubIndexPlayed > 3 &&*/ Input.GetKeyDown (KeyCode.K)) {

			if (!InstructionUI.activeSelf) {
				InstructionUI.SetActive (true);

				foreach (MonoBehaviour m in panda.GetComponents<MonoBehaviour>())
					m.enabled = false;
			} else {
				InstructionUI.SetActive (false);

				foreach (MonoBehaviour m in panda.GetComponents<MonoBehaviour>())
					m.enabled = true;
			}

			
		}
#endif
        /*
        if (Input.GetMouseButtonDown(1) && InventryOnclick.getstamp && setStamp ==false)
        {
            cursor4.SetActive(false);
            InventryOnclick.getstamp = false;
            foreach (Button b in buttons)
            {
                if (b.image.sprite.name == buttonImages[9].name)
                {
                    b.image.sprite = buttonImages[8];
                    break;
                }
            }
        }
        
        if (Input.GetMouseButtonDown(1) && InventryOnclick.putLetter)
        {
            cursor4.SetActive(false);
            InventryOnclick.putLetter = false;
            foreach (Button b in buttons)
            {
                if (b.image.sprite.name == buttonImages[9].name)
                {
                    b.image.sprite = buttonImages[10];
                    break;
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && InventryOnclick.ClickedBagNbatry && putBagNbattery ==false)
        {
            cursor4.SetActive(false);
            InventryOnclick.ClickedBagNbatry = false;
            foreach (Button b in buttons)
            {
                if (b.image.sprite.name == buttonImages[9].name)
                {
                    b.image.sprite = buttonImages[3];
                    break;
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && InventryOnclick.bintocursor && putcans ==false)
        {
            cursor4.SetActive(false);
            InventryOnclick.bintocursor = false;
            foreach (Button b in buttons)
            {
                if (b.image.sprite.name == buttonImages[9].name)
                {
                    b.image.sprite = buttonImages[6];
                    break;
                }
            }
        }
        
       
 
        if (Input.GetMouseButtonDown(1) && InventryOnclick.GetKey)
        {
            cursor4.SetActive(false);
            InventryOnclick.GetKey = false;
            foreach (Button b in buttons)
            {
                if (b.image.sprite.name == buttonImages[9].name)
                {
                    if (unlockedDoor == false)
                    {
                        b.image.sprite = buttonImages[0];
                        break;
                    }
                    
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && InventryOnclick.putSerial && gotSerialBox ==false)
        {
            cursor4.SetActive(false);
            InventryOnclick.putSerial = false;
            foreach (Button b in buttons)
            {
                if (b.image.sprite.name == buttonImages[9].name)
                {
                    b.image.sprite = buttonImages[5];
                    break;
                }
            }
        }
        if(Input .GetMouseButtonDown(1) && InventryOnclick.getSalard && putsalardTobin == false){
           // inventory2.AddItem(buttonImages[7], hit.collider.tag);
          // ShowCursor(knob);
            cursor4.SetActive(false);
            foreach (Button b in buttons)
            {
                if (b.image.sprite.name == buttonImages[9].name)
                {                    
                    b.image.sprite = buttonImages[7];
                   break;
                 //   Debug.Log("oook");
                }
            }
            InventryOnclick.getSalard = false;
            //ShowCursor(knob);
        }
		if (cursor4.activeSelf)
        {
            foreach (Button b in buttons)
            {
                b.interactable = false;
            }
        }
		else if (cursor4.activeSelf == false)
        {
            foreach (Button b in buttons)
            {
                b.interactable = true;
            }
        }
        */
	}

  

    public void ResetSelectedItem()
    {
        if (InventryOnclick.getstamp && setStamp == false)
        {
            cursor4.SetActive(false);
            pointerDot.SetActive(true);
            InventryOnclick.getstamp = false;
            foreach (Image b in buttons)
            {
                if (b.sprite.name == buttonImages[9].name)
                {
                    b.sprite = buttonImages[8];
                    break;
                }
            }
        }

        if (InventryOnclick.putLetter)
        {
            cursor4.SetActive(false);
            pointerDot.SetActive(true);
            InventryOnclick.putLetter = false;
            foreach (Image b in buttons)
            {
                if (b.sprite.name == buttonImages[9].name)
                {
                    b.sprite = buttonImages[10];
                    break;
                }
            }
        }
        if (InventryOnclick.ClickedBagNbatry && putBagNbattery == false)
        {
            cursor4.SetActive(false);
            pointerDot.SetActive(true);
            InventryOnclick.ClickedBagNbatry = false;
            foreach (Image b in buttons)
            {
                if (b.sprite.name == buttonImages[9].name)
                {
                    b.sprite = buttonImages[3];
                    break;
                }
            }
        }
        if (InventryOnclick.bintocursor && putcans == false)
        {
            cursor4.SetActive(false);
            pointerDot.SetActive(true);
            InventryOnclick.bintocursor = false;
            foreach (Image b in buttons)
            {
                if (b.sprite.name == buttonImages[9].name)
                {
                    b.sprite = buttonImages[6];
                    break;
                }
            }
        }

        if (InventryOnclick.GetKey)
        {
            cursor4.SetActive(false);
            pointerDot.SetActive(true);
            InventryOnclick.GetKey = false;
            foreach (Image b in buttons)
            {
                if (b.sprite.name == buttonImages[9].name)
                {
                    if (unlockedDoor == false)
                    {
                        b.sprite = buttonImages[0];
                        break;
                    }

                }
            }
        }
        if (InventryOnclick.putSerial && gotSerialBox == false)
        {
            cursor4.SetActive(false);
            pointerDot.SetActive(true);
            InventryOnclick.putSerial = false;
            foreach (Image b in buttons)
            {
                if (b.sprite.name == buttonImages[9].name)
                {
                    b.sprite = buttonImages[5];
                    break;
                }
            }
        }
        if (InventryOnclick.getSalard && putsalardTobin == false)
        {
            cursor4.SetActive(false);
            pointerDot.SetActive(true);
            foreach (Image b in buttons)
            {
                if (b.sprite.name == buttonImages[9].name)
                {
                    b.sprite = buttonImages[7];
                    break;
                }
            }
            InventryOnclick.getSalard = false;
        }
        if (cursor4.activeSelf)
        {
            foreach (Image b in buttons)
            {
                b.gameObject.transform.GetChild(0).GetComponent<Button>().interactable = false;
            }
        }
        else if (cursor4.activeSelf == false)
        {
            foreach (Image b in buttons)
            {
                b.gameObject.transform.GetChild(0).GetComponent<Button>().interactable = true;
            }
        }
    }

    public void SelectMethod(bool m_ClickedStatus = false)
    {

#if !(UNITY_IOS || UNITY_ANDROID)
        m_ClickedStatus = Input.GetMouseButtonDown(0);
#endif

        if (formCamFocused && m_ClickedStatus)
        {
            if (!cursor4.activeSelf && cursor4.GetComponent<Image>().sprite.name != buttonImages[8].name)
            {

                formCam.depth = -1f;
                foreach (MonoBehaviour m in panda.GetComponents<MonoBehaviour>())
                    m.enabled = true;
                formCamFocused = false;
            }
            else if (!setStamp)
            {

                formCam.depth = -1f;
                formCamFocused = false;
                //              Destroy (stampGlow);
                //              cursor4.SetActive (false);

                ticks[0].SetActive(false);
                ticks[1].SetActive(false);
                ticks[2].SetActive(false);
                Destroy(stampGlow);
                cursor4.SetActive(false);
                pointerDot.SetActive(true);

#if UNITY_WEBGL
                movieTexture = new WebGLMovieTexture("StreamingAssets/panda_formfill.mp4");

                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                inventory.GetComponent<inventory2> ().enabled = false;
                inventoryPanel.SetActive (false);
                display.transform.GetChild(0).gameObject.SetActive(true);
                display.transform.GetChild(1).gameObject.SetActive(true);
                pointerDot.SetActive (false);

                foreach(MonoBehaviour b in behav){
                b.enabled = false;
                }

                Destroy(display.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture);
                System.GC.Collect();
                display.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = movieTexture;
#else
                //StartCoroutine (movieManager.PlayClip (moviez [1]));
                PlayVideo(Application.streamingAssetsPath + "/panda_formfill.mp4");
#endif

                stamp.SetActive(true);
                setStamp = true;
                PandaForm.GetComponent<Renderer>().material.mainTexture = texform;
            }

            return;
        }


        Ray ray = Camera.main.ScreenPointToRay(midPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3f))
        {
            if (m_ClickedStatus && cursor4.activeSelf &&
            (cursor4.GetComponent<Image>().sprite.name == buttonImages[7].name
                || cursor4.GetComponent<Image>().sprite.name == buttonImages[6].name
            || cursor4.GetComponent<Image>().sprite.name == buttonImages[3].name) &&
                (hit.collider.tag == "recycleBinred" || hit.collider.tag == "recycleBinyellow" || hit.collider.tag == "recycleBingreen" || hit.collider.tag == "compost"))
            {

            }
            else if(m_ClickedStatus && cursor4.activeSelf && cursor4.GetComponent<Image>().sprite.name == buttonImages[0].name && hit.collider.tag == "door2")
            {

            }
            else if (m_ClickedStatus && cursor4.activeSelf && cursor4.GetComponent<Image>().sprite.name == buttonImages[8].name && hit.collider.tag == "applicationForm")
            {

            }
            else if (m_ClickedStatus && cursor4.activeSelf && cursor4.GetComponent<Image>().sprite.name == buttonImages[5].name && hit.collider.tag == "tabletop")
            {

            }
            else if (m_ClickedStatus && cursor4.activeSelf)
            {
                ResetSelectedItem();
                return;
            }
        

            switch (hit.collider.tag)
        {
            case "switch":
                ShowCursor(handTexture);
                if (m_ClickedStatus)
                {
                    if (lightsOn)
                    {
                        for (int i = 0; i < lights.transform.childCount; i++)
                        {
                            lights.transform.GetChild(i).GetComponent<Renderer>().material = lightMat[0];
                        }
                        SendMessageUpwards("PlaySourceClip", SwitchSound, SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
                        for (int i = 0; i < lights.transform.childCount; i++)
                        {
                            lights.transform.GetChild(i).GetComponent<Renderer>().material = lightMat[1];
                        }
                        SendMessageUpwards("PlaySourceClip", SwitchSound, SendMessageOptions.DontRequireReceiver);
                    }
                    lightsOn = !lightsOn;
                }
                break;
            case "drawer":
                if (startGlow)
                {
                    ShowCursor(handTexture);
                    if (m_ClickedStatus)
                    {
                        isDrawerOpen = !isDrawerOpen;
                            Soundmanager2.instance.playSingle(audioClip[25]);
                            drawer.GetComponent<Animator>().SetBool("isOpen", isDrawerOpen);
                    }
                }
                break;
            case "fridge":
                if (startGlow)
                {
                    ShowCursor(handTexture);
                    if (m_ClickedStatus)
                    {
                        isFridgeOpen = !isFridgeOpen;
                            fridge.GetComponent<AudioSource>().Play();
                            fridge.GetComponent<Animator>().SetBool("isOpen", isFridgeOpen);
                        if (isFridgeOpen && frigdeGlow.activeSelf)
                        {
                            Soundmanager2.instance.playSingle(audioClip[4]);
                            StartCoroutine(ShowSubtitle("Eww…"));
                        }

                        if (binGlow != null && binGlow.activeSelf)
                        {
                            m_ItemSelectStatus = "Bin";
                        }
                        else if (frigdeGlow != null && frigdeGlow.activeSelf)
                        {
                            m_ItemSelectStatus = "Friedge";
                        }
                        else if (bateryGlow != null && bateryGlow.activeSelf)
                        {
                            m_ItemSelectStatus = "Battery";
                        }
                        else if (!putsalardTobin || !putcans || !putBagNbattery)
                        {
                            m_ItemSelectStatus = "GardenBin";
                        }
                        else
                        {
                            m_ItemSelectStatus = null;
                        }
                    }
                }
                break;
            case "drawerup":
                if (startGlow)
                {

                    ShowCursor(handTexture);
                    if (m_ClickedStatus)
                    {
                        isDrawerUpOpen = !isDrawerUpOpen;
                            Soundmanager2.instance.playSingle(audioClip[26]);
                            drawerup.GetComponent<Animator>().SetBool("isOpen", isDrawerUpOpen);
                    }
                }
                break;
            case "radio":
                ShowCursor(handTexture);
                if (m_ClickedStatus)
                {
                    if (isRadiOn)
                    {
                        SendMessageUpwards("PlaySourceClip", SwitchSound, SendMessageOptions.DontRequireReceiver);
                        radio.GetComponent<AudioSource>().Pause();
                    }
                    else
                    {
                        SendMessageUpwards("PlaySourceClip", SwitchSound, SendMessageOptions.DontRequireReceiver);
                        radio.GetComponent<AudioSource>().Play();
                    }
                    isRadiOn = !isRadiOn;
                }
                break;
            case "cupdown":
                ShowCursor(handTexture);
                if (m_ClickedStatus)
                {
                    iscupdownOpen = !iscupdownOpen;
                        Soundmanager2.instance.playSingle(audioClip[26]);
                        hit.collider.gameObject.GetComponent<Animator>().SetBool("isOpen", iscupdownOpen);
                }
                break;
            case "tap":
                ShowCursor(handTexture);
                if (m_ClickedStatus)
                {

                    isTapOpen = !isTapOpen;

                    hit.collider.gameObject.GetComponent<Animator>().SetBool("isOpen", isTapOpen);
                    tap.SetActive(isTapOpen);
                    if (isTapOpen == true)
                    {
                        Soundmanager2.instance.playSingle(audioClip[9]);
                        //  Soundmanager2.instance.playwater(audioClip[2]);
                        // Soundmanager2.instance.playwater(audioClip[9]);
                        //if ()
                        //{

                        //}
                        // yield return new WaitForSeconds(3);
                        StartCoroutine(ShowSubtitle("Mustn’t waste water!"));
                    }
                }
                break;
            case "bin":
                if (startGlow)
                {
                    if (binExplored)
                    {
                        ShowCursor(handTexture);
                        //  if(Input.GetMouseButtonDown(0) &&)
                        if (m_ClickedStatus && binDestroy == false)
                        {
                            binDestroy = true;
                            inventory2.AddItem(buttonImages[6], hit.collider.tag);
                            Destroy(GameObject.FindGameObjectWithTag("cans"));
                            binGlow.SetActive(false);

                            SendMessageUpwards("PlaySourceClip", ItemPickAudio, SendMessageOptions.DontRequireReceiver);

                            if (binGlow != null && binGlow.activeSelf)
                            {
                                m_ItemSelectStatus = "Bin";
                            }
                            else if (frigdeGlow != null && frigdeGlow.activeSelf)
                            {
                                m_ItemSelectStatus = "Friedge";
                            }
                            else if (bateryGlow != null && bateryGlow.activeSelf)
                            {
                                m_ItemSelectStatus = "Battery";
                            }
                            else if (!putsalardTobin || !putcans || !putBagNbattery)
                            {
                                m_ItemSelectStatus = "GardenBin";
                            }
                            else
                            {
                                m_ItemSelectStatus = null;
                            }
                        }
                    }
                    else
                    {
                        ShowCursor(magnifierTexture);
                        if (m_ClickedStatus)
                        {
                            Soundmanager2.instance.playSingle(audioClip[8]);
                            StartCoroutine(ShowSubtitle("That can be recycled!"));

                            binExplored = true;
                        }
                    }
                }
                break;
            case "serialbox":
                ShowCursor(handTexture);
                if (m_ClickedStatus && !gotSerialBox)
                {

                    SendMessageUpwards("PlaySourceClip", ItemPickAudio, SendMessageOptions.DontRequireReceiver);

                    tableGlow.SetActive(true);
                    Destroy(cubordGlow);
                    serialBox.SetActive(false);
                    inventory2.AddItem(buttonImages[5], hit.collider.tag);
                    Soundmanager2.instance.playSingle(NewAudioClips[0]);
                    StartCoroutine(ShowSerialSubtitle("Select the cereal from your rufflesack using the ‘R’ key and place it on the table."));

                }
                else if (m_ClickedStatus && InventryOnclick.PutSerial())
                {

                    InventryOnclick.putSerial = false;
                    startGlow = true;
                    binGlow.SetActive(true);
                    frigdeGlow.SetActive(true);
                    CupbordUpDrowerGlow.SetActive(true);
                    Drawers3Glow.SetActive(true);
                    bateryGlow.SetActive(true);

#if UNITY_WEBGL
                    IsBreakfastMoviePlaying = true;
                    movieTexture = new WebGLMovieTexture("StreamingAssets/panda_breakfast.mp4");

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    inventory.GetComponent<inventory2> ().enabled = false;
                    inventoryPanel.SetActive (false);
                    display.transform.GetChild(0).gameObject.SetActive(true);
                    display.transform.GetChild(1).gameObject.SetActive(true);
                    pointerDot.SetActive (false);

                    foreach(MonoBehaviour b in behav){
                        b.enabled = false;
                    }

                    display.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = movieTexture;
#else
                    //StartCoroutine(movieManager.PlayClip(moviez[0]));
                    PlayVideo(Application.streamingAssetsPath + "/panda_breakfast.mp4");
#endif


                    serialBox.GetComponent<BoxCollider>().enabled = false;

                    //PandaForm.SetActive (true);

                    playedSerial = true;

                    m_ItemSelectStatus = "Bin";

                }

                break;

            case "key":
                if (startGlow)
                {
                    //  if(gotSerialBox)
                    ShowCursor(handTexture);
                    if (m_ClickedStatus)
                    {
                        SendMessageUpwards("PlaySourceClip", ItemPickAudio, SendMessageOptions.DontRequireReceiver);
                        Destroy(hit.collider.gameObject);
                        //binDestroy = true;
                        keyDestroy = true;
                        CupbordUpDrowerGlow.SetActive(false);
                        inventory2.AddItem(buttonImages[0], hit.collider.tag);
                        Soundmanager2.instance.playSingle(audioClip[16]);
                        StartCoroutine(ShowSubtitle("The kitchen door key!"));

                    }
                }
                break;

            case "bag":
                if (startGlow)
                {
                    ShowCursor(handTexture);
                    if (m_ClickedStatus)
                    {
                        SendMessageUpwards("PlaySourceClip", ItemPickAudio, SendMessageOptions.DontRequireReceiver);
                        Drawers3Glow.SetActive(false);
                        ShowCursor(handTexture);
                        Destroy(hit.collider.gameObject);
                        inventory2.AddItem(buttonImages[1], hit.collider.tag);
                    }
                }
                break;
            case "door2":

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[0].name && cursor4.activeSelf && !unlockedDoor)
                {
                    cursor4.GetComponent<Image>().color = Color.green;
                }

                if (unlockedDoor == true)
                {
                    ShowCursor(handTexture);
                    if (m_ClickedStatus)
                    {
                        isDoor2Open = !isDoor2Open;
                            Soundmanager2.instance.playSingle(audioClip[24]);
                            hit.collider.gameObject.GetComponent<Animator>().SetBool("isOpen", isDoor2Open);
                    }
                }
                else if (m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[0].name && cursor4.activeSelf)
                {
                    isDoor2Open = !isDoor2Open;
                    hit.collider.gameObject.GetComponent<Animator>().SetBool("isOpen", isDoor2Open);
                    cursor2.SetActive(false);
                    unlockedDoor = true;
                    cursor4.SetActive(false);
                        pointerDot.SetActive(true);
                        Soundmanager2.instance.playSingle(audioClip[19]);

                }
                else
                {
                    ShowCursor(magnifierTexture);
                    if (m_ClickedStatus)
                    {
                        exploreMagnifire = true;
                        if (keyDestroy)
                        {
                            //StartCoroutine (ShowSubtitle ("The door is locked, I should take the key out from my rucksack !"));
                            if (m_SubIndexPlayed == 4)
                            {
                                m_SubIndexPlayed++;
                                //                              Soundmanager2.instance.playSingle (NewAudioClips [4]);
                                //                              StartCoroutine (ShowSubtitle ("The door is locked. Select the key from your rufflesack"));
                            }
                            Soundmanager2.instance.playSingle(NewAudioClips[4]);
                            StartCoroutine(ShowSubtitle("The door is locked. Select the key from your rufflesack."));
                        }
                        else if (keyDestroy == false)
                        {
                            exploreMagnifire = true;
                            Soundmanager2.instance.playSingle(audioClip[20]);
                            StartCoroutine(ShowSubtitle("The door is locked, I need to find a key!"));
                        }
                    }
                }

                break;
            case "batry":
                if (explorebatry)
                {
                    ShowCursor(handTexture);
                }
                else
                {
                    ShowCursor(magnifierTexture);
                }

                if (m_ClickedStatus && explorebatry)
                {

                    SendMessageUpwards("PlaySourceClip", ItemPickAudio, SendMessageOptions.DontRequireReceiver);

                    bateryGlow.SetActive(false);
                    Soundmanager2.instance.playSingle(audioClip[3]);
                    StartCoroutine(ShowSubtitle1("Tut…"));

                    //StartCoroutine (ShowSubtitle1 ("I will need a special bag for this battery!"));
                    Destroy(hit.collider.gameObject);
                    inventory2.AddItem(buttonImages[2], hit.collider.tag);

                    if (binGlow != null && binGlow.activeSelf)
                    {
                        m_ItemSelectStatus = "Bin";
                    }
                    else if (frigdeGlow != null && frigdeGlow.activeSelf)
                    {
                        m_ItemSelectStatus = "Friedge";
                    }
                    else if (bateryGlow != null && bateryGlow.activeSelf)
                    {
                        m_ItemSelectStatus = "Battery";
                    }
                    else if (!putsalardTobin || !putcans || !putBagNbattery)
                    {
                        m_ItemSelectStatus = "GardenBin";
                    }
                    else
                    {
                        m_ItemSelectStatus = null;
                    }
                }
                else if (m_ClickedStatus)
                {
                    explorebatry = true;
                    Soundmanager2.instance.playSingle(audioClip[6]);
                    StartCoroutine(ShowSubtitle("Never leave old batteries just lying about!"));

                }
                break;
            case "recycleBinred":
                ShowCursor(magnifierTexture);

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[7].name && cursor4.activeSelf && !putsalardTobin)
                {
                    cursor4.GetComponent<Image>().color = Color.red;
                }

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[6].name && cursor4.activeSelf && !putcans)
                {
                    cursor4.GetComponent<Image>().color = Color.red;
                }

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[3].name && cursor4.activeSelf && !putBagNbattery)
                {
                    cursor4.GetComponent<Image>().color = Color.red;
                }

                if (m_ClickedStatus && !HasQuizStarted)
                {
                    Soundmanager2.instance.playSingle(audioClip[10]);
                    StartCoroutine(ShowSubtitle("The household bin – for landfill waste."));
                    //exploregreenbin = true;
                }

                break;
            case "telephone":
                ShowCursor(magnifierTexture);
                if (m_ClickedStatus)
                {
                    Soundmanager2.instance.playSingle(audioClip[14]);
                    StartCoroutine(ShowSubtitle("I wish Dad would buy a modern telephone!"));
                }
                break;
            //          case "ipod2":
            //              Destroy (hit.collider.gameObject);
            //              inventory2.AddItem (buttonImages [4], hit.collider.tag);
            //              break;
            case "salard":

                ShowCursor(handTexture);
                if (m_ClickedStatus)
                {
                    SendMessageUpwards("PlaySourceClip", ItemPickAudio, SendMessageOptions.DontRequireReceiver);
                    frigdeGlow.SetActive(false);
                    Destroy(hit.collider.gameObject);
                    inventory2.AddItem(buttonImages[7], hit.collider.tag);

                    if (binGlow != null && binGlow.activeSelf)
                    {
                        m_ItemSelectStatus = "Bin";
                    }
                    else if (frigdeGlow != null && frigdeGlow.activeSelf)
                    {
                        m_ItemSelectStatus = "Friedge";
                    }
                    else if (bateryGlow != null && bateryGlow.activeSelf)
                    {
                        m_ItemSelectStatus = "Battery";
                    }
                    else if (!putsalardTobin || !putcans || !putBagNbattery)
                    {
                        m_ItemSelectStatus = "GardenBin";
                    }
                    else
                    {
                        m_ItemSelectStatus = null;
                    }
                }

                break;
            case "recycleBinyellow":
                ShowCursor(magnifierTexture);


                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[7].name && cursor4.activeSelf && !putsalardTobin)
                {
                    cursor4.GetComponent<Image>().color = Color.green;
                }

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[6].name && cursor4.activeSelf && !putcans)
                {
                    cursor4.GetComponent<Image>().color = Color.red;
                }

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[3].name && cursor4.activeSelf && !putBagNbattery)
                {
                    cursor4.GetComponent<Image>().color = Color.red;
                }

                if (m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[7].name && cursor4.activeSelf && !putsalardTobin)
                {
                    //PandaForm.SetActive(true);
                    cursor4.GetComponent<Image>().color = Color.white;
                    cursor4.SetActive(false);
                        pointerDot.SetActive(true);
                        ticks[2].SetActive(true);
                    // ShowStatsQues(2);
                    quizMB.ShowQuiz1(2);
#if !(UNITY_IOS || UNITY_ANDROID)
                    Cursor.visible = true;
#endif
                    putsalardTobin = true;

                    if (!putcans || !putBagNbattery)
                    {
                        m_ItemSelectStatus = "GardenBin";
                    }
                    else
                    {
                        m_ItemSelectStatus = null;
                    }

                    //   entryFormGlow.SetActive(true);

                }
                else if (m_ClickedStatus && !HasQuizStarted)
                {
                    Soundmanager2.instance.playSingle(audioClip[12]);
                    StartCoroutine(ShowSubtitle("The food waste bin."));
                    //exploreyellowbin = true;
                }
                break;
            case "applicationForm":

                if (setStamp == false && !cursor4.activeSelf)
                {//&& FormOk && !cursor4.activeSelf && cursor4.GetComponent<Image> ().sprite.name != buttonImages [8].name) {
                    ShowCursor(magnifierTexture);
                }
                else if (setStamp)
                {
                    ShowCursor(handTexture);
                    PandaForm.GetComponent<Renderer>().material.mainTexture = finaltexForm;
                }

                if (m_ClickedStatus && !cursor4.activeSelf)
                {

                    //if (!toggleCam) {
                    if (formCam.depth > 0)
                    {
                        formCam.depth = -1f;
                        foreach (MonoBehaviour m in panda.GetComponents<MonoBehaviour>())
                            m.enabled = true;
                        formCamFocused = false;
                    }
                    else
                    {
                        formCam.depth = 1f;
                        foreach (MonoBehaviour m in panda.GetComponents<MonoBehaviour>())
                            m.enabled = false;
                        formCamFocused = true;
                    }
                    //toggleCam = !toggleCam;
                }

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[8].name && cursor4.activeSelf && !setStamp)
                {
                    cursor4.GetComponent<Image>().color = Color.green;
                }

                if (m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[8].name && cursor4.activeSelf)
                {
                    ticks[0].SetActive(false);
                    ticks[1].SetActive(false);
                    ticks[2].SetActive(false);
                    Destroy(stampGlow);
                    cursor4.SetActive(false);
                        pointerDot.SetActive(true);

#if UNITY_WEBGL
                    movieTexture = new WebGLMovieTexture("StreamingAssets/panda_formfill.mp4");

                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    inventory.GetComponent<inventory2> ().enabled = false;
                    inventoryPanel.SetActive (false);
                    display.transform.GetChild(0).gameObject.SetActive(true);
                    display.transform.GetChild(1).gameObject.SetActive(true);
                    pointerDot.SetActive (false);

                    foreach(MonoBehaviour b in behav){
                        b.enabled = false;
                    }

                    Destroy(display.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture);
                    System.GC.Collect();
                    display.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = movieTexture;
#else
                        //StartCoroutine (movieManager.PlayClip (moviez [1]));
                        PlayVideo(Application.streamingAssetsPath + "/panda_formfill.mp4");
#endif

                    stamp.SetActive(true);
                    setStamp = true;
                    PandaForm.GetComponent<Renderer>().material.mainTexture = texform;
                }
                else if (m_ClickedStatus && setStamp)
                {

                    SendMessageUpwards("PlaySourceClip", ItemPickAudio, SendMessageOptions.DontRequireReceiver);
                    Destroy(stamp);
                    Destroy(hit.collider.gameObject);
                    inventory2.AddItem(buttonImages[10], hit.collider.tag);

                    formCam.depth = -1f;
                    foreach (MonoBehaviour m in panda.GetComponents<MonoBehaviour>())
                        m.enabled = true;
                    formCamFocused = false;

                    StartCoroutine(AfterPuttingTheStampToEntry());
                }


                if (m_ClickedStatus && putcans && putsalardTobin && putBagNbattery && canGetStamp == false && medalFinish == false)
                {
                    //                   medal.SetActive(true);
                    //                    Soundmanager2.instance.playSingle(audioClip[21]);
                    //                    medalImage.SetActive(true);
                    medalFinish = true;

                        //Janidu Added
#if UNITY_IOS || UNITY_ANDROID
                        JoyStick.gameObject.SetActive(false);
#endif
                        panda.transform.GetChild(0).gameObject.SetActive(false);
                    RecycleMedal.SetActive(true);
                    medalImage.SetActive(true);
                    SaveDataStatic.AwardedMedalList.Add("Recycle");

                    m_ItemSelectStatus = null;
                }

                //if (medal.active)
                if (RecycleMedal.activeSelf)
                {
                    formCam.depth = -1;
                }
                if (m_ClickedStatus && putsalardTobin && /*putsalardTobin*/medalFinish && putBagNbattery && canGetStamp == false && InventryOnclick.MedalOk == true)
                {

                    //   StartCoroutine(movieManager.PlayClip(moviez[1]));
                    //   findStamp = true;
                    Soundmanager2.instance.playSingle(audioClip[15]);
                    StartCoroutine(ShowSubtitle("Now…to find a stamp!"));
                    Destroy(entryFormGlow);
                    stampGlow.SetActive(true);
                    canGetStamp = true;
                    StampDrawerGlow.SetActive(true);

                    m_ItemSelectStatus = "Stamp";
                }
                if (m_ClickedStatus)
                {
                    //StampDrawerGlow.SetActive(true);
                    //canGetStamp = true;
                    //Destroy(PandaForm);
                    //StartCoroutine(movieManager.PlayClip(moviez[1]));
                    playedSerial = true;

                    //FormOk = false;
                    //sformCam.depth = 1.0F;
                }
                //FormOk = true;

                break;
            case "stampDrawer":
                if (canGetStamp)
                {
                    ShowCursor(handTexture);
                    if (m_ClickedStatus)
                    {
                        stampDrawerOprn = !stampDrawerOprn;
                            Soundmanager2.instance.playSingle(audioClip[25]);
                            StampDrawer.GetComponent<Animator>().SetBool("isOpen", stampDrawerOprn);
                    }
                }
                break;
            case "stamp":
                if (m_ClickedStatus)
                {
                    SendMessageUpwards("PlaySourceClip", ItemPickAudio, SendMessageOptions.DontRequireReceiver);
                    stampOk = true;
                    Destroy(StampDrawerGlow);
                    Destroy(hit.collider.gameObject);
                    inventory2.AddItem(buttonImages[8], hit.collider.tag);
                }
                break;
            case "door3":
                ShowCursor(handTexture);

                if (m_ClickedStatus)
                {
                    if (isTapOpen || iscupdownOpen || isFridgeOpen || isDrawerOpen || isDrawerUpOpen || isDoor2Open || stampOk == false || stampDrawerOprn || lightsOn)
                    {
                        Soundmanager2.instance.playSingle(audioClip[0]);
                        StartCoroutine(ShowSubtitle("No, no, no…"));

                        m_ItemSelectStatus = "DoorsOpen";
                    }
                    else
                    {
                        //Application.LoadLevel (0);
                        //Janidu Commented
                        LoadingTxt.SetActive(true);
                        SceneManager.LoadSceneAsync("Town");
                    }
                }
                break;
            case "recycleBingreen":

                ShowCursor(magnifierTexture);

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[7].name && cursor4.activeSelf && !putsalardTobin)
                {
                    cursor4.GetComponent<Image>().color = Color.red;
                }

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[6].name && cursor4.activeSelf && !putcans)
                {
                    cursor4.GetComponent<Image>().color = Color.green;
                }

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[3].name && cursor4.activeSelf && !putBagNbattery)
                {
                    cursor4.GetComponent<Image>().color = Color.green;
                }


                else if (m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[6].name && cursor4.activeSelf && !putcans)
                {
                    cursor4.GetComponent<Image>().color = Color.white;
                    cursor4.SetActive(false);
                        pointerDot.SetActive(true);
                        ticks[0].SetActive(true);
                    putcans = true;
                    // ShowStatsQues(0);
                    quizMB.ShowQuiz(0);
                    // Cursor.visible = true;

                    if (binGlow != null && binGlow.activeSelf)
                    {
                        m_ItemSelectStatus = "Bin";
                    }
                    else if (frigdeGlow != null && frigdeGlow.activeSelf)
                    {
                        m_ItemSelectStatus = "Friedge";
                    }
                    else if (bateryGlow != null && bateryGlow.activeSelf)
                    {
                        m_ItemSelectStatus = "Battery";
                    }
                    else if (!putsalardTobin || !putcans || !putBagNbattery)
                    {
                        m_ItemSelectStatus = "GardenBin";
                    }
                    else
                    {
                        m_ItemSelectStatus = null;
                    }

                }
                else if (m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[3].name && cursor4.activeSelf && !putBagNbattery)
                {
                    cursor4.GetComponent<Image>().color = Color.white;
                    cursor4.SetActive(false);
                        pointerDot.SetActive(true);
                        //   if (InventryOnclick.bagNBattery () && exploreRedbin) {
                        ticks[1].SetActive(true);
                    // ShowStatsQues(1);
                    quizMB.ShowQuiz(1);
                    // Cursor.visible = true;
                    putBagNbattery = true;
                    //InventryOnclick.bagNbattery = false;

                    if (binGlow != null && binGlow.activeSelf)
                    {
                        m_ItemSelectStatus = "Bin";
                    }
                    else if (frigdeGlow != null && frigdeGlow.activeSelf)
                    {
                        m_ItemSelectStatus = "Friedge";
                    }
                    else if (bateryGlow != null && bateryGlow.activeSelf)
                    {
                        m_ItemSelectStatus = "Battery";
                    }
                    else if (!putsalardTobin || !putcans || !putBagNbattery)
                    {
                        m_ItemSelectStatus = "GardenBin";
                    }
                    else
                    {
                        m_ItemSelectStatus = null;
                    }

                    if (InventryOnclick.bintocursor)
                    {
                        if (m_ClickedStatus)
                        {
                            putbin = true;

                        }
                    }
                }
                else if (m_ClickedStatus && !HasQuizStarted)
                {
                    Soundmanager2.instance.playSingle(audioClip[11]);
                    StartCoroutine(ShowSubtitle("The recycle bin."));
                    //exploreRedbin = true;
                }
                break;
            case "pot":
                ShowCursor(magnifierTexture);
                if (m_ClickedStatus)
                {
                    Soundmanager2.instance.playSingle(audioClip[5]);
                    StartCoroutine(ShowSubtitle("Mum’s last surviving pot plant!"));
                }
                break;
            case "pic":
                ShowCursor(magnifierTexture);
                if (m_ClickedStatus)
                {
                    Soundmanager2.instance.playSingle(audioClip[18]);
                    StartCoroutine(ShowSubtitle("This just confuses me!"));
                }
                break;
            case "frame":
                ShowCursor(magnifierTexture);
                if (m_ClickedStatus)
                {
                    Soundmanager2.instance.playSingle(audioClip[1]);
                    StartCoroutine(ShowSubtitle("That was me in year 4!"));
                }
                break;

            case "flowers":
                ShowCursor(magnifierTexture);
                if (m_ClickedStatus && !HasQuizStarted)
                {
                    Soundmanager2.instance.playSingle(audioClip[7]);
                    StartCoroutine(ShowSubtitle("Sniff…sigh…flowers…"));
                }
                break;
            case "compost":
                ShowCursor(magnifierTexture);

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[7].name && cursor4.activeSelf && !putsalardTobin)
                {
                    cursor4.GetComponent<Image>().color = Color.green;
                }

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[6].name && cursor4.activeSelf && !putcans)
                {
                    cursor4.GetComponent<Image>().color = Color.red;
                }

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[3].name && cursor4.activeSelf && !putBagNbattery)
                {
                    cursor4.GetComponent<Image>().color = Color.red;
                }

                if (m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[7].name && cursor4.activeSelf && !putsalardTobin)
                {
                    cursor4.SetActive(false);
                        pointerDot.SetActive(true);
                        ticks[2].SetActive(true);
                    // ShowStatsQues(2);
                    quizMB.ShowQuiz1(2);
                    // Cursor.visible = true;
                    //   Cursor.SetCursor(handTexture, );

                    putsalardTobin = true;

                }
                else if (m_ClickedStatus && !HasQuizStarted)
                {
                    Soundmanager2.instance.playSingle(audioClip[17]);
                    StartCoroutine(ShowSubtitle("Dad’s compost bin!"));

                }
                break;
            case "tabletop":

                if (!m_ClickedStatus && cursor4.GetComponent<Image>().sprite.name == buttonImages[5].name && cursor4.activeSelf && InventryOnclick.putSerial && gotSerialBox == false)
                {
                    cursor4.GetComponent<Image>().color = Color.green;
                }

                if (m_ClickedStatus)
                {
                    if (cursor4.GetComponent<Image>().sprite.name == buttonImages[5].name && cursor4.activeSelf)
                    {
                        serialBox.SetActive(true);
                        //Debug.Log(InventryOnclick.putSerial);

                        Destroy(tableGlow);
                        serialBox.transform.position = new Vector3(-1f, 0.9f, -2f);
                        serialBox.transform.rotation = Quaternion.Euler(new Vector3(0f, 145f, 0f));
                        gotSerialBox = true;
                        cursor4.SetActive(false);
                            pointerDot.SetActive(true);

                            Soundmanager2.instance.playSingle(NewAudioClips[1]);
                        StartCoroutine(ShowSerialSubtitle("Now grab the box to have breakfast. Amanda does not take milk with this cereal."));
                    }
                }
                break;
            default:
                cursor.SetActive(false);
                if (cursor4.activeSelf)
                {
                    cursor4.GetComponent<Image>().color = Color.white;
                }
                else
                    {
                        pointerDot.SetActive(true);
                    }
                    break;
        }

    }
    else
    {
            if (cursor != null)
            {
                cursor.SetActive(false);
                pointerDot.SetActive(true);
            }

        if (cursor4.activeSelf)
        {
            cursor4.GetComponent<Image>().color = Color.white;
        }
    }

        if (formCam.depth > 0)
        {
            ShowCursor(magnifierTexture);
        }

#if UNITY_WEBGL
        if (!IsBreakfastMoviePlaying && playedSerial) {
            formCam.depth = 1.0F;
            playedSerial = false;
            FormOk = true;
            ShowCursor(magnifierTexture);
            panda.GetComponent<MonoBehaviour>().enabled = false;

        } else if (m_ClickedStatus && formCam.depth > 0) {
            formCam.depth = -1;
            panda.GetComponent<MonoBehaviour>().enabled = true;

            if (m_SubIndexPlayed == 2) {
                Soundmanager2.instance.playSingle (NewAudioClips [2]);
                StartCoroutine (ShowSerialSubtitle ("Now you are free to look about and start to " +
                    "complete the competition entry. Follow the instructions written on it. If an item " +
                    "glows or flashes or perhaps has an arrow above it, you should interact with it. If you " +
                    "get stuck, call on me by pressing the ‘H’ key for a hint!’"));
            }
        }
#else
        if (!IsBreakfastMoviePlaying && playedSerial && !FormOk)
        {
            formCam.depth = 1.0F;
            playedSerial = false;
            FormOk = true;
            ShowCursor(magnifierTexture);
            panda.GetComponent<MonoBehaviour>().enabled = false;

        }
        else if (m_ClickedStatus && formCam.depth > 0 && m_SubIndexPlayed == 2)
        {
            formCam.depth = -1;
            panda.GetComponent<MonoBehaviour>().enabled = true;

            if (m_SubIndexPlayed == 2)
            {
#if UNITY_IOS || UNITY_ANDROID
                JoyStick.gameObject.transform.Find("Hint").gameObject.SetActive(true);
#endif

                Soundmanager2.instance.playSingle(NewAudioClips[2]);
                StartCoroutine(ShowSerialSubtitle("Now you are free to look about and start to " +
                    "complete the competition entry. Follow the instructions written on it. If an item " +
                    "glows or flashes or perhaps has an arrow above it, you should interact with it. If you " +
                    "get stuck, call on me by pressing the ‘H’ key for a hint!’"));
            }
        }
#endif

        if(m_ClickedStatus)
            ResetSelectedItem();
            }

    public void HintMethod()
    {
        if (formCamFocused) return;

        // Hint 
        if (m_SubIndexPlayed > 3 && !m_IsHintDisplaying)
        {

            StartCoroutine(ShowHint());
        }
    }

    //	public static bool UnlockDoor(){
    //	
    //		return unlockedDoor;
    //	}

    private bool m_HasStampPuttingCompleted = false;

	IEnumerator AfterPuttingTheStampToEntry()
	{
		yield return new WaitForSeconds(0.2f);

	
		m_HasStampPuttingCompleted = true;


		Soundmanager2.instance.playSingle(NowToMakeSureIHaveClosedDoorAudio);
		//StartCoroutine(ShowSerialSubtitle("Now to make sure I have closed all doors and cupboards, turned off the lights and get this competition entry in the post!"));

		panda.GetComponent<FirstPersonController> ().enabled = false;

		IsSubtitleStillDisplaying =true;
		//subtitleText.text = "Now to make sure I have closed all doors and cupboards, turned off the lights and get this competition entry in the post!";
        subtitleManager.DisplaySubtitle("Now to make sure I have closed all doors and cupboards, turned off the lights and get this competition entry in the post!", Soundmanager2.instance.getEfxSource().clip.length, true);

        float _time = 0;
		while(Soundmanager2.instance.getEfxSource ().isPlaying)
		{
			_time += Time.deltaTime;
			yield return null;
		}

		if(_time < 13)
		{
			yield return new WaitForSeconds(13-_time);
		}

		panda.GetComponent<FirstPersonController> ().enabled = true;
		//subtitleText.text = "";
		IsSubtitleStillDisplaying =false;
	}
    
	IEnumerator ShowSubtitle(string text){

		if(IsSubtitleStillDisplaying) 
		{
			yield break;
		}

		IsSubtitleStillDisplaying =true;
        //subtitleText.text = text;
        if (subtitleManager)
        {
            subtitleManager.DisplaySubtitle(text, Soundmanager2.instance.getEfxSource().clip.length, true);
        }

        float _time = 0;
		while(Soundmanager2.instance.getEfxSource ().isPlaying)
		{
			_time += Time.deltaTime;
			yield return null;
		}

		if(_time < 3)
		{
			yield return new WaitForSeconds(3-_time);
		}

	//	subtitleText.text = "";
		IsSubtitleStillDisplaying =false;

	}

	IEnumerator ShowSerialSubtitle(string text){

		if(IsSubtitleStillDisplaying) 
		{
			yield break;
		}

		m_SubIndexPlayed++;

		if (m_SubIndexPlayed >= 1) {
			panda.GetComponent<FirstPersonController> ().enabled = false;
		}

		IsSubtitleStillDisplaying =true;
//		subtitleText.text = text;

        subtitleManager.DisplaySubtitle(text, Soundmanager2.instance.getEfxSource().clip.length, true);

  //      float _time = 0;
		while(Soundmanager2.instance.getEfxSource ().isPlaying)
		{
	//		_time += Time.deltaTime;
			yield return null;
		}

        //if(_time < 7)
        //{
        //	yield return new WaitForSeconds(7-_time);
        //}

        yield return new WaitForSeconds(1);

		if (m_SubIndexPlayed >= 1 && !panda.GetComponent<FirstPersonController> ().enabled) {
			panda.GetComponent<FirstPersonController> ().enabled = true;
		}

//		subtitleText.text = "";
		IsSubtitleStillDisplaying =false;

		if (m_SubIndexPlayed == 3) {
           
			m_SubIndexPlayed++;
#if !(UNITY_IOS || UNITY_ANDROID)
			Soundmanager2.instance.playSingle(NewAudioClips[3]);
			StartCoroutine (ShowSubtitle ("Press ‘K’ to be reminded of what keys to use at any time."));
#endif
		}
	}

	IEnumerator ShowSubtitle1(string text){

		IsSubtitleStillDisplaying = true;

        //		subtitleText.text = text;

        subtitleManager.DisplaySubtitle(text, Soundmanager2.instance.getEfxSource().clip.length, true);

        while (Soundmanager2.instance.getEfxSource ().isPlaying)
		{
			yield return null;
		}

        Soundmanager2.instance.playSingle(audioClip[13]);
//		subtitleText.text = "I will need a special bag for this battery!";

        subtitleManager.DisplaySubtitle("I will need a special bag for this battery!", Soundmanager2.instance.getEfxSource().clip.length, true);

        while (Soundmanager2.instance.getEfxSource ().isPlaying)
		{
			yield return null;
		}

        yield return new WaitForSeconds(1);
//		subtitleText.text = "";

		// Janidu Added
		Soundmanager2.instance.playSingle(NewAudioClips[5]);
//		subtitleText.text = "You will need to put the battery into the special bag that is located in the kitchen drawer. Combine them when they are inside your rufflesack.";

        subtitleManager.DisplaySubtitle("You will need to put the battery into the special bag that is located in the kitchen drawer. Combine them when they are inside your rufflesack.", Soundmanager2.instance.getEfxSource().clip.length, true);

        float _time = 0;
		while(Soundmanager2.instance.getEfxSource ().isPlaying)
		{
			_time += Time.deltaTime;
			yield return null;
		}
		if(_time < 7)
		{
			yield return new WaitForSeconds(7-_time);
		}
//		subtitleText.text = "";

		IsSubtitleStillDisplaying = false;
	}

	void ShowCursor(Sprite spr){
		if (cursor != null) {
			cursor.GetComponent<Image> ().sprite = spr;
           
       // if (cursor4.active == false && cursoractive == false){
			cursor.SetActive (true);
            pointerDot.SetActive(false);
       //     cursoractive = true;
        //}
		}
	}

    private void ShowStatsQues(int index)
    {
      Soundmanager2.instance.playSingle(bobSounds[index]);
    }

    public void HideQues(int index)
    {
        statsQues[index].SetActive(false);
        panda.GetComponent<MonoBehaviour>().enabled = true;
#if !(UNITY_IOS || UNITY_ANDROID)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
#endif
    }
  
}
