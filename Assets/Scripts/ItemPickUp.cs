using UnityEngine;

public class ItemPickUp : Interactable {
	public Item item;

	public override void Interact()
	{
		base.Interact ();
		PickUp ();
	}

	void PickUp()
	{
		Debug.Log ("Try Picking up an " + item.name);
		if (Inventory.instance.Add (item))
			Destroy (gameObject);
	}
}
