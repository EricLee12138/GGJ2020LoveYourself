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
	[SerializeField]
	GameObject hair;

	public GameObject character;
	public GameObject legBrokenObject;
	public GameObject headBrokenObject;

	public GameObject heartBrokenObject;
	public GameObject hungryObject;
	

	CharacterState state;

	int baldness = 2;	bool end = false;	// Start is called before the first frame update	void Start () {		end = false;	}	// Update is called once per frame	void Update () {
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
			headBrokenObject.SetActive(false);
			heartBrokenObject.SetActive(true);
		}

		// TODO: Other feedback
	}

	public int FixBaldness () {
		baldness--;

		if (state == CharacterState.Bald) {
			if (baldness == 1) {
				// Mid-baldness feedback
				hair.GetComponent<Animator>().SetTrigger("Seed");			} else if (baldness == 0) {				hair.GetComponent<Animator>().SetTrigger("Growth");				state = CharacterState.Hungry;

				heartBrokenObject.SetActive(false);
				hungryObject.SetActive(true);
			}		}

		return baldness;
	}

	public void FixHunger () {
		if (state == CharacterState.Hungry)
			state = CharacterState.Lonely;		heartBrokenObject.SetActive(true);
		hungryObject.SetActive(false);
		// TODO: Other feedback
	}

	public void FixLoneliness () {
		if (state == CharacterState.Lonely)
			state = CharacterState.Healthy;		if (!end)
		{
			StartCoroutine(ShowTitle());			end = true;
		}		// TODO: Other feedback	}

	IEnumerator ShowTitle()
	{
		yield return new WaitForSeconds(7f);
		GameObject title = GameObject.Find("Title");
		title.transform.parent = Camera.main.transform;

		StartCoroutine(RepositionTitle(title));
		

	}

	IEnumerator RepositionTitle(GameObject title)
	{
		yield return new WaitForEndOfFrame();
		title.transform.localPosition = new Vector3(0f, 0f, 2f);
		title.transform.localScale = Vector3.one;
		title.transform.localRotation = Quaternion.identity;
		title.GetComponentInChildren<SpriteRenderer>(true).enabled = true;
	}

}