using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public GameObject PointCloud;
    public GameObject HUD;
    public Text Scored;
    public Text Missed;
    public int EnemyThreshold = 11;

    private List<DetectedPlane> allPlanes = new List<DetectedPlane>();
    private bool playing;
    private int passedEnemies = 0;
    private Player player;

    void Start()
    {
        Window.SetActive(false);
        HUD.SetActive(false);
        GameObject playerGameObj = GameObject.FindGameObjectWithTag("Player");
        if (playerGameObj != null)
        {
            player = playerGameObj.GetComponent<Player>();
        }
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
        if (playing)
        {
            return;
        }

        playing = true;
        MenuUI.SetActive(false);
        Mesh.SetActive(false);
        PointCloud.SetActive(false);
        Window.SetActive(true);
        HUD.SetActive(true);

        var anchor = hit.Trackable.CreateAnchor(hit.Pose);
        Window.transform.parent = anchor.transform;
    }

    private void ShowSearchingUI()
    {
        Session.GetTrackables<DetectedPlane>(allPlanes);
        bool showSearchingUI = true;
        for (int i = 0; i < allPlanes.Count; i++)
        {
            if (allPlanes[i].TrackingState == TrackingState.Tracking)
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
        passedEnemies += 1;
        Missed.text = string.Format("Missed: {0} / {1}", passedEnemies, EnemyThreshold);
        if (passedEnemies > EnemyThreshold)
        {
            GameOver();
        }
    }

    public void CountUp()
    {
        player.score++;
        Scored.text = string.Format("Scored: {0}", player.score);
    }

    public void GameOver()
    {
        HUD.SetActive(false);
        SceneManager.LoadScene("EndOfGame");
    }
}
