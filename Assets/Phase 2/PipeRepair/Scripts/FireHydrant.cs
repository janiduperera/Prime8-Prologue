using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FireHydrant : MonoBehaviour, ISelectObject {

	public GameObject WaterFont;
	public GameObject SurfaceSplash;
	public GameObject WaterOnFloor;

	void Start () {

		name = "Hydrant";
		TownController.GetInstance ().FireHydrant = gameObject;
	}

	public void StartWaterLeak()
	{
		WaterFont.SetActive(true);
		SurfaceSplash.SetActive(true);
		WaterOnFloor.SetActive(true);
	}

	public void DeactivateWaterLeak()
	{
		WaterFont.gameObject.SetActive(false);
		SurfaceSplash.gameObject.SetActive(false);
	}

	public void DeactivateOrActivateWaterOnFloor(bool _doActivate)
	{
		WaterOnFloor.SetActive(_doActivate);
	}

	#region ISelectObject Interface
	private bool m_IsCharacter 		= false;
	public bool IsCharacter
	{
		get { return m_IsCharacter; }
		set { m_IsCharacter = value; }
	}

	private string m_Name 		= "FireHydrant";
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
		if(TownController.GetInstance().GetActiveISelObj().Name == "Panda")
		{
			TownController.GetInstance().SetSubtitleText(TownController.GetInstance().NeedHugiSubtitle ,10, TownController.GetInstance().NeedHugiAudio);

			return;
		}
		
		if(InventoryPanel.GetInstance().GetInventoryItemSelected() == "MonkeyWrench")
		{
			//Start the Pipe Repair Mini Game
			TownController.GetInstance().GetActiveISelObj().DeactivateFPS();
			TownController.GetInstance().SetGameStartPanel(true);

			TownController.GetInstance().GetActiveISelObj().RemoveFromInventory(InventoryPanel.GetInstance().GetInventoryItemSelected());
		}
		else if(String.IsNullOrEmpty(InventoryPanel.GetInstance().GetInventoryItemSelected()))
		{
			TownController.GetInstance().SetSubtitleText("Select the \"Monkey Wrench\" from the rufflesack and click on the hydrant to fix the leak.", 7, TownController.GetInstance().SelectMonkeyWrenchAudio);
			TownController.GetInstance().SetTargets(transform);
		}
		else
		{
			TownController.GetInstance().SetSubtitleText("Select the \"Monkey Wrench\" from the rufflesack and click on the hydrant to fix the leak.", 7, TownController.GetInstance().SelectMonkeyWrenchAudio);
			TownController.GetInstance().SetTargets(transform);
			InventoryPanel.GetInstance().RemoveInventorySelectedImage();
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
