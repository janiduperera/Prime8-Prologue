using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	//public static Inventory instance = null;
	public  bool setStairs = false;
	public GameObject inventoryPanel;
	public Material stairMat;
	private  GameObject panda;
	public GameObject stairsHid;
	public Texture2D cursorHandTexture;
	private  Button[] buttons = new Button[9];
	private  GameObject buttonList;
	public  int index;
	private bool panelHideFlag = true;
	public AudioClip zipSound;

	public  Dictionary<int ,string> buttonMap;

    private int tapCount = 0;

	public GameController	GameCont;

	void Start(){

		panda = GameObject.Find ("Panda");
		inventoryPanel.SetActive (false);
		buttonList = inventoryPanel.transform.GetChild(0).Find("RuffSackSection").Find("Items").gameObject;

        if (buttonList == null) {
			//Debug.Log ("null");
			return;
		}
		for (int i = 0; i < 9; i++) {
			buttons[i] = buttonList.transform.GetChild(i).GetComponent<Button>();
		}

		// Lazy Singleton
	
		index = 0;
		buttonMap = new Dictionary<int, string> ();
	}

	public  void AddItem(Sprite image, string tag){
		buttons [index].GetComponent<Image>().sprite = image;
		buttons [index].tag = tag;
		buttonMap.Add (index, tag);
		if (index > 9)
			index = 0;
		index++;
	}

	bool m_ExecuteTheCode = false;
	void Update(){


		m_ExecuteTheCode = false;
		if (GameCont.HasInitialInstructionCompleted) {
			m_ExecuteTheCode = true;
		} else if (RoomInstructions.RoomInstructionNo == 44) { 
			m_ExecuteTheCode = true;
		} else {
			m_ExecuteTheCode = false;
		}

#if !(UNITY_IOS || UNITY_ANDROID)
		if (m_ExecuteTheCode && Input.GetKeyDown(KeyCode.R))
        {
			CloseOrOpenInventory ();
        }
#endif

		// Janidu Commented
      /*  if (tapCount == 1)
        {
            inventoryPanel.SetActive(panelHideFlag);
            Cursor.visible = panelHideFlag;
            Cursor.lockState = panelHideFlag ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.SetCursor(cursorHandTexture, Vector2.zero, CursorMode.Auto);
            panda.GetComponent<MonoBehaviour>().enabled = !panelHideFlag;
            panelHideFlag = !panelHideFlag;
            tapCount = 0;
        }
		*/

	}



	public void CloseOrOpenInventory()
	{
		if(RoomInstructions.RoomInstructionNo == 44 && !panelHideFlag)
		{
			RoomInstructions.RoomInstructionNo++;
		}

		OnPointerExit ();

		inventoryPanel.SetActive(panelHideFlag);

		if (inventoryPanel.activeSelf == true)
		{
			SoundManager.instance.playSingle(zipSound);
            RuffSackSection.SetActive(true);
            MissionSection.SetActive(false);
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

        if(tapCount > 0)
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


	//Janidu Added
	public Sprite DefaultInvButtonHolder;
	public Text InventoryItemNameTxt;

	public void OnPointerEnter(Transform _parentBtn)
	{
		if (_parentBtn.gameObject.GetComponent<Image> ().sprite == DefaultInvButtonHolder)
			return;

		InventoryItemNameTxt.text = _parentBtn.gameObject.GetComponent<Image> ().sprite.name;

		InventoryItemNameTxt.gameObject.transform.SetParent (_parentBtn);
		InventoryItemNameTxt.gameObject.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (98, 33, 0);
	}

	public void OnPointerExit()
	{
		InventoryItemNameTxt.text = "";
		InventoryItemNameTxt.gameObject.transform.SetParent (null);
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

                if (no == 1)
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
                        go.transform.Find("CheckMark").gameObject.GetComponent<Image>().enabled = true;
                        go.transform.Find("CheckBox").gameObject.GetComponent<Image>().enabled = false;
                        go.transform.Find("CheckPointReached").gameObject.GetComponent<Text>().text = "";
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
