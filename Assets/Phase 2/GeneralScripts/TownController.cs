using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;
using System;
using Narrate;
using System.Text;

public class TownController : MonoBehaviour {

	#region Singleton
	private static TownController m_Instane;
	private TownController(){}
	public static TownController GetInstance()
	{
		return m_Instane;
	}
	#endregion Singleton

	#region General

	public AudioSource	PandaAdHocAudio;
	public AudioSource	TownControllerAudio;
	public AudioClip	NeedRhinoAudio;
	public string		NeedRhinoSubtitle;
	public AudioClip	NeedLitterBeltAudio;
	public string 		NeedLitterBeltSubtitle;
	public AudioClip	NeedLitterPickerAudio;
	public string 		NeedLitterPickerSubtitle;
	public AudioClip	GoBackToPlayAreaAudio;
	public string 		GoBackToPlayAreaSubtitle;

	public AudioClip    RealLifeCrossingAudio;
	public AudioClip    MapInfoAudio;

	public AudioClip	NeedHugiAudio;
	public string		NeedHugiSubtitle;
	public AudioClip	OhLeakIMustFixThis;
	public string 		OhLeakIMustFixThisSubtitle;

	public AudioClip	NeedMonkeyWrenchAudio;
	public string 		NeedMonkeyWrenchSubtitle;
	public AudioClip	NeedBroomAudio;
	public string 		NeedBroomSubtitle;
	public AudioClip	NeedPlungerAudio;
	public string 		NeedPlungerSubtitle;
	public AudioClip	SelectMonkeyWrenchAudio;
	public AudioClip	NowClearTheDrainAudio;
	public string 		NowClearTheDrainSubtitle;
	private bool 		m_HasPlayedNowClearTheDrainAudio = false;

	public AudioClip	NeedGorrilaAudio;
	public string		NeedGorillaSubtitle;
	public AudioClip	WhereIsCaterpillarAudio;
	public string 		WhereIsCaterpillarString;
	public AudioClip	IHaveToFineSamAudio;
	public string 		IHaveToFineSamSubtitle;

	public AudioClip	NeedClothForSamAudio;
	public string 		NeedClothForSamSubtitle;
	public AudioClip	HeadBackToCarParkAudio;
	public string 		HeadBackToCarParkSubtitle;

	public AudioClip	StopTheSmokeWithClothAudio;
	public AudioClip	NeedCaterpillarAudio;
	public string 		YouNeedCaterpillarSubtitle;

	public AudioClip	NeedCarKeysAudio;
	public string 		NeedCarKeysSubtitle;
	public AudioClip    NeedClothThatSamUsedAudio;
	public string 		NeedClothThatSamUsedSubtitle;
	public AudioClip	NeedPrime8SuperRemoverAudio;
	public string 		NeedPrime8SuperRemoverSubtitle;

	public AudioClip 	SelectBroomAndPlungerAudio;
	public AudioClip 	CombineSuperRemoverWithClothAudio;
	public AudioClip  	PostTheEntryAudio;

	public AudioSource	BackGroundAudio;

	public bool 		HasQuizStarted = false;

	public Image 		LoadingImgTop;
	public Image 		LoadingImg;
	public Text 		LoadingTxt;
	public float		LoadingPercentage;

	public GameObject	RainBasic;

	void Awake()
	{
		m_Instane = this;

#if UNITY_EDITOR
		Application.runInBackground = true;

        //TODO : Remove
        if (SaveDataStatic.MissionList.Count == 0)
        {
            SaveDataStatic.MissionList.Add("Complete recycling competition", "null");
            SaveDataStatic.MissionList.Add("Collect Litter!", "null");
            SaveDataStatic.MissionList.Add("Repair pipe and clear that drain!", "null");
            SaveDataStatic.MissionList.Add("Help Tiny Tiger with Sapling Tree!", "null");
            SaveDataStatic.MissionList.Add("Stop the smoking car!", "null");
            SaveDataStatic.MissionList.Add("Help Postman to clear Graffiti!", "null");
        }
#endif

    }

    private void Start()
    {
#if UNITY_IOS || UNITY_ANDROID
        subtitleManager.OnSubtitileDisplayBeginEvent += SubtitleManager_OnSubtitileDisplayBeginEvent;
        subtitleManager.OnSubtitileDisplayEndEvent += SubtitleManager_OnSubtitileDisplayEndEvent;
#endif

//       
        Time.timeScale = 1; // Because bringing Save message, could pause the game. 
    }

    public void StartAfterObjectsLoaded()
	{
		LoadingImg.gameObject.transform.parent.gameObject.SetActive(false);

		BackGroundAudio.Play ();

        StatScreen.alpha = 1;

        //ScoreTxt.text = "";
        //ScoreUI.SetActive(false);
        m_SavedTime = PlayerPrefs.GetFloat("GameTime", 0);
        SetCharacterPositions(); 
        ReloadToCharacterPositions();

		m_LitterCollectGamePreString = "The Rhino is a litter and packaging expert. Use him to collect all " +
			"the litter in under 60 seconds and enable Amanda to carry on towards the post box!";
		m_PipeRepairGamePreString = "Hugi Hippo has great knowledge of water related items. Use him to fix the leak. " +
			"Select a suitable replacement piece with the mouse, rotate as needed and move into place.";
		m_SaplingTreeGamePreString = "Little Gorilla and Tiny Tiger have different strengths and levels of dexterity (hand & eye skills)." +
			" Follow the pattern to use them at their best.";
		m_SmokyCarGamePreString = "Sam Snake is a master at pollution reduction. Sneak up using items of cover" +
			" where needed to plug the smoking exhaust pipe and allow Caterpillar to escape!";
		m_GraffitiGamePreString = "Caterpillar studies good exercise and healthy eating. He also hates graffiti! Guide him carefully to clean " +
			"off the graffiti and let the postman collect the post for onward delivery.";

        if(SaveDataStatic.StorySequence != "Litter")
        {
            GameTimerStartup();
        }

        if (SaveDataStatic.StorySequence == "Litter")
		{
			PlayMovie();
		}
        else if(SaveDataStatic.StorySequence == "LitterEnd")
        {
            FireHydrant.GetComponent<FireHydrant>().StartWaterLeak();

            m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
            m_ActiveISelObj.SelectObject();

            MonkeyWrench = (GameObject)Instantiate(MonkeyWrench);
            MonkeyWrench.name = "MonkeyWrench";
            Broom = (GameObject)Instantiate(Broom);
            Broom.name = "Broom";
            Plunger = (GameObject)Instantiate(Plunger);
            Plunger.name = "Plunger";

            OnMoviePlayCompletion();
        }
        else if(SaveDataStatic.StorySequence == "PRepStart") // Pipe repair game lost
		{
			Hippo.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);

			SetHasUIComeUP(true);
		
			GameCompletePanel.SetActive(true);
			GameCompletePanel.GetComponent<GameFinish>().SetGameFinishPanel(false);

			SetQuizAnswerCursor();

        }
		else if(SaveDataStatic.StorySequence == "PRepEnd") // Pipe Repair game won
		{
			Hippo.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);

			SetHasUIComeUP(true);

			FireHydrant.GetComponent<FireHydrant>().StartWaterLeak();
			FireHydrant.GetComponent<FireHydrant>().DeactivateWaterLeak();

			GameCompletePanel.SetActive(true);
			GameCompletePanel.GetComponent<GameFinish>().SetGameFinishPanel(true);

