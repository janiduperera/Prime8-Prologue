using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Exauster : MonoBehaviour, ISelectObject {

	private bool m_IsCharacter 		= false;
	public bool IsCharacter
	{
		get { return m_IsCharacter; }
		set { m_IsCharacter = value; }
	}

	private string m_Name 		= "Exauster";
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
       // Debug.Log("InventoryPanel.GetInstance().GetInventoryItemSelected()  : " + InventoryPanel.GetInstance().GetInventoryItemSelected());
		if(InventoryPanel.GetInstance().GetInventoryItemSelected() == "Cloth")
		{
			//Start the Pipe Repair Mini Game
			TownController.GetInstance().GetActiveISelObj().DeactivateFPS();
			TownController.GetInstance().AfterExausterBlocked();
		}
		else if(String.IsNullOrEmpty(InventoryPanel.GetInstance().GetInventoryItemSelected()))
		{
			TownController.GetInstance().SetSubtitleText("Select the cloth from your rufflesack and place it in the exhaust to stop the smoke!", 3, TownController.GetInstance().StopTheSmokeWithClothAudio);
			TownController.GetInstance().SetTargets(transform);
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
}