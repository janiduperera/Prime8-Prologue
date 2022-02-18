using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PostBox : MonoBehaviour, ISelectObject {

	public Material PostBoxMat;
	public Texture2D GrafitiTexture;
	public Texture2D CleanTexture;

	void Start()
	{
		name = "PostBox";
		TownController.GetInstance ().PostBox = gameObject;

		if(SaveDataStatic.WasPostBoxCleaned)
		{
			PostBoxMat.SetTexture("_MainTex", CleanTexture);
		}
		else
		{
			PostBoxMat.SetTexture("_MainTex", GrafitiTexture);
		}
	}

	public void SetCleanPostBoxTexture()
	{
		PostBoxMat.SetTexture("_MainTex", CleanTexture);
	}
	#region ISelectObject Interface
	private bool m_IsCharacter 		= false;
	public bool IsCharacter
	{
		get { return m_IsCharacter; }
		set { m_IsCharacter = value; }
	}

	private string m_Name 		= "PostBox";
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
		TownController.GetInstance().OnPostBoxSelected();
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