			SetQuizAnswerCursor();
        }
		else if(SaveDataStatic.StorySequence == "Drain") // States added due to Save functionality
		{
			Hippo.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);
			m_ActiveISelObj = Hippo.GetComponent<ISelectObject>();
			m_ActiveISelObj.SelectObject ();

			SetTargets(WaterOnFloor.transform);
			SetPointArrowPosition(GetTarget());

			FireHydrant.GetComponent<FireHydrant>().StartWaterLeak();
			FireHydrant.GetComponent<FireHydrant>().DeactivateWaterLeak();

			m_ActiveISelObj.AddInventoryItem ("Plunger");
			m_ActiveISelObj.AddInventoryItem ("Broom");
        }
        else if(SaveDataStatic.StorySequence == "DrainFinish")
        {
            Panda.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);
            m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
            m_ActiveISelObj.SelectObject();

            StartCoroutine(StartSamplingTreeJob());

            StartCoroutine(ExpressionTxtSplashing("Let's go to the post box!"));
        }
        else if(SaveDataStatic.StorySequence == "SaplingTreeVideoStart")
        {
            PlayMovie();
        }
        else if(SaveDataStatic.StorySequence == "SaplingTree") // Sampling Tree Lost
		{
			FireHydrant.GetComponent<FireHydrant>().DeactivateWaterLeak();

			m_ActiveISelObj = Gorilla.GetComponent<ISelectObject>();
			Gorilla.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);

			SetHasUIComeUP(true);

			GameCompletePanel.SetActive(true);
			GameCompletePanel.GetComponent<GameFinish>().SetGameFinishPanel(false);

			SetQuizAnswerCursor();
        }
		else if(SaveDataStatic.StorySequence == "SaplingTreeEnd") // Sampling Tree Won
		{
			m_ActiveISelObj = Gorilla.GetComponent<ISelectObject>();
			Gorilla.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);

			SetHasUIComeUP(true);

			GameCompletePanel.SetActive(true);
			GameCompletePanel.GetComponent<GameFinish>().SetGameFinishPanel(true);

			FireHydrant.GetComponent<FireHydrant>().DeactivateWaterLeak();

			SetQuizAnswerCursor();

			ActivateRain (Gorilla);
        }
		else if(SaveDataStatic.StorySequence == "Graffiti") // States added due to Save functionality
		{
            FireHydrant.GetComponent<FireHydrant>().DeactivateWaterLeak();
            PlayMovie();
        }
		else if(SaveDataStatic.StorySequence == "SmokeCarBegin") // States added due to Save functionality
		{
//			m_ActiveISelObj = Caterpillar.GetComponent<ISelectObject>();
//			m_ActiveISelObj.AddInventoryItem ("Cloth");
            FireHydrant.GetComponent<FireHydrant>().DeactivateWaterLeak();
            PlayMovie();
            SmokingCar.GetComponent<SmokingCar>().StartSmokeInCar(); 
        }
        else if(SaveDataStatic.StorySequence == "SeaLife") // States added due to Save functionality
		{
			Panda.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);
			m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
			m_ActiveISelObj.SelectObject ();
			Quiz.GetInstance().ShowQuez(6);
			FireHydrant.GetComponent<FireHydrant>().DeactivateWaterLeak();

			StopRain ();
		}
		else if(SaveDataStatic.StorySequence == "PostBox") // States added due to Save functionality
		{
			FireHydrant.GetComponent<FireHydrant>().DeactivateWaterLeak();

			m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
			m_ActiveISelObj.SelectObject();

			SmokingCar.GetComponent<SmokingCar>().ClothThatBlock.SetActive(true); 

			CarKey = Instantiate(CarKey);
			CarKey.name = "CarKey";

			SetTargets(Caterpillar.transform);
			SetTargets(CarKey.transform);
			SetTargets(CatepillarCloth.transform);
			SetTargets(Prime8SuperRemover.transform);
			SetTargets(PostBox.transform);
			SetPointArrowPosition(GetTarget());
			StartCoroutine(ExpressionTxtSplashing("Use Caterpillar to help the postman!"));
			SetSubtitleText("You need Caterpillar to help you clean off that graffiti!", 5, NeedCaterpillarAudio);

			ActivateRain (Panda);
		}
		else if(SaveDataStatic.StorySequence == "SmokeCarEnd")
		{
			m_ActiveISelObj = SamSnake.GetComponent<ISelectObject>();
			m_ActiveISelObj.SelectObject();
			m_ActiveISelObj.DeactivateFPS();

			FireHydrant.GetComponent<FireHydrant>().DeactivateWaterLeak();

			SmokingCar.GetComponent<SmokingCar>().ClothThatBlock.SetActive(true); 

			CarKey = Instantiate(CarKey);
			CarKey.name = "CarKey";

			ActivateRain (SamSnake);

			if(!SaveDataStatic.forTesting)
			{
				Quiz.GetInstance().ShowQuez(4);
			}
			else
			{
                SetTimerUI(false);

                BackToAmandaPanda("", null);
			}
        }
		else if(SaveDataStatic.StorySequence == "GrafitiBegin")
		{
			m_ActiveISelObj = Caterpillar.GetComponent<ISelectObject>();
			Caterpillar.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);

			SetHasUIComeUP(true);

			GameCompletePanel.SetActive(true);
			GameCompletePanel.GetComponent<GameFinish>().SetGameFinishPanel(false);

			SetQuizAnswerCursor();

			FireHydrant.GetComponent<FireHydrant>().DeactivateWaterLeak();

			StopRain ();
        }
		else if(SaveDataStatic.StorySequence == "GrafitiEnd")
		{
			FireHydrant.GetComponent<FireHydrant>().DeactivateWaterLeak();

			m_ActiveISelObj = Caterpillar.GetComponent<ISelectObject>();
			Caterpillar.transform.Find("FirstPersonCharacter").gameObject.SetActive(true);

			SetHasUIComeUP(true);

			GameCompletePanel.SetActive(true);
			GameCompletePanel.GetComponent<GameFinish>().SetGameFinishPanel(true);

			SetQuizAnswerCursor();

			StopRain ();

            SaveDataStatic.WasPostBoxCleaned = true;
            PostBox.GetComponent<PostBox>().SetCleanPostBoxTexture();
        }

        SaveDataStatic.IsObjectLoadComplete = true;
	}

	public ISelectObject GetActiveISelObj()
	{
		return m_ActiveISelObj;
	}

	private void ActivateRain(GameObject _parent)
	{
		RainBasic.SetActive (true);
//		RainBasic.transform.parent = _parent.transform;
//		RainBasic.transform.localPosition = new Vector3 (0, RainBasic.transform.localPosition.y, 0);
		RainBasic.transform.position = new Vector3(_parent.transform.position.x, 15, _parent.transform.position.z);

		foreach (Transform t in RainBasic.transform)
			t.gameObject.GetComponent<ParticleSystem>().Play();

		if (RenderSettings.skybox.name != "sky5X1") {
			RenderSettings.skybox = (Material)Resources.Load ("sky5X_skyboxes/sky5X1");
		}
	}

	private void StopRain()
	{
		foreach (Transform t in RainBasic.transform)
			t.gameObject.GetComponent<ParticleSystem>().Stop();

		RainBasic.SetActive (false);

		if (RenderSettings.skybox.name != "sky5X2") {
			RenderSettings.skybox = (Material)Resources.Load ("sky5X_skyboxes/sky5X2");
		}
	}

	void Update()
	{
		if (SaveDataStatic.IsObjectLoadComplete == false)
			return;

#if !(UNITY_IOS || UNITY_ANDROID)
        m_ClickedStatus = Input.GetMouseButtonDown(0);
#endif
        SelectMethod();
    }

    private Vector3 m_MidPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);

    private bool m_ClickedStatus = false;
   
    public void JoyStickSelect()
    {
        m_ClickedStatus = true;
    }

    public void SelectMethod()
    {
        //#if !(UNITY_IOS || UNITY_ANDROID)
        //        m_ClickedStatus = Input.GetMouseButtonDown(0);
        //#endif
        if (m_ActiveISelObj != null)
            m_ActiveISelObj.CharacterUpdate(m_ClickedStatus);

        if (Camera.main != null && !m_HasUIComeUp)
        {
            ray = Camera.main.ScreenPointToRay(m_MidPosition);

            //if (LitterCollectGameStart && LitterList.Count > 0) // Game Start
            //{
            //    GetActiveISelObj().CharacterUpdate(ray);
            //    return;
            //}

            if (Physics.Raycast(ray, out hit, 10f))
            {
                if (hit.collider.tag == "Target")
                {
                    if (m_PandaClickOnPostBoxFirstTime)
                    {
                        ShowMagnifierCursor(false);

                        if (m_ClickedStatus && !m_GraffitiPostBoxImgStatus)
                        {
                            EnableOrDisableGraffitiPostBoxImg(true);
                        }
                        else if (m_ClickedStatus && m_GraffitiPostBoxImgStatus)
                        {
                            EnableOrDisableGraffitiPostBoxImg(false);
                            m_PandaClickOnPostBoxFirstTime = false;
                            ShowCursor(false);
                        }
                    }
                    else
                    {
                        ShowCursor(false);

                        if (m_ClickedStatus)
                        {
                            ISelectObject m_ISelObj = hit.collider.gameObject.GetComponent<ISelectObject>();
                            if (m_ISelObj.IsCharacter)
                            {
                                m_ActiveISelObj.DeSelectObject();
                                m_ActiveISelObj = hit.collider.gameObject.GetComponent<ISelectObject>();

                                if (RainBasic.activeSelf)
                                {
                                    ActivateRain(hit.collider.gameObject);
                                }
                            }

                            hit.collider.gameObject.tag = "Untagged";
                            m_ISelObj.SelectObject();

                            m_Target = GetTarget();
                            if (m_Target)
                            {
                                SetPointArrowPosition(m_Target);
                            }
                            else
                            {
                                ResetPointingArrow();
                            }
                        }
                    }

                    //if (SaveDataStatic.StorySequence == "PRepStart" && hit.collider.gameObject.name == "Hydrant")
                    if (SaveDataStatic.StorySequence == "LitterEnd" && hit.collider.gameObject.name == "Hydrant")
                    {
                        if (InventoryPanel.GetInstance().GetInventoryItemSelected() == "MonkeyWrench")
                        {
                            InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(false);
                        }
                        else
                        {
                            InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(true);
                        }
                    }
                    else if (SaveDataStatic.StorySequence == "Drain" && hit.collider.gameObject.name == "DrainWater")
                    {

                        if (InventoryPanel.GetInstance().GetInventoryItemSelected() == "Plunger" || InventoryPanel.GetInstance().GetInventoryItemSelected() == "Broom")
                        {
                            InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(false);
                        }
                        else
                        {
                            InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(true);
                        }
                    }
                    else if (SaveDataStatic.StorySequence == "SmokeCarBegin" && hit.collider.gameObject.name == "Exauster")
                    {

                        if (InventoryPanel.GetInstance().GetInventoryItemSelected() == "Cloth")
                        {
                            InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(false);
                        }
                        else
                        {
                            InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(true);
                        }
                    }
                    else if (SaveDataStatic.StorySequence == "PostBox" && hit.collider.gameObject.name == "PostBox")
                    {

                        if (InventoryPanel.GetInstance().whatIsSelectedtoCombine == "CombinedForGraffiti")
                        {
                            InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(false);
                        }
                        else
                        {
                            InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(true);
                        }
                    }
                    else if (SaveDataStatic.StorySequence == "GrafitiEnd" && hit.collider.gameObject.name == "PostBox")
                    {

                        if (InventoryPanel.GetInstance().GetInventoryItemSelected() == "LetterCase")
                        {
                            InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(false);
                        }
                        else
                        {
                            InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(true);
                        }
                    }
                }
                else
                {
                   ShowCursor(true);

                    //if (SaveDataStatic.StorySequence == "PRepStart" && !String.IsNullOrEmpty(InventoryPanel.GetInstance().GetInventoryItemSelected()))
                    if (SaveDataStatic.StorySequence == "LitterEnd" && !String.IsNullOrEmpty(InventoryPanel.GetInstance().GetInventoryItemSelected()))
                    {
                        InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(null);
                    }
                    else if (SaveDataStatic.StorySequence == "Drain" && !String.IsNullOrEmpty(InventoryPanel.GetInstance().GetInventoryItemSelected()))
                    {
                        InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(null);
                    }
                    else if (SaveDataStatic.StorySequence == "SmokeCarBegin" && !String.IsNullOrEmpty(InventoryPanel.GetInstance().GetInventoryItemSelected()))
                    {
                        InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(null);
                    }
                    else if (SaveDataStatic.StorySequence == "PostBox" && !String.IsNullOrEmpty(InventoryPanel.GetInstance().GetInventoryItemSelected()))
                    {
                        InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(null);
                    }
                    else if (SaveDataStatic.StorySequence == "GrafitiEnd" && !String.IsNullOrEmpty(InventoryPanel.GetInstance().GetInventoryItemSelected()))
                    {
                        InventoryPanel.GetInstance().ChangeColorOfInventorySelectedImage(null);
                    }
                }
            }
            else
            {
                ShowCursor(true);
            }
        }
        else
        {

            if (DirectionArrow.activeSelf == true)
            {
                DirectionArrow.SetActive(false);
            }
        }

        if (m_GraffitiPostBoxImgStatus && SaveDataStatic.StorySequence == "Litter")
        {

            if (m_ClickedStatus)
            {
                // GraffitiPostBoxImg.SetActive(false);
                EnableOrDisableGraffitiPostBoxImg(false);

                LitterPicker = Instantiate(LitterPicker);
                LitterPicker.name = "Litter Picker";

                PickerHolster = Instantiate(PickerHolster);
                PickerHolster.name = "Litter Picker Holster Belt";

                m_SequinceExpressions.Enqueue("Collect Litter!");
                SetTargets(Rhino.transform);
                SetTargets(PickerHolster.transform);
                SetTargets(LitterPicker.transform);
                SetTargets(LitterBin.transform);

                SetPointArrowPosition(GetTarget());

                string m_Expression = m_SequinceExpressions.Peek();
                m_SequinceExpressions.Dequeue();
                StartCoroutine(ExpressionTxtSplashing(m_Expression));

                Panda.GetComponent<FirstPersonController>().enabled = false;

                SetSubtitleText(NeedRhinoSubtitle, 4, NeedRhinoAudio);
                SetSubtitleText("Please remember that like in real life, you must use safe places to cross roads! The crossings are also indicated on the map.", 0, RealLifeCrossingAudio);
                SetSubtitleText("The map at the top of the screen shows the " +
                    "layout of the town. Your current position is indicated by the blue dot." +
                    " The location or object you need to find is indicated by the flashing red dot. " +
                    "The red writing at the top also explains what you need to do.", 5, MapInfoAudio);
                m_HasUIComeUp = false;
            }
        }

        //if (m_GraffitiPostBoxImgStatus && SaveDataStatic.StorySequence == "SeaLife")
        //{
        //    if (m_ClickedStatus)
        //    {
        //        StopAllCoroutines();
        //        SceneLoad("PilotEnd");
        //        enabled = false;
        //    }
        //}

#if !(UNITY_IOS || UNITY_ANDROID)
        RuffleSackOpen();

        //if (m_IsHintDisplaying)
        //{
        //    if (!TownControllerAudio.isPlaying)
        //    {
        //        m_IsHintDisplaying = false;
        //        m_HintText.text = "";
        //        HintTextObj.SetActive(false);
        //        m_IsHintDisplaying = false;
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.H))
        {
            ShowHint();
        }

        if (m_SubIndex > 2 && Input.GetKeyDown(KeyCode.K))
        {
            if (!InstructionUI.activeSelf)
            {
                InstructionUI.SetActive(true);
                m_HasUIComeUp = true;
                SetQuizAnswerCursor();
            }
            else
            {
                m_HasUIComeUp = false;

                LockCursorForGame();
                ShowCursor(true);

                InstructionUI.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitMenuCall();
        }

#endif
//        Debug.Log("m_SubtitleQueue.Count :  " + m_SubtitleQueue.Count + " and IsSubtitleStillDisplaying : " + IsSubtitleStillDisplaying);
        if (m_SubtitleQueue.Count > 0 && !IsSubtitleStillDisplaying)
        {
            m_ClearSubtitleCoroutine = StartCoroutine(ClearSubtitleTextAfterDelay());
        }

        if (m_ClickedStatus)
        {
            //InventoryPanel.GetInstance().UnsetInvSelectedImage(false);
            InventoryPanel.GetInstance().RemoveInventorySelectedImage();
        }

        m_ClickedStatus = false;
       
    }

    public void RuffleSackOpen()
    {
#if !(UNITY_IOS || UNITY_ANDROID)
        if (Input.GetKeyDown(KeyCode.R) && !HasQuizStarted && !StartMenuPanel.activeSelf && !LitterCollectGameStart)
        {
            CloseOrOpenInventory();
        }
#else
        if (!HasQuizStarted && !StartMenuPanel.activeSelf && !LitterCollectGameStart && !m_GraffitiPostBoxImgStatus)
        {
            CloseOrOpenInventory();
        }
#endif
    }

    public void ExitMenuCall()
    {
        if (m_GraffitiPostBoxImgStatus) return;

        if (QuitGameMsgPanel.activeSelf)
        {
            SetOrUnsetQuitGamePanel(false);
        }
        else
        {
            SetOrUnsetQuitGamePanel(true);
        }
    }

    public GameObject InstructionUI;


	bool m_IsHintDisplaying = false;
	public GameObject	HintTextObj;
	public AudioClip	HintAudioClip;
	private Text		m_HintText;

	public void ShowHint()
	{
        if (m_GraffitiPostBoxImgStatus) return;

        if (m_SubIndex > 2 && !m_IsHintDisplaying && !MoviePlayer.activeSelf)
        {
            m_IsHintDisplaying = true;
            HintTextObj.SetActive(true);

            if (m_HintText == null)
            {
                m_HintText = HintTextObj.GetComponent<Text>();
            }
            m_HintText.text = "Remember you need to investigate the location indicated by the flashing red dot on the map";

            TownControllerAudio.clip = HintAudioClip;
            TownControllerAudio.Play();

            StartCoroutine(WaitForHintToFinish());
        }
	}

    IEnumerator WaitForHintToFinish()
    {
        yield return new WaitForSeconds(HintAudioClip.length);
        m_IsHintDisplaying = false;
        m_HintText.text = "";
        HintTextObj.SetActive(false);
    }

    public void PlayMovie()
	{
		if(IsSubtitleStillDisplaying) IsSubtitleStillDisplaying = false;

        StopTimer();

        //SubtitleTxt.text = "";
        subtitleManager.Stop();

        JoyStickSettings(false);
        ControlMapCamera(false);

        LockCursorForGame();
		HideCursor();
		MoviePlayer.SetActive(true);
		MoviePlayer.GetComponent<MoviePlayer>().PlayMovie();
	}


    public void OnMoviePlayCompletion()
	{
		ShowCursor(true);

        StatScreen.alpha = 1;

		string m_Expression = "";

        GameTimerStartup();


        if (SaveDataStatic.StorySequence == "Litter")
		{
			if (SaveDataStatic.PlayTheSeagullVideo == false) {
				m_HasUIComeUp = true;
				//GraffitiPostBoxImg.SetActive (true);
                EnableOrDisableGraffitiPostBoxImg(true);
				
				m_ActiveISelObj = Panda.GetComponent<ISelectObject> ();
				m_ActiveISelObj.SelectObject ();
				
				UnHideCursor ();
				
				ShowMagnifierCursor (false);

                GameTimerStartup(); // Starting up the Timer

            } else {

				//MapCamera.enabled = true;
				SaveDataStatic.PlayTheSeagullVideo = false;
				m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
				m_ActiveISelObj.SelectObject();

                //StatScreen.SetActive(true);
                SetTimerUI(false);
				StartCoroutine(ExpressionTxtSplashing("Collect Litter!"));
            }
		}
        else if(SaveDataStatic.StorySequence == "LitterEnd")
        {
            m_SequinceExpressions.Enqueue("Repair pipe and clear that drain!");
            SetTargets(FireHydrant.transform);
            SetTargets(Hippo.transform);
            SetTargets(MonkeyWrench.transform);
            SetTargets(Broom.transform);
            SetTargets(Plunger.transform);
            SetTargets(FireHydrant.transform);

            SetPointArrowPosition(GetTarget());

            m_Expression = m_SequinceExpressions.Peek();
            m_SequinceExpressions.Dequeue();
            StartCoroutine(ExpressionTxtSplashing(m_Expression));
        }
        else if(SaveDataStatic.StorySequence == "PRepStart")
		{
			//m_SequinceExpressions.Enqueue("Repair pipe and clear that drain!");
			//SetTargets(FireHydrant.transform);
			//SetTargets(Hippo.transform);
			//SetTargets(MonkeyWrench.transform);
			//SetTargets(Broom.transform);
			//SetTargets(Plunger.transform);
			//SetTargets(FireHydrant.transform);

			//SetPointArrowPosition(GetTarget());

			//m_Expression = m_SequinceExpressions.Peek();
			//m_SequinceExpressions.Dequeue();
			//StartCoroutine(ExpressionTxtSplashing(m_Expression));
		}
		else if(SaveDataStatic.StorySequence == "Drain")
		{
			Debug.Log("Drain Video Completed");

			m_ActiveISelObj = Hippo.GetComponent<ISelectObject>();
			m_ActiveISelObj.SelectObject();
			m_ActiveISelObj.DeactivateFPS();

			if(!SaveDataStatic.forTesting)
			{
				Quiz.GetInstance().ShowQuez(2);
			}
			else
			{
                SetTimerUI(false);

                BackToAmandaPanda("", null);
			}
		}
		//else if(SaveDataStatic.StorySequence == "SaplingTree")
        else if(SaveDataStatic.StorySequence == "SaplingTreeVideoStart")
		{
			Debug.Log("Tiny Tiger Video completed");

			m_SequinceExpressions.Enqueue("Help Tiny Tiger with Sapling Tree!");
			SetTargets(SaplingTree.transform);
			SetTargets(Gorilla.transform);
			SetTargets(SaplingTree.transform);
			SetPointArrowPosition(GetTarget());

			m_Expression = m_SequinceExpressions.Peek();
			m_SequinceExpressions.Dequeue();
			StartCoroutine(ExpressionTxtSplashing(m_Expression));

			m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
			m_ActiveISelObj.SelectObject();
		}
		else if(SaveDataStatic.StorySequence == "SaplingTreeEnd") // Sapling Tree Job Completed
		{
			m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
			m_ActiveISelObj.DeSelectObject();
			m_ActiveISelObj.DoLocalAnimation(0);

			SaplingTree.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
			//SaplingTree.transform.FindChild("").gameObject.SetActive(false);

			ActivateRain (Panda);
		}
		else if(SaveDataStatic.StorySequence == "Graffiti")
		{
            StatScreen.alpha = 1;
			m_SequinceExpressions.Enqueue("Help Postman to clear Graffiti!");
			SetTargets(PostBox.transform);

			m_PandaClickOnPostBoxFirstTime = true;

			SetPointArrowPosition(GetTarget());

			m_Expression = m_SequinceExpressions.Peek();
			m_SequinceExpressions.Dequeue();
			StartCoroutine(ExpressionTxtSplashing(m_Expression));

			m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
			m_ActiveISelObj.SelectObject();

			ActivateRain (Panda);
		}
		else if(SaveDataStatic.StorySequence == "SmokeCarBegin")
		{
            StatScreen.alpha = 1;
			m_SequinceExpressions.Enqueue("Stop the smoking car!");
			SetTargets(SamSnake.transform);
			SetTargets(Cloth.transform);
			SetTargets(TrashBinfull.transform);
			SetTargets(Exauster.transform);

			m_Expression = m_SequinceExpressions.Peek();
			m_SequinceExpressions.Dequeue();
			StartCoroutine(ExpressionTxtSplashing(m_Expression));
			SetPointArrowPosition(GetTarget());

			m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
			m_ActiveISelObj.SelectObject();

			ActivateRain (Panda);

			SetSubtitleText(IHaveToFineSamSubtitle, 10, IHaveToFineSamAudio);
            SmokingCar.GetComponent<SmokingCar>().StartSmokeInCar();
        }
		else if(SaveDataStatic.StorySequence == "SmokeCarEnd")
		{
			m_ActiveISelObj = SamSnake.GetComponent<ISelectObject>();
			m_ActiveISelObj.SelectObject();
			m_ActiveISelObj.DeactivateFPS();

			CarKey = Instantiate(CarKey);
			CarKey.name = "CarKey";

			ActivateRain (SamSnake);

			if(!SaveDataStatic.forTesting)
			{
				Quiz.GetInstance().ShowQuez(4);
			}
			else
			{
                SetTimerUI(false);

                BackToAmandaPanda("", null);
			}
		}
		else if(SaveDataStatic.StorySequence == "GrafitiEnd")
		{
			SaveDataStatic.StorySequence = "SeaLife";
            SaveCharacterPositionAtEachSavePoint();
            Quiz.GetInstance().ShowQuez(6);

		}
	}

	public GameObject		MoviePlayer;
	private Queue<string>	m_SequinceExpressions = new Queue<string>();

	private Queue<Transform>	m_SequinceTargets = new Queue<Transform>();
	public void SetTargets(Transform _target)
	{
		m_SequinceTargets.Enqueue(_target);
	}

	private Transform m_Target;
	private Transform GetTargetForSelection()
	{
		if (m_SequinceTargets.Count > 0) {
			return m_SequinceTargets.Dequeue ();
		} else {
			return null;
		}
	}

	public Transform GetTarget()
	{
		Transform _target = null;

		if(m_SequinceTargets.Count > 0)
		{
			_target = m_SequinceTargets.Dequeue();

			if(_target.gameObject.name == "Litter Picker Holster Belt")
			{
				SetSubtitleText(NeedLitterBeltSubtitle, 10, NeedLitterBeltAudio);
			}
			else if(_target.gameObject.name == "Litter Picker")
			{
				SetSubtitleText(NeedLitterPickerSubtitle, 10, NeedLitterPickerAudio);
			}
			else if(_target.gameObject.name == "trash bin closed")
			{
				SetSubtitleText(GoBackToPlayAreaSubtitle, 10, GoBackToPlayAreaAudio);
			}

			else if(_target.gameObject.name == "MonkeyWrench")
			{
				SetSubtitleText(NeedMonkeyWrenchSubtitle, 10, NeedMonkeyWrenchAudio);
			}
			else if(_target.gameObject.name == "Broom")
			{
				SetSubtitleText(NeedBroomSubtitle, 10, NeedBroomAudio);
			}
			else if(_target.gameObject.name == "Plunger")
			{
				SetSubtitleText(NeedPlungerSubtitle, 10, NeedPlungerAudio);
			}
			else if(_target.gameObject.name == "DrainWater")
			{
				if (!m_HasPlayedNowClearTheDrainAudio) {
					m_HasPlayedNowClearTheDrainAudio = true;
					SetSubtitleText (NowClearTheDrainSubtitle, 8, NowClearTheDrainAudio);
				}
			}

			else if(_target.gameObject.name == "Cloth")
			{
				SetSubtitleText(NeedClothForSamSubtitle, 10, NeedClothForSamAudio);
			}

			else if(_target.gameObject.name == "CarKey")
			{
				SetSubtitleText(NeedCarKeysSubtitle, 10, NeedCarKeysAudio);
			}
			else if(_target.gameObject.name == "ExausterBlocked")
			{
				SetSubtitleText(NeedClothThatSamUsedSubtitle, 10, NeedClothThatSamUsedAudio);
			}
			else if(_target.gameObject.name == "Prime8SuperRemover")
			{
				SetSubtitleText(NeedPrime8SuperRemoverSubtitle, 10, NeedPrime8SuperRemoverAudio);
			}

		}
		else
		{
			_target = null;
		}

		return _target;
	}

	private Ray				ray;
	private RaycastHit		hit;

	private ISelectObject	m_ActiveISelObj;

	public Camera			MapCamera;
	public Transform 		ToTransform;
	public Transform		FromTransform;
	public GameObject		DirectionArrow;

	public Transform		PointingArrowTra;



    public void SetPointArrowPosition(Transform _destTra)
	{
		if(PointingArrowTra.gameObject.activeSelf == false) PointingArrowTra.gameObject.SetActive(true);

        if(_destTra.GetComponent<Renderer>() != null)
        {
           float m_Height = _destTra.GetComponent<Renderer>().bounds.size.y;
           PointingArrowTra.position = new Vector3(_destTra.position.x, _destTra.position.y + m_Height + 3, _destTra.position.z);
        }
        else
        {
            PointingArrowTra.position = new Vector3(_destTra.position.x, _destTra.position.y + (_destTra.localScale.y / 2) + 6, _destTra.position.z);
        }
       
		PointingArrowTra.gameObject.GetComponent<PointingArrow>().enabled = true;
		PointingArrowTra.gameObject.GetComponent<PointingArrow>().StartPointingArrow();

		_destTra.gameObject.tag = "Target";


		ToTransform.position = new Vector3(PointingArrowTra.position.x, 200, PointingArrowTra.position.z);

        ControlMapCamera(true);
    }

	public void ResetPointingArrow()
	{
		PointingArrowTra.gameObject.GetComponent<PointingArrow>().enabled = false;

        if (SaveDataStatic.StorySequence != "GrafitiEnd")
        {
            ControlMapCamera(false);
        }

        if (SaveDataStatic.StorySequence == "Litter") // Start the Litter Collecting Game
		{
			if(SaveDataStatic.forTesting)
			{
				AfterRhinoWinMsgBoxDoneBtnPressed();
			}
			else
			{
				HideCursor();
                //JoyStickSettings(false);
                //ControlMapCamera(false);
                m_ActiveISelObj.DoLocalAnimation(0);
			}
		}

	}

    public Text MapLabel;
    public void ControlMapCamera(bool _activate)
    {
        MapCamera.enabled = _activate;
        MapOpenButton.interactable = _activate;
        MapLabel.enabled = _activate;

        if(_activate)
            StatScreen.alpha = 1;
        else
            StatScreen.alpha = 0; 
    }

    public bool DidSmokingCarStarted = false;
    public Button MapOpenButton;
    private bool m_MapOpenButtonClick = false;
    public void OnMapOpenButtonClicked()
    {
        if (IsSubtitleStillDisplaying) return;

        if (DidSmokingCarStarted) return;

        if(m_MapOpenButtonClick == false)
        {
#if UNITY_IOS || UNITY_ANDROID
            JoyStickSettings(false);
#endif

            MapOpenButton.interactable = false;
            m_MapOpenButtonClick = true;
            StartCoroutine(MapOpenAnimation(true));
            StatScreen.alpha = 0; 
        }
    }

    public void OnMapCloseButtonclicked()
    {
        MapOpenButton.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(MapOpenAnimation(false));
    }

    IEnumerator MapOpenAnimation(bool _open)
    {
        if (_open == true)
        {
            while (MapCamera.rect.x > 0)
            {
                MapCamera.rect = new Rect(MapCamera.rect.x - 0.1f, MapCamera.rect.y - 0.1f, 1, 1);
                yield return new WaitForSeconds(0.1f);
            }

            MapCamera.rect = new Rect(0, 0, 1, 1);
            MapOpenButton.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            while (MapCamera.rect.x < 0.9f)
            {
                MapCamera.rect = new Rect(MapCamera.rect.x + 0.1f, MapCamera.rect.y + 0.1f, 1, 1);
                yield return new WaitForSeconds(0.1f);
            }

            MapCamera.rect = new Rect(0.9f, 0.9f, 1, 1);

            m_MapOpenButtonClick = false; 
            MapOpenButton.interactable = true;
#if UNITY_IOS || UNITY_ANDROID
            JoyStickSettings(true);
#endif
            StatScreen.alpha = 1;
        }
    }

