using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class MainController : MonoBehaviour
{
    public Camera FirstPersonCamera;
    public GameObject WindowPrefab;
    public GameObject MenuUI;
    public GameObject SearchingForPlaneUI;
    public GameObject TapToPlayUI;

    private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();

    public void Update()
    {
        // Hide snackbar when currently tracking at least one plane.
        ShowSearchingUI();

        // If the player has not touched the screen, we are done with this update.
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        // Raycast against the location the player touched to search for planes.
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if ((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position,
                    hit.Pose.rotation * Vector3.up) < 0)
            {
                Debug.Log("Hit at back of the current DetectedPlane");
            }
            else
            {
                MenuUI.SetActive(false);

                // Instantiate window at the hit pose.
                var andyObject = Instantiate(WindowPrefab, hit.Pose.position, hit.Pose.rotation);

                // Compensate for the hitPose rotation facing away from the raycast (i.e. camera).
                andyObject.transform.Rotate(0, 180.0f, 0, Space.Self);

                // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
                // world evolves.
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                // Make window a child of the anchor.
                andyObject.transform.parent = anchor.transform;
            }
        }
    }

    private void ShowSearchingUI()
    {
        Session.GetTrackables<DetectedPlane>(m_AllPlanes);
        bool showSearchingUI = true;
        for (int i = 0; i < m_AllPlanes.Count; i++)
        {
            if (m_AllPlanes[i].TrackingState == TrackingState.Tracking)
            {
                showSearchingUI = false;
                break;
            }
        }

        SearchingForPlaneUI.SetActive(showSearchingUI);
        TapToPlayUI.SetActive(!showSearchingUI);
    }
}
