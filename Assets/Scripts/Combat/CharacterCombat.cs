using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour {

	CharacterStats myStats;
	public float attackSpeed = 1f;
	private float attackCooldown = 0f;
	public float attackDelay = .6f;
	public event System.Action OnAttack;

	// Called at initialization
	void Start (){
		myStats = GetComponent<CharacterStats> ();
	}

	// Called each frame
	void Update (){
		attackCooldown -= Time.deltaTime;
	}

	// Update the amount of health when hit
	public void Attack (CharacterStats targetStats){
		if (attackCooldown <= 0f) {
			StartCoroutine(DoDamage(targetStats, attackDelay));
			if (OnAttack != null)
				OnAttack();
			attackCooldown = 1f / attackSpeed;
		}
	}

	// The coroutine with a delay to let the animation works properly
	IEnumerator DoDamage (CharacterStats stats, float delay)
	{
		yield return new WaitForSeconds (delay);
		stats.TakeDamage (myStats.damage.GetValue ());
	}
}
