using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using Narrate;

public class GameController : MonoBehaviour {
	//public static GameController instance = null;
	public GameObject panda;
	public GameObject display;
	public GameObject inventoryPanel;
	private static GameObject inventory;

	public AudioClip SwitchSound;

	public Transform fpsCam;
	public GameObject clock;
	private Animator clockAC;
	public GameObject stairsHid;

	public Sprite magnifierTexture;
	public Sprite handTexture;

	public GameObject clockGlow;
	public GameObject pullyGlow;
	public GameObject cupDoorGlow;
	public GameObject ipodGlow;
	public GameObject ruckSackGlow;
	public GameObject doorGlow;
	public GameObject switchGlow;

	public Material stairMat;

	private AudioSource clockAlarmSound;

	//public Text actionText;
	public Text subtitleText;
    public SubtitleManager subtitleManager;


    public GameObject cupboard;
	private Animator cupAC;

	public GameObject stairs;
	public GameObject ipod;
	public GameObject ruckSack;
	public GameObject slippers;

	public GameObject lcurt;
	public GameObject rcurt;
	public GameObject pointerDot;

	// Flags
	private bool alarmTurnedOff = false;
	private bool isCupOpen = false;
	private bool gotStairs = false;
	private bool gotIpod = false;
	private bool gotSlippers = false;
	private bool gotRuckSack = false;
	private bool lampOn = true;
	private bool switchOn = false;
	private bool curtainsOpened = false;
	
	private bool expRS = false;
	private bool expIpod = false;
	private bool expSlippers = false;
	private bool exploredClock = false;
	private bool exploredCupboard = false;
	private bool exploredDoor = false;

	public GameObject[] lights;
	public GameObject roomLights;

	public GameObject lamp;
	public Material[] materials;

	// sound clips
	public AudioClip[] audioClips;
    // Movies

#if UNITY_WEBGL
	WebGLMovieTexture movieTexture;
	
#else
    public VideoPlayer videoPlayer;
    //public MovieTexture[] movies;
#endif

    public CanvasGroup JoyStick;

    MonoBehaviour[] behav;
    public AudioSource VideoAudioSource;

    // UI button Images
    public Sprite[] buttonImages;
	public GameObject cursor;
    public GameObject FocusCursor;

	private Vector3 midPoint = Vector3.zero;

	//public MovieManager movieManager;
	private GameObject music;

	private GameObject mainmenu;

	//Additional Instruction sounds clips by Janidu
	public AudioClip  LookAboutUsingYourMouseAudio;
	public AudioClip  PressWToMoveForwardAudio;
	public AudioClip  YouCanMoveLeftAndRighAudio;
	public AudioClip  HoldDownShiftKeyToMoveFastAudio;
	public AudioClip 	ItemPickAudio;
	public AudioClip[]  NewAudioClips;

	public bool 	 HasInitialInstructionCompleted = false;

	public GameObject InstructionUI;
    public GameObject BlackPanel;

    public GameObject StepsAtCurtain;
    public Inventory Inventory;

	//void Awake(){

		//DontDestroyOnLoad (this.gameObject);
	//}
	// Use this for initialization
	void Start () {
        Time.timeScale = 1; // Because bringing Save message, could pause the game
        mainmenu = GameObject.Find ("MainMenu");
		if (mainmenu)
			Destroy (mainmenu);
		inventory = GameObject.Find ("Inventory");

        subtitleManager.OnSubtitileDisplayBeginEvent += SubtitleManager_OnSubtitileDisplayBeginEvent;
        subtitleManager.OnSubtitileDisplayEndEvent += SubtitleManager_OnSubtitileDisplayEndEvent;

        //inventoryPanel = GameObject.Find ("InventoryPanel");
#if !(UNITY_IOS || UNITY_ANDROID)
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
#endif

        midPoint = new Vector3 (Screen.width / 2f, Screen.height / 2f, 0f);
		clockAC = clock.GetComponent<Animator> ();
		cupAC = cupboard.GetComponent<Animator> ();
		clockAlarmSound = clock.GetComponent<AudioSource> ();
        //		movieManager = new MovieManager (panda, display, pointerDot, inventory, inventoryPanel, VideoPlayerObj);

        behav = panda.GetComponents<MonoBehaviour>();
#if UNITY_WEBGL

		movieTexture = new WebGLMovieTexture("StreamingAssets/panda_wakeup.mp4");
		display.transform.GetChild(0).gameObject.SetActive (true);
		display.transform.GetChild(1).gameObject.SetActive(true);

		display.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = movieTexture;
		//StartCoroutine(PlayClip ());

		//movieManager.SetPlayBackComplete (false);
        m_MovieFinishedPlaying = false;

		inventory.GetComponent<Inventory> ().enabled = false;
		inventoryPanel.SetActive (false);


		pointerDot.SetActive (false);
		
		foreach(MonoBehaviour b in behav){
			b.enabled = false;
		}
#else
        videoPlayer.started += VideoPlayer_Started;
        videoPlayer.prepareCompleted += VideoPlayer_PrepareCompleted;
        videoPlayer.loopPointReached += EndReachedOnVideo;
        PlayVideo(Application.streamingAssetsPath + "/panda_wakeup.mp4");
		//StartCoroutine(movieManager.PlayClip (movies [0]));
        //clockAlarmSound.PlayDelayed(movies[0].Duration);
        //if (ButtonChange.musicOn)
        //{
        //    SoundManager.instance.playMusic(movies[0].Duration);
        //}

#endif
        clockGlow.SetActive (true);
		switchGlow.SetActive(true);
    }

