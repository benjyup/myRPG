using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	public float lookRadius = 10f;
	Transform target;
	NavMeshAgent agent;
	CharacterCombat combat;

	// Use this for initialization
	void Start () {
		target = PlayerManager.instance.player.transform;
		agent = GetComponent<NavMeshAgent> ();
		combat = GetComponent<CharacterCombat> ();
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance (target.position, transform.position);

		//Playing the walkSong for the enemy
		if (agent.velocity.magnitude > 2 && FindObjectOfType<AudioManager> ().IsPlaying("EnemyStep") == false)
			FindObjectOfType<AudioManager> ().Play ("EnemyStep");
		else if (agent.velocity.magnitude < 2 && FindObjectOfType<AudioManager> ().IsPlaying("EnemyStep") == true)
			FindObjectOfType<AudioManager> ().Stop ("EnemyStep");
		// If the player is close to the enemy range view, the enemy will following him
		if (distance <= lookRadius) {
			agent.stoppingDistance = 2 * .8f;
			agent.SetDestination(target.position);
			// if the player is too close to the enemy, the enemy can attack the player
			if (distance <= agent.stoppingDistance){
				CharacterStats targetStats = target.GetComponent<CharacterStats>();
				if (targetStats != null)
					combat.Attack(targetStats);
				FaceTarget();
			}
		}
	}

	// The method permit to the enemy to properly and smoothly face his target
	void FaceTarget()
	{
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	// This method permit to see in the scene the radius of range view
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, lookRadius);
	}
}
