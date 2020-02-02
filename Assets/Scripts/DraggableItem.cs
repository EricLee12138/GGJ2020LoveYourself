using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// Set up touch input propagation while using Instant Preview in the editor.#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;#endif
public class DraggableItem : MonoBehaviour, IPointerClickHandler {

	[SerializeField]
	GameObject dropTarget;

	[SerializeField]	private EventTrigger.Entry m_Delegates;

	Vector3 objectPosition;
	Vector3 objectPositionVector;

	Vector3 upVector = Vector3.up;

	Camera mainCamera;

	bool dragged = false;
	bool onTouchHold = false;

	int prevTouchCount;

	void Start () {
		mainCamera = GameFlowManager.camera;
		upVector = GameFlowManager.upVector == null ? Vector3.up : GameFlowManager.upVector;	}	void Update () {		RaycastHit hit;
		Touch touch;		if (Input.touchCount < 1 ||			EventSystem.current.IsPointerOverGameObject ((touch = Input.GetTouch (0)).fingerId)) {			return;		}		switch (touch.phase) {		case TouchPhase.Began:			Vector3 touchWorldPosition = mainCamera.ScreenToWorldPoint (				new Vector3 (					touch.position.x,					touch.position.y,					mainCamera.nearClipPlane				)			);			Vector3 touchDirection = touchWorldPosition - mainCamera.transform.position;			if (Physics.Raycast (touchWorldPosition, touchDirection, out hit)) {				if (hit.transform.gameObject == gameObject) {					dragged = true;					objectPosition = transform.position;					objectPositionVector = objectPosition - mainCamera.transform.position;				}			}			break;		case TouchPhase.Moved:		case TouchPhase.Stationary:			if (dragged) {				DragObject ();				TriggerDragEvent ();				// Make the object follow the touch raycasting line				touchWorldPosition = mainCamera.ScreenToWorldPoint (					new Vector3 (						touch.position.x,						touch.position.y,						mainCamera.nearClipPlane					)				);				touchDirection = touchWorldPosition - mainCamera.transform.position;				float x = objectPositionVector.y / touchDirection.y;				Vector3 movementVector = x * touchDirection - objectPositionVector;				transform.position = objectPosition + movementVector;			}			break;		default:			dragged = false;			DropObject ();			TriggerDropEvent ();			break;		}	}	/// Some effect/feedback for dragging the object
	public void DragObject () {
		if (transform.localScale.x < .5f) {
			transform.localScale = new Vector3 (
				transform.localScale.x + .01f,
				transform.localScale.y + .01f,
				transform.localScale.z + .01f
			);
		} else {
			transform.localScale = new Vector3 (.5f, .5f, .5f);
		}
	}	// Some effect/feedback for dropping the object	public void DropObject () {
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

	public void TriggerResultEvent () {	}

	public void OnPointerClick (PointerEventData eventData) {		print (eventData);	}
}