    void VideoPlayer_PrepareCompleted(VideoPlayer source)
    {
        StartCoroutine(RemoveBlackPanel());
    }

    IEnumerator RemoveBlackPanel()
    {
        yield return new WaitForSeconds(1f);
        BlackPanel.SetActive(false);
    }


    void VideoPlayer_Started(VideoPlayer source)
    {

    }


    void SubtitleManager_OnSubtitileDisplayBeginEvent()
    {
        JoyStickSettings(false);
    }


    void SubtitleManager_OnSubtitileDisplayEndEvent()
    {
        JoyStickSettings(true);
    }


    void PlayVideo(string clipName)
    {
        display.SetActive(true);
        display.transform.GetChild(1).gameObject.SetActive(true);
        FocusCursor.GetComponent<Image>().enabled = false;
        JoyStickSettings(false);
        videoPlayer.gameObject.SetActive(true);
        inventory.GetComponent<Inventory>().enabled = false;
        inventoryPanel.SetActive(false);
        pointerDot.SetActive(false);
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
        display.SetActive (false);
        display.transform.GetChild(1).gameObject.SetActive(false);
        //blackLine.SetActive(false);
        FocusCursor.GetComponent<Image>().enabled = true; // This and PointerDot is same
        pointerDot.SetActive(true);
        inventoryPanel.SetActive(false);
        inventory.GetComponent<Inventory>().enabled = true;

        //if (_vp.url == Application.streamingAssetsPath + "/panda_wakeup.mp4")
        //{
        //    clockAlarmSound.Play();
        //    if (ButtonChange.musicOn)
        //    {
        //        SoundManager.instance.playMusic(0.1f);
        //    }
        //}

        if (!alarmTurnedOff)
        {
            clockAC.enabled = true;
            clockAC.SetBool("off", false);
            clockAlarmSound.Play();
            if (ButtonChange.musicOn)
            {
                SoundManager.instance.playMusic(0.1f);
            }

#if UNITY_IOS || UNITY_ANDROID
            RoomInstructions.RoomInstructionNo = 8;
#endif
        }
        m_MovieFinishedPlaying = true;
        videoPlayer.gameObject.SetActive(false);
        JoyStickSettings(true);

        if(_vp.url == Application.streamingAssetsPath + "/panda_curtain.mp4")
        {
            StepsAtCurtain.SetActive(true);
        }
    }

