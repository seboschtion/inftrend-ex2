using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using Input = GoogleARCore.InstantPreviewInput;
#endif

public class MainController : MonoBehaviour
{
    public Camera FirstPersonCamera;
    public GameObject Window;
    public GameObject MenuUI;
    public GameObject SearchingForPlaneUI;
    public GameObject TapToPlayUI;
    public GameObject Mesh;
    public GameObject Player;
    public GameObject Targets;
    public GameObject HUD;
    public Text Debugger;
    public int EnemyThreshold = 11;

    private List<DetectedPlane> m_AllPlanes = new List<DetectedPlane>();
    private bool _playing;
    private int _passedEnemies = 0;

    void Start()
    {
        Window.SetActive(false);
        HUD.SetActive(false);
    }

    public void Update()
    {
        ShowSearchingUI();

        // if player has not touched screen, exit
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        // raycast against location the player touched to search for planes
        TrackableHit hit;
        TrackableHitFlags raycastFilter = TrackableHitFlags.PlaneWithinPolygon |
            TrackableHitFlags.FeaturePointWithSurfaceNormal;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            // Use hit pose and camera pose to check if hittest is from the
            // back of the plane, if it is, no need to create the anchor.
            if (!((hit.Trackable is DetectedPlane) &&
                Vector3.Dot(FirstPersonCamera.transform.position - hit.Pose.position, hit.Pose.rotation * Vector3.up) < 0))
            {
                StartGame(hit);
            }
        }
    }

    private void StartGame(TrackableHit hit)
    {
        if (_playing)
        {
            return;
        }

        _playing = true;
        MenuUI.SetActive(false);
        Mesh.SetActive(false);
        PointCloud.SetActive(false);
        Window.SetActive(true);
        Targets.SetActive(true);
        HUD.SetActive(true);

        var anchor = hit.Trackable.CreateAnchor(hit.Pose);
        Window.transform.parent = anchor.transform;
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

    public void EnemyPassed()
    {
        _passedEnemies += 1;
        if(_passedEnemies > EnemyThreshold)
        {
            GameOver();
        }
    }

    public void CountUp()
    {
        // TODO: Muriel
        DebugScreen("CountUp");
    }

    public void GameOver()
    {
        // TODO: Muriel
        DebugScreen("GameOver");
    }

    private void DebugScreen(string msg)
    {
        Debug.Log(msg);
        Debugger.text = msg;
    }
}
