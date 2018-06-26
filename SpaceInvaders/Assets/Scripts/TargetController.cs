using UnityEngine;

public class TargetController : MonoBehaviour
{ 
    void Update()
    {
        if (gameObject.transform.position.z < 35)
        {
            Destroy(gameObject);
            GetMainController().EnemyPassed();
        }
    }

    MainController GetMainController()
    {
        GameObject go = GameObject.Find("Main Controller");
        return (MainController)go.GetComponent(typeof(MainController));
    }
}
