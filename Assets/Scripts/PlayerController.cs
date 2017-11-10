using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	Camera cam;
	PlayerMotor motor;
	public LayerMask movementMask;
	public Interactable focus;

	void Start () {
		cam = Camera.main;
		motor = GetComponent<PlayerMotor> ();
	}

	void Update () {

		// Check if the use is not hovering the UI
		if (EventSystem.current.IsPointerOverGameObject ())
			return;

		// If the user press left button mouse
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, movementMask)){
				motor.MoveToPoint(hit.point);
				RemoveFocus();
			}
		}
		
		// If the user press right button mouse
		if (Input.GetMouseButtonDown (1)) {
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, movementMask)){
				Interactable interactable = hit.collider.GetComponent<Interactable>();

				if (interactable != null){
					SetFocus(interactable);
				}
			}
		}
	}

	void SetFocus(Interactable newFocus){
		if (newFocus != focus) {
			if (focus != null)
				focus.OnDefocused ();
			focus = newFocus;
			motor.FollowTarget (newFocus);
		}
		focus.OnFocused (transform);
	}

	void RemoveFocus(){
		if (focus != null)
			focus.OnDefocused ();
		focus = null;
		motor.StopFollowingTarget ();
	}
}
