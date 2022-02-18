using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;

public class Tiger : MonoBehaviour, ISelectObject {

	private FirstPersonController 	m_TigerFPS;
	private GameObject				m_TigerFPSCamera;
	private GameObject				m_Tiger;

	private bool m_IsCharacter 		= true;
	public bool IsCharacter
	{
		get { return m_IsCharacter; }
		set { m_IsCharacter = value; }
	}

	private string m_Name 		= "Tiger";
	public string Name
	{
		get { return m_Name; }
		set { m_Name = value; }
	}

	void Awake()
	{
		m_TigerFPS 			= GetComponent<FirstPersonController>();
		m_TigerFPSCamera 	= transform.Find("FirstPersonCharacter").gameObject;
		m_Tiger		 		= transform.Find("Tiger").gameObject;
	}

	public void SelectObject() 
	{
		m_TigerFPS.enabled = true;

		m_TigerFPSCamera.SetActive(true);
		m_Tiger.SetActive(true);

		TownController.GetInstance().LockCursorForGame();
		TownController.GetInstance().ShowCursor(true);

		TownController.GetInstance().FromTransform.gameObject.GetComponent<CurrentLocation>().enabled = true;
		TownController.GetInstance().FromTransform.gameObject.GetComponent<CurrentLocation>().SetMyLocation(transform);
	}

	public void DeSelectObject()
	{
		m_TigerFPS.enabled = false;

		m_TigerFPSCamera.SetActive(false);

		m_Tiger.SetActive(true);

	}

	public void DoLocalAnimation(int animNo)
	{

	}

	public void DeactivateFPS()
	{
		m_TigerFPS.enabled = false;
	}

	public void AddInventoryItem(string _name)
	{
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
