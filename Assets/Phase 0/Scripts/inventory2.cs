using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class inventory2 : MonoBehaviour {

        public AudioClip zipSound;
		public  inventory2 instance = null;
		public  bool setStairs = false;
		public GameObject inventoryPanel;
		//public Material stairMat;
		private  GameObject panda;
		//public GameObject stairsHid;
		public Texture2D cursorHandTexture;
		private  Image[] buttons = new Image[9];
		private  GameObject buttonList;
		public  int index;
		public  bool panelHideFlag = true;
       // public GameObject cursor4;
		public  Dictionary<int ,string> buttonMap;

        private int tapCount = 0;
		
	public KitchenGamePlay	KitchGamePlayCont;

		void Start(){

			panda = GameObject.Find ("Panda");
			inventoryPanel.SetActive (false);
			buttonList = inventoryPanel.transform.GetChild(0).Find("RuffSackSection").Find("Items").gameObject;
			if (buttonList == null) {
				//Debug.Log ("null");
				return;
			}
			for (int i = 0; i < 9; i++) {
				buttons[i] = buttonList.transform.GetChild(i).GetComponent<Image>();
			}

			// Lazy Singleton

			index = 0;
			buttonMap = new Dictionary<int, string> ();
		}
				

		public  void AddItem(Sprite image, string tag){
			buttons [index].sprite = image;
			buttons [index].tag = tag;
			buttonMap.Add (index, tag);
			if (index > 9)
				index = 0;
			index++;
		}



#if !(UNITY_IOS || UNITY_ANDROID)
		void Update(){


		if (Input.GetKeyDown(KeyCode.R) && !KitchGamePlayCont.IsSubtitleStillDisplaying)
            {
			CloseOrOpenInventory ();
            }


}
#endif

    public InventryOnclick m_InvOnClickScript;

	public void CloseOrOpenInventory()
	{
        if(KitchGamePlayCont.cursor4.activeSelf)
        {
            return;
        }

        if(KitchGamePlayCont.FormCamFocus)
        {
            return;
        }

        if (inventoryPanel.activeSelf == true)
			panelHideFlag = false;
		else
			panelHideFlag = true;

		m_InvOnClickScript.OnPointerExit ();

		inventoryPanel.SetActive(panelHideFlag);
		if (inventoryPanel.activeSelf == true)
		{
			Soundmanager2.instance.playSingle(zipSound);
            RuffSackSection.SetActive(true);
            MissionSection.SetActive(false);
            KitchGamePlayCont.JoyStickSettings(false);
        }
        else
        {
            KitchGamePlayCont.JoyStickSettings(true);
        }
#if !(UNITY_IOS || UNITY_ANDROID)
		Cursor.visible = panelHideFlag;
		Cursor.lockState = panelHideFlag ? CursorLockMode.None : CursorLockMode.Locked;

		Cursor.SetCursor(cursorHandTexture, Vector2.zero, CursorMode.Auto);
#endif
        panda.GetComponent<MonoBehaviour>().enabled = !panelHideFlag;
		panelHideFlag = !panelHideFlag;
		tapCount = 0;
	}


        IEnumerator ResetDoubleTap()
        {
            yield return new WaitForSeconds(0.2f);

            if (tapCount > 0)
                tapCount = 0;
        }

		public void ButtonAction(int id){
			for (int i = 0; i < buttons.Length; i++) {
				if(i == id){
					if(buttonMap.ContainsKey(id)){
						string tag = buttonMap [id];
						switch(tag){
						case "ipod":break;
						case "stairs":break;
						default:break;
						}
					}
				}
			}
		}


    #region Mission
    private List<string> m_MissionList = new List<string>();
    public GameObject RuffSackSection, MissionSection;
    public Transform ScrollContainer;
    public GameObject MissionItemPrefab;
    public Sprite CompleteSprite, NormalSprite;
    public Sprite CompleteBackSprite, NormalBackSprite;
    public void OnMissionButtonClick()
    {
        RuffSackSection.SetActive(false);
        MissionSection.SetActive(true);

        List<GameObject> m_TempObj = new List<GameObject>();
        for (int i = 0; i < ScrollContainer.childCount; i++)
        {
            m_TempObj.Add(ScrollContainer.GetChild(i).gameObject);
        }
        foreach (GameObject g in m_TempObj)
        {
            Destroy(g);
        }

        int no = 1;
        GameObject go;
        foreach (KeyValuePair<string, string> pair in SaveDataStatic.MissionList)
        {
            go = Instantiate(MissionItemPrefab);
            if (pair.Value == "null")
            {
                go.GetComponent<Image>().sprite = NormalBackSprite;
                go.transform.Find("Image").gameObject.GetComponent<Image>().sprite = NormalSprite;
                go.transform.Find("Image").Find("Text").gameObject.GetComponent<Text>().text = no + "";
                go.transform.Find("PercentComplete").gameObject.GetComponent<Text>().text = "";
                go.transform.Find("CheckMark").gameObject.GetComponent<Image>().enabled = false;

                if(no == 1)
                {
                    go.transform.Find("CheckBox").gameObject.GetComponent<Image>().enabled = false;
                    go.transform.Find("CheckPointReached").gameObject.GetComponent<Text>().text = "";
                }
            }
            else
            {
                go.GetComponent<Image>().sprite = CompleteBackSprite;
                go.transform.Find("Image").gameObject.GetComponent<Image>().sprite = CompleteSprite;
                go.transform.Find("Image").Find("Text").gameObject.GetComponent<Text>().text = "";
                if (pair.Value.Contains("|"))
                {
                    if (pair.Key == "Complete recycling competition")
                    {
                        go.transform.Find("PercentComplete").gameObject.GetComponent<Text>().text = "Score awarded: " + pair.Value.Split('|')[0] + "" + pair.Value.Split('|')[1];
                        go.transform.Find("CheckMark").gameObject.GetComponent<Image>().enabled = false;
                        go.transform.Find("CheckBox").gameObject.GetComponent<Image>().enabled = false;
                        go.transform.Find("CheckPointReached").gameObject.GetComponent<Text>().text = "";
                    }
                    else
                    {
                        go.transform.Find("PercentComplete").gameObject.GetComponent<Text>().text = "Score awarded: " + pair.Value.Split('|')[0] + "                                       Completed in: " + pair.Value.Split('|')[1];
                        go.transform.Find("CheckMark").gameObject.GetComponent<Image>().enabled = true;
                    }
                }
                else // Skipped
                {
                    go.transform.Find("PercentComplete").gameObject.GetComponent<Text>().text = "Score awarded: " + pair.Value + "     \"Skipped\"";

                    go.transform.Find("CheckMark").gameObject.GetComponent<Image>().enabled = false;
                    if (no == 1)
                    {
                        go.transform.Find("CheckBox").gameObject.GetComponent<Image>().enabled = false;
                        go.transform.Find("CheckPointReached").gameObject.GetComponent<Text>().text = "";
                    }
                }

            }

            go.transform.Find("WhatToDo").gameObject.GetComponent<Text>().text = pair.Key;
            no++;

            go.transform.SetParent(ScrollContainer);
            go.transform.localScale = Vector3.one;
        }
    }

    public void OnInventoryButtonClick()
    {
        RuffSackSection.SetActive(true);
        MissionSection.SetActive(false);
    }
    #endregion Mission
}



