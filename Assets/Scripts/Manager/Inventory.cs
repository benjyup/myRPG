using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public static Inventory instance;
	public List<Item> items = new List<Item>();
	public int maxSpace = 15;
	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	#region Singleton
	void Awake()
	{
		if (instance != null) {
			Debug.LogWarning("More than one instance of Inventory found!");
			return;
		}
		instance = this;
	}
	#endregion

	public bool Add (Item item)
	{
		if (!item.isDefaultItem) {
			if (items.Count >= maxSpace){
				Debug.Log ("Not enough space");
				return false;
				}
			Debug.Log("You have picked up a " + item.name);
			items.Add (item);
			if (onItemChangedCallback != null)
				onItemChangedCallback.Invoke();
		}
		return true;
	}
	public void Remove (Item item)
	{
		items.Remove (item);
		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
	}
}
