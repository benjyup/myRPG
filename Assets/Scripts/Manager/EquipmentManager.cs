using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

	#region Singleton
	public static EquipmentManager instance;

	void Awake()
	{
		instance = this;
	}
	#endregion

	public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
	public OnEquipmentChanged onEquipmentChanged;
	public Equipment[] currentEquipment;
	SkinnedMeshRenderer[] currentMeshes;
	public SkinnedMeshRenderer targetMesh;
	public Equipment[] defaultItems;

	Inventory inventory;
	EquipmentUI equipmentUI;
	void Start ()
	{
		equipmentUI = EquipmentUI.instance;
		inventory = Inventory.instance;
		int numSlots = System.Enum.GetNames(typeof(EquipmentSlots)).Length;
		currentEquipment = new Equipment[numSlots];
		currentMeshes = new SkinnedMeshRenderer[numSlots];
		EquipDefaultItems ();
	}


	void Update()
	{
		// Check if the user click u button
		if (Input.GetKeyDown (KeyCode.U))
			UnequipAll ();
	}

	public void Equip(Equipment newItem)
	{
		int slotIndex = (int)newItem.equipSlot;
		Equipment oldItem = Unequip (slotIndex);

		// Prevent that something changed
		if (onEquipmentChanged != null) {
			onEquipmentChanged.Invoke(newItem, oldItem);
		}
		SetEquipmentBlendShapes (newItem, 100);
		currentEquipment [slotIndex] = newItem;
		SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer> (newItem.mesh);
		newMesh.transform.parent = targetMesh.transform;
		newMesh.bones = targetMesh.bones;
		newMesh.rootBone = targetMesh.rootBone;
		currentMeshes [slotIndex] = newMesh;
	}

	public Equipment Unequip(int slotIndex)
	{
		if (currentEquipment[slotIndex] != null){

			if (currentMeshes[slotIndex] != null){
				Destroy(currentMeshes[slotIndex].gameObject);
			}

			Equipment oldItem = currentEquipment[slotIndex];
			SetEquipmentBlendShapes(oldItem, 0);
			inventory.Add(oldItem);
			currentEquipment[slotIndex] = null;
			
			// Prevent that something changed
			if (onEquipmentChanged != null) {
				onEquipmentChanged.Invoke(null, oldItem);
			}
			return oldItem;
		}
		return null;
	}

	public void UnequipAll()
	{
		equipmentUI.RemoveAllItems ();
		for (int i = 0; i < currentEquipment.Length; i++)
			Unequip (i);
		EquipDefaultItems ();
	}

	void SetEquipmentBlendShapes(Equipment item, int weight)
	{
		foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegion) {
			targetMesh.SetBlendShapeWeight((int)blendShape, weight);
		}
	}

	void EquipDefaultItems()
	{
		foreach (Equipment item in defaultItems) {
			SetEquipmentBlendShapes (item, 100);
			Equip (item);
		}
	}
}