#region Joystick Settings
    private void SubtitleManager_OnSubtitileDisplayBeginEvent()
    {
        JoyStickSettings(false);
    }

    private void SubtitleManager_OnSubtitileDisplayEndEvent()
    {
        JoyStickSettings(true);
    }

    public CanvasGroup JoyStick;
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
#endregion Joystick Settings

    public void SceneLoad(string _sceneName)
	{
        PlayerPrefs.SetFloat("GameTime", m_TimeDiff);
        SaveCharacterPositionAtEachSavePoint();
        SceneManager.LoadSceneAsync(_sceneName);
	}

    void SetCharacterPositions()
    {
        if (SaveDataStatic.StorySequence == "Litter")
        {
            SaveDataStatic.PandaPosition = new Vector3(70f, 2.375f, 34.25f); // Production

            SaveDataStatic.HippoPosition = new Vector3(-380.5f, 2.375f, -27f);
            SaveDataStatic.RhinoPosition = new Vector3(-385.41f, 2.375f, -25.4f);
            SaveDataStatic.GorillaPosition = new Vector3(-1271.31f, 3.22f, -138.97f);
            SaveDataStatic.SnakePosition = new Vector3(-966.1f, 2.375f, 282.9f);
            SaveDataStatic.TigerPosition = new Vector3(-1041.33f, 2.375f, -19.5f);
            SaveDataStatic.CaterpillarPosition = new Vector3(-1229.45f, 2.375f, 515.75f);
        }
        else if (SaveDataStatic.StorySequence == "LitterEnd")
        {
            SaveDataStatic.PandaPosition = new Vector3(-380.1609f, 2.420297f, -22.98996f);
            SaveDataStatic.RhinoPosition = new Vector3(-361.7f, 2.375f, -82.1f);
            SaveDataStatic.HippoPosition = new Vector3(-380.5f, 2.375f, -27f);
            SaveDataStatic.GorillaPosition = new Vector3(-1271.31f, 3.22f, -138.97f);
            SaveDataStatic.SnakePosition = new Vector3(-966.1f, 2.375f, 282.9f);
            SaveDataStatic.TigerPosition = new Vector3(-1041.33f, 2.375f, -19.5f);
            SaveDataStatic.CaterpillarPosition = new Vector3(-1229.45f, 2.375f, 515.75f);

        }
        else if (SaveDataStatic.StorySequence == "PRepStart" || SaveDataStatic.StorySequence == "PRepEnd" || SaveDataStatic.StorySequence == "Drain" || SaveDataStatic.StorySequence == "DrainFinish") // Pipe repair game lost
        {
            SaveDataStatic.PandaPosition = new Vector3(-380.1609f, 2.420297f, -22.98996f);
            SaveDataStatic.RhinoPosition = new Vector3(-361.7f, 2.375f, -82.1f);
            SaveDataStatic.HippoPosition = new Vector3(-646.7879f, 2.420304f, -7.759869f);
            SaveDataStatic.GorillaPosition = new Vector3(-1271.31f, 3.22f, -138.97f);
            SaveDataStatic.SnakePosition = new Vector3(-966.1f, 2.375f, 282.9f);
            SaveDataStatic.TigerPosition = new Vector3(-1041.33f, 2.375f, -19.5f);
            SaveDataStatic.CaterpillarPosition = new Vector3(-1229.45f, 2.375f, 515.75f);

        }
        else if (SaveDataStatic.StorySequence == "SaplingTreeVideoStart")
        {
            SaveDataStatic.PandaPosition = new Vector3(-898.1613f, 2.42034f, 66.22928f);
            SaveDataStatic.RhinoPosition = new Vector3(-361.7f, 2.375f, -82.1f);
            SaveDataStatic.HippoPosition = new Vector3(-646.7879f, 2.420304f, -7.759869f);
            SaveDataStatic.GorillaPosition = new Vector3(-1271.31f, 3.22f, -138.97f);
            SaveDataStatic.SnakePosition = new Vector3(-966.1f, 2.375f, 282.9f);
            SaveDataStatic.TigerPosition = new Vector3(-1041.33f, 2.375f, -19.5f);
            SaveDataStatic.CaterpillarPosition = new Vector3(-1229.45f, 2.375f, 515.75f);
        }
        else if (SaveDataStatic.StorySequence == "SaplingTree" || SaveDataStatic.StorySequence == "SaplingTreeEnd" || SaveDataStatic.StorySequence == "Graffiti") // Sampling Tree Lost
        {
            SaveDataStatic.PandaPosition = new Vector3(-1266.522f, 2.305559f, -141.2513f);
            SaveDataStatic.RhinoPosition = new Vector3(-361.7f, 2.375f, -82.1f);
            SaveDataStatic.HippoPosition = new Vector3(-646.7879f, 2.420304f, -7.759869f);
            SaveDataStatic.GorillaPosition = new Vector3(-1042.419f, 2.305546f, -31.61875f);
            SaveDataStatic.SnakePosition = new Vector3(-966.1f, 2.375f, 282.9f);
            SaveDataStatic.TigerPosition = new Vector3(-1041.33f, 2.375f, -19.5f);
            SaveDataStatic.CaterpillarPosition = new Vector3(-1229.45f, 2.375f, 515.75f);
        }
        else if (SaveDataStatic.StorySequence == "SmokeCarBegin") // States added due to Save functionality
        {
            SaveDataStatic.PandaPosition = new Vector3(-655.0433f, 2.420524f, 453.9811f);
            SaveDataStatic.RhinoPosition = new Vector3(-361.7f, 2.375f, -82.1f);
            SaveDataStatic.HippoPosition = new Vector3(-646.7879f, 2.420304f, -7.759869f);
            SaveDataStatic.GorillaPosition = new Vector3(-1042.419f, 2.305546f, -31.61875f);
            SaveDataStatic.SnakePosition = new Vector3(-966.1f, 2.375f, 282.9f);
            SaveDataStatic.TigerPosition = new Vector3(-1041.33f, 2.375f, -19.5f);
            SaveDataStatic.CaterpillarPosition = new Vector3(-1229.45f, 2.375f, 515.75f);
        }
        else if (SaveDataStatic.StorySequence == "SmokeCarEnd" || SaveDataStatic.StorySequence == "PostBox")
        {
            SaveDataStatic.PandaPosition = new Vector3(-959.9567f, 2.420524f, 285.5522f);
            SaveDataStatic.RhinoPosition = new Vector3(-361.7f, 2.375f, -82.1f);
            SaveDataStatic.HippoPosition = new Vector3(-646.7879f, 2.420304f, -7.759869f);
            SaveDataStatic.GorillaPosition = new Vector3(-1042.419f, 2.305546f, -31.61875f);
            SaveDataStatic.SnakePosition = new Vector3(-1187.203f, 2.375f, 507.4108f);
            SaveDataStatic.TigerPosition = new Vector3(-1041.33f, 2.375f, -19.5f);
            SaveDataStatic.CaterpillarPosition = new Vector3(-1229.45f, 2.375f, 515.75f);
        }
        else if (SaveDataStatic.StorySequence == "GrafitiBegin" || SaveDataStatic.StorySequence == "GrafitiEnd")
        {
            SaveDataStatic.PandaPosition = new Vector3(-1217.64f, 2.420524f, 514.5163f);
            SaveDataStatic.RhinoPosition = new Vector3(-361.7f, 2.375f, -82.1f);
            SaveDataStatic.HippoPosition = new Vector3(-646.7879f, 2.420304f, -7.759869f);
            SaveDataStatic.GorillaPosition = new Vector3(-1042.419f, 2.305546f, -31.61875f);
            SaveDataStatic.SnakePosition = new Vector3(-1187.203f, 2.375f, 507.4108f);
            SaveDataStatic.TigerPosition = new Vector3(-1041.33f, 2.375f, -19.5f);
            SaveDataStatic.CaterpillarPosition = new Vector3(-655.0433f, 2.375f, 453.9811f);
        }
        else if (SaveDataStatic.StorySequence == "SeaLife") // States added due to Save functionality
        {
            SaveDataStatic.PandaPosition = new Vector3(-649.9197f, 2.420524f, 453.7818f);
            SaveDataStatic.RhinoPosition = new Vector3(-361.7f, 2.375f, -82.1f);
            SaveDataStatic.HippoPosition = new Vector3(-646.7879f, 2.420304f, -7.759869f);
            SaveDataStatic.GorillaPosition = new Vector3(-1042.419f, 2.305546f, -31.61875f);
            SaveDataStatic.SnakePosition = new Vector3(-1187.203f, 2.375f, 507.4108f);
            SaveDataStatic.TigerPosition = new Vector3(-1041.33f, 2.375f, -19.5f);
            SaveDataStatic.CaterpillarPosition = new Vector3(-655.0433f, 2.375f, 453.9811f);
        }
        else
        {
            SaveDataStatic.PandaPosition = Panda.transform.position;
            SaveDataStatic.RhinoPosition = Rhino.transform.position;
            SaveDataStatic.HippoPosition = Hippo.transform.position;
            SaveDataStatic.GorillaPosition = Gorilla.transform.position;
            SaveDataStatic.SnakePosition = SamSnake.transform.position;
            SaveDataStatic.TigerPosition = Tiger.transform.position;
            SaveDataStatic.CaterpillarPosition = Caterpillar.transform.position;
        }

        if (Panda)
        {
            SaveDataStatic.PandaYRotation = Panda.transform.rotation.y;
            SaveDataStatic.RhinoYRotation = Rhino.transform.rotation.y;
            SaveDataStatic.HippoYRotation = Hippo.transform.rotation.y;
            SaveDataStatic.GorillaYRotation = Gorilla.transform.rotation.y;
            SaveDataStatic.TigerYRotation = Tiger.transform.rotation.y;
            SaveDataStatic.SnakeYRotation = SamSnake.transform.rotation.y;
            SaveDataStatic.CaterpillarYRotation = Caterpillar.transform.rotation.y;
        }
        else
        {
            SaveDataStatic.ResetCharacterRotation();
        }
    }

    private void SaveCharacterPositionAtEachSavePoint()
    {
        SetCharacterPositions();

        SaveDataStatic.PandaYRotation = Panda.transform.rotation.y;
        SaveDataStatic.RhinoYRotation = Rhino.transform.rotation.y;
        SaveDataStatic.HippoYRotation = Hippo.transform.rotation.y;
        SaveDataStatic.GorillaYRotation = Gorilla.transform.rotation.y;
        SaveDataStatic.TigerYRotation = Tiger.transform.rotation.y;
        SaveDataStatic.SnakeYRotation = SamSnake.transform.rotation.y;
        SaveDataStatic.CaterpillarYRotation = Caterpillar.transform.rotation.y;

        ISelectObject iSel = Panda.GetComponent<ISelectObject>();
        SaveDataStatic.PandaInvItemNameList = iSel.InventoryNameList;

        iSel = Rhino.GetComponent<ISelectObject>();
        SaveDataStatic.RhinoInvItemNameList = iSel.InventoryNameList;

        iSel = Hippo.GetComponent<ISelectObject>();
        SaveDataStatic.HippoInvItemNameList = iSel.InventoryNameList;

        iSel = Tiger.GetComponent<ISelectObject>();
        SaveDataStatic.TigerInvItemNameList = iSel.InventoryNameList;

        iSel = Gorilla.GetComponent<ISelectObject>();
        SaveDataStatic.GorillaInvItemNameList = iSel.InventoryNameList;

        iSel = SamSnake.GetComponent<ISelectObject>();
        SaveDataStatic.SnakeInvItemNameList = iSel.InventoryNameList;

        iSel = Caterpillar.GetComponent<ISelectObject>();
        SaveDataStatic.CaterpillarItemNameList = iSel.InventoryNameList;

    }

    private string m_PosString = "";
	private string m_RotString = "";
	private string m_MedalString = "";

	public void SaveGameOnExit()
	{
		m_PosString = "";
		m_RotString = "";

		//SaveDataStatic.PandaPosition = Panda.transform.position;
		//SaveDataStatic.RhinoPosition = Rhino.transform.position;
		//SaveDataStatic.HippoPosition = Hippo.transform.position;
		//SaveDataStatic.GorillaPosition = Gorilla.transform.position;
		//SaveDataStatic.TigerPosition = Tiger.transform.position;
		//SaveDataStatic.SnakePosition = SamSnake.transform.position;
		//SaveDataStatic.CaterpillarPosition = Caterpillar.transform.position;

		m_PosString += SaveDataStatic.PandaPosition.x + "," + SaveDataStatic.PandaPosition.y + "," + SaveDataStatic.PandaPosition.z + "|";
		m_PosString += SaveDataStatic.RhinoPosition.x + "," + SaveDataStatic.RhinoPosition.y + "," + SaveDataStatic.RhinoPosition.z + "|";
		m_PosString += SaveDataStatic.HippoPosition.x + "," + SaveDataStatic.HippoPosition.y + "," + SaveDataStatic.HippoPosition.z + "|";
		m_PosString += SaveDataStatic.GorillaPosition.x + "," + SaveDataStatic.GorillaPosition.y + "," + SaveDataStatic.GorillaPosition.z + "|";
		m_PosString += SaveDataStatic.TigerPosition.x + "," + SaveDataStatic.TigerPosition.y + "," + SaveDataStatic.TigerPosition.z + "|";
		m_PosString += SaveDataStatic.SnakePosition.x + "," + SaveDataStatic.SnakePosition.y + "," + SaveDataStatic.SnakePosition.z + "|";
		m_PosString += SaveDataStatic.CaterpillarPosition.x + "," + SaveDataStatic.CaterpillarPosition.y + "," + SaveDataStatic.CaterpillarPosition.z + "";

		//SaveDataStatic.PandaYRotation = Panda.transform.rotation.eulerAngles.y;
		//SaveDataStatic.RhinoYRotation = Rhino.transform.rotation.eulerAngles.y;
		//SaveDataStatic.HippoYRotation = Hippo.transform.rotation.eulerAngles.y;
		//SaveDataStatic.GorillaYRotation = Gorilla.transform.rotation.eulerAngles.y;
		//SaveDataStatic.TigerYRotation = Tiger.transform.rotation.eulerAngles.y;
		//SaveDataStatic.SnakeYRotation = SamSnake.transform.rotation.eulerAngles.y;
		//SaveDataStatic.CaterpillarYRotation = Caterpillar.transform.rotation.eulerAngles.y;


		m_RotString += SaveDataStatic.PandaYRotation + "|";
		m_RotString += SaveDataStatic.RhinoYRotation + "|";
		m_RotString += SaveDataStatic.HippoYRotation + "|";
		m_RotString += SaveDataStatic.GorillaYRotation + "|";
		m_RotString += SaveDataStatic.TigerYRotation + "|";
		m_RotString += SaveDataStatic.SnakeYRotation + "|";
		m_RotString += SaveDataStatic.CaterpillarYRotation + "";

		m_MedalString = "";
		foreach (string st in SaveDataStatic.AwardedMedalList) {
			m_MedalString += st + "|";
		}
		m_MedalString = m_MedalString.Remove (m_MedalString.Length - 1);

		PlayerPrefs.SetString ("Pos", m_PosString);
		PlayerPrefs.SetString ("Rot", m_RotString);
		PlayerPrefs.SetString ("Medal", m_MedalString);
		PlayerPrefs.SetString ("Story", SaveDataStatic.StorySequence);
		PlayerPrefs.SetString ("Scene", "Town");


        StringBuilder missionList = new StringBuilder();
        foreach (KeyValuePair<string, string> pair in SaveDataStatic.MissionList)
        {
            missionList.Append(pair.Key).Append("`").Append(pair.Value).Append('*');
        }
        string m_MissListStr = missionList.ToString().TrimEnd(',');
        PlayerPrefs.SetString("Missions", m_MissListStr);

        PlayerPrefs.SetInt("Score", SaveDataStatic.Score);
//        PlayerPrefs.SetFloat("GameTime", m_TimeDiff);
        PlayerPrefs.SetInt("ChoosenAvatar", SaveDataStatic.ChoosenAvatar);
    }

    private void ReloadToCharacterPositions()
    {
        Panda = AssetLoader.GetInstance().GetCharacter(0);
        Rhino = AssetLoader.GetInstance().GetCharacter(1);
        Hippo = AssetLoader.GetInstance().GetCharacter(2);
        Gorilla = AssetLoader.GetInstance().GetCharacter(3);
        Tiger = AssetLoader.GetInstance().GetCharacter(4);
        SamSnake = AssetLoader.GetInstance().GetCharacter(5);
        Caterpillar = AssetLoader.GetInstance().GetCharacter(6);

        Panda.transform.position = SaveDataStatic.PandaPosition;
        Rhino.transform.position = SaveDataStatic.RhinoPosition;
        Hippo.transform.position = SaveDataStatic.HippoPosition;
        Gorilla.transform.position = SaveDataStatic.GorillaPosition;
        Tiger.transform.position = SaveDataStatic.TigerPosition;
        SamSnake.transform.position = SaveDataStatic.SnakePosition;
        Caterpillar.transform.position = SaveDataStatic.CaterpillarPosition;

        Panda.transform.rotation = Quaternion.Euler(new Vector3(0, SaveDataStatic.PandaYRotation, 0));
        Rhino.transform.rotation = Quaternion.Euler(new Vector3(0, SaveDataStatic.RhinoYRotation, 0));
        Hippo.transform.rotation = Quaternion.Euler(new Vector3(0, SaveDataStatic.HippoYRotation, 0));
        Gorilla.transform.rotation = Quaternion.Euler(new Vector3(0, SaveDataStatic.GorillaYRotation, 0));
        Tiger.transform.rotation = Quaternion.Euler(new Vector3(0, SaveDataStatic.TigerYRotation, 0));
        SamSnake.transform.rotation = Quaternion.Euler(new Vector3(0, SaveDataStatic.SnakeYRotation, 0));
        Caterpillar.transform.rotation = Quaternion.Euler(new Vector3(0, SaveDataStatic.CaterpillarYRotation, 0));

        ISelectObject iSel = Panda.GetComponent<ISelectObject>();

        if (SaveDataStatic.PandaInvItemNameList.Count == 0 && (SaveDataStatic.StorySequence != "GrafitiEnd" || SaveDataStatic.StorySequence != "SeaLife"))
        {
            SaveDataStatic.PandaInvItemNameList.Add("LetterCase");
            SaveDataStatic.PandaInvItemNameList.Add("ipod");
        }

        foreach (string str in SaveDataStatic.PandaInvItemNameList)
            iSel.AddInventoryItem(str);
        SaveDataStatic.PandaInvItemNameList.Clear();

        iSel = Rhino.GetComponent<ISelectObject>();
        foreach (string str in SaveDataStatic.RhinoInvItemNameList)
            iSel.AddInventoryItem(str);
        SaveDataStatic.RhinoInvItemNameList.Clear();

        iSel = Hippo.GetComponent<ISelectObject>();
        foreach (string str in SaveDataStatic.HippoInvItemNameList)
        {
            iSel.AddInventoryItem(str);
        }
        SaveDataStatic.HippoInvItemNameList.Clear();

        iSel = Tiger.GetComponent<ISelectObject>();
        foreach (string str in SaveDataStatic.TigerInvItemNameList)
            iSel.AddInventoryItem(str);
        SaveDataStatic.TigerInvItemNameList.Clear();

        iSel = Gorilla.GetComponent<ISelectObject>();
        foreach (string str in SaveDataStatic.GorillaInvItemNameList)
            iSel.AddInventoryItem(str);
        SaveDataStatic.GorillaInvItemNameList.Clear();

        iSel = SamSnake.GetComponent<ISelectObject>();
        foreach (string str in SaveDataStatic.SnakeInvItemNameList)
            iSel.AddInventoryItem(str);
        SaveDataStatic.SnakeInvItemNameList.Clear();

        iSel = Caterpillar.GetComponent<ISelectObject>();
        foreach (string str in SaveDataStatic.CaterpillarItemNameList)
            iSel.AddInventoryItem(str);
        SaveDataStatic.CaterpillarItemNameList.Clear();

        //if (!SaveDataStatic.AwardedMedalList.Contains ("Recycle")) { 
        //  SaveDataStatic.AwardedMedalList.Add ("Recycle");
        //}
        SaveDataStatic.AddToAwardedMedalList("Recycle");
    }

    public GameObject 		Rhino;
	public GameObject		Panda;
	public GameObject		Hippo;
	public GameObject		Gorilla;
	public GameObject		Tiger;

	public GameObject		FireHydrant;
	public GameObject 		WaterOnFloor;

	public GameObject		SaplingTree;

	public GameObject		Caterpillar;
	public GameObject		PostBox;
	public GameObject		Prime8SuperRemover;
	public GameObject		CatepillarCloth;

	public GameObject		SamSnake;
	public GameObject		Cloth;
	public GameObject		TrashBinfull;
	public GameObject		SmokingCar;
	public GameObject		Exauster;
	public GameObject		CarKey;

	public void BackToAmandaPanda(string _expression, Transform _pointArrowObj)
	{
		BackGroundAudio.volume = 1f;
		BackGroundAudio.Play();

		SetHasUIComeUP(false);

		ShowCursor(true);

		ExpressionTxt.text = _expression;

		m_ActiveISelObj.SelectObject();

		if(SaveDataStatic.StorySequence == "Litter")
		{
			StopAllCoroutines();

			StartCoroutine(StartPipeRepairJob());

		}
		else if(SaveDataStatic.StorySequence == "Drain") // Drain Job Complete
		{
			StopAllCoroutines();

			m_ActiveISelObj.DeSelectObject();
			m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
			m_ActiveISelObj.SelectObject();

			StartCoroutine(StartSamplingTreeJob());

			StartCoroutine(ExpressionTxtSplashing("Let's go to the post box!"));

            SaveDataStatic.StorySequence = "DrainFinish";
            SaveCharacterPositionAtEachSavePoint();

        }
		else if(SaveDataStatic.StorySequence == "SaplingTreeEnd") // Sapling Tree Job Complete
		{
			StopAllCoroutines();

            SaveCharacterPositionAtEachSavePoint();
            SaveDataStatic.StorySequence = "Graffiti"; 
			m_ActiveISelObj.DeSelectObject();

			PlayMovie();
		}
		else if(SaveDataStatic.StorySequence == "SmokeCarEnd") 
		{
            SaveCharacterPositionAtEachSavePoint();
            SaveDataStatic.StorySequence = "PostBox"; 
			m_ActiveISelObj.DeSelectObject();
			m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
			m_ActiveISelObj.SelectObject();


			SetTargets(Caterpillar.transform);
			SetTargets(CarKey.transform);
			SetTargets(CatepillarCloth.transform);
			SetTargets(Prime8SuperRemover.transform);
			SetTargets(PostBox.transform);
			SetPointArrowPosition(GetTarget());
			StartCoroutine(ExpressionTxtSplashing("Use Caterpillar to help the postman!"));
			SetSubtitleText("You need Caterpillar to help you clean off that graffiti!", 5, NeedCaterpillarAudio);

			ActivateRain (Panda);

            StatScreen.alpha = 1;

        }
		else if(SaveDataStatic.StorySequence == "GrafitiEnd") 
		{
			m_ActiveISelObj.DeSelectObject();
			m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
			m_ActiveISelObj.SelectObject();

			StopRain();

			SetTargets(PostBox.transform);
			SetPointArrowPosition(GetTarget());
			StartCoroutine(ExpressionTxtSplashing("Post the competition entry!"));
		}
		else if(SaveDataStatic.StorySequence == "SeaLife") 
		{

			TownControllerAudio.Stop();

			StopAllCoroutines();
			m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
			m_ActiveISelObj.SelectObject();

			m_HasUIComeUp = true;
            //GraffitiPostBoxImg.SetActive (true);
            EnableOrDisableGraffitiPostBoxImg(true);
			GraffitiPostBoxImg.GetComponent<Image> ().sprite = PostBoxWithoutGraffiti;

			UnHideCursor ();

			ShowMagnifierCursor (false);

			//SceneLoad("PilotEnd");
		}

	}

	IEnumerator StartPipeRepairJob()
	{
		FireHydrant.GetComponent<FireHydrant>().StartWaterLeak();

        //SaveDataStatic.StorySequence = "PRepStart";
        SaveDataStatic.StorySequence = "LitterEnd";
        SaveCharacterPositionAtEachSavePoint();

        MonkeyWrench = (GameObject)Instantiate(MonkeyWrench);
		MonkeyWrench.name = "MonkeyWrench";
		Broom = (GameObject)Instantiate(Broom);
		Broom.name = "Broom";
		Plunger = (GameObject)Instantiate(Plunger);
		Plunger.name = "Plunger";

		yield return new WaitForSeconds(0.1f);

		OnMoviePlayCompletion();
	}

	IEnumerator StartSamplingTreeJob()
	{
		SetTargets(PostBox.transform);
		SetPointArrowPosition(GetTarget());

		yield return new WaitForSeconds(8);

        SaveCharacterPositionAtEachSavePoint();
        //SaveDataStatic.StorySequence = "SaplingTree";
        SaveDataStatic.StorySequence = "SaplingTreeVideoStart";

        m_ActiveISelObj.DeSelectObject();

		StopAllCoroutines();
		PlayMovie();
	}
