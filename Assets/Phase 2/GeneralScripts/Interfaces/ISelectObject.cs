using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public interface ISelectObject 
{
	Sprite RuffSackSprite { get; }
	List<Sprite> InventoryItemList { get; }
	List<string> InventoryNameList { get; }
	List<Sprite> MedalSpriteList { get; }
	bool IsCharacter { get; set; }
	string Name { get; set; }

	void SelectObject();
	void DeSelectObject();
	void DoLocalAnimation(int animNo);
	void DeactivateFPS();
	void AddInventoryItem(string _name);
	void RemoveFromInventory(string _name);

	void SetInventoryItem(Sprite item);
	void SetInventoryItemName(string name);
	void SetMedalSprite(Sprite medal);
    void CharacterUpdate(bool clickedStatus);
}