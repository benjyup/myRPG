using UnityEngine;

public class ItemPickUp : Interactable {
	public Item item;

	public override void Interact(){
		base.Interact ();
		PickUp ();
	}

	//Pick the object from ground and destroy it from the scene
	void PickUp(){
		if (Inventory.instance.Add (item))
			Destroy (gameObject);
	}
}
