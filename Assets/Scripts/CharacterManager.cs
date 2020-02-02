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
	CharacterState state;
	public GameObject character;
	public GameObject legBrokenObject;
	public GameObject headBrokenObject;
	public GameObject heartBrokenObject;
	int baldness = 2;	// Start is called before the first frame update	void Start () {	}	// Update is called once per frame	void Update () {
		switch (state) {
		case CharacterState.LegBroken:
			break;		case CharacterState.HeadBroken:
			break;		case CharacterState.Bald:
			switch (baldness) {			case 2:
				break;			case 1:
				break;			case 0:
				break;			}
			break;		case CharacterState.Hungry:

			break;		case CharacterState.Lonely:
			break;		case CharacterState.Healthy:
			break;		}	}

	public void FixLeg () {
		print ("Leg Fixed");

		if (state == CharacterState.LegBroken)
		{
			state = CharacterState.HeadBroken;
			legBrokenObject.SetActive(false);
			headBrokenObject.SetActive(true);
		}
	}	public void FixHead () {
		print("Head Fixed");
		if (state == CharacterState.HeadBroken)
		{
			state = CharacterState.Bald;

		}		// TODO: Other feedback	}

	public int FixBaldness () {
		baldness--;

		if (state == CharacterState.Bald) {
			if (baldness == 1) {
				// Mid-baldness feedback			} else if (baldness == 0) {				state = CharacterState.Hungry;
			}		}

		return baldness;
	}

	public void FixHunger () {
		if (state == CharacterState.Hungry)
			state = CharacterState.Lonely;		// TODO: Other feedback	}

	public void FixLoneliness () {
		if (state == CharacterState.Lonely)
			state = CharacterState.Healthy;		// TODO: Other feedback	}

}