#endregion General

#region Cursor
	public Image			CursorImage;
	public Sprite			DefaultCursorPoint;
//	public Sprite			FocusFoundCursorPoint;
	public Sprite			MagnifireCursorPoint;
	public Texture2D		ClothTexture;
	public Texture2D		Prime8RemoverTexture;
    private float m_CursorSize = 60;

	public void ShowCursor(bool IsDefault)
	{
		if(IsDefault)
		{
            //Debug.Log("ShowCursor : Is Default");
			CursorImage.sprite = DefaultCursorPoint;
			CursorImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(m_CursorSize, m_CursorSize);
		}
		else
		{
           // Debug.Log("ShowCursor : Magnifier");
            CursorImage.sprite = Resources.Load<Sprite>("Cursors/HandCursor/" + GetActiveISelObj().Name); ;
			CursorImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(m_CursorSize, m_CursorSize);
		}
	}

	public void ShowMagnifierCursor(bool IsDefault)
	{
		if(IsDefault)
		{

            CursorImage.sprite = DefaultCursorPoint;
			CursorImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(m_CursorSize, m_CursorSize);
		}
		else
		{
            //Debug.Log("ShowMagnifierCursor : Magnifier");
            CursorImage.sprite = MagnifireCursorPoint;
			CursorImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(m_CursorSize, m_CursorSize);
		}
	}

	public void LockCursorForGame()
	{
		UnHideCursor();
        JoyStickSettings(true);
#if !(UNITY_IOS || UNITY_ANDROID)
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
#endif
    }

	public void UnLockCursorFromGame()
	{
		HideCursor();
#if !(UNITY_IOS || UNITY_ANDROID)
		Cursor.lockState = CursorLockMode.None;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
#endif
        JoyStickSettings(false);
    }

	public void SetQuizAnswerCursor()
	{
		HideCursor();
		UnLockCursorFromGame();

#if !(UNITY_IOS || UNITY_ANDROID)
        Texture2D m_HandTexture = Resources.Load("Cursors/HandIcons/"+ GetActiveISelObj().Name) as Texture2D;
        Cursor.SetCursor(m_HandTexture, Vector2.zero, CursorMode.Auto);
#endif


        //switch (GetActiveISelObj().Name)
        //{
        //    case "Panda":
        //        Cursor.SetCursor(HandTextures[0], Vector2.zero, CursorMode.Auto);
        //        break;
        //   case "Hippo":
        //        Cursor.SetCursor(HandTextures[1], Vector2.zero, CursorMode.Auto);
        //        break;
        //   case "Rhino":
        //        Cursor.SetCursor(HandTextures[2], Vector2.zero, CursorMode.Auto);
        //        break;
        //    case "Gorilla":
        //        Cursor.SetCursor(HandTextures[3], Vector2.zero, CursorMode.Auto);
        //        break;
        //    case "Snake":
        //        Cursor.SetCursor(HandTextures[4], Vector2.zero, CursorMode.Auto);
        //        break;
        //    case "Tiger":
        //        Cursor.SetCursor(HandTextures[5], Vector2.zero, CursorMode.Auto);
        //        break;
        //   case "Caterpillar":
        //        Cursor.SetCursor(HandTextures[6], Vector2.zero, CursorMode.Auto);
        //        break;
        //}
    }

    public void SetClothOrPrime8RemoverCursor(bool _isCloth)
	{
		HideCursor();
		UnLockCursorFromGame();

		if(_isCloth)
		{
			Cursor.SetCursor(ClothTexture, Vector2.zero, CursorMode.Auto);
		}
		else
		{
			Cursor.SetCursor(Prime8RemoverTexture, Vector2.zero, CursorMode.Auto);
		}
	}

	public void HideCursor()
	{
		CursorImage.gameObject.SetActive(false);
	}

	private void UnHideCursor()
	{
		CursorImage.gameObject.SetActive(true);
	}
