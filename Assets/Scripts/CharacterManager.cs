using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState {	LegBroken = 0,
	HeadBroken,
	Bald,
	Hungry,
	Lonely,
	Healthy}

public class CharacterManager : MonoBehaviour {
	CharacterState state;	// Start is called before the first frame update	void Start () {	}	// Update is called once per frame	void Update () {
		switch (state) {
		case CharacterState.LegBroken:
			break;		case CharacterState.HeadBroken:
			break;		case CharacterState.Bald:
			break;		case CharacterState.Hungry:
			break;		case CharacterState.Lonely:
			break;		case CharacterState.Healthy:
			break;		}	}

	public void FixLeg () {
		print ("Leg Fixed");

		if (state == CharacterState.LegBroken)
			state = CharacterState.HeadBroken;

		// TODO: Other feedback	}	public void FixHead () {
		if (state == CharacterState.HeadBroken)			state = CharacterState.HeadBroken;		// TODO: Other feedback	}

	public void FixBaldness () {
		if (state == CharacterState.Bald)			state = CharacterState.HeadBroken;
		// TODO: Other feedback	}

	public void FixHunger () {
		if (state == CharacterState.Hungry)
			state = CharacterState.Lonely;		// TODO: Other feedback	}

	public void FixLoneliness () {
		if (state == CharacterState.Lonely)
			state = CharacterState.Healthy;		// TODO: Other feedback	}

}