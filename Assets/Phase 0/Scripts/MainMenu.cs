using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour {

	public GameObject userDetailPanel;
	public GameObject welcomePanel;
    public GameObject LoadingPanel;
	public InputField nameField;
	public Text textField;

	private GameObject soundManager;
	private GameObject inventory;

	public GameObject MusicLoop;

	private string m_SceneToLoad;

	public GameObject LoadNewGameOrStartNew;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start(){

#if UNITY_EDITOR
       // PlayerPrefs.DeleteAll ();
#endif

        App42Leaderboard.GetInstace().Initialize();

        GetSavedData ();

		//if (m_SceneToLoad == "Intro" || m_SceneToLoad == "Room" || m_SceneToLoad == "Kitchen" || m_SceneToLoad == "Town"){

		//	LoadNewGameOrStartNew.SetActive (true);
		//}
		//else{
			MusicLoop.SetActive (true);

        SelectAvatarFace.sprite = AvatarFaces[SaveDataStatic.ChoosenAvatar];



            nameField.text = PlayerPrefs.GetString ("Prime8Player", "");
            #if !(UNITY_IOS || UNITY_ANDROID)
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
#endif
			
			soundManager = GameObject.Find ("SoundManager");
			inventory = GameObject.Find ("Inventory");
			if (soundManager)
				Destroy (soundManager);
			if (inventory)
				Destroy (inventory);
		//}

        //LoadingPanel = GameObject.FindWithTag("Loading");
        //if(LoadingPanel)
        //{
        //    LoadingPanel.transform.Find("GameLoading").gameObject.GetComponent<Animator>().enabled = true;
        //}

        if (SaveDataStatic.SceneName == "")
        {
            LoadingPanel.SetActive(true);
            StartCoroutine(LoadingAnimation());
        }
        else
        {
            SaveDataStatic.SceneName = "";
            LoadingPanel.SetActive(false);
        }
    }

	void Update(){
#if UNITY_EDITOR
        if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit();
		}
#endif
    }

	public void ClickOkay(){
		userDetailPanel.SetActive (false);

        //SaveDataStatic.StorySequence = "SeaLife";
        //m_SceneToLoad = "Town";
        Debug.Log("Click Submit Pressed :  " + m_SceneToLoad);
        if (m_SceneToLoad == "Intro" || m_SceneToLoad == "Room" || m_SceneToLoad == "Kitchen" || m_SceneToLoad == "Town")
        {
            LoadNewGameOrStartNew.SetActive(true);
        }
        else
        {
            string m_NickName = nameField.text;
            welcomePanel.SetActive(true);

            textField.text = "Hello! " + m_NickName + "\n";

            PlayerPrefs.SetInt("GameTime", 0);
            PlayerPrefs.SetString("Prime8Player", nameField.text.Trim());
            Debug.Log("Submit Else Working :  " + nameField.text.Trim());
        }
    }

    IEnumerator LoadingAnimation()
    {

        float x = 0;
        Text m_LoadingTxt = LoadingPanel.transform.GetChild(0).Find("Text").gameObject.GetComponent<Text>();
        m_LoadingTxt.text = "Loading";
        string m_Dot = "";
        while (x < 6)
        //while(!m_LeaderboardLoaded)
        {
            if(m_Dot.Length >= 3)
            {
                m_Dot = "";
            }
            m_Dot += ".";
            x += 1f;
            m_LoadingTxt.text = "Loading" + m_Dot;
            yield return new WaitForSeconds(1f);
        }


        //LoadingPanel.SetActive(false);
        LoadingPanel.GetComponent<Animator>().enabled = true;
    }

   

    public void NavigateLevel(int index){

        //SceneManager.LoadSceneAsync (index);
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadSceneAsync("Intro");
    }

    public void Quit(){
		Application.Quit();
	}

	//void OnApplicationQuit()
	//{
	//	SaveGameOnExit ();
	//}

    private void OnApplicationPause(bool pause)
    {
        //if(pause)
        //{
        //    SaveGameOnExit();
        //}
    }

    public void SaveGameOnExit()
	{
		PlayerPrefs.SetString ("Scene", "welcome");
	}

	private void GetSavedData()
	{
		m_SceneToLoad = PlayerPrefs.GetString ("Scene", "welcome");
		SaveDataStatic.StorySequence = PlayerPrefs.GetString ("Story", "Litter");

//#if UNITY_EDITOR
        Debug.Log("Scene to Load :  " + m_SceneToLoad + " - Story : " + SaveDataStatic.StorySequence);
//#endif

        SaveDataStatic.MissionList.Clear();
        string m_MissStr = PlayerPrefs.GetString("Missions", "null");
        if(m_MissStr == "null")
        {
            SaveDataStatic.MissionList.Add("Complete recycling competition", "null");
            SaveDataStatic.MissionList.Add("Collect Litter!", "null");
            SaveDataStatic.MissionList.Add("Repair pipe and clear that drain!", "null");
            SaveDataStatic.MissionList.Add("Help Tiny Tiger with Sapling Tree!", "null");
            SaveDataStatic.MissionList.Add("Stop the smoking car!", "null");
            SaveDataStatic.MissionList.Add("Help Postman to clear Graffiti!", "null");
        }
        else
        {

            // Divide all pairs (remove empty strings)
            string[] tokens3 = m_MissStr.Split(new char[] { '`', '*' },
              System.StringSplitOptions.RemoveEmptyEntries);
            // Walk through each item
            for (int i = 0; i < tokens3.Length; i += 2)
            {
                string Key = tokens3[i];
                string Val = tokens3[i + 1];

                SaveDataStatic.MissionList.Add(Key, Val);
            }

        }

        SaveDataStatic.ChoosenAvatar = PlayerPrefs.GetInt("ChoosenAvatar", 0);

        //This is taken to the Start of the Towncontroller. 
       // string[] TimeSplit = PlayerPrefs.GetString("GameTime", "0:0:0").Split(':');


        SaveDataStatic.Score = PlayerPrefs.GetInt("Score", 0);

        string m_CharPosString = PlayerPrefs.GetString ("Pos", "");

		if (!System.String.IsNullOrEmpty (m_CharPosString)) {

			string[] m_CharPosArray = m_CharPosString.Split ('|'); 

			for (int i = 0; i < m_CharPosArray.Length; i++) {

				string[] m_IndividualCharPosArray = m_CharPosArray [i].Split (',');

				switch (i) {
				case 0:
					SaveDataStatic.PandaPosition = new Vector3 (float.Parse(m_IndividualCharPosArray [0]), float.Parse(m_IndividualCharPosArray [1]), float.Parse(m_IndividualCharPosArray [2]));
					break;
				case 1:
					SaveDataStatic.RhinoPosition = new Vector3 (float.Parse(m_IndividualCharPosArray [0]), float.Parse(m_IndividualCharPosArray [1]), float.Parse(m_IndividualCharPosArray [2]));
					break;
				case 2:
					SaveDataStatic.HippoPosition = new Vector3 (float.Parse(m_IndividualCharPosArray [0]), float.Parse(m_IndividualCharPosArray [1]), float.Parse(m_IndividualCharPosArray [2]));
					break;
				case 3:
					SaveDataStatic.GorillaPosition = new Vector3 (float.Parse(m_IndividualCharPosArray [0]), float.Parse(m_IndividualCharPosArray [1]), float.Parse(m_IndividualCharPosArray [2]));
					break;
				case 4:
					SaveDataStatic.TigerPosition = new Vector3 (float.Parse(m_IndividualCharPosArray [0]), float.Parse(m_IndividualCharPosArray [1]), float.Parse(m_IndividualCharPosArray [2]));
					break;
				case 5:
					SaveDataStatic.SnakePosition = new Vector3 (float.Parse(m_IndividualCharPosArray [0]), float.Parse(m_IndividualCharPosArray [1]), float.Parse(m_IndividualCharPosArray [2]));
					break;
				case 6:
					SaveDataStatic.CaterpillarPosition = new Vector3 (float.Parse(m_IndividualCharPosArray [0]), float.Parse(m_IndividualCharPosArray [1]), float.Parse(m_IndividualCharPosArray [2]));
					break;
				}
			}
		} 

		string m_CharRotString = PlayerPrefs.GetString ("Rot", "");

		if (!System.String.IsNullOrEmpty (m_CharRotString)) {

			string[] m_CharRotArray = m_CharRotString.Split ('|'); 

			for (int i = 0; i < m_CharRotArray.Length; i++) {

				switch (i) {
				case 0:
					SaveDataStatic.PandaYRotation = float.Parse(m_CharRotArray[i]);
					break;
				case 1:
					SaveDataStatic.RhinoYRotation = float.Parse(m_CharRotArray[i]);
					break;
				case 2:
					SaveDataStatic.HippoYRotation = float.Parse(m_CharRotArray[i]);
					break;
				case 3:
					SaveDataStatic.GorillaYRotation = float.Parse(m_CharRotArray[i]);
					break;
				case 4:
					SaveDataStatic.TigerYRotation = float.Parse(m_CharRotArray[i]);
					break;
				case 5:
					SaveDataStatic.SnakeYRotation = float.Parse(m_CharRotArray[i]);
					break;
				case 6:
					SaveDataStatic.CaterpillarYRotation = float.Parse(m_CharRotArray[i]);
					break;
				}
			}
		} 

		string m_MedalString = PlayerPrefs.GetString ("Medal", "");

		SaveDataStatic.AwardedMedalList.Clear ();
		if (!System.String.IsNullOrEmpty (m_MedalString)) {

			string[] m_CharMedalArray = m_MedalString.Split ('|'); 

			for (int i = 0; i < m_CharMedalArray.Length; i++) {
				SaveDataStatic.AddToAwardedMedalList(m_CharMedalArray [i]);
			}
		}


        //
        //SaveDataStatic.MissionList.Clear();
        //SaveDataStatic.MissionList.Add("Collect Litter!", "");
        //SaveDataStatic.MissionList.Add("Repair pipe and clear that drain!", "");
        //SaveDataStatic.MissionList.Add("Help Tiny Tiger with Sapling Tree!", "");
        //SaveDataStatic.MissionList.Add("Stop the smoking car!", "");
        //SaveDataStatic.MissionList.Add("Help Postman to clear Graffiti!", "");
    }

	public void LoadSavedGame()
	{
        GameObject go = GameObject.Find("MusicLoop");
        if (go != null)
        {
            Destroy(go);
        }
        SceneManager.LoadSceneAsync (m_SceneToLoad);
	}

	public void StartANewGame()
	{
        SaveDataStatic.ResetCharacterPositionAndRotation();
        string m_NickName = PlayerPrefs.GetString("Prime8Player", "");
        int m_ScoreShare = PlayerPrefs.GetInt("IsFirstTimeSharing", 0);
        PlayerPrefs.DeleteAll ();
        Time.timeScale = 1;
        GetSavedData();
        //LoadNewGameOrStartNew.SetActive (false);
        //MusicLoop.SetActive (true);

        PlayerPrefs.SetString("Prime8Player", m_NickName.Trim());
        PlayerPrefs.SetInt("IsFirstTimeSharing", m_ScoreShare);
       

        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
        //DontDestroyOnLoad (gameObject);
        //soundManager = GameObject.Find ("SoundManager");
        //inventory = GameObject.Find ("Inventory");
        //if (soundManager)
        //	Destroy (soundManager);
        //if (inventory)
        //Destroy (inventory);
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadSceneAsync("Intro");
    }

    #region Leaderboard
    public GameObject LeaderBoardPanel;
    public Transform LBScrollContainer;
    public GameObject LeaderBoardContPrefab;
    public GameObject MessagePanel;
    public GameObject LbLoadingPanel;

    public void OnLeaderBoardBtnClick()
    {
//#if UNITY_EDITOR
//        LeaderBoardPanel.SetActive(true);
//        GameObject go;
//        for (int i = 0; i < 10; i++)
//        {
//            go = Instantiate(LeaderBoardContPrefab);
//            go.transform.Find("Image").Find("Text").gameObject.GetComponent<Text>().text = (i + 1) + "";
//            go.transform.Find("NickName").gameObject.GetComponent<Text>().text = "Aiyoo";
//            go.transform.Find("Score").gameObject.GetComponent<Text>().text = "slkdjf";
//            go.transform.Find("Time").gameObject.GetComponent<Text>().text = "slkjdf";
//            go.transform.SetParent(LBScrollContainer);
//            go.transform.localScale = Vector3.one;
//        }

//#else
        //StartCoroutine(GetLeaderboard());
        LbLoadingPanel.SetActive(true);
        App42Leaderboard.GetInstace().GetLeaderboard(this);
//#endif
    }

    public void OpenLB()
    {
        //if (SaveDataStatic.LeaderboardList.Count - 1 == 0)
        //{
        //    MessagePanel.SetActive(true);
        //    LbLoadingPanel.SetActive(false);
        //    return;
        //}
        //LeaderBoardPanel.SetActive(true);
        //List<GameObject> m_TempObj = new List<GameObject>();
        //for (int i = 0; i < LBScrollContainer.childCount; i++)
        //{
        //    m_TempObj.Add(LBScrollContainer.GetChild(i).gameObject);
        //}
        //foreach (GameObject g in m_TempObj)
        //{
        //    Destroy(g);
        //}

        //GameObject go;
        //for (int i = 0; i < SaveDataStatic.LeaderboardList.Count - 1; i++)
        //{
        //    go = Instantiate(LeaderBoardContPrefab);
        //    go.transform.Find("Image").Find("Text").gameObject.GetComponent<Text>().text = (i + 1) + "";
        //    go.transform.Find("NickName").gameObject.GetComponent<Text>().text = SaveDataStatic.LeaderboardList[i].Split('|')[1].Split('=')[1];
        //    go.transform.Find("Score").gameObject.GetComponent<Text>().text = SaveDataStatic.LeaderboardList[i].Split('|')[2].Split('=')[1];
        //    go.transform.Find("Time").gameObject.GetComponent<Text>().text = SaveDataStatic.LeaderboardList[i].Split('|')[3].Split('=')[1];
        //    go.transform.SetParent(LBScrollContainer);
        //    go.transform.localScale = Vector3.one;
        //}

        //LbLoadingPanel.SetActive(false);

        // Above commented section is for PHP backend. Below is for App42 cloud API

        if (SaveDataStatic.LeaderboardList.Count == 0)
        {
            //MessagePanel.SetActive(true);
            BringInMessagePanel();
            LbLoadingPanel.SetActive(false);
            return;
        }
        LeaderBoardPanel.SetActive(true);
        List<GameObject> m_TempObj = new List<GameObject>();
        for (int i = 0; i < LBScrollContainer.childCount; i++)
        {
            m_TempObj.Add(LBScrollContainer.GetChild(i).gameObject);
        }
        foreach (GameObject g in m_TempObj)
        {
            Destroy(g);
        }

        GameObject go;
        for (int i = 0; i < SaveDataStatic.LeaderboardList.Count; i++)
        {
            go = Instantiate(LeaderBoardContPrefab);
            go.transform.Find("Image").Find("Text").gameObject.GetComponent<Text>().text = (i + 1) + "";
            //go.transform.Find("NickName").gameObject.GetComponent<Text>().text = SaveDataStatic.LeaderboardList[i].Split('|')[1].Split('=')[1];
            //go.transform.Find("Score").gameObject.GetComponent<Text>().text = SaveDataStatic.LeaderboardList[i].Split('|')[2].Split('=')[1];
            //go.transform.Find("Time").gameObject.GetComponent<Text>().text = SaveDataStatic.LeaderboardList[i].Split('|')[3].Split('=')[1];
            //go.transform.Find("Avatar").gameObject.GetComponent<Image>().sprite = AvatarFaces[System.Convert.ToInt32(SaveDataStatic.LeaderboardList[i].Split('|')[4].Split('=')[1])];

            go.transform.Find("NickName").gameObject.GetComponent<Text>().text = SaveDataStatic.LeaderboardList[i].NickName;
            go.transform.Find("Score").gameObject.GetComponent<Text>().text = SaveDataStatic.LeaderboardList[i].Score+"";
            go.transform.Find("Time").gameObject.GetComponent<Text>().text = SaveDataStatic.LeaderboardList[i].TimeInString;
            go.transform.Find("Avatar").gameObject.GetComponent<Image>().sprite = AvatarFaces[System.Convert.ToInt32(SaveDataStatic.LeaderboardList[i].AvatarImageName)];

            go.transform.SetParent(LBScrollContainer);
            go.transform.localScale = Vector3.one;
        }

        LbLoadingPanel.SetActive(false);
    }

    public void OnLeaderBoardCloseClick()
    {
        LeaderBoardPanel.SetActive(false);
    }

    IEnumerator GetLeaderboard()
    {
        LbLoadingPanel.SetActive(true);
        UnityWebRequest m_WWWWebRequest = UnityWebRequest.Get(SaveDataStatic.URL + "GetLeaderboard.php");
        yield return m_WWWWebRequest.SendWebRequest();


        if (m_WWWWebRequest.isNetworkError || m_WWWWebRequest.isHttpError)
        {
            Debug.Log(m_WWWWebRequest.error);
            //MessagePanel.SetActive(true);
            BringInMessagePanel();
            LbLoadingPanel.SetActive(false);
        }
        else
        {
            Debug.Log(m_WWWWebRequest.downloadHandler.text);

            if (m_WWWWebRequest.downloadHandler.text == "Empty")
            {
                //MessagePanel.SetActive(true);
                BringInMessagePanel();
                LbLoadingPanel.SetActive(false);
            }
            else
            {
                SaveDataStatic.LeaderboardList.Clear();

                string[] split = m_WWWWebRequest.downloadHandler.text.Split(';');
                foreach (string s in split)
                {
//                    SaveDataStatic.LeaderboardList.Add(s);
                }

                OpenLB();
            }
        }
    }
    #endregion Leaderboard

    #region Message Panel
    public void BringInMessagePanel(string _msg = "Please check your internet connection.")
    {
        MessagePanel.SetActive(true);
        MessagePanel.transform.GetChild(0).Find("Text").gameObject.GetComponent<Text>().text = _msg;
    }
    #endregion Message Panel

    #region Hall of Fame
    public GameObject HallOfFamePanel;
    public Transform HallOfFameScrollContainer;

    public void OpenHallofFameButtonClick()
    {
        LbLoadingPanel.SetActive(true);
        App42Leaderboard.GetInstace().GetPrime8HallOfFame(this);
    }

    public void OpenHallOfFame()
    {
        if (SaveDataStatic.HallOfFameList.Count == 0)
        {
            // MessagePanel.SetActive(true);
            BringInMessagePanel();
            LbLoadingPanel.SetActive(false);
            return;
        }

        HallOfFamePanel.SetActive(true);
        List<GameObject> m_TempObj = new List<GameObject>();
        for (int i = 0; i < HallOfFameScrollContainer.childCount; i++)
        {
            m_TempObj.Add(HallOfFameScrollContainer.GetChild(i).gameObject);
        }
        foreach (GameObject g in m_TempObj)
        {
            Destroy(g);
        }

        GameObject go;
        for (int i = 0; i < SaveDataStatic.HallOfFameList.Count; i++)
        {
            go = Instantiate(LeaderBoardContPrefab);
            go.transform.Find("Image").Find("Text").gameObject.GetComponent<Text>().text = (i + 1) + "";
            go.transform.Find("NickName").gameObject.GetComponent<Text>().text = SaveDataStatic.HallOfFameList[i].NickName;
            go.transform.Find("Score").gameObject.GetComponent<Text>().text = SaveDataStatic.HallOfFameList[i].Score+"";
            go.transform.Find("Time").gameObject.GetComponent<Text>().text = SaveDataStatic.HallOfFameList[i].TimeInString;
            go.transform.Find("Avatar").gameObject.GetComponent<Image>().sprite = AvatarFaces[System.Convert.ToInt32(SaveDataStatic.HallOfFameList[i].AvatarImageName)];
            //go.transform.Find("NickName").gameObject.GetComponent<Text>().text = SaveDataStatic.HallOfFameList[i].Split('|')[1].Split('=')[1];
            //go.transform.Find("Score").gameObject.GetComponent<Text>().text = SaveDataStatic.HallOfFameList[i].Split('|')[2].Split('=')[1];
            //go.transform.Find("Time").gameObject.GetComponent<Text>().text = SaveDataStatic.HallOfFameList[i].Split('|')[3].Split('=')[1];
            //go.transform.Find("Avatar").gameObject.GetComponent<Image>().sprite = AvatarFaces[System.Convert.ToInt32(SaveDataStatic.HallOfFameList[i].Split('|')[4].Split('=')[1])];
            go.transform.SetParent(HallOfFameScrollContainer);
            go.transform.localScale = Vector3.one;
        }

        LbLoadingPanel.SetActive(false);
    }

    public void OnHallOfFameCloseClick()
    {
        HallOfFamePanel.SetActive(false);
    }

    public void OnTermsAndConditionClick()
    {
        TnCPanel.SetActive(true);
    }
    #endregion Hall of Fame

    #region Terms and Conditions
    public GameObject TnCPanel;
    public void CloseTnCPanel()
    {
        TnCPanel.SetActive(false);
    }
    #endregion Terms and Conditions

    #region Select Avatar
    public GameObject AvatarPanel;
    public Sprite[] AvatarFaces;
    public Image SelectAvatarFace;
    public void OnAvaterSelectButtonClick()
    {
        AvatarPanel.SetActive(true);
    }
    public void OnAvatarPanelCloseClick()
    {
        AvatarPanel.SetActive(false);
    }

    public void OnAvaterButtonClick(int _no)
    {
        SaveDataStatic.ChoosenAvatar = _no;
        SelectAvatarFace.sprite = AvatarFaces[SaveDataStatic.ChoosenAvatar];
        OnAvatarPanelCloseClick();
    }
    #endregion Select Avatar

    #region About Us
    public GameObject AboutUsPanel;
    public void AboutUsButtonClick()
    {
        if (AboutUsPanel.activeSelf == false)
        {
            AboutUsPanel.SetActive(true);
        }
        AboutUsPanel.GetComponent<Animator>().SetBool("BringIn", true);
        StartCoroutine(PlayCreditScroll());
    }

    IEnumerator PlayCreditScroll()
    {
        yield return new WaitForSeconds(1);

        AboutUsPanel.transform.GetChild(0).Find("CreditScroller").gameObject.GetComponent<CreditScroll>().StartScroll();
    }

    public void AboutUsBackBtn()
    {
        AboutUsPanel.GetComponent<Animator>().SetBool("BringIn", false);
        StartCoroutine(StopTheScroll());

    }
    IEnumerator StopTheScroll()
    {
        yield return new WaitForSeconds(1);

        AboutUsPanel.transform.GetChild(0).Find("CreditScroller").gameObject.GetComponent<CreditScroll>().StopScroll();
    }
#endregion About Us

    public void OnWebSiteBtnClick()
    {
        Application.OpenURL("https://www.primasia.org/");
    }

    public GameObject InstructionUI;
    public void InstructionBtnClick()
    {
        if (InstructionUI.activeSelf == false)
        {
            InstructionUI.SetActive(true);
        }
        InstructionUI.GetComponent<Animator>().SetBool("BringIn", true);
    }

    public void OnInstructionBackBtnClick()
    {
        InstructionUI.GetComponent<Animator>().SetBool("BringIn", false);
    }
}
