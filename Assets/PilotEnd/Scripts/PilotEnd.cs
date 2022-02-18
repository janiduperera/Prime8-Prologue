using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Networking;

public class PilotEnd : MonoBehaviour {

	public Text       SubtitleTxt;
	public AudioSource CreditAudio;
	public GameObject  CreaditScroll;
	public GameObject  SongMovie;
	public Texture2D		HandTexture;

	public GameObject 	BlackBackground;
    public GameObject EndSlide;

	#if UNITY_WEBGL
	private WebGLMovieTexture	m_MovieTexture;
	#else
	public VideoPlayer	    m_MovieTexture;
	#endif
	private AudioSource		m_MovieAudioSource;

	public GameObject		QuitGame;

	// Use this for initialization
	void Start () {
#if UNITY_WEBGL
#else

#endif
#if UNITY_ANDROID
        QuitButton.SetActive(true);
#else
         QuitButton.SetActive(false);
#endif

        //StartCoroutine(ShowCredits());

        App42Leaderboard.GetInstace().Initialize();
        StartCoroutine(ShowEndScreen());
	}

    public Text FinalScore, FinalTime;

    private IEnumerator ShowEndScreen()
    {
        float m_Alpha = 0;

        SubtitleTxt.color = new Color(1, 1, 1, m_Alpha);

        SubtitleTxt.text = "To be continued...";

        while (m_Alpha < 1)
        {
            yield return new WaitForSeconds(0.5f);

            m_Alpha++;

            SubtitleTxt.color = new Color(1, 1, 1, m_Alpha);
        }

        SubtitleTxt.color = new Color(1, 1, 1, 1);

        yield return new WaitForSeconds(1.5f);

        SubtitleTxt.color = new Color(1, 1, 1, 0);
        SubtitleTxt.gameObject.transform.parent.parent.gameObject.SetActive(false);

        CreditAudio.Play();
        FinalScore.text = SaveDataStatic.Score + "";
        FinalTime.text = SaveDataStatic.GameTime + "";

        //yield return new WaitForSeconds(3);

        if (CreditPanel.activeSelf == false)
        {
            CreditPanel.SetActive(true);
        }

        CreditPanel.transform.GetChild(0).Find("CreditScroller").gameObject.GetComponent<CreditScroll>().StartScroll();

        //EndSlide.GetComponent<Animator>().enabled = true;
    }

    public GameObject CreditPanel;
    public void CreditSkipButtonPressed()
    {
        CreditPanel.transform.GetChild(0).Find("CreditScroller").gameObject.GetComponent<CreditScroll>().StopScroll();
        CreditPanel.SetActive(false);
        StartCoroutine(StopTheCreditScroll());
    }

    IEnumerator StopTheCreditScroll()
    {
        yield return new WaitForSeconds(3);
        EndSlide.GetComponent<Animator>().enabled = true;
    }

    public void WebSiteBtnClicked()
    {
        Application.OpenURL("https://www.primasia.org/");
    }

    public void PlayAgainClicked()
    {
        string m_NickName = PlayerPrefs.GetString("Prime8Player", "");
        int m_ScoreShare = PlayerPrefs.GetInt("IsFirstTimeSharing", 0);

        PlayerPrefs.DeleteAll(); // No need to save after game end

        PlayerPrefs.SetString("Prime8Player", m_NickName.Trim());
        PlayerPrefs.SetInt("IsFirstTimeSharing", m_ScoreShare);

        SaveDataStatic.ResetCharacterPositionAndRotation();
        SaveDataStatic.SceneName = "ComingToHome";
        SceneManager.LoadSceneAsync("welcome");
    }

    public GameObject QuitButton;
    public void QuitClicked()
    {
        string m_NickName = PlayerPrefs.GetString("Prime8Player", "");
        int m_ScoreShare = PlayerPrefs.GetInt("IsFirstTimeSharing", 0);

        PlayerPrefs.DeleteAll(); // No need to save after game end

        PlayerPrefs.SetString("Prime8Player", m_NickName.Trim());
        PlayerPrefs.SetInt("IsFirstTimeSharing", m_ScoreShare);
        Application.Quit();
    }

    IEnumerator ShowCredits()
	{
		float m_Alpha = 0;

		SubtitleTxt.color = new Color (1, 1, 1,  m_Alpha);

		SubtitleTxt.text = "To be continued...";

		while(m_Alpha < 1)
		{
			yield return new WaitForSeconds(0.5f);

			m_Alpha++;

			SubtitleTxt.color = new Color (1, 1, 1,  m_Alpha);
		}

		SubtitleTxt.color = new Color (1, 1, 1,  1);

		yield return new WaitForSeconds(1.5f);

		SubtitleTxt.color = new Color (1, 1, 1,  0);

		CreaditScroll.SetActive(true);
		CreditAudio.Play();

	}

