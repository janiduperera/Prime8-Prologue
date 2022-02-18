using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections.Generic;

public class Panda : MonoBehaviour, ISelectObject {

	private FirstPersonController 	m_PandaFPS;
	private GameObject				m_PandaFPSCamera;
	private GameObject				m_AmandaPanda;
	private GameObject				m_PandaClap;

	public AudioClip 				PandaClapAudio;
	private AudioSource				m_PandaAudioSource;

	public Sprite 					LetterCaseSprite;
	public Sprite 					IPodSprite;

	private bool m_IsCharacter 		= true;
	public bool IsCharacter
	{
		get { return m_IsCharacter; }
		set { m_IsCharacter = value; }
	}

	private string m_Name 		= "Panda";
	public string Name
	{
		get { return m_Name; }
		set { m_Name = value; }
	}

	void Awake()
	{
		m_PandaFPS 			= GetComponent<FirstPersonController>();
		m_PandaFPSCamera 	= transform.Find("FirstPersonCharacter").gameObject;
		m_AmandaPanda 		= transform.Find("AmandaPanda").gameObject;
		m_PandaClap 		= transform.Find("PandaClap").gameObject;

		m_PandaAudioSource 	= GetComponent<AudioSource>();
	}

	public void SelectObject() 
	{
		m_PandaFPS.enabled = true;

		m_PandaFPSCamera.SetActive(true);
		m_AmandaPanda.SetActive(true);

		m_PandaClap.SetActive(false);

		TownController.GetInstance().LockCursorForGame();
		TownController.GetInstance().ShowCursor(true);

		TownController.GetInstance().FromTransform.gameObject.GetComponent<CurrentLocation>().enabled = true;
		TownController.GetInstance().FromTransform.gameObject.GetComponent<CurrentLocation>().SetMyLocation(transform);
	}

	public void DeSelectObject()
	{
		m_PandaFPS.enabled = false;

		m_PandaFPSCamera.SetActive(false);

		m_AmandaPanda.SetActive(true);

		m_PandaClap.SetActive(false);

	}

	public void DoLocalAnimation(int animNo)
	{
		TownController.GetInstance().SetHasUIComeUP(true);

		if(animNo == 0)
		{
			DeSelectObject();
			m_AmandaPanda.SetActive(false);

			StartCoroutine(AfterClapAudioEnd());
		}
	}

	IEnumerator AfterClapAudioEnd()
	{
		m_PandaClap.SetActive(true);

		m_PandaAudioSource.clip = PandaClapAudio;
		m_PandaAudioSource.loop = true;
		m_PandaAudioSource.Play();

		yield return new WaitForSeconds(1.5f);

		m_PandaClap.GetComponent<Animator>().Play("Clapping", -1, 0.0f);
	}

	public void DeactivateFPS()
	{
		m_PandaFPS.enabled = false;
	}

	public void AddInventoryItem(string _name)
	{
		if(m_InventoryNameList.Contains(_name)) return;

		if(_name == "LetterCase")
		{
			m_InventoryItemList.Add(LetterCaseSprite);
		}
		else if(_name == "ipod")
		{
			m_InventoryItemList.Add(IPodSprite);
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