#endregion Cursor

#region Litter Collect
	public GameObject 				LitterPicker;
	public GameObject				PickerHolster;
	public GameObject				LitterBin;
	public GameObject[]				LitterObjects;
	public bool						LitterCollectGameStart = false;
	public List<GameObject>			LitterList = new List<GameObject>();
	private float					m_GameTime = 60f;
    public string LitterGameTime
    {
        get
        { 
            if(Math.Abs(m_GameTime) < Mathf.Epsilon)
            {
                return "01:00";
            }
            else if ((60-m_GameTime) < 10)
            {
                return "00:0" + (60 - m_GameTime);
            }
            else
            {
                return "00:" + (60 - m_GameTime);
            }
        }
    }

    private GameObject				m_LitterParent;

	private Coroutine				m_GameTimerCoroutine = null;

	IEnumerator GameTimer()
	{
        SetTimerUI();

        if(m_TimerProgressBar == null)
        {
            m_TimerProgressBar = TimerUI.transform.Find("ProgressBar").gameObject.GetComponent<Image>();
            m_TimerTxt = TimerUI.transform.Find("TimerTxt").gameObject.GetComponent<Text>();
        }

        m_TimerProgressBar.fillAmount = 1;
        m_TimerTxt.text = "00:01:00";

		AudioSource m_CountDownAudio = m_TimerTxt.gameObject.GetComponent<AudioSource>();	

		while(m_GameTime > 0)
		{
			yield return new WaitForSeconds(1);

			//TimerTxt.text = (m_GameTime-0.01f).ToString("0.00");
			//m_GameTime = m_GameTime-0.01f;

            m_GameTime = m_GameTime - 1;
            if (m_GameTime < 10)
            {
                m_TimerTxt.text = "00:00:0" + m_GameTime;
            }
            else
            {
                m_TimerTxt.text = "00:00:" + m_GameTime;
            }

            m_TimerProgressBar.fillAmount = m_TimerProgressBar.fillAmount - (1f / 60f);


            if (m_CountDownAudio.isPlaying == false)
				m_CountDownAudio.Play();
		}

		if(m_GameTime <= 0)
		{
            m_TimerTxt.text = "00:00:00";

            m_TimerProgressBar.fillAmount = 0;

            m_CountDownAudio.Stop();

			OnLitterCollectGameCompletion();
		}
	}

    public void SetTimerUI(bool _activate = true)
    {
        TimerUI.SetActive(_activate);

        if (_activate)
            StatScreen.alpha = 0; 
        else
            StatScreen.alpha = 1;
       // MapCamera.enabled = !_activate;
        ControlMapCamera(!_activate);
    }


    IEnumerator TestDealyWhileRhinoWinAnimationIsPlayed()
	{
		yield return new WaitForSeconds(2f);

		AfterRhinoWinAnimationIsPlayed();
	}

	public void AfterRhinoWinAnimationIsPlayed()
	{
		GameCompletePanel.SetActive(true);
		GameCompletePanel.GetComponent<GameFinish>().SetGameFinishPanel(true);

		SetQuizAnswerCursor();
	}

	public void AfterRhinoWinMsgBoxDoneBtnPressed()
	{
		LockCursorForGame();
		HideCursor();

		m_ActiveISelObj.DeSelectObject();

        TimerUI.SetActive(false);

		// Play Panda Clap Animation
		m_ActiveISelObj = Panda.GetComponent<ISelectObject>();
		m_ActiveISelObj.DoLocalAnimation(0);
	}

	public void AfterLitterCollectTryAgainPressed()
	{
		LockCursorForGame();
		HideCursor();
        //StatScreen.SetActive(true);
        SetTimerUI(true);

		m_ActiveISelObj.DoLocalAnimation(0);
	}

	public void AfterSumbitButtonPressed()
	{
		m_ActiveISelObj.DeSelectObject();

		LockCursorForGame();
		HideCursor();
        JoyStickSettings(true);
        ControlMapCamera(true);
    }
