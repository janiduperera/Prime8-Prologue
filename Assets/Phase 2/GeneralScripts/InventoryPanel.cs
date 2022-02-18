using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class InventoryPanel : MonoBehaviour {

	public AudioClip ZipSound;
	public GameObject InventoryObj;

	public Image RuffSackImage;
	public Image[] InventoryButtons;
	public Sprite DefaultInvButtonHolder;
	private string[] m_InventoryItemNames = new string[9];
	public Image[] MedalImages;
	public Image InvSelectedImage;

	public Sprite[] MedalSprites;

	private string m_InventoryItemSelected = "";
	public Text InventoryItemNameTxt;

	#region Singleton
	private static InventoryPanel m_Instane;
	private InventoryPanel(){}
	public static InventoryPanel GetInstance()
	{
		return m_Instane;
	}

	void Awake()
	{
		m_Instane = this;
	}

	#endregion Singleton

	public bool IsInventoryActive()
	{
		return InventoryObj.activeSelf;
	}

	public void UnSetInventory()
	{
		TownController.GetInstance().SetHasUIComeUP(false);

		TownController.GetInstance().LockCursorForGame();
		TownController.GetInstance().ShowCursor(true);

		InventoryObj.SetActive(false);
	}


	public void SetInventory(Sprite[] _itemSpriteList, string[] _itemNameList, Sprite _rufSackSprite, Sprite[] _medalSprites)
	{
		if(InventoryObj.activeSelf) return;

		TownController.GetInstance().SetHasUIComeUP(true);
		TownController.GetInstance().SetQuizAnswerCursor();

		InventoryObj.SetActive(true);

		TownController.GetInstance ().TownControllerAudio.clip = ZipSound;
		TownController.GetInstance ().TownControllerAudio.Play ();

		for(int i = 0; i < m_InventoryItemNames.Length; i++)
		{
			m_InventoryItemNames[i] = "";
			InventoryButtons[i].sprite = DefaultInvButtonHolder;
		}

		for(int i = 0; i < MedalImages.Length; i++)
		{
			MedalImages[i].color = new Color(1f, 1f, 1f, 0);
		}

		RuffSackImage.sprite = _rufSackSprite;
		
		for(int i = 0; i < _itemSpriteList.Length; i++)
		{
			if(String.IsNullOrEmpty(m_InventoryItemNames[i]))
			{
				InventoryButtons[i].sprite = _itemSpriteList[i];
				m_InventoryItemNames[i] = _itemNameList[i];
			}
			else
			{
				continue;
			}
		}

		foreach(Image img in MedalImages)
		{
			//img.sprite = null;
			img.sprite = DefaultInvButtonHolder;
			img.color = new Color(1, 1, 1, 0);
		}

		switch(TownController.GetInstance().GetActiveISelObj().Name)
		{
		case "Panda":
			int r = 0;
			foreach(string str in SaveDataStatic.AwardedMedalList)
			{
				if(str == "LeaveItClean")
				{
					MedalImages[r].sprite = MedalSprites[0];
					MedalImages[r].color = new Color(1f, 1f, 1f, 1f);
				}
				else if(str == "WaterConservation")
				{
					MedalImages[r].sprite = MedalSprites[1];
					MedalImages[r].color = new Color(1f, 1f, 1f, 1f);
				}
				else if(str == "TreeProtector")
				{
					MedalImages[r].sprite = MedalSprites[2];
					MedalImages[r].color = new Color(1f, 1f, 1f, 1f);
				}
				else if(str == "AntiPolution")
				{
					MedalImages[r].sprite = MedalSprites[3];
					MedalImages[r].color = new Color(1f, 1f, 1f, 1f);
				}
				else if(str == "CleanEnvironment")
				{
					MedalImages[r].sprite = MedalSprites[4];
					MedalImages[r].color = new Color(1f, 1f, 1f, 1f);
				}
				else if(str == "SeaLifeAndOcean")
				{
					MedalImages[r].sprite = MedalSprites[5];
					MedalImages[r].color = new Color(1f, 1f, 1f, 1f);
				}
				else if(str == "Recycle")
				{
					MedalImages[r].sprite = MedalSprites[6];
					MedalImages[r].color = new Color(1f, 1f, 1f, 1f);
				}
				else if(str == "Conservation")
				{
					MedalImages[r].sprite = MedalSprites[7];
					MedalImages[r].color = new Color(1f, 1f, 1f, 1f);
				}

				r++;
			}

			break;
//		case "Hippo":
//			foreach(string str in SaveDataStatic.AwardedMedalList)
//			{
//				if(str == "WaterConservation")
//				{
//					MedalImages[0].sprite = MedalSprites[1];
//					MedalImages[0].color = new Color(1f, 1f, 1f, 1f);
//					break;
//				}
//			}
//			break;
//		case "Rhino":
//			foreach(string str in SaveDataStatic.AwardedMedalList)
//			{
//				if(str == "LeaveItClean")
//				{
//					MedalImages[0].sprite = MedalSprites[0];
//					MedalImages[0].color = new Color(1f, 1f, 1f, 1f);
//					break;
//				}
//			}
//			break;
//		case "Tiger":
//			foreach(string str in SaveDataStatic.AwardedMedalList)
//			{
//				if(str == "TreeProtector")
//				{
//					MedalImages[0].sprite = MedalSprites[2];
//					MedalImages[0].color = new Color(1f, 1f, 1f, 1f);
//					break;
//				}
//			}
//			break;
//		case "Gorilla":
//			foreach(string str in SaveDataStatic.AwardedMedalList)
//			{
//				if(str == "Conservation")
//				{
//					MedalImages[0].sprite = MedalSprites[7];
//					MedalImages[0].color = new Color(1f, 1f, 1f, 1f);
//					break;
//				}
//			}
//			break;
//		case "Caterpillar":
//			foreach(string str in SaveDataStatic.AwardedMedalList)
//			{
//				if(str == "CleanEnvironment")
//				{
//					MedalImages[0].sprite = MedalSprites[4];
//					MedalImages[0].color = new Color(1f, 1f, 1f, 1f);
//					break;
//				}
//			}
//			break;
//		case "Snake":
//			foreach(string str in SaveDataStatic.AwardedMedalList)
//			{
//				if(str == "AntiPolution")
//				{
//					MedalImages[0].sprite = MedalSprites[3];
//					MedalImages[0].color = new Color(1f, 1f, 1f, 1f);
//					break;
//				}
//			}
//			break;
		}
	}

	public string whatIsSelectedtoCombine = null;
	public Sprite ClothAfterSoackedWithSuperRemover;

	public void OnInventoryItemSelected(int itemNo)
	{
		m_InventoryItemSelected = m_InventoryItemNames[itemNo];

		if(String.IsNullOrEmpty(m_InventoryItemSelected)) return;

		if(SaveDataStatic.StorySequence == "PostBox")
		{
			if(m_InventoryItemSelected == "ExausterBlocked" && !Array.Exists(m_InventoryItemNames, element => element == "Prime8SuperRemover"))
			{
				m_InventoryItemSelected = "";
				TownController.GetInstance().SetSubtitleText("Need \"Prime 8 Super Remover\" to use with \"Cloth\" to clean Graffiti.", 4, null);
				UnSetInventory();
				TownController.GetInstance().SetHasUIComeUP(false);

				TownController.GetInstance().GetActiveISelObj().SelectObject();
				return;
			}
			else if(m_InventoryItemSelected == "Prime8SuperRemover" && !Array.Exists(m_InventoryItemNames, element => element == "ExausterBlocked"))
			{
				m_InventoryItemSelected = "";
				TownController.GetInstance().SetSubtitleText("Need to use with \"Cloth\" with \"Prime 8 Super Remover\" to clean Graffiti.", 4, null);
				UnSetInventory();
				TownController.GetInstance().SetHasUIComeUP(false);

				TownController.GetInstance().GetActiveISelObj().SelectObject();
				return;
			}
			else if(whatIsSelectedtoCombine == "ExausterBlocked" && m_InventoryItemSelected == "Prime8SuperRemover")
			{
				
				//SetInvSelectedImage(InventoryButtons[itemNo].sprite);
				SetInvSelectedImage(ClothAfterSoackedWithSuperRemover); // This is to get the "Cloth soaked in Prime8 Super Remover" to the cursor

				UnSetInventory();

				TownController.GetInstance().SetHasUIComeUP(false);

				TownController.GetInstance().GetActiveISelObj().SelectObject();

				TownController.GetInstance().HideCursor();

				whatIsSelectedtoCombine = "CombinedForGraffiti";
			}
			else if(whatIsSelectedtoCombine == "Prime8SuperRemover" && m_InventoryItemSelected == "ExausterBlocked")
			{
				SetInvSelectedImage(InventoryButtons[itemNo].sprite);

				UnSetInventory();

				TownController.GetInstance().SetHasUIComeUP(false);

				TownController.GetInstance().GetActiveISelObj().SelectObject();

				TownController.GetInstance().HideCursor();

				whatIsSelectedtoCombine = "CombinedForGraffiti";
			}
			else if(m_InventoryItemSelected == "ExausterBlocked" )
			{
				whatIsSelectedtoCombine = "ExausterBlocked";
				TownController.GetInstance().SetClothOrPrime8RemoverCursor(true);
				InventoryButtons[itemNo].sprite = DefaultInvButtonHolder;
			}
			else if(m_InventoryItemSelected == "Prime8SuperRemover")
			{
				whatIsSelectedtoCombine = "Prime8SuperRemover";
				TownController.GetInstance().SetClothOrPrime8RemoverCursor(false);
				InventoryButtons[itemNo].sprite = DefaultInvButtonHolder;
			}
			else if(String.IsNullOrEmpty(whatIsSelectedtoCombine))
			{
				SetInvSelectedImage(InventoryButtons[itemNo].sprite);

				UnSetInventory();

				TownController.GetInstance().SetHasUIComeUP(false);

				TownController.GetInstance().GetActiveISelObj().SelectObject();

				TownController.GetInstance().HideCursor();
			}
			else
			{
				whatIsSelectedtoCombine = null;
				m_InventoryItemSelected = "";
				TownController.GetInstance().SetSubtitleText("Need to combine \"Prime8 super remover\" with the \"Cloth\" to remove Graffiti.", 4, null);
			}
		}
		else
		{
			SetInvSelectedImage(InventoryButtons[itemNo].sprite);

			UnSetInventory();

			TownController.GetInstance().SetHasUIComeUP(false);

			TownController.GetInstance().GetActiveISelObj().SelectObject();

			TownController.GetInstance().HideCursor();
		}
	}

	public int GetInventoryItemNo(string _itemName)
	{
		for(int i = 0; i < m_InventoryItemNames.Length ; i++)
		{
			if(m_InventoryItemNames[i] == _itemName)
			{
				return i;
			}
		}

		return 0;
	}

	public void RemoveInventorySelectedImage()
	{
        if (InvSelectedImage.sprite != null)
        {
            TownController.GetInstance().SetHasUIComeUP(false);
            UnsetInvSelectedImage();
            //		TownController.GetInstance().GetActiveISelObj().AddInventoryItem(name);
            ClearInventoryItemSelected();
        }
	}

	public string GetInventoryItemSelected()
	{
		return m_InventoryItemSelected;
	}

	public void ClearInventoryItemSelected()
	{
		m_InventoryItemSelected = "";
	}

	private void SetInvSelectedImage(Sprite _selImage)
	{
		InvSelectedImage.sprite = _selImage;
		InvSelectedImage.color = new Color(1, 1, 1, 1);
	}

	public void ChangeColorOfInventorySelectedImage(bool? _isCorrect)
	{
		if(InvSelectedImage.sprite == null) return;

		if(_isCorrect == true)
		{
			InvSelectedImage.color = new Color(221f/255f, 15f/225f, 62f/255f, 1);
		}
		else if(_isCorrect == false)
		{
			InvSelectedImage.color = new Color(22f/255f, 221f/225f, 15f/255f, 1);
		}
		else
		{
			InvSelectedImage.color = new Color(1, 1, 1, 1);
		}
	}

	public void UnsetInvSelectedImage(bool _lockCursorForGame = true)
	{
		InvSelectedImage.sprite = null;
		InvSelectedImage.color = new Color(1, 1, 1, 0);

        if(_lockCursorForGame)
		    TownController.GetInstance().LockCursorForGame();
	}


    string m_ParentSpriteName;
	public void OnPointerEnter(Transform _parentBtn)
	{
		if (_parentBtn.gameObject.GetComponent<Image> ().sprite == DefaultInvButtonHolder)
			return;

		m_ParentSpriteName = _parentBtn.gameObject.GetComponent<Image> ().sprite.name;

		if (_parentBtn.gameObject.name.Contains ("Medal")) {

			if (m_ParentSpriteName == "Recycle") {
				InventoryItemNameTxt.text = "Amanda : Recycling Medal";
			}
			else if (m_ParentSpriteName == "AntiPolu") {
				InventoryItemNameTxt.text = "Caterpillar : Exercise & Diet";
			}
			else if (m_ParentSpriteName == "CleanEnv") {
				InventoryItemNameTxt.text = "Sam Snake : Air Pollution & Energy Use";
			}
			else if (m_ParentSpriteName == "Conservation") {
				InventoryItemNameTxt.text = "Little Gorilla : Wildlife & Rainforest";
			}
			else if (m_ParentSpriteName == "LeaveItClean") {
				InventoryItemNameTxt.text = "Rhino : Litter & Packaging";
			}
			else if (m_ParentSpriteName == "SeaOcean") {
				InventoryItemNameTxt.text = "Wanda Whale : Healthy Seas & Oceans";
			}
			else if (m_ParentSpriteName == "Tree") {
				InventoryItemNameTxt.text = "Tiny Tiger : Conversation & Biodiversity";
			}
			else if (m_ParentSpriteName == "WaterCons") {
				InventoryItemNameTxt.text = "Hugi Hippo : Water Conservation";
			}
			
		} else {
			InventoryItemNameTxt.text = m_ParentSpriteName;
		}


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
        for(int i = 0; i < ScrollContainer.childCount; i++)
        {
            m_TempObj.Add(ScrollContainer.GetChild(i).gameObject);
        }
        foreach(GameObject g in m_TempObj)
        {
            Destroy(g);
        }

        int no = 1;
        GameObject go;
        foreach(KeyValuePair<string, string> pair in SaveDataStatic.MissionList)
        {
            go = Instantiate(MissionItemPrefab);
            if(pair.Value == "null")
            {
                go.GetComponent<Image>().sprite = NormalBackSprite;
                go.transform.Find("Image").gameObject.GetComponent<Image>().sprite = NormalSprite;
                go.transform.Find("Image").Find("Text").gameObject.GetComponent<Text>().text = no + "";
                go.transform.Find("PercentComplete").gameObject.GetComponent<Text>().text = "";
                go.transform.Find("CheckMark").gameObject.GetComponent<Image>().enabled = false;
            }
            else
            {
                go.GetComponent<Image>().sprite = CompleteBackSprite;
                go.transform.Find("Image").gameObject.GetComponent<Image>().sprite = CompleteSprite;
                go.transform.Find("Image").Find("Text").gameObject.GetComponent<Text>().text = "";
                go.transform.Find("CheckMark").gameObject.GetComponent<Image>().enabled = true;
                if (pair.Value.Contains("|"))
                {
                    if (pair.Key == "Complete recycling competition")
                    {
                        go.transform.Find("PercentComplete").gameObject.GetComponent<Text>().text = "Score awarded: " + pair.Value.Split('|')[0] + "" + pair.Value.Split('|')[1];
                       
                    }
                    else
                    {
                        go.transform.Find("PercentComplete").gameObject.GetComponent<Text>().text = "Score awarded: " + pair.Value.Split('|')[0] + "                                       Completed in: " + pair.Value.Split('|')[1];

                    }
                }
                else // Skipped
                {
                    go.transform.Find("PercentComplete").gameObject.GetComponent<Text>().text = "Score awarded: " + pair.Value + "     \"Skipped\"";
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
