namespace GoogleARCore.Examples.HelloAR
{
    using System.Collections.Generic;
    using GoogleARCore;
    using GoogleARCore.Examples.Common;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class Interactions : MonoBehaviour
    {
        public Camera FirstPersonCamera;
        public GameObject DetectedPlanePrefab;
        public PlaneController planeController;

        private const float k_ModelRotation = 180.0f;
        private bool m_IsQuitting = false;
        private bool gamePlaced = false;

        public void Update()
        {
            _UpdateApplicationLifecycle();

            Touch touch;
            if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began || gamePlaced)
            {
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }

            TrackableHit hit;
            TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
                TrackableHitFlags.FeaturePointWithSurfaceNormal;

            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                if ((hit.Trackable is DetectedPlane) &&
                    Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                        hit.Pose.rotation * Vector3.up) < 0)
                {
                    Debug.Log("Hit at back of the current DetectedPlane");
                }
                else
                {

                    planeController.transform.SetPositionAndRotation(hit.Pose.position, hit.Pose.rotation);
                    planeController.SetActive(true);
                    var planeAnchor = hit.Trackable.CreateAnchor(hit.Pose);
                    gamePlaced = true;
                    planeController.transform.parent = planeAnchor.transform;
                    Rigidbody rb = planeController.GetComponent<Rigidbody>();
                    rb.transform.LookAt(planeController.transform.position);
                    StopPlaneDetection(false);

                }
            }
        }

        private void StopPlaneDetection(bool flag)
        {
            foreach (GameObject plane in GameObject.FindGameObjectsWithTag("PlaneVisualizer"))
            {
                Renderer r = plane.GetComponent<Renderer>();
                DetectedPlaneVisualizer t = plane.GetComponent<DetectedPlaneVisualizer>();
                r.enabled = flag;
                t.enabled = flag;
            }
        }

        private void _UpdateApplicationLifecycle()
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (Session.Status != SessionStatus.Tracking)
            {
                const int lostTrackingSleepTimeout = 15;
                Screen.sleepTimeout = lostTrackingSleepTimeout;
            }
            else
            {
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }

            if (m_IsQuitting)
            {
                return;
            }

            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                _ShowAndroidToastMessage("Camera permission is needed to run this application.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
            else if (Session.Status.IsError())
            {
                _ShowAndroidToastMessage(
                    "ARCore encountered a problem connecting.  Please start the app again.");
                m_IsQuitting = true;
                Invoke("_DoQuit", 0.5f);
            }
        }

        private void _DoQuit()
        {
            Application.Quit();
        }

        private void _ShowAndroidToastMessage(string message)
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity =
                unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            if (unityActivity != null)
            {
                AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>(
                            "makeText", unityActivity, message, 0);
                    toastObject.Call("show");
                }));
            }
        }
    }
}