	public void AfterCreditFinish()
	{
		CreditAudio.Stop();
		StartCoroutine(ShowMeanWhileBackInModernDay());
	}

	IEnumerator ShowMeanWhileBackInModernDay()
	{
		SubtitleTxt.text = "Meanwhile back in the modern day…";

		float m_Alpha = 0;

		SubtitleTxt.color = new Color (1, 1, 1,  m_Alpha);

		while(m_Alpha < 1)
		{
			yield return new WaitForSeconds(0.5f);

			m_Alpha++;

			SubtitleTxt.color = new Color (1, 1, 1,  m_Alpha);
		}

		SubtitleTxt.color = new Color (1, 1, 1,  1);

		yield return new WaitForSeconds(1.5f);

		SubtitleTxt.color = new Color (1, 1, 1,  0);
        EndSlide.SetActive(true);

        BlackBackground.SetActive(false);


//		SongMovie.SetActive(true);

//		SongMovie.transform.localScale = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x-Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x,
//			Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y-Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y, 1);

//		m_MovieAudioSource = SongMovie.GetComponent<AudioSource>();


//#if UNITY_WEBGL
//		m_MovieTexture = new WebGLMovieTexture("StreamingAssets/Prime 8 - FinalSongAndCredit.mp4");
//		SongMovie.GetComponent<Renderer>().material.mainTexture = m_MovieTexture;
//		yield return null;
//#else
//        //SongMovie.GetComponent<Renderer>().material.mainTexture = SongVideo;
//        //m_MovieTexture = SongMovie.GetComponent<Renderer>().material.mainTexture as MovieTexture;
//        //m_MovieAudioSource.clip = m_MovieTexture.audioClip;

//        //m_MovieAudioSource.Play();
//        //m_MovieTexture.Play();
//        m_MovieTexture.loopPointReached += EndReachedOnVideo;
//        m_MovieTexture.url = Application.streamingAssetsPath + "/Prime 8 - FinalSongAndCredit.mp4";

//        //Set Audio Output to AudioSource
//        m_MovieTexture.audioOutputMode = VideoAudioOutputMode.AudioSource;

//        //Assign the Audio from Video to AudioSource to be played
//        m_MovieTexture.EnableAudioTrack(0, true);
//        m_MovieTexture.SetTargetAudioSource(0, m_MovieAudioSource);
//        m_MovieTexture.controlledAudioTrackCount = 1;
//        m_MovieTexture.Play();
//#endif


	}

    void EndReachedOnVideo(VideoPlayer _vp)
    {
        SongMovie.SetActive(false);

        QuitGame.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Cursor.SetCursor(HandTexture, Vector2.zero, CursorMode.Auto);
        m_MovieTexture.gameObject.SetActive(false);
    }

    void Update()
	{
#if UNITY_WEBGL
		if(SongMovie.activeSelf)
		{
			m_MovieTexture.Update ();

			if (Mathf.Approximately (m_MovieTexture.time, m_MovieTexture.duration)) {

				Destroy(SongMovie.GetComponent<Renderer>().material.mainTexture);
				SongMovie.SetActive(false);

				QuitGame.SetActive(true);

				Cursor.lockState = CursorLockMode.None;
				Cursor.lockState = CursorLockMode.Confined;
				Cursor.visible = true;
				Cursor.SetCursor(HandTexture, Vector2.zero, CursorMode.Auto);

				System.GC.Collect();
			}
		}
#endif
	}

	public void PlayAgainYes()
	{
		SceneManager.LoadSceneAsync("Instructions");
	}

	public void PlayAgainNo()
	{
		Application.Quit();
	}

#region Scores
    public GameObject LoadingPanel;
    public GameObject ShareButton, InputSection;
    public GameObject EmailInputSection;

    public void ShareButtonClick()
    {
        //PlayerPrefs.SetString("Prime8Player", "JPPerera");
        //SaveDataStatic.Score = 10100;
        //SaveDataStatic.GameTime = "01:05:54";
        //App42Leaderboard.GetInstace().SaveScore(PlayerPrefs.GetString("Player"), PlayerPrefs.GetString("Email"), SaveDataStatic.Score, SaveDataStatic.GameTime, null, this);

        //return;
        if (System.String.IsNullOrEmpty(PlayerPrefs.GetString("Prime8Player", "")))
        {
            BringInMessagePanel("We need your \"nickname\". Enter a \"nickname\" and submit the score again.");
            ShareButton.SetActive(false);
            InputSection.SetActive(true);
            EmailInputSection.SetActive(false);
        }
        else
        {
            LoadingPanel.SetActive(true);
            App42Leaderboard.GetInstace().GetStorageData("CheckNickName", this);
        }

       // StartCoroutine(ShareScoreAPICall());
    }

