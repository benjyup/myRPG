using UnityEngine.UI;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour {
	Equipment item;
	public Image icon;
	public Button removeButton;
	public int slotIndex;

	// Add an item to the slot in the equipmentGUI
	public void AddItem (Equipment newItem){
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
		slotIndex = (int)item.equipSlot;
	}

	// Clear the slot in the equipmentGUI
	public void ClearSlot (){
		item = null;
		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
		slotIndex = -1;
	}

	// Remove and place the item in the inventory
	public void OnRemoveButton(){
		Debug.Log ("Your equipment " + item.name + " has been placed in your inventory!");
		// TO DO
			// Check if the inventory is full
		EquipmentManager.instance.Equip(EquipmentManager.instance.defaultItems[slotIndex]);
		ClearSlot ();
	}

	//Not used for now
	public void UseItem(){
		if (item != null)
			item.Use ();
	}
}
