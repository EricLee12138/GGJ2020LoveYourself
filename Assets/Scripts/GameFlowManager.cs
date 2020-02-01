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
		character = characterManager;	}	// Update is called once per frame	void Update () {	}	public static void StartGame () {		GameFlowManager.gameStarted = true;	}

	public static void HitLeg () {
		character.FixLeg ();
	}
}