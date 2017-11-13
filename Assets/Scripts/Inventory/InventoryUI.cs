
using UnityEngine;

public class InventoryUI : MonoBehaviour {

	public Transform itemsParent;
	Inventory inventory;
	InventorySlot[] slots;
	public GameObject inventoryUI;

	// Called at the initialization
	void Start () {
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;
		slots = itemsParent.GetComponentsInChildren<InventorySlot> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Inventory")) // Edit/ProjectSettings/Input
			inventoryUI.SetActive (!inventoryUI.activeSelf);
	}

	// Update the UI
	void UpdateUI()
	{
		for (int i = 0; i < slots.Length; i++) {
		if (i < inventory.items.Count)
				slots[i].AddItem(inventory.items[i]);
		else
			slots[i].ClearSlot();
		}
	}
}
