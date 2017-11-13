using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour {

	Item item;
	public Image icon;
	public Button removeButton;

	// Add the item to the InventoryGUI
	public void AddItem (Item newItem){
		item = newItem;
		icon.sprite = item.icon;
		icon.enabled = true;
		removeButton.interactable = true;
	}

	// Remove the item from the InventoryGUI
	public void ClearSlot (){
		item = null;
		icon.sprite = null;
		icon.enabled = false;
		removeButton.interactable = false;
	}

	// Remove the item from inventory
	public void OnRemoveButton(){
		Inventory.instance.Remove (item);
		//To do
			// Drop the item in the floor
	}

	public void UseItem()
	{
		if (item != null)
			item.Use ();
	}
}
