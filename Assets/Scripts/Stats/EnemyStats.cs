using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats {

	public override void Die()
	{
		base.Die ();
		// death animation
		FindObjectOfType<AudioManager> ().Play ("EnemyDeath");
		Destroy (gameObject);
	}

	public override void TakeDamage(int damage)
	{
		base.TakeDamage (damage);
		FindObjectOfType<AudioManager> ().Play ("EnemyHit");
	}
}