#endregion Litter Collect

#region UI
	private string 			m_LitterCollectGamePreString;
	private string 			m_PipeRepairGamePreString;
	private string 			m_SaplingTreeGamePreString;
	private string 			m_SmokyCarGamePreString;
	private string 			m_GraffitiGamePreString;

	public AudioClip		MiniGameInsLitterAudio;
	public AudioClip		MiniGameInsPipeAudio;
	public AudioClip		MiniGameInsSamplingAudio;
	public AudioClip		MiniGameInsSmokeCarAudio;
	public AudioClip		MiniGameInsGraffitiAudio;

	public Text				GamePreTxtHolderTxt;

	private bool			m_HasUIComeUp = false;
	public void SetHasUIComeUP(bool val)
	{
		m_HasUIComeUp = val;
        JoyStickSettings(!val);
        ControlMapCamera(!val);
	}


	public Text			 	ExpressionTxt;

	public IEnumerator ExpressionTxtSplashing(string _expression)
	{
		while(true)
		{
			ExpressionTxt.text = _expression;
			yield return new WaitForSeconds(1f);
			ExpressionTxt.text = "";
			yield return new WaitForSeconds(1f);
		}
	}

    //public Text				ScoreTxt;
    public GameObject ScoreUI;
    private Text m_ScoreTxt;

	public void SetScore(int _score)
	{
        if (ScoreUI.activeSelf == false)
        {
            ScoreUI.SetActive(true);
        }

        if (m_ScoreTxt == null)
        {
            m_ScoreTxt = ScoreUI.transform.Find("ScoreTxt").gameObject.GetComponent<Text>();
        }

        SaveDataStatic.Score = SaveDataStatic.Score + _score;
        m_ScoreTxt.text = SaveDataStatic.Score + "";
    }

    void GameTimerStartup()
    {
        CancelInvoke("SetGameTime");
        InvokeRepeating("SetGameTime", 0.1f, 1f);

        if (m_ScoreTxt == null)
        {
            m_ScoreTxt = ScoreUI.transform.Find("ScoreTxt").gameObject.GetComponent<Text>();
        }
        m_ScoreTxt.text = SaveDataStatic.Score + "";
    }

    void StopTimer()
    {
        CancelInvoke("SetGameTime");
    }

    private float m_TimeFreq = 0;
    private float m_SavedTime = 0;
    public Text gameTimeTxt;
    private float m_TownActionStartTime, m_TimeDiff;
    private int m_Sec, m_Min, m_Hours;
    private string m_DisplayTimeStr;
    private void SetGameTime()
    {
        // m_TimeDiff = (m_SavedTime+Time.time) - m_TownActionStartTime;
        // m_TimeDiff = Time.time - m_TownActionStartTime;
        m_TimeDiff = m_TimeFreq + m_SavedTime;
        m_Sec = (int)(m_TimeDiff % 60);
        m_Min = (int)((m_TimeDiff / 60) % 60);
        m_Hours = (int)((m_TimeDiff / 3600) % 24);

        m_DisplayTimeStr = "";
        if (m_Hours < 10)
            m_DisplayTimeStr += ("0" + m_Hours);
        else
            m_DisplayTimeStr += m_Hours +"";

        m_DisplayTimeStr += ":";
        if (m_Min < 10)
            m_DisplayTimeStr += ("0" + m_Min);
        else
            m_DisplayTimeStr += m_Min + "";

        m_DisplayTimeStr += ":";
        if (m_Sec < 10)
            m_DisplayTimeStr += ("0" + m_Sec);
        else
            m_DisplayTimeStr += m_Sec + "";

    
        gameTimeTxt.text = m_DisplayTimeStr;
        SaveDataStatic.GameTime = m_DisplayTimeStr;

        m_TimeFreq++;
    }

    public GameObject TimerUI;
    private Image m_TimerProgressBar;
	private Text				m_TimerTxt;

	public GameObject StartMenuPanel;
	private Coroutine m_MiniGameReadingCorotine;

	public void SetGameStartPanel(bool _activate)
	{
		StartMenuPanel.SetActive(_activate);

		if(_activate)
		{
			SetQuizAnswerCursor();

			if(SaveDataStatic.StorySequence == "Litter")
			{
				GamePreTxtHolderTxt.text = m_LitterCollectGamePreString;

				TownControllerAudio.clip = MiniGameInsLitterAudio;
				TownControllerAudio.Play();
			}
            else if (SaveDataStatic.StorySequence == "LitterEnd")
            //else if(SaveDataStatic.StorySequence == "PRepStart")
			{
				GamePreTxtHolderTxt.text = m_PipeRepairGamePreString;

				TownControllerAudio.clip = MiniGameInsPipeAudio;
				TownControllerAudio.Play();
			}
			//else if(SaveDataStatic.StorySequence == "SaplingTree")
            else if(SaveDataStatic.StorySequence == "SaplingTreeVideoStart")
			{
				GamePreTxtHolderTxt.text = m_SaplingTreeGamePreString;

				TownControllerAudio.clip = MiniGameInsSamplingAudio;
				TownControllerAudio.Play();
			}
			else if(SaveDataStatic.StorySequence == "SmokeCarBegin")
			{
				GamePreTxtHolderTxt.text = m_SmokyCarGamePreString;

				TownControllerAudio.clip = MiniGameInsSmokeCarAudio;
				TownControllerAudio.Play();
			}
			else if(SaveDataStatic.StorySequence == "PostBox")
			{
				GamePreTxtHolderTxt.text = m_GraffitiGamePreString;

				TownControllerAudio.clip = MiniGameInsGraffitiAudio;
				TownControllerAudio.Play();
			}
		}
		else
		{
			UnLockCursorFromGame();
			ShowCursor(true);
		}
	}


	public void OnPlayButtonClicked()
	{
		TownControllerAudio.Stop();
		if(SaveDataStatic.StorySequence == "Litter")
		{
			//Rhino.transform.position = SaveDataStatic.RhinoLitterGameStartPos;

			if(m_LitterParent) Destroy(m_LitterParent);
			
			m_LitterParent = (GameObject)Instantiate(LitterObjects[0], Vector3.zero, Quaternion.identity);

			LitterList.Clear();
			//foreach(Transform t in m_LitterParent.transform)
			//{
			//	LitterList.Add(t.gameObject);
			//}
            for(int i = 0; i < m_LitterParent.transform.childCount; i++)
            {
                LitterList.Add(m_LitterParent.transform.GetChild(i).gameObject);
            }

            LitterCollectGameStart = true;
			SetGameStartPanel(false);

			m_ActiveISelObj.SelectObject();

			m_GameTime = 60f;

			m_GameTimerCoroutine = StartCoroutine(GameTimer());
		}
		//else if(SaveDataStatic.StorySequence == "PRepStart")
        else if(SaveDataStatic.StorySequence == "LitterEnd")
		{
			SceneLoad("PipeRepair");
		}
		//else if(SaveDataStatic.StorySequence == "SaplingTree")
        else if(SaveDataStatic.StorySequence == "SaplingTreeVideoStart")
		{
			SceneLoad("SaplingTree");
		}
		else if(SaveDataStatic.StorySequence == "SmokeCarBegin")
		{
			SmokingCar.GetComponent<SmokingCar>().StartSmokingCarGame();

			SetGameStartPanel(false);

			m_ActiveISelObj.SelectObject();
		}
		else if(SaveDataStatic.StorySequence == "PostBox")
		{
			SceneLoad("GraftiRemove");
		}
	}

	public GameObject GameCompletePanel;
	public void OnLitterCollectGameCompletion()
	{
		LitterCollectGameStart = false;

		if(m_GameTimerCoroutine != null)
		{
			StopCoroutine(m_GameTimerCoroutine);
		}

		HideCursor();

		if(m_GameTime > 0 && LitterList.Count == 0)
		{
			//Win

			m_ActiveISelObj.DoLocalAnimation(1);

            //TODO: For Testing, Remove in Production
            //StartCoroutine(TestDealyWhileRhinoWinAnimationIsPlayed());

        }
		else
		{
			//Lost

			GameCompletePanel.SetActive(true);

			SetQuizAnswerCursor();

			m_ActiveISelObj.DeactivateFPS();

			GameCompletePanel.GetComponent<GameFinish>().SetGameFinishPanel(false);
		}
	}

	public CanvasGroup		StatScreen;

    public Text TestSubtitileTxt;
	public string				SubtitleTxt;
    public SubtitleManager  subtitleManager;

    private Queue<SubtitleStruct>	m_SubtitleQueue = new Queue<SubtitleStruct>();
	public bool			IsSubtitleStillDisplaying = false;
	private int 		m_SubIndex = 0;

	struct SubtitleStruct
	{
		public string 	 Subtitle;
		public float 	 Seconds;
		public AudioClip SubAudio;
	};

	public void SetSubtitleText(string _subtitle, float _seconds, AudioClip _subAudio)
	{
		SubtitleStruct subStruct;
		subStruct.Subtitle = _subtitle;
		subStruct.Seconds = _seconds;
		subStruct.SubAudio = _subAudio;
		m_SubtitleQueue.Enqueue(subStruct);
	}

	public void StopSubtitleCoroutine()
	{
		if(m_ClearSubtitleCoroutine != null)
		{
			StopCoroutine(m_ClearSubtitleCoroutine);
			m_ClearSubtitleCoroutine = null;
			//SubtitleTxt.text = "";
            subtitleManager.Stop();
            IsSubtitleStillDisplaying = false;
		}
	}

	private Coroutine m_ClearSubtitleCoroutine = null;
	float m_Time = 0;
	IEnumerator ClearSubtitleTextAfterDelay()
	{
		IsSubtitleStillDisplaying = true;

		SubtitleStruct subStruct = (SubtitleStruct)m_SubtitleQueue.Dequeue();
		//SubtitleTxt.text = subStruct.Subtitle;
        SubtitleTxt = subStruct.Subtitle;

        //m_Time = 0;

        if (subStruct.SubAudio != null) {
			TownControllerAudio.clip = subStruct.SubAudio;
			TownControllerAudio.Play ();

			BackGroundAudio.volume = 0.3f;
			PandaAdHocAudio.mute = true;
			SmokingCar.GetComponent<AudioSource> ().volume = 0.3f;

            if (SaveDataStatic.forTesting || SaveDataStatic.forSubsOnlyTesting) {
                TestSubtitileTxt.text = subStruct.Subtitle;
            }
            else
            {
                subtitleManager.DisplaySubtitle(subStruct.Subtitle, subStruct.SubAudio.length, true);
        
                while (TownControllerAudio.isPlaying)
    			{
    				//m_Time += Time.deltaTime;
    				yield return null;
    			}
            }

            PandaAdHocAudio.mute = false;
			SmokingCar.GetComponent<AudioSource> ().volume = 1;
			BackGroundAudio.volume = 1;


        }
        else
        {
            if (SaveDataStatic.forTesting)
            {
                TestSubtitileTxt.text = subStruct.Subtitle;
            }
            else
            {
                subtitleManager.DisplaySubtitle(subStruct.Subtitle, 5, true);
            }
        }

        //      if (m_Time < subStruct.Seconds)
        //{
        //	yield return new WaitForSeconds(subStruct.Seconds-m_Time);
        //}

        //SubtitleTxt.text = "";
        TestSubtitileTxt.text = "";

       m_SubIndex++;

		if (m_SubIndex == 3 && SaveDataStatic.StorySequence == "Litter") {
			Panda.GetComponent<FirstPersonController> ().enabled = true;

			Instantiate (SeaGullVidTrigger);
		}

		IsSubtitleStillDisplaying = false;
	}

	public GameObject SeaGullVidTrigger;
	public void PlayTheSeagullVideo()
	{
		if (MapCamera.enabled)
			MapCamera.enabled = false;
		
		SaveDataStatic.PlayTheSeagullVideo = true;
		m_ActiveISelObj.DeSelectObject();
		StopAllCoroutines();
        StatScreen.alpha = 0; 
        PlayMovie();
	}
