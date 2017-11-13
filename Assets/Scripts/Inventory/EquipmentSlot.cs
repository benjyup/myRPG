using UnityEngine.UI;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour {
	Equipment item;
	public Image icon;
	public Button removeButton;
	public int slotIndex;

	public void AddItem (Equipment newItem)
	{
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
		slotIndex = (int)item.equipSlot;
	}
	public void ClearSlot ()
	{
		item = null;
		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
		slotIndex = -1;
	}

	public void OnRemoveButton()
	{
		Debug.Log ("Your equipment " + item.name + " has been placed in your inventory!");
		EquipmentManager.instance.Equip(EquipmentManager.instance.defaultItems[slotIndex]);
		ClearSlot ();
	}
	
	public void UseItem()
	{
		if (item != null)
			item.Use ();
	}
}
