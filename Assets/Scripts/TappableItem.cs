using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using GoogleARCore;#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.using Input = GoogleARCore.InstantPreviewInput;#endif
public class TappableItem : MonoBehaviour {

	[SerializeField]
	UnityEvent resultEvent;

	// Start is called before the first frame update	void Start () {	}	// Update is called once per frame	void Update () {		RaycastHit hit;
		Touch touch;		// If the player has not touched the screen, we are done with this update.		if (Input.touchCount < 1 || (touch = Input.GetTouch (0)).phase != TouchPhase.Began) {
			return;
		}		// Should not handle input if the player is pointing on UI.		if (EventSystem.current.IsPointerOverGameObject (touch.fingerId)) {
			return;
		}


		Vector3 touchWorldPosition = GameFlowManager.camera.ScreenToWorldPoint (
			new Vector3 (
				touch.position.x,
				touch.position.y,
				GameFlowManager.camera.nearClipPlane
			)
		);
		Vector3 touchDirection = touchWorldPosition - GameFlowManager.camera.transform.position;		//Debug.DrawRay (touchWorldPosition, touchDirection * 100f, Color.red, 10f);		if (Physics.Raycast (touchWorldPosition, touchDirection, out hit)) {
			if (hit.transform.gameObject == gameObject) {
				TriggerResultEvent ();
			}
		}
	}

	void TriggerResultEvent () {		resultEvent.Invoke ();	}
}