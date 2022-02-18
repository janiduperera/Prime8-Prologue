using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaplingTree : MonoBehaviour, ISelectObject {

	void Start () {

		name = "SaplingTree";
		TownController.GetInstance ().SaplingTree = gameObject;
	}

	#region ISelectObject Interface
	private bool m_IsCharacter 		= false;
	public bool IsCharacter
	{
		get { return m_IsCharacter; }
		set { m_IsCharacter = value; }
	}

	private string m_Name 		= "SaplingTree";
	public string Name
	{
		get { return m_Name; }
		set { m_Name = value; }
	}

	public Sprite m_RuffSackSprite;
	public Sprite RuffSackSprite
	{
		get { return m_RuffSackSprite; }
	}

	public void SelectObject() 
	{
//		TownController.GetInstance().GetActiveISelObj().DeSelectObject();
//		BobBird.SetActive(true);
//		TownController.GetInstance().SetQuizAnswerCursor();

		if(TownController.GetInstance().GetActiveISelObj().Name == "Panda")
		{
			if (TownController.GetInstance ().NeedGorrilaAudio) {
//				TownController.GetInstance().TownControllerAudio.clip = TownController.GetInstance().NeedGorrilaAudio;
//				TownController.GetInstance().TownControllerAudio.Play();
				TownController.GetInstance ().SetSubtitleText (TownController.GetInstance ().NeedGorillaSubtitle, 10, TownController.GetInstance ().NeedGorrilaAudio);
			} else {
				TownController.GetInstance ().SetSubtitleText (TownController.GetInstance ().NeedGorillaSubtitle, 10, null);
			}


			return;
		}

		TownController.GetInstance().GetActiveISelObj().DeactivateFPS();
		TownController.GetInstance().SetGameStartPanel(true);
	}

	public void DeSelectObject() 
	{
	}

	public void DoLocalAnimation(int animNo)
	{
	}

	public void DeactivateFPS()
	{
	}

	public void AddInventoryItem(string _name)
	{
	}

	public void RemoveFromInventory(string _name)
	{
	}

	private List<Sprite> m_InventoryItemList = new List<Sprite>();
	public List<Sprite> InventoryItemList
	{
		get { return m_InventoryItemList; }
	}
	public void SetInventoryItem(Sprite item)
	{
	}

	private List<string> m_InventoryNameList = new List<string>();
	public List<string> InventoryNameList
	{
		get { return m_InventoryNameList; }
	}

	public void SetInventoryItemName(string name)
	{
	}

	private List<Sprite> m_MedalSpriteList = new List<Sprite>();
	public List<Sprite> MedalSpriteList
	{
		get { return m_MedalSpriteList; }
	}
	public void SetMedalSprite(Sprite medal)
	{
	}

    public void CharacterUpdate(bool clickedStatus)
    {
    }
    #endregion ISelectObject Interface
}
