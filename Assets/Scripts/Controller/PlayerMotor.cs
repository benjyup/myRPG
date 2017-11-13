using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {

	NavMeshAgent agent;
	Transform target;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}

	void Update () {
		//Playing the walk sound when the player is moving
		if (agent.velocity.magnitude > 2 && FindObjectOfType<AudioManager> ().IsPlaying("PlayerStep") == false)
			FindObjectOfType<AudioManager> ().Play ("PlayerStep");
		else if (agent.velocity.magnitude < 2 && FindObjectOfType<AudioManager> ().IsPlaying("PlayerStep") == true)
			FindObjectOfType<AudioManager> ().Stop ("PlayerStep");
		if (target != null) {
			agent.SetDestination (target.position);
			FaceTarget();
		}
	}

	public void MoveToPoint (Vector3 point){
		agent.SetDestination (point);
	}
	
	public void FollowTarget(Interactable newTarget){
		agent.stoppingDistance = newTarget.radius * .8f;
		agent.updateRotation = false;
		target = newTarget.interactionTransform;
	}

	public void StopFollowingTarget(){
		agent.stoppingDistance = 0f;
		agent.updateRotation = true;
		target = null;
	}

	// This method permit to properly and smoothly face the target
	void FaceTarget ()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		if (new Vector3 (direction.x, 0f, direction.z) != Vector3.zero) {
			Quaternion lookRotation = Quaternion.LookRotation (new Vector3 (direction.x, 0f, direction.z));
			transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, Time.deltaTime * 5f);
		}
	}
}