    public void AfterCheckNickNameFinish(bool _IsPresent)
    {
        if(_IsPresent)
        {
            if (PlayerPrefs.GetInt("IsFirstTimeSharing", 0) == 0) // Is Sharing for first time, Check if NickName Exist
            {
                LoadingPanel.SetActive(false);
                BringInMessagePanel("Sorry...this nickname is already in use. Please try again!");
                ShareButton.SetActive(false);
                InputSection.SetActive(true);
                EmailInputSection.SetActive(false);
            }
            else // User has already posted score with current NickName. UPDATE Query. 
            {
                if (PlayerPrefs.GetString("Email", "null") == "null")
                {
                    LoadingPanel.SetActive(false);

                    ShareButton.SetActive(false);
                    InputSection.SetActive(false);
                    EmailInputSection.SetActive(true);
                }
                else
                {
                    App42Leaderboard.GetInstace().SaveScore(PlayerPrefs.GetString("Prime8Player", ""), PlayerPrefs.GetString("Email"), SaveDataStatic.Score, SaveDataStatic.GameTime, null, this);

                }
            }
        }
        else // User posting the score for the first time with Current Nickname. INSERT Query.
        {
            if (PlayerPrefs.GetString("Email", "null") == "null")
            {
                LoadingPanel.SetActive(false);

                ShareButton.SetActive(false);
                InputSection.SetActive(false);
                EmailInputSection.SetActive(true);
            }
            else
            {
                App42Leaderboard.GetInstace().SaveScore(PlayerPrefs.GetString("Prime8Player", ""), PlayerPrefs.GetString("Email"), SaveDataStatic.Score, SaveDataStatic.GameTime, null, this);

            }
        }
    }

    public void OnSubmitWithNickName()
    {
        if (System.String.IsNullOrEmpty(InputSection.transform.Find("NickNameInputField").gameObject.GetComponent<InputField>().text))
        {
            BringInMessagePanel("We need your \"nickname\". Enter a \"nickname\" and submit the score again.");
        }
        else
        {
            string m_NickName = InputSection.transform.Find("NickNameInputField").gameObject.GetComponent<InputField>().text.Trim();
            PlayerPrefs.SetString("Prime8Player", m_NickName);
            LoadingPanel.SetActive(true);
            App42Leaderboard.GetInstace().GetStorageData("CheckNickName", this);
            // PlayerPrefs.SetString("Prime8Player", InputSection.transform.Find("NickNameInputField").gameObject.GetComponent<InputField>().text);
            // StartCoroutine(ShareScoreAPICall());
        }
    }

    public void OnEmailSectionBtnPress(bool _submit)
    {
        if(_submit)
        {
            if (System.String.IsNullOrEmpty(EmailInputSection.transform.Find("InputEmail").Find("EmailInputField").gameObject.GetComponent<InputField>().text))
            {
                PlayerPrefs.SetString("Email", "User didn't put email.");
            }
            else
            {
                string m_Email = EmailInputSection.transform.Find("InputEmail").Find("EmailInputField").gameObject.GetComponent<InputField>().text.Trim();
                PlayerPrefs.SetString("Email", m_Email);
                LoadingPanel.SetActive(true);
            }
        }
        else
        {
            PlayerPrefs.SetString("Email", "User didn't put email.");
        }
       
        LoadingPanel.SetActive(true);
        App42Leaderboard.GetInstace().SaveScore(PlayerPrefs.GetString("Prime8Player", ""), PlayerPrefs.GetString("Email"), SaveDataStatic.Score, SaveDataStatic.GameTime, null, this);
    }

    //IEnumerator ShareScoreAPICall() // This is not used
    //{
    //    LoadingPanel.SetActive(true);
    //    if(PlayerPrefs.GetInt("IsFirstTimeSharing", 0) == 0) // Is Sharing for first time, Check if NickName Exist
    //    {
    //        if(System.String.IsNullOrEmpty(PlayerPrefs.GetString("Player")))
    //        {
    //            LoadingPanel.SetActive(false);
    //            BringInMessagePanel("We need your \"nickname\". Enter a \"nickname\" and submit the score again.");
    //            ShareButton.SetActive(false);
    //            InputSection.SetActive(true);
    //        }
    //        else
    //        {
    //            /*
    //            WWWForm form = new WWWForm();
    //            form.AddField("NickName", PlayerPrefs.GetString("Player"));

