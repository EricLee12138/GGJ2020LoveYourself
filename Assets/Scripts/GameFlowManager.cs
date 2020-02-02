using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour {
	[SerializeField]
	Camera firstPersonCamera;
	[SerializeField]
	ToolsManager toolsManager;

	[SerializeField]
	CharacterManager characterManager;	public static bool gameStarted { get; private set; }
	public static Camera camera;
	public static ToolsManager tools { get; private set; }
	public static CharacterManager character { get; private set; }
	public static Vector3 upVector { get; private set; }	// Start is called before the first frame update	void Start () {
		camera = firstPersonCamera;
		tools = toolsManager;
		character = characterManager;	}	// Update is called once per frame	void Update () {	}	public static void StartGame () {		GameFlowManager.gameStarted = true;
		tools.hammer.SetActive (true);	}

	public void HitLeg () {
		print ("Hit leg");
		character.FixLeg ();
	}

	public void HitHead () {
		print("Hit Head");
		character.FixHead ();		tools.bulb.SetActive(true);	}

	public void LightUp () {
		print("Tap light up");
		character.FixBaldness ();
	}

	public void Water () {
		character.FixBaldness ();
	}

	public void FeedApple () {
		print ("Feed apple");
		character.FixHunger ();	}

	public void GrabTissue () {
		character.FixLoneliness ();
	}
}