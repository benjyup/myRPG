using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

	public EquipmentSlots equipSlot;
	public SkinnedMeshRenderer mesh;
	public int armorModifier;
	public int damageModifier;
	public EquipmentMeshRegion[] coveredMeshRegion;
	EquipmentUI equipmentUI;

	public override void Use()
	{
		base.Use ();
		Debug.Log ("You have equiped a " + this.name);
		equipmentUI = EquipmentUI.instance;
		equipmentUI.AddItem (this);
		EquipmentManager.instance.Equip (this);
		RemoveFromInventory ();
	}
}

public enum EquipmentSlots { Head, Chest, Legs, Weapon, Shield, Feet }
public enum EquipmentMeshRegion { Legs, Arms, Torso}; // Boody blendshapes