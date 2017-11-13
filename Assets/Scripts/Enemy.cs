using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {


	PlayerManager playerManager;
	CharacterStats myStats;

	// Called at start
	void Start ()
	{
		playerManager = PlayerManager.instance;
		myStats = GetComponent<CharacterStats> ();
	}

	// function from class Interactable
	public override void Interact()
	{
		base.Interact ();
		CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat> ();
		if (playerCombat != null) {
			playerCombat.Attack(myStats);
		}
	}
}
