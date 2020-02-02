using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
// Set up touch input propagation while using Instant Preview in the editor.
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class PlaneGenerator : MonoBehaviour {
    [SerializeField]
    Camera firstPersonCamera;
    
    [SerializeField]
    GameObject planePrefab;

    [SerializeField]
    GameObject planeDetectedPrefab;

    [SerializeField]
    GameObject target;

    [SerializeField]
    UnityEvent resultEvent;

    GameObject plane;
    GameObject planeDetected;

    const float _PrefabRotation = 180.0f;
    bool _IsQuitting = false;

    public void Awake () {
        // Enable ARCore to target 60fps camera capture frame rate on supported devices.
        // Note, Application.targetFrameRate is ignored when QualitySettings.vSyncCount != 0.
        Application.targetFrameRate = 60;
    }

    public void Update () {
        _UpdateApplicationLifecycle ();

		if (GameFlowManager.gameStarted) return;

        // Raycast against the location the screen mid-point to search for planes.
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast (Screen.width / 2f, Screen.height / 2f, raycastFilter, out hit)) {
            if (hit.Trackable is DetectedPlane &&
                (hit.Trackable as DetectedPlane).PlaneType == DetectedPlaneType.HorizontalUpwardFacing) {
                if (planeDetected == null) {
                    planeDetected = Instantiate (planeDetectedPrefab);					planeDetected.transform.position = hit.Pose.position;					//planeDetected.transform.Rotate (0, _PrefabRotation, 0, Space.Self);					planeDetected.transform.parent = hit.Trackable.CreateAnchor (hit.Pose).transform;
                } else {
                    planeDetected.transform.position = hit.Pose.position;
                    //planeDetected.transform.rotation = hit.Pose.rotation;
                    //planeDetected.transform.Rotate (0, _PrefabRotation, 0, Space.Self);
                }
            }
        }

        Touch touch;
        // If the player has not touched the screen, we are done with this update.
        if (Input.touchCount < 1 || (touch = Input.GetTouch (0)).phase != TouchPhase.Began) {
            return;
        }

        // Should not handle input if the player is pointing on UI.
        if (EventSystem.current.IsPointerOverGameObject (touch.fingerId)) {
            return;
        }

        if (Frame.Raycast (touch.position.x, touch.position.y, raycastFilter, out hit)) {
            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot (firstPersonCamera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0) {
                Debug.Log ("Hit at back of the current DetectedPlane");
            } else {
                if (hit.Trackable is DetectedPlane &&
                    (hit.Trackable as DetectedPlane).PlaneType == DetectedPlaneType.HorizontalUpwardFacing) {
					planeDetected.SetActive (false);					plane = Instantiate (planePrefab);					plane.transform.position = hit.Pose.position;
                    //plane.transform.Rotate (0, _PrefabRotation, 0, Space.Self);
                    //gameObject.transform.parent = hit.Trackable.CreateAnchor (hit.Pose).transform;

                    GameFlowManager.tools.Water = plane;
                    GameFlowManager.plane.transform.parent = plane.transform;

                    plane.GetComponent<DraggableItem>().AddTarget(target);
                    plane.GetComponent<DraggableItem>().AddResultEvent(resultEvent);

                    StartCoroutine (TriggerPlaneGeneration ());

                    if (!GameFlowManager.gameStarted) {
                        GameFlowManager.StartGame();
                    }
                }
            }
        }
    }

    private void _UpdateApplicationLifecycle () {
        // Exit the app when the 'back' button is pressed.
        if (Input.GetKey (KeyCode.Escape)) {
            Application.Quit ();
        }

        // Only allow the screen to sleep when not tracking.
        if (Session.Status != SessionStatus.Tracking) {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        } else {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }

        if (_IsQuitting) {
            return;
        }

        // Quit if ARCore was unable to connect and give Unity some time for the toast to
        // appear.
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted) {
            _ShowAndroidToastMessage ("Camera permission is needed to run this application.");
            _IsQuitting = true;
            Invoke ("_DoQuit", 0.5f);
        } else if (Session.Status.IsError ()) {
            _ShowAndroidToastMessage (
                "ARCore encountered a problem connecting.  Please start the app again.");
            _IsQuitting = true;
            Invoke ("_DoQuit", 0.5f);
        }
    }

    private void _DoQuit () {
        Application.Quit ();
    }

    private void _ShowAndroidToastMessage (string message) {
        AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
        AndroidJavaObject unityActivity =
            unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");

        if (unityActivity != null) {
            AndroidJavaClass toastClass = new AndroidJavaClass ("android.widget.Toast");
            unityActivity.Call ("runOnUiThread", new AndroidJavaRunnable (() => {
                AndroidJavaObject toastObject =
                    toastClass.CallStatic<AndroidJavaObject> (
                        "makeText", unityActivity, message, 0);
                toastObject.Call ("show");
            }));
        }
    }

	IEnumerator TriggerPlaneGeneration() {		yield return new WaitForSeconds (1f);
		plane.GetComponent<Animator> ().SetTrigger ("TriggerGeneration");
	}
}