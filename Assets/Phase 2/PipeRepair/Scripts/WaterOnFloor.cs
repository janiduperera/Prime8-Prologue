using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WaterOnFloor : MonoBehaviour, ISelectObject {

	private bool	m_IsPlungerSelected = false;
	private bool	m_IsBroomSelected = false;

	#region ISelectObject Interface
	private bool m_IsCharacter 		= false;
	public bool IsCharacter
	{
		get { return m_IsCharacter; }
		set { m_IsCharacter = value; }
	}

	private string m_Name 		= "WaterOnFloor";
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

	void Start()
	{
		TownController.GetInstance ().WaterOnFloor = gameObject;
	}

	public void SelectObject() 
	{
		if(TownController.GetInstance().SubtitleTxt == "Now clear the drain!")
		{
			TownController.GetInstance().StopSubtitleCoroutine();
		}

		if(String.IsNullOrEmpty(InventoryPanel.GetInstance().GetInventoryItemSelected()))
		{
			TownController.GetInstance().SetSubtitleText("Select the \"Broom\" AND \"Plunger\" from rufflesack and click on to the drain to clear it!", 2, TownController.GetInstance().SelectBroomAndPlungerAudio);
			TownController.GetInstance().SetTargets(transform);
		}
		else if(InventoryPanel.GetInstance().GetInventoryItemSelected() == "Broom" && !m_IsPlungerSelected)
		{
			m_IsBroomSelected = true;
			TownController.GetInstance().SetSubtitleText("We also need the \"Plunger\". Select it from Rufflesack and click on to the blocked drain to clear it!", 2, null);
			TownController.GetInstance().SetTargets(transform);
			InventoryPanel.GetInstance().UnsetInvSelectedImage();

			TownController.GetInstance().GetActiveISelObj().RemoveFromInventory(InventoryPanel.GetInstance().GetInventoryItemSelected());
		}
		else if(InventoryPanel.GetInstance().GetInventoryItemSelected() == "Plunger" && !m_IsBroomSelected)
		{
			m_IsPlungerSelected = true;
			TownController.GetInstance().SetSubtitleText("We also need the \"Broom\". Select it from Rufflesack and click on to the blocked drain to clear it!", 3, null);
			TownController.GetInstance().SetTargets(transform);
			InventoryPanel.GetInstance().UnsetInvSelectedImage();

			TownController.GetInstance().GetActiveISelObj().RemoveFromInventory(InventoryPanel.GetInstance().GetInventoryItemSelected());
		}
		else
		{
			InventoryPanel.GetInstance().UnsetInvSelectedImage();
			TownController.GetInstance().GetActiveISelObj().RemoveFromInventory(InventoryPanel.GetInstance().GetInventoryItemSelected());
			TownController.GetInstance().GetActiveISelObj().DeSelectObject();
			TownController.GetInstance().PlayMovie();

			gameObject.SetActive(false);
		}
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
