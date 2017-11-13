using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentUI : MonoBehaviour {

	public Transform itemsParent;
	Inventory inventory;
	EquipmentSlot[] slots;
	public GameObject equipmentUI;

	// Permit to access this class instance in other classes
	#region Singleton
	public static EquipmentUI instance;
	
	void Awake(){
		instance = this;
	}
	#endregion

	// Called at initialization
	void Start () {
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;
		slots = itemsParent.GetComponentsInChildren<EquipmentSlot> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Equipment")) // Edit/ProjectSettings/Input
			equipmentUI.SetActive (!equipmentUI.activeSelf);
	}

	public void AddItem(Equipment item)
	{
		// We check the type of the item to send it to the correct slot int the EquipmentGUI
		switch ((int)item.equipSlot)
		{
		case 0:
			slots [0].AddItem (item);
			break;
		case 1:
			slots [1].AddItem (item);
			break;
		case 2:
			slots [2].AddItem (item);
			break;
		case 3:
			slots [4].AddItem (item);
			break;
		case 4:
			slots [5].AddItem (item);
			break;
		case 5:
			slots [3].AddItem (item);
			break;
		default:
			break;
		}
	}

	// Clear the slot when the player hit the 'U' button
	public void RemoveAllItems()
	{
		for (int i = 0; i < slots.Length; i++) {
				slots[i].ClearSlot();
		}
	}

	// Do not touch!
	void UpdateUI()
	{
		//for (int i = 0; i < slots.Length; i++) {
		//	if (i < inventory.items.Count)
		//		slots[i].AddItem(inventory.items[i]);
		//	else
		//		slots[i].ClearSlot();
		//}
	}
}
