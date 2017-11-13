using UnityEngine;

public class Interactable : MonoBehaviour {
	public float radius = 3f;
	bool isFocus = false;
	Transform player;
	bool hasInteracted = false;
	public Transform interactionTransform;

	public virtual void Interact (){
	}

	// Called each frame
	void Update (){
		// We are checking if the player is focusing an object
		if (isFocus && !hasInteracted) {
			float distance = Vector3.Distance(player.position, interactionTransform.position);
			// We make the player interact with the target if he's close to it
			if (distance <= radius){
				Interact();
				hasInteracted = true;
			}
		}
	}

	// When the player focus the object
	public void OnFocused (Transform playerTransform){
		isFocus = true;
		player = playerTransform;
		hasInteracted = false;
	}

	// The reverse
	public void OnDefocused(){
		isFocus = false;
		player = null;
		hasInteracted = false;
	}


	// This method permit to see the radius of interaction in the scene
	void OnDrawGizmosSelected(){
		if (interactionTransform == null)
			interactionTransform = transform;
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (interactionTransform.position, radius);
	}
}
