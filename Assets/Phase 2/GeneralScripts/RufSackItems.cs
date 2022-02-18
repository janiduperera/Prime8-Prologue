using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RufSackItems : MonoBehaviour, ISelectObject {

	private bool m_IsCharacter 		= false;
	public bool IsCharacter
	{
		get { return m_IsCharacter; }
		set { m_IsCharacter = value; }
	}

	private string m_Name 		= "RuffSackItem";
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
		if(name == "trash bin closed") return;

		TownController.GetInstance().GetActiveISelObj().AddInventoryItem(name);

		if(name == "Trash_Bin_Full_New")
		{
			TownController.GetInstance().AfterTrashBinFullSelected();
			return;
		}

		if (name == "Cloth") {
			TownController.GetInstance ().AfterClothSelected ();
		} else if (name == "CarKey") {
			if (TownController.GetInstance ().SubtitleTxt == "First you need to collect the car keys") {
				TownController.GetInstance ().StopSubtitleCoroutine ();
			}
		} else if (name == "ExausterBlocked") {
			if (TownController.GetInstance ().SubtitleTxt == "Grab cloth that Sam used") {
				TownController.GetInstance ().StopSubtitleCoroutine ();
			}
		} else if (name == "Prime8SuperRemover") {
			if (TownController.GetInstance ().SubtitleTxt == "Find Prime8 Super Remover") {
				TownController.GetInstance ().StopSubtitleCoroutine ();
			}
		} else if (name == "Plunger") {
			TownController.GetInstance().SetSubtitleText(TownController.GetInstance().NowClearTheDrainSubtitle, 4, TownController.GetInstance().NowClearTheDrainAudio);
		}

		ItemSelectAudio.GetInstance ().PlayAudio (true);

		Destroy(gameObject);
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