//	IEnumerator ShowTheSeagullVideoAfterSomeTime()
//	{
//		yield return new WaitForSeconds (10);
//
//	}

	public GameObject	QuitGameMsgPanel;
	public void SetOrUnsetQuitGamePanel(bool _activate)
	{
		QuitGameMsgPanel.SetActive(_activate);

		if(_activate) 
		{
			SetQuizAnswerCursor();
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
			LockCursorForGame();
			ShowCursor(true);
		}
	}

	public void OnQuitYesClicked()
	{
        SaveDataStatic.SceneName = "ComingToHome";
        SaveCharacterPositionAtEachSavePoint();
        SaveGameOnExit ();
        SceneLoad("welcome");
	}

	public void OnQuitNoClicked()
	{
		SetOrUnsetQuitGamePanel(false);
	}
    #endregion UI

    #region Pipe Repair
    public GameObject	MonkeyWrench;
	public GameObject	Broom;
	public GameObject	Plunger;

	public void AfterHippoWinMsgBoxDoneBtnPressed()
	{
		m_ActiveISelObj = Hippo.GetComponent<ISelectObject>();
		m_ActiveISelObj.SelectObject();
		m_ActiveISelObj.DeactivateFPS();

		if(!SaveDataStatic.forTesting)
		{
			Quiz.GetInstance().ShowQuez(1);
		}
		else
		{
			AfterPipReparingComplete();
		}
	}

	public void AfterPipReparingComplete()
	{
        StatScreen.alpha = 1;
		BackGroundAudio.volume = 1;
		BackGroundAudio.Play();

		SetHasUIComeUP(false);

        m_ActiveISelObj.SelectObject();

		SetTargets(WaterOnFloor.transform);
		SetPointArrowPosition(GetTarget());

		SaveDataStatic.StorySequence = "Drain"; 

		m_ActiveISelObj.AddInventoryItem ("Plunger");
		m_ActiveISelObj.AddInventoryItem ("Broom");
	}