    //            UnityWebRequest m_WWWWebRequest = UnityWebRequest.Post(SaveDataStatic.URL+ "CheckUserAvailability.php", form);
    //            yield return m_WWWWebRequest.SendWebRequest();

    //            if (m_WWWWebRequest.isNetworkError || m_WWWWebRequest.isHttpError)
    //            {
    //                Debug.Log(m_WWWWebRequest.error);
    //                LoadingPanel.SetActive(false);
    //                BringInMessagePanel("Your internet connection is not working. Please check your connection.");
    //                ShareButton.SetActive(true);
    //                InputSection.SetActive(false);
    //            }
    //            else
    //            {
    //                Debug.Log(m_WWWWebRequest.downloadHandler.text);

    //                if (m_WWWWebRequest.downloadHandler.text == "True")
    //                {
    //                    //Nickname used already
    //                    LoadingPanel.SetActive(false);
    //                    BringInMessagePanel("Ah! Sorry, it seems your nickname has been used by someone else. Let's try with a different nickname.");
    //                    ShareButton.SetActive(false);
    //                    InputSection.SetActive(true);
    //                }
    //                else if (m_WWWWebRequest.downloadHandler.text == "False")
    //                {
    //                    //Nickname NOT Used
    //                    PlayerPrefs.SetInt("IsFirstTimeSharing", 1);
    //                    StartCoroutine(SetLeaderBoard());
    //                }
    //                else
    //                {
    //                    //Problem
    //                    LoadingPanel.SetActive(false);
    //                    LoadingPanel.SetActive(false);
    //                    BringInMessagePanel("Your internet connection is not working. Please check your connection.");
    //                    ShareButton.SetActive(true);
    //                    InputSection.SetActive(false);
    //                }
    //            }
    //            */

    //            //Nickname NOT Used
    //            PlayerPrefs.SetInt("IsFirstTimeSharing", 1);
    //            //StartCoroutine(SetLeaderBoard());
    //            App42Leaderboard.GetInstace().SaveScore(PlayerPrefs.GetString("Player"), SaveDataStatic.Score, SaveDataStatic.GameTime, null, this);
    //        }
    //    }
    //    else
    //    {
    //        App42Leaderboard.GetInstace().SaveScore(PlayerPrefs.GetString("Player"), SaveDataStatic.Score, SaveDataStatic.GameTime, null, this);
    //        //StartCoroutine(SetLeaderBoard());
    //    }
    //    yield return null;
    //}

    /*
    IEnumerator SetLeaderBoard()
    {
        WWWForm form = new WWWForm();
        form.AddField("NickName", PlayerPrefs.GetString("Prime8Player"));
        form.AddField("FinalScore", SaveDataStatic.Score);
        form.AddField("GameTime", SaveDataStatic.GameTime);

        UnityWebRequest m_WWWWebRequest = UnityWebRequest.Post(SaveDataStatic.URL + "SetLeaderboard.php", form);
        yield return m_WWWWebRequest.SendWebRequest();

        if (m_WWWWebRequest.isNetworkError || m_WWWWebRequest.isHttpError)
        {
            Debug.Log(m_WWWWebRequest.error);
            LoadingPanel.SetActive(false);
            BringInMessagePanel("Your internet connection is not working. Please check your connection.");
            ShareButton.SetActive(true);
            InputSection.SetActive(false);
        }
        else
        {
            Debug.Log(m_WWWWebRequest.downloadHandler.text);

            if (m_WWWWebRequest.downloadHandler.text == "Error_Update" || m_WWWWebRequest.downloadHandler.text == "Error_Insert")
            {
                //Some Error in updating or inserting scores.
                BringInMessagePanel("Your internet connection is not working. Please check your connection.");
                ShareButton.SetActive(true);
                InputSection.SetActive(false);

            }
            else if (m_WWWWebRequest.downloadHandler.text == "Ok_Update" || m_WWWWebRequest.downloadHandler.text == "Ok_Insert")
            {
                //Update or Insert successful
                BringInMessagePanel("Thank you! for posting your score with us.");
                ShareButton.SetActive(true);
                InputSection.SetActive(false);
            }
            else
            {
                //Problem
                BringInMessagePanel("Your internet connection is not working. Please check your connection.");
                ShareButton.SetActive(true);
                InputSection.SetActive(false);
            }
        }

        LoadingPanel.SetActive(false);
    }
    */

    public GameObject MessagePanel;
    public void BringInMessagePanel(string _msg)
    {
        MessagePanel.SetActive(true);
        MessagePanel.transform.GetChild(0).Find("Text").gameObject.GetComponent<Text>().text = _msg;
    }
#endregion Scores

}
