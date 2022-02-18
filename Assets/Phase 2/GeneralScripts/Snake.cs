using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;

public class Snake : MonoBehaviour, ISelectObject {

	private FirstPersonController 	m_SnakeFPS;
	private GameObject				m_SnakeFPSCamera;
	private GameObject				m_Snake;

	public Sprite 					ClothSprite;

	private bool m_IsCharacter 		= true;
	public bool IsCharacter
	{
		get { return m_IsCharacter; }
		set { m_IsCharacter = value; }
	}

	private string m_Name 		= "Snake";
	public string Name
	{
		get { return m_Name; }
		set { m_Name = value; }
	}

	void Awake()
	{
		m_SnakeFPS 			= GetComponent<FirstPersonController>();
		m_SnakeFPSCamera 	= transform.Find("FirstPersonCharacter").gameObject;
		m_Snake		 		= transform.Find("Sam_Snake").gameObject;
	}

	public void SelectObject() 
	{
		m_SnakeFPS.enabled = true;

		m_SnakeFPSCamera.SetActive(true);
		m_Snake.SetActive(true);

		TownController.GetInstance().LockCursorForGame();
		TownController.GetInstance().ShowCursor(true);

		TownController.GetInstance().FromTransform.gameObject.GetComponent<CurrentLocation>().enabled = true;
		TownController.GetInstance().FromTransform.gameObject.GetComponent<CurrentLocation>().SetMyLocation(transform);
	}

	public void DeSelectObject()
	{
		m_SnakeFPS.enabled = false;

		m_SnakeFPSCamera.SetActive(false);

		m_Snake.SetActive(true);

	}

	public void DoLocalAnimation(int animNo)
	{

	}

	public void DeactivateFPS()
	{
		m_SnakeFPS.enabled = false;
	}

	public void AddInventoryItem(string _name)
	{
		if(m_InventoryNameList.Contains(_name)) return;

		if(_name == "Cloth")
		{
			m_InventoryItemList.Add(ClothSprite);
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