    private void JoyStickSettings(bool _activate)
    {
#if UNITY_IOS || UNITY_ANDROID
        if(_activate)
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

    public GameObject GetInventPanel(){
		return inventoryPanel;
	}
		
	private Coroutine m_InsCoroutine = null;
	private bool m_ExecuteTheCode = false;
	// Update is called once per frame
	void Update () {
        //DontDestroyOnLoad (gameObject);

#if UNITY_WEBGL
		//if(display.transform.GetChild(0).gameObject.activeSelf)
        if(!m_MovieFinishedPlaying)
		{	movieTexture.Update();

			if (Mathf.Approximately (movieTexture.time, movieTexture.duration)) {
				foreach(MonoBehaviour b in behav){
					b.enabled = true;
				}

				display.transform.GetChild(0).gameObject.SetActive (false);
				display.transform.GetChild(1).gameObject.SetActive(false);

				pointerDot.SetActive (true);
				inventory.GetComponent<Inventory> ().enabled = true;

				//movieManager.SetPlayBackComplete (true);
                m_MovieFinishedPlaying = true;

				if(!alarmTurnedOff){
        clockAC.enabled = true;
         clockAC.SetBool("off", false);
					clockAlarmSound.Play ();
					if (ButtonChange.musicOn) {
						SoundManager.instance.playMusic (0.1f);
					}
				}

        if(_vp.url == Application.streamingAssetsPath + "/panda_curtain.mp4")
        {
            StepsAtCurtain.SetActive(true);
        }
			}
		}
#endif

        if (isInstructionsStillPlaying) return;

		if(m_MovieFinishedPlaying)
		{
			if(RoomInstructions.RoomInstructionNo == 0)
			{
				RoomInstructions.RoomInstructionNo++;

				SoundManager.instance.playSingle(LookAboutUsingYourMouseAudio);
				m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Look about using your mouse or touchpad!", 0));
			}
			else if(RoomInstructions.RoomInstructionNo == 2)
			{
				RoomInstructions.RoomInstructionNo++;

				SoundManager.instance.playSingle(PressWToMoveForwardAudio);

				m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Press \"W\" to move forwards and \"S\" to move backwards!", 0));
			}
			else if(RoomInstructions.RoomInstructionNo == 4)
			{
				RoomInstructions.RoomInstructionNo++;

				SoundManager.instance.playSingle(YouCanMoveLeftAndRighAudio);

				m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("You can move left and right using \"A\" and \"D\" keys!", 0));
			}
			else if(RoomInstructions.RoomInstructionNo == 6)
			{
				RoomInstructions.RoomInstructionNo++;

				SoundManager.instance.playSingle(HoldDownShiftKeyToMoveFastAudio);

				m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Hold down the SHIFT key to move faster!", 0));
			}
			else if(RoomInstructions.RoomInstructionNo == 8)
			{
				RoomInstructions.RoomInstructionNo++;

				SoundManager.instance.playSingle(NewAudioClips[0]);

				m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Now look at the alarm clock.", 0));
			}
			else if(RoomInstructions.RoomInstructionNo == 18)
			{
				RoomInstructions.RoomInstructionNo++;

				SoundManager.instance.playSingle(NewAudioClips[4]);

				m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Try looking up at the glowing curtain pully.", 0));
			}
			else if(RoomInstructions.RoomInstructionNo == 21)
			{
				RoomInstructions.RoomInstructionNo++;

				SoundManager.instance.playSingle(NewAudioClips[5]);

				m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Amanda cannot reach it. Perhaps there are some steps somewhere? Have a look in the wardrobe.", 0));
			}
			else if(RoomInstructions.RoomInstructionNo == 26)
			{
				RoomInstructions.RoomInstructionNo++;

				SoundManager.instance.playSingle(NewAudioClips[7]);

				m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Feel free to try and leave the bedroom by using the door.", 0));
			}
			else if(RoomInstructions.RoomInstructionNo == 30)
			{
				RoomInstructions.RoomInstructionNo++;

				SoundManager.instance.playSingle(NewAudioClips[8]);

				m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Perhaps some slippers might help? Did you see them in the wardrobe?", 0));
			}
			else if(RoomInstructions.RoomInstructionNo == 34)
			{
				RoomInstructions.RoomInstructionNo++;

				ruckSackGlow.SetActive (true);

				SoundManager.instance.playSingle(NewAudioClips[9]);

				m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Now to collect two essential items. Firstly, pick up the rufflesack on the floor.", 0));
			}
			else if(RoomInstructions.RoomInstructionNo == 39)
			{
				RoomInstructions.RoomInstructionNo++;

				ipodGlow.SetActive (true);

				SoundManager.instance.playSingle(NewAudioClips[11]);

				m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Now take the EYEPOD from the chair.", 0));
			}
			else if(RoomInstructions.RoomInstructionNo == 45)
			{
				RoomInstructions.RoomInstructionNo++;

				SoundManager.instance.playSingle(NewAudioClips[13]);

				m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Objects can be selected and used from inside your rufflesack and on ocassion you will need to combine some items before they can be used.", 0));
			}
			else if(RoomInstructions.RoomInstructionNo == 47)
			{
				RoomInstructions.RoomInstructionNo++;

				SoundManager.instance.playSingle(NewAudioClips[14]);

				m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Now leave the bedroom and search for Amanda’s breakfast cereal in the kitchen.", 0));
			}
		}


        SelectMethod();

        if (gotRuckSack && gotIpod && gotSlippers && !switchOn && !isCupOpen) {
			if(doorGlow != null)
			doorGlow.SetActive(true);
		}
#if !(UNITY_IOS || UNITY_ANDROID)
        if (/*RoomInstructions.RoomInstructionNo > 27 &&*/ Input.GetKeyDown(KeyCode.K))
        {

            if (!InstructionUI.activeSelf)
            {
                InstructionUI.SetActive(true);

                foreach (MonoBehaviour m in panda.GetComponents<MonoBehaviour>())
                    m.enabled = false;
            }
            else
            {
                InstructionUI.SetActive(false);

                foreach (MonoBehaviour m in panda.GetComponents<MonoBehaviour>())
                    m.enabled = true;
            }


        }
