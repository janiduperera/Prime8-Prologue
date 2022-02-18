using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;

public class CaterpillarCharacter : MonoBehaviour, ISelectObject {

	private FirstPersonController 	m_CaterpillarFPS;
	private GameObject				m_CaterpillarFPSCamera;
	private GameObject				m_Caterpillar;

	public Sprite					CarKeySprite;
	public Sprite					ExausterBlockClothSprite;
	public Sprite					Prime8SuperRemvoerSprite;

	private bool m_IsCharacter 		= true;
	public bool IsCharacter
	{
		get { return m_IsCharacter; }
		set { m_IsCharacter = value; }
	}

	private string m_Name 		= "Caterpillar";
	public string Name
	{
		get { return m_Name; }
		set { m_Name = value; }
	}

	void Awake()
	{
		m_CaterpillarFPS 			= GetComponent<FirstPersonController>();
		m_CaterpillarFPSCamera 		= transform.Find("FirstPersonCharacter").gameObject;
		m_Caterpillar		 		= transform.Find("caterpiller").gameObject;
	}

	public void SelectObject() 
	{
		m_CaterpillarFPS.enabled = true;

		m_CaterpillarFPSCamera.SetActive(true);
		m_Caterpillar.SetActive(true);

		TownController.GetInstance().LockCursorForGame();
		TownController.GetInstance().ShowCursor(true);

		TownController.GetInstance().FromTransform.gameObject.GetComponent<CurrentLocation>().enabled = true;
		TownController.GetInstance().FromTransform.gameObject.GetComponent<CurrentLocation>().SetMyLocation(transform);
	}

	public void DeSelectObject()
	{
		m_CaterpillarFPS.enabled = false;

		m_CaterpillarFPSCamera.SetActive(false);

		m_Caterpillar.SetActive(true);

	}

	public void DoLocalAnimation(int animNo)
	{
//		TownController.GetInstance().SetHasUIComeUP(true);
//
//		if(animNo == 0)
//		{
//			DeSelectObject();
//			m_AmandaPanda.SetActive(false);
//
//			m_PandaClap.SetActive(true);
//
//			m_PandaAudioSource.clip = PandaClapAudio;
//			m_PandaAudioSource.Play();
//		}
	}

	public void DeactivateFPS()
	{
		m_CaterpillarFPS.enabled = false;
	}

	public void AddInventoryItem(string _name)
	{
		if(m_InventoryNameList.Contains(_name)) return;

		if(_name == "ExausterBlocked")
		{
			m_InventoryItemList.Add(ExausterBlockClothSprite);
		}
		else if(_name == "CarKey")
		{
			m_InventoryItemList.Add(CarKeySprite);
		}
		else if(_name == "Prime8SuperRemover")
		{
			m_InventoryItemList.Add(Prime8SuperRemvoerSprite);
		}

		m_InventoryNameList.Add(_name);
	}
	public void RemoveFromInventory(string _name)
	{
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
