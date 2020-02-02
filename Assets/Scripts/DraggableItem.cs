using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Set up touch input propagation while using Instant Preview in the editor.
#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class DraggableItem : MonoBehaviour, IPointerClickHandler {

	[SerializeField]
	GameObject target;

	[SerializeField]
	UnityEvent moveEvent;

	[SerializeField]
	UnityEvent resultEvent;

	[SerializeField]
	float stationaryTimeLimit = 0.5f;	Vector3 objectPosition;
	Vector3 objectPositionVector;

	Vector3 upVector = Vector3.up;

	Camera mainCamera;

	bool dragged = false;

	float stationaryTime;

	void Start () {
		mainCamera = GameFlowManager.camera;
		upVector = GameFlowManager.upVector == null ? Vector3.up : GameFlowManager.upVector;
	}

	void Update () {
		RaycastHit hit;
		Touch touch;

		if (Input.touchCount < 1 ||
			EventSystem.current.IsPointerOverGameObject ((touch = Input.GetTouch (0)).fingerId)) {
			return;
		}

		if (touch.phase == TouchPhase.Stationary) {			stationaryTime += Time.deltaTime;		} else {			stationaryTime = 0;		}

		if (stationaryTime >= stationaryTimeLimit) {
			stationaryTime = 0;
			touch.phase = TouchPhase.Ended;
		}

		switch (touch.phase) {

		case TouchPhase.Began:

			Vector3 touchWorldPosition = mainCamera.ScreenToWorldPoint (
				new Vector3 (
					touch.position.x,
					touch.position.y,
					mainCamera.nearClipPlane
				)
			);
			Vector3 touchDirection = touchWorldPosition - mainCamera.transform.position;

			if (Physics.Raycast (touchWorldPosition, touchDirection, out hit)) {
				if (hit.transform.gameObject == gameObject) {
					dragged = true;
					objectPosition = transform.position;
					objectPositionVector = objectPosition - mainCamera.transform.position;
				}
			}

			break;
		case TouchPhase.Moved:
		case TouchPhase.Stationary:			if (dragged) {
				DragObject ();
				TriggerDragEvent ();

				// Make the object follow the touch raycasting line
				touchWorldPosition = mainCamera.ScreenToWorldPoint (
					new Vector3 (
						touch.position.x,
						touch.position.y,
						mainCamera.nearClipPlane
					)
				);
				touchDirection = touchWorldPosition - mainCamera.transform.position;
				float x = objectPositionVector.y / touchDirection.y;
				Vector3 movementVector = x * touchDirection - objectPositionVector;
				transform.position = objectPosition + movementVector;				// Check if the target object intersects with the ray
				if (Physics.Raycast (touchWorldPosition, touchDirection, out hit)) {					if (hit.transform.gameObject == target) {
						dragged = false;						DropObject ();
						TriggerDropEvent ();					}				}			}

			break;

		case TouchPhase.Ended:
			if (dragged) {				dragged = false;
				DropObject ();			}

			break;
		}


	}

	/// Some effect/feedback for dragging the object
	public void DragObject () {
		if (transform.localScale.x < .12f) {
			transform.localScale = new Vector3 (
				transform.localScale.x + .01f,
				transform.localScale.y + .01f,
				transform.localScale.z + .01f
			);
		} else {
			transform.localScale = new Vector3 (.12f, .12f, .12f);
		}
	}

	// Some effect/feedback for dropping the object
	public void DropObject () {
		if (transform.localScale.x > .1f) {
			transform.localScale = new Vector3 (
				transform.localScale.x - .01f,
				transform.localScale.y - .01f,
				transform.localScale.z - .01f
			);
		} else {
			transform.localScale = new Vector3 (.1f, .1f, .1f);
		}
	}

	public void TriggerDragEvent () {

	}

	public void TriggerDropEvent () {
		TriggerResultEvent ();
	}

	public void TriggerResultEvent () {
		resultEvent.Invoke ();
	}

	public void OnPointerClick (PointerEventData eventData) {
		print (eventData);
	}

}