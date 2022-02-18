using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

public class Hippo : MonoBehaviour, ISelectObject {

	public Sprite MonkeyWrenchSprite;
	public Sprite DrainPlungerSprite;
	public Sprite YardBroomSprite;

	private FirstPersonController 	m_HippoFPS;
	private GameObject				m_HippoFPSCamera;
	private GameObject				m_Hippo;

	private AudioSource				m_HippoAudioSource;

	private bool					OhIMustFixThis = false;


	private bool m_IsCharacter 		= true;
	public bool IsCharacter
	{
		get { return m_IsCharacter; }
		set { m_IsCharacter = value; }
	}

	private string m_Name 		= "Hippo";
	public string Name
	{
		get { return m_Name; }
		set { m_Name = value; }
	}

	// Use this for initialization
	void Awake () {

		m_HippoFPS 			= GetComponent<FirstPersonController>();
		m_HippoFPSCamera 	= transform.Find("FirstPersonCharacter").gameObject;
		m_Hippo		 		= transform.Find("Hippo").gameObject;

		m_HippoAudioSource 	= GetComponent<AudioSource>();
	
	}

	public void SelectObject() 
	{
		m_HippoFPS.enabled = true;

		m_HippoFPSCamera.SetActive(true);
		m_Hippo.SetActive(true);

		TownController.GetInstance().LockCursorForGame();
		TownController.GetInstance().ShowCursor(true);

		//if(SaveDataStatic.StorySequence == "PRepStart" && !OhIMustFixThis)
        if(SaveDataStatic.StorySequence == "LitterEnd" && !OhIMustFixThis)
		{
			OhIMustFixThis = true;

//			TownController.GetInstance().TownControllerAudio.clip = TownController.GetInstance().OhLeakIMustFixThis;
//			TownController.GetInstance().TownControllerAudio.Play();
            TownController.GetInstance().SetSubtitleText(TownController.GetInstance().OhLeakIMustFixThisSubtitle, 10, TownController.GetInstance().OhLeakIMustFixThis);
		}

		TownController.GetInstance().FromTransform.gameObject.GetComponent<CurrentLocation>().enabled = true;
		TownController.GetInstance().FromTransform.gameObject.GetComponent<CurrentLocation>().SetMyLocation(transform);
	}

	public void DeSelectObject()
	{
		m_HippoFPS.enabled = false;

		m_HippoFPSCamera.SetActive(false);

		m_Hippo.SetActive(true);
	}

	public void DoLocalAnimation(int animNo)
	{
		TownController.GetInstance().SetHasUIComeUP(true);
	}

	public void DeactivateFPS()
	{
		m_HippoFPS.enabled = false;
	}

	public void AddInventoryItem(string _name)
	{
		if(m_InventoryNameList.Contains(_name)) return;

		if(_name == "MonkeyWrench")
		{
			m_InventoryItemList.Add(MonkeyWrenchSprite);
		}
		else if(_name == "Broom")
		{
			m_InventoryItemList.Add(YardBroomSprite);
		}
		else if(_name == "Plunger")
		{
			m_InventoryItemList.Add(DrainPlungerSprite);
		}
		m_InventoryNameList.Add(_name);
	}

	public void RemoveFromInventory(string _name)
	{
		for (int i = 0; i < m_InventoryItemList.Count; i++) {

			if (m_InventoryNameList [i] == _name) {
				m_InventoryItemList.RemoveAt(i);
				m_InventoryNameList.RemoveAt(i);
				break;
			}
		}
	}

	public Sprite m_RuffSackSprite;
	public Sprite RuffSackSprite
	{
		get { return m_RuffSackSprite; }
	}

	private List<Sprite> m_InventoryItemList = new List<Sprite>();
	public List<Sprite> InventoryItemList
	{
		get { return m_InventoryItemList; }
	}

	public void SetInventoryItem(Sprite item)
	{
		m_InventoryItemList.Add(item);
	}

	private List<string> m_InventoryNameList = new List<string>();
	public List<string> InventoryNameList
	{
		get { return m_InventoryNameList; }
	}

	public void SetInventoryItemName(string name)
	{
		m_InventoryNameList.Add(name);
	}

	private List<Sprite> m_MedalSpriteList = new List<Sprite>();
	public List<Sprite> MedalSpriteList
	{
		get { return m_MedalSpriteList; }
	}

	public void SetMedalSprite(Sprite medal)
	{
		m_MedalSpriteList.Add(medal);
	}

    public void CharacterUpdate(bool clickedStatus)
    {
    }
}
