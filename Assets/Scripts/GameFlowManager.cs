using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlowManager : MonoBehaviour {
	[SerializeField]
	Camera firstPersonCamera;
	[SerializeField]
	ToolsManager toolsManager;

	[SerializeField]
	CharacterManager characterManager;	[SerializeField]
	GameObject worldPlane;	public static bool gameStarted { get; private set; }
	
	public static Camera camera;
	public static ToolsManager tools { get; private set; }
	public static CharacterManager character { get; private set; }
	public static GameObject plane { get; private set; }
	public static Vector3 upVector { get; private set; }	bool litUp, watered;	// Start is called before the first frame update	void Start () {
		camera = firstPersonCamera;
		tools = toolsManager;
		character = characterManager;		plane = worldPlane;		litUp = watered = false;	}	// Update is called once per frame	void Update () {	}	public static void StartGame () {		GameFlowManager.gameStarted = true;
		character.character.SetActive(true);
		tools.hammer.SetActive (true);	}

	public void HitLeg () {
		print ("Hit leg");
		character.FixLeg ();
	}

	public void HitHead () {
		print("Hit Head");
		character.FixHead ();		tools.hammer.SetActive(false);		tools.bulb.SetActive(true);		plane.transform.Find("Tears").gameObject.GetComponentInChildren<TappableItem>(true).enabled = true;			}

	public void LightUp () {
		print("Tap light up");
		if (!litUp)
		{
			litUp = true;
			if (character.FixBaldness() == 0)
			{
				tools.Apple.SetActive(true);
				tools.bulb.SetActive(false);
			}
		}
	}

	public void Cup()
	{
		tools.cup.SetActive(true);
	}

	public void Water () {
		if (!watered)
		{
			print("jmfdngdijgdkmj");
			watered = true;
			if (character.FixBaldness() == 0)
			{
				tools.Apple.SetActive(true);
				tools.cup.SetActive(false);
			}
		}
	}

	public void FeedApple () {
		print ("Feed apple");
		character.FixHunger ();		tools.Tissue.SetActive(true);		tools.Apple.SetActive(false);	}

	public void GrabTissue () {
		character.FixLoneliness ();
	}
}