#endif
    }


    public void SelectMethod(bool m_ClickedStatus = false)
    {
#if !(UNITY_IOS || UNITY_ANDROID)
        m_ClickedStatus = Input.GetMouseButtonDown(0);
#endif
        Ray ray = Camera.main.ScreenPointToRay(midPoint);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10f))
        {
            switch (hit.collider.tag)
            {
                case "clock":

                    m_ExecuteTheCode = false;
                    if (HasInitialInstructionCompleted)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else if (RoomInstructions.RoomInstructionNo == 10 || RoomInstructions.RoomInstructionNo == 12 || RoomInstructions.RoomInstructionNo == 14 || RoomInstructions.RoomInstructionNo == 16)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else
                    {
                        m_ExecuteTheCode = false;
                    }

                    if (m_ExecuteTheCode)
                    {
                        if (!exploredClock || alarmTurnedOff)
                        {
                            ShowCursor(magnifierTexture);


                        }
                        else
                        {
                            ShowCursor(handTexture);

                        }

                        if (m_ClickedStatus && !alarmTurnedOff && exploredClock)
                        {
                            clockAC.SetBool("off", true);
                            clockAlarmSound.Stop();
                            clock.transform.GetChild(0).gameObject.SetActive(false);
                            pullyGlow.SetActive(true);
                            clockGlow.SetActive(false);
                            alarmTurnedOff = true;
                            exploredClock = true;

                            if (RoomInstructions.RoomInstructionNo == 16)
                            {
                                RoomInstructions.RoomInstructionNo++;

                                SoundManager.instance.playSingle(NewAudioClips[3]);
                                m_InsCoroutine = StartCoroutine(ShowInstructionSubtitles("Now it is time to help Amanda Panda open the curtains before she leaves her bedroom to have her breakfast.", 0));

                            }

                        }
                        else if (m_ClickedStatus)
                        {
                            SoundManager.instance.playSingle(audioClips[4]);
                            if (RoomInstructions.RoomInstructionNo == 12)
                                RoomInstructions.RoomInstructionNo++;
                            exploredClock = true;
                            StartCoroutine(ShowSubtitle("My retro alarm clock – never fails!"));
                        }
                    }
                    break;
                case "pully":
                    m_ExecuteTheCode = false;
                    if (HasInitialInstructionCompleted)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else if (RoomInstructions.RoomInstructionNo == 20)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else
                    {
                        m_ExecuteTheCode = false;
                    }

                    if (m_ExecuteTheCode)
                    {
                        if (!curtainsOpened)
                            ShowCursor(handTexture);

                        if (m_ClickedStatus)
                        {
                            if (!gotStairs)
                            {
                                if (!(SoundManager.instance.getEfxSource().isPlaying))
                                    SoundManager.instance.playSingle(audioClips[7]);
                                StartCoroutine(ShowSubtitle("Argh..."));
                                cupDoorGlow.SetActive(true);
                            }
                        }
                    }
                    break;
                case "cupboard":

                    m_ExecuteTheCode = false;
                    if (HasInitialInstructionCompleted)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else if (RoomInstructions.RoomInstructionNo == 23)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else
                    {
                        m_ExecuteTheCode = false;
                    }

                    if (m_ExecuteTheCode)
                    {
                        if (exploredCupboard)
                        {
                            ShowCursor(handTexture);
                        }
                        else
                        {
                            ShowCursor(magnifierTexture);
                        }

                        if (m_ClickedStatus && exploredCupboard)
                        {
                            isCupOpen = !isCupOpen;
                            cupAC.SetBool("isOpen", isCupOpen);
                            cupAC.GetComponent<AudioSource>().Play();
                            cupDoorGlow.SetActive(false);

                            if (RoomInstructions.RoomInstructionNo == 23)
                            {
                                RoomInstructions.RoomInstructionNo++;

                                StartCoroutine(PlayUseTheStepsAfterADelay());
                                //                          SoundManager.instance.playSingle(NewAudioClips[6]);
                                //                          m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Use the steps to help Amanda open the curtains", 0));

                            }
                        }
                        else if (m_ClickedStatus)
                        {
                            SoundManager.instance.playSingle(audioClips[11]);
                            StartCoroutine(ShowSubtitle("Where I keep all my stuff!"));
                            exploredCupboard = true;
                        }
                    }
                    break;
                case "stairs":
                    m_ExecuteTheCode = false;
                    if (HasInitialInstructionCompleted)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else if (RoomInstructions.RoomInstructionNo == 25)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else
                    {
                        m_ExecuteTheCode = false;
                    }

                    if (m_ExecuteTheCode)
                    {
                        ShowCursor(handTexture);

                        if (m_ClickedStatus)
                        {
                            stairs.SetActive(false);
                            //Inventory.instance.AddItem(buttonImages[3],hit.collider.tag);
                            stairsHid.SetActive(true);
                            ruckSackGlow.SetActive(false);
                            gotStairs = true;
                            if (!curtainsOpened)
                            {
#if UNITY_WEBGL
                            display.transform.GetChild(0).gameObject.SetActive (true);
                            display.transform.GetChild(1).gameObject.SetActive(true);

                            Destroy(display.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture);
                            System.GC.Collect();
                                 
                            movieTexture = new WebGLMovieTexture("StreamingAssets/panda_curtain.mp4");
                            display.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture = movieTexture;

                            //movieManager.SetPlayBackComplete (false);
                                m_MovieFinishedPlaying = false;
                            inventory.GetComponent<Inventory> ().enabled = false;
                            inventoryPanel.SetActive (false);

                            pointerDot.SetActive (false);

                            foreach(MonoBehaviour b in behav){
                                b.enabled = false;
                            }


#else
                                //StartCoroutine(movieManager.PlayClip (movies [1]));
                                PlayVideo(Application.streamingAssetsPath + "/panda_curtain.mp4");
#endif
                                lcurt.transform.localPosition = new Vector3(-9.22f, -2.58f, -30.56f);
                                rcurt.transform.localPosition = new Vector3(-21.48f, -2.58f, -30.56f);
                                stairsHid.AddComponent<MeshRenderer>();
                                stairsHid.GetComponent<Renderer>().material = stairMat;
                                panda.transform.position = new Vector3(0.3f, 3.68f, 2f);
                                pullyGlow.SetActive(false);
                                curtainsOpened = true;


                                if (RoomInstructions.RoomInstructionNo == 25)
                                {
                                    RoomInstructions.RoomInstructionNo = 26;
                                }
                            }
                        }
                    }
                    break;
                case "door":
                    m_ExecuteTheCode = false;
                    if (HasInitialInstructionCompleted)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else if (RoomInstructions.RoomInstructionNo == 28 || RoomInstructions.RoomInstructionNo == 29 || RoomInstructions.RoomInstructionNo == 49)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else
                    {
                        m_ExecuteTheCode = false;
                    }

                    if (m_ExecuteTheCode)
                    {
                        if (exploredDoor)
                        {
                            ShowCursor(handTexture);
                        }
                        else
                        {
                            ShowCursor(magnifierTexture);
                        }
                        //actionText.text = "Exit the Room";
                        if (m_ClickedStatus && exploredDoor)
                        {
                            if (!gotSlippers)
                            {
                                if (RoomInstructions.RoomInstructionNo == 29)
                                {
                                    SoundManager.instance.playSingle(audioClips[12]);
                                    StartCoroutine(ShowSubtitle("Floor’s too cold!"));
                                }
                            }
                            else if (gotSlippers && !(gotRuckSack && gotIpod))
                            {
                                SoundManager.instance.playSingle(audioClips[5]);
                                StartCoroutine(ShowSubtitle("No, no, no!"));
                                ipodGlow.SetActive(true);
                                ruckSackGlow.SetActive(true);
                            }
                            else if (curtainsOpened == false)
                            {
                                SoundManager.instance.playSingle(audioClips[5]);
                                StartCoroutine(ShowSubtitle("No, no, no!"));
                            }
                            else
                            {
                                if (switchOn || isCupOpen)
                                {
                                    SoundManager.instance.playSingle(audioClips[5]);
                                    StartCoroutine(ShowWarningSubtitle("No, no, no!"));
                                }
                                else
                                {
                                    SoundManager.instance.getMusicSource().Stop();
                                    gotRuckSack = false;
                                    //Application.LoadLevel (4);
                                    //Destroy(display.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.mainTexture);
                                    //System.GC.Collect();
                                    hit.collider.gameObject.GetComponent<AudioSource>().Play();
                                    StartCoroutine(LoadSceneAfterClearingGarbage());

                                }
                            }
                        }
                        else if (m_ClickedStatus)
                        {
                            SoundManager.instance.playSingle(audioClips[9]);
                            StartCoroutine(ShowSubtitle("This leads to the hallway..."));
                            exploredDoor = true;
                        }
                    }
                    break;
                case "ipod":

                    m_ExecuteTheCode = false;
                    if (HasInitialInstructionCompleted)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else if (RoomInstructions.RoomInstructionNo == 41 || RoomInstructions.RoomInstructionNo == 42)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else
                    {
                        m_ExecuteTheCode = false;
                    }

                    if (m_ExecuteTheCode)
                    {
                        if (!expIpod)
                            ShowCursor(magnifierTexture);
                        else
                            ShowCursor(handTexture);
                        //              if(!gotIpod){
                        //                  actionText.text = "Pick up";
                        //              }
                        if (m_ClickedStatus && !gotIpod && expIpod)
                        {

                            SendMessageUpwards("PlaySourceClip", ItemPickAudio, SendMessageOptions.DontRequireReceiver);

                            ipod.SetActive(false);
                            Inventory.AddItem(buttonImages[0], hit.collider.tag);
                            ipodGlow.SetActive(false);
                            gotIpod = true;
                            Debug.Log("Ipod " + RoomInstructions.RoomInstructionNo);
                            if (RoomInstructions.RoomInstructionNo == 42)
                            {
                                RoomInstructions.RoomInstructionNo++;
#if UNITY_IOS || UNITY_ANDROID
                                JoyStick.gameObject.transform.Find("RuffleSackOpen").gameObject.SetActive(true);
#endif
                                SoundManager.instance.playSingle(NewAudioClips[12]);
                                m_InsCoroutine = StartCoroutine(ShowInstructionSubtitles("Press the ‘R’ key at any time to look inside your rufflesack.", 0));

                            }
                        }
                        else if (m_ClickedStatus)
                        {
                            expIpod = true;
                            SoundManager.instance.playSingle(audioClips[8]);
                            StartCoroutine(ShowSubtitle("Never leave home without your EYEPOD!"));
                        }
                    }
                    break;
                case "poster":
                    if (HasInitialInstructionCompleted)
                    {
                        ShowCursor(magnifierTexture);
                        if (m_ClickedStatus)
                        {
                            if (!SoundManager.instance.getEfxSource().isPlaying)
                            {
                                SoundManager.instance.playSingle(audioClips[0]);
                                StartCoroutine(ShowSubtitle("Ahhh…those guys!"));
                            }
                        }
                    }
                    break;
                case "rucksack":
                    m_ExecuteTheCode = false;
                    if (HasInitialInstructionCompleted)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else if (RoomInstructions.RoomInstructionNo == 36 || RoomInstructions.RoomInstructionNo == 37)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else
                    {
                        m_ExecuteTheCode = false;
                    }

                    if (m_ExecuteTheCode)
                    {
                        if (!expRS)
                            ShowCursor(magnifierTexture);
                        else
                            ShowCursor(handTexture);
                        //              if(!gotRuckSack){
                        //                  actionText.text = "Pick up";
                        //              }

                        if (RoomInstructions.RoomInstructionNo == 36)
                        {
                            if (m_ClickedStatus)
                            {
                                expRS = true;
                                SoundManager.instance.playSingle(audioClips[1]);
                                StartCoroutine(ShowSubtitle("My trusty rufflesack!"));
                            }
                        }
                        else if (RoomInstructions.RoomInstructionNo == 37)
                        {
                            if (m_ClickedStatus && !gotRuckSack && expRS)
                            {
                                ruckSack.SetActive(false);

                                gotRuckSack = true;

                                SendMessageUpwards("PlaySourceClip", ItemPickAudio, SendMessageOptions.DontRequireReceiver);

                                RoomInstructions.RoomInstructionNo++;

                                SoundManager.instance.playSingle(NewAudioClips[10]);

                                m_InsCoroutine = StartCoroutine(ShowInstructionSubtitles("Now that you are carrying your rufflesack, this is used to store any items that you pick up during your journey.", 0));

                            }
                        }
                    }
                    break;
                case "bin":
                    if (HasInitialInstructionCompleted)
                    {
                        ShowCursor(magnifierTexture);
                        if (m_ClickedStatus)
                        {
                            SoundManager.instance.playSingle(audioClips[3]);
                            StartCoroutine(ShowSubtitle("My bedroom bin – which seems to fill up all too quickly!"));
                        }
                    }
                    break;
                case "slippers":

                    m_ExecuteTheCode = false;
                    if (HasInitialInstructionCompleted)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else if (RoomInstructions.RoomInstructionNo == 32 || RoomInstructions.RoomInstructionNo == 33)
                    {
                        m_ExecuteTheCode = true;
                    }
                    else
                    {
                        m_ExecuteTheCode = false;
                    }

                    if (m_ExecuteTheCode)
                    {
                        if (!expSlippers)
                            ShowCursor(magnifierTexture);
                        else
                            ShowCursor(handTexture);

                        if (m_ClickedStatus && !gotSlippers && expSlippers)
                        {
                            slippers.SetActive(false);

                            SendMessageUpwards("PlaySourceClip", ItemPickAudio, SendMessageOptions.DontRequireReceiver);

                            SoundManager.instance.playSingle(audioClips[13]);
                            StartCoroutine(ShowSubtitle("Much more cosy!"));
                            //Inventory.instance.AddItem(buttonImages[2],hit.collider.tag);
                            gotSlippers = true;
                        }
                        else if (m_ClickedStatus)
                        {
                            expSlippers = true;
                            SoundManager.instance.playSingle(audioClips[10]);
                            StartCoroutine(ShowSubtitle("Useful where there are cold floors!"));
                        }
                    }
                    break;
                case "frame":
                    if (HasInitialInstructionCompleted)
                    {
                        ShowCursor(magnifierTexture);
                        if (m_ClickedStatus)
                        {
                            if (!SoundManager.instance.getEfxSource().isPlaying)
                            {
                                SoundManager.instance.playSingle(audioClips[6]);
                                StartCoroutine(ShowSubtitle("That was me in year 4!"));
                            }
                        }
                    }
                    break;
                case "switch":
                    if (HasInitialInstructionCompleted)
                    {
                        ShowCursor(handTexture);
                        //              if(switchOn){
                        //                  actionText.text = "Turn off lights";
                        //              }else{
                        //                  actionText.text = "Turn on lights";
                        //              }
                        if (m_ClickedStatus)
                        {
                            if (switchOn)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    lights[i].GetComponent<Renderer>().material = materials[2];
                                    lights[i].transform.GetChild(0).GetComponent<Renderer>().material = materials[4];
                                }
                                SendMessageUpwards("PlaySourceClip", SwitchSound, SendMessageOptions.DontRequireReceiver);
                                switchGlow.SetActive(false);
                            }
                            else
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    lights[i].GetComponent<Renderer>().material = materials[3];
                                    lights[i].transform.GetChild(0).GetComponent<Renderer>().material = materials[5];
                                }
                                SoundManager.instance.playSingle(audioClips[2]);
                                StartCoroutine(ShowSubtitle("There’s no need for lights when I can open the curtains!"));
                                SendMessageUpwards("PlaySourceClip", SwitchSound, SendMessageOptions.DontRequireReceiver);
                                switchGlow.SetActive(true);
                            }
                            switchOn = !switchOn;
                            roomLights.SetActive(switchOn);
                            lampOn = !lampOn;
                            if (lampOn)
                            {
                                lamp.GetComponent<Renderer>().material = materials[0];
                            }
                            else
                            {
                                lamp.GetComponent<Renderer>().material = materials[1];
                            }
                        }
                    }
                    break;
                default:
                    if (cursor != null)
                    {
                        cursor.SetActive(false);
                        FocusCursor.SetActive(true);
                    }
                    //actionText.text = "";
                    break;
            }
        }
        else
        {
            if (cursor != null)
            {
                cursor.SetActive(false);
                FocusCursor.SetActive(true);
            }
        }

#if !(UNITY_IOS || UNITY_ANDROID)
#else
        m_ClickedStatus = false;
#endif
    }

    IEnumerator LoadSceneAfterClearingGarbage()
	{
		yield return new WaitForSeconds(1);

		SceneManager.LoadScene("Kitchen");
	}

	IEnumerator PlayUseTheStepsAfterADelay()
	{
		yield return new WaitForSeconds (1f);
		SoundManager.instance.playSingle(NewAudioClips[6]);
		m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("Use the steps to help Amanda open the curtains.", 0));
	}

	bool isInstructionsStillPlaying = false;
	IEnumerator ShowInstructionSubtitles(string text, float _delayToWaitForNextInstruction)
	{
		if (RoomInstructions.RoomInstructionNo >= 11) {
			panda.GetComponent<FirstPersonController> ().enabled = false;
		}
		isInstructionsStillPlaying = true;
//		subtitleText.text = text;

        subtitleManager.DisplaySubtitle(text, SoundManager.instance.getEfxSource().clip.length, true);

        float _time = 0;
		while(SoundManager.instance.getEfxSource ().isPlaying)
		{
			_time += Time.deltaTime;
			yield return null;
		}

		if (RoomInstructions.RoomInstructionNo != 9) { // Now Look at the Alarm clock
			if (_time < 7) {
				yield return new WaitForSeconds (7 - _time);
			}
		}

//		subtitleText.text = "";

#if !(UNITY_IOS || UNITY_ANDROID)
		if(RoomInstructions.RoomInstructionNo == 27) {
			SoundManager.instance.playSingle(NewAudioClips[16]);
            //			subtitleText.text = "Press ‘K’ to be reminded of what keys to use at any time.";
            subtitleManager.DisplaySubtitle("Press ‘K’ to be reminded of what keys to use at any time.", NewAudioClips[16].length, true);
          
            while (SoundManager.instance.getEfxSource().isPlaying)
            {
                yield return null;
            }

		}
#endif

		if (RoomInstructions.RoomInstructionNo >= 11 && !panda.GetComponent<FirstPersonController> ().enabled) {
			panda.GetComponent<FirstPersonController> ().enabled = true;
		}


		isInstructionsStillPlaying = false;

		yield return new WaitForSeconds(_delayToWaitForNextInstruction);

		RoomInstructions.RoomInstructionNo++;

		if (RoomInstructions.RoomInstructionNo == 49) {
			HasInitialInstructionCompleted = true;
		}
	}

	IEnumerator ShowSubtitle(string text)
    {
		isInstructionsStillPlaying = true;
        //       subtitleText.text = text;
        subtitleManager.DisplaySubtitle(text, SoundManager.instance.getEfxSource().clip.length, true);
        while (SoundManager.instance.getEfxSource().isPlaying)
        {
            yield return null;
        }
        //yield return new WaitForSeconds(3);
        yield return new WaitForSeconds(1);
   //     subtitleText.text = "";

		if(RoomInstructions.RoomInstructionNo == 13)
		{
			yield return new WaitForSeconds(1);
			RoomInstructions.RoomInstructionNo = 14;
		}
		else if(RoomInstructions.RoomInstructionNo == 20)
		{
			yield return new WaitForSeconds(1);
			RoomInstructions.RoomInstructionNo = 21;
		}
		else if(RoomInstructions.RoomInstructionNo == 28)
		{
			yield return new WaitForSeconds(1);
			RoomInstructions.RoomInstructionNo = 29;
		}
		else if(RoomInstructions.RoomInstructionNo == 29)
		{
			yield return new WaitForSeconds(1);
			RoomInstructions.RoomInstructionNo = 30;
		}
		else if(RoomInstructions.RoomInstructionNo == 32)
		{
			yield return new WaitForSeconds(1);
			RoomInstructions.RoomInstructionNo = 33;
		}
		else if(RoomInstructions.RoomInstructionNo == 33)
		{
			yield return new WaitForSeconds(1);
			RoomInstructions.RoomInstructionNo = 34;
		}
		else if(RoomInstructions.RoomInstructionNo == 36)
		{
			yield return new WaitForSeconds(1);
			RoomInstructions.RoomInstructionNo = 37;
		}
		else if(RoomInstructions.RoomInstructionNo == 41)
		{
			yield return new WaitForSeconds(1);
			RoomInstructions.RoomInstructionNo = 42;
		}

		isInstructionsStillPlaying = false;
	}

    IEnumerator ShowWarningSubtitle(string text)
    {
		isInstructionsStillPlaying = true;
        switch (text)
        {
            case "No, no, no!":

                //subtitleText.text = text;
                subtitleManager.DisplaySubtitle(text, SoundManager.instance.getEfxSource().clip.length, true);
                float _time = 0;
			while(SoundManager.instance.getEfxSource ().isPlaying)
			{
				_time += Time.deltaTime;
				yield return null;
			}
			if (_time < 3) {
				yield return new WaitForSeconds (3 - _time);
			}
                //yield return new WaitForSeconds(3);
			SoundManager.instance.playSingle (NewAudioClips[15]);
               // subtitleText.text = "Always be sure to close ANY doors or cupboards and switch off the lights after use!";
                subtitleManager.DisplaySubtitle("Always be sure to close ANY doors or cupboards and switch off the lights after use!", NewAudioClips[15].length, true);
                while (SoundManager.instance.getEfxSource().isPlaying)
                {
                    yield return null;
                }
                //yield return new WaitForSeconds(5);
                // subtitleText.text = "";

                break;

            default:

                // subtitleText.text = text;
                subtitleManager.DisplaySubtitle(text, SoundManager.instance.getEfxSource().clip.length, true);
                while (SoundManager.instance.getEfxSource().isPlaying)
                {
                    yield return null;
                }
               // yield return new WaitForSeconds(3);
              //  subtitleText.text = "";

                break;
        }

		isInstructionsStillPlaying = false;
    }

	public bool isRuckSackCollected(){
		return gotRuckSack;
	}

	void ShowCursor(Sprite spr){
		if (cursor != null) {

			if(spr.name == magnifierTexture.name)
			{
				if(RoomInstructions.RoomInstructionNo == 10)
				{
					RoomInstructions.RoomInstructionNo++;
					SoundManager.instance.getEfxSource().Stop();
					SoundManager.instance.playSingle(NewAudioClips[1]);

					m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("During play, if you see a magnifying glass it " +
						"means you can examine something. Use the left click function of your mouse or touchpad for this purpose.", 0));
				}
			}
			else if(spr.name == handTexture.name)
			{
				if(RoomInstructions.RoomInstructionNo == 14)
				{
					RoomInstructions.RoomInstructionNo++;
					SoundManager.instance.getEfxSource().Stop();
					SoundManager.instance.playSingle(NewAudioClips[2]);

					m_InsCoroutine = StartCoroutine (ShowInstructionSubtitles ("When you see the hand icon, it means something can be used or collected. Again, use the left click function.", 0));

				}
			}
			cursor.GetComponent<Image> ().sprite = spr;
			cursor.SetActive (true);
            FocusCursor.SetActive(false);

        }
	}

}