#endregion Pipe Repair

#region Inventory Control
	public void CloseOrOpenInventory()
	{
		if(!InventoryPanel.GetInstance().IsInventoryActive())
		{
			if(!String.IsNullOrEmpty(InventoryPanel.GetInstance().GetInventoryItemSelected()))
			{
				InventoryPanel.GetInstance().RemoveInventorySelectedImage();
			}

			m_ActiveISelObj.DeactivateFPS();

			InventoryPanel.GetInstance ().OnPointerExit ();

			InventoryPanel.GetInstance().SetInventory(m_ActiveISelObj.InventoryItemList.ToArray(), 
				m_ActiveISelObj.InventoryNameList.ToArray(), m_ActiveISelObj.RuffSackSprite, m_ActiveISelObj.MedalSpriteList.ToArray());

			SetQuizAnswerCursor();
            JoyStickSettings(false);
		}
		else
		{
			m_ActiveISelObj.SelectObject();
			InventoryPanel.GetInstance().UnSetInventory();
            JoyStickSettings(true);
        }
	}
#endregion Inventory Control

#region Graffiti
	private  bool m_PandaClickOnPostBoxFirstTime = false;
	public GameObject GraffitiPostBoxImg;
    private bool m_GraffitiPostBoxImgStatus = false;
    private void EnableOrDisableGraffitiPostBoxImg(bool _activate)
    {
        m_GraffitiPostBoxImgStatus = _activate;
        GraffitiPostBoxImg.SetActive(_activate);

        if(_activate)
        {
#if UNITY_IOS || UNITY_ANDROID
            GraffitiPostBoxImg.transform.GetChild(0).Find("Text").gameObject.GetComponent<Text>().text = "Press \"Select\" to proceed!";
#else
            GraffitiPostBoxImg.transform.GetChild(0).Find("Text").gameObject.GetComponent<Text>().text = "Click anywhere to proceed!";
#endif

            StatScreen.alpha = 0; 
        }
        else
        {
            StatScreen.alpha = 1;
        }
    }
    public Sprite	PostBoxWithoutGraffiti;
	public GameObject CaterpillarPhotoImg;

	public void OnPostBoxSelected()
	{
		if(GetActiveISelObj().Name == "Caterpillar")
		{
			if(String.IsNullOrEmpty(InventoryPanel.GetInstance().GetInventoryItemSelected()))
			{
				SetSubtitleText("Open your rufflesack and combine the super remover and cloth. Then use the cloth on the graffiti!", 5, CombineSuperRemoverWithClothAudio);
				SetTargets(PostBox.transform);
				SetPointArrowPosition(GetTarget());
			}
			else if(InventoryPanel.GetInstance().whatIsSelectedtoCombine == "CombinedForGraffiti")
			{
				InventoryPanel.GetInstance().whatIsSelectedtoCombine = null;
				m_ActiveISelObj.DeactivateFPS();
				SetGameStartPanel(true);
			}
			else
			{
				SetSubtitleText("Open your rufflesack and combine the super remover and cloth. Then use the cloth on the graffiti!", 5, CombineSuperRemoverWithClothAudio);
				SetTargets(PostBox.transform);
				SetPointArrowPosition(GetTarget());
				InventoryPanel.GetInstance().RemoveInventorySelectedImage();
			}
		}
		else
		{
			if(SaveDataStatic.StorySequence != "GrafitiEnd")
			{
				StartCoroutine(PandaSelectedPostBox());
			}
			else
			{
                if (InventoryPanel.GetInstance().GetInventoryItemSelected() == "LetterCase")
				{
					GetActiveISelObj().RemoveFromInventory(InventoryPanel.GetInstance().GetInventoryItemSelected());
					InventoryPanel.GetInstance().RemoveInventorySelectedImage();
					m_ActiveISelObj.DeSelectObject();
					StopAllCoroutines();
                    StatScreen.alpha = 0; 
                    PlayMovie();
				}
				//else if(String.IsNullOrEmpty(InventoryPanel.GetInstance().GetInventoryItemSelected()))
				//{
				//	SetSubtitleText("Select your competition entry from your rufflesack and place it in the postbox.", 2, PostTheEntryAudio);
				//	SetTargets(PostBox.transform);
				//	SetPointArrowPosition(GetTarget());
				//}
				else
				{
					SetSubtitleText("Select your competition entry from your rufflesack and place it in the postbox.", 2, PostTheEntryAudio);
					SetTargets(PostBox.transform);
					SetPointArrowPosition(GetTarget());
				}
			}
		}
	}

	IEnumerator PandaSelectedPostBox()
	{
		SetSubtitleText(WhereIsCaterpillarString, 10, WhereIsCaterpillarAudio);

		while(TownControllerAudio.isPlaying){ yield return null; }

		yield return new WaitForSeconds(3);

		//SubtitleTxt.text = "";
        subtitleManager.Stop();

		SaveDataStatic.StorySequence = "SmokeCarBegin";
		m_ActiveISelObj.DeSelectObject();
		StopAllCoroutines();
        StatScreen.alpha = 0; 
        PlayMovie();

		SmokingCar.GetComponent<SmokingCar>().StartSmokeInCar();
	}

	public void AfterClothSelected()
	{
		SetSubtitleText(HeadBackToCarParkSubtitle, 10, HeadBackToCarParkAudio);
	}

	public void AfterTrashBinFullSelected()
	{
		m_ActiveISelObj.DeactivateFPS();
		SetGameStartPanel(true);
	}

	public void AfterExausterBlocked()
	{
		InventoryPanel.GetInstance().UnsetInvSelectedImage();

		SmokingCar.GetComponent<SmokingCar>().StopTheSmokingCar();
		SaveDataStatic.StorySequence = "SmokeCarEnd";

        StartCoroutine(StartPlayingTheMovieAfterSmokeCar());
	}

	IEnumerator StartPlayingTheMovieAfterSmokeCar()
	{
		AudioSource m_SmokingCarAudioSource = SmokingCar.GetComponent<SmokingCar>().GetSmokingCarAudioSource();
//		m_SmokingCarAudioSource.clip = SmokingCar.GetComponent<SmokingCar>().BlastAudioClip;
//		m_SmokingCarAudioSource.Play();
//
//		yield return new WaitForSeconds(SmokingCar.GetComponent<SmokingCar>().BlastAudioClip.length);


		m_SmokingCarAudioSource.clip = SmokingCar.GetComponent<SmokingCar>().LaughterAudioClip;
		m_SmokingCarAudioSource.Play();

		yield return new WaitForSeconds(SmokingCar.GetComponent<SmokingCar>().LaughterAudioClip.length);

		BackGroundAudio.Play();

		m_ActiveISelObj.DeSelectObject();
		StopAllCoroutines();
        StatScreen.alpha = 0; 
        PlayMovie();
	}
#endregion Graffiti
}
