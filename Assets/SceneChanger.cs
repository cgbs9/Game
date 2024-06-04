using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    public Vector3 targetPoint;
    public float threshold = 0.1f;

    void Update()
    {
        if (CheckObjectReached(object1) && CheckObjectReached(object2) && CheckObjectReached(object3))
        {
            ChangeScene();
        }
    }

    bool CheckObjectReached(GameObject obj)
    {
        return Vector3.Distance(obj.transform.position, targetPoint) < threshold;
    }

    void ChangeScene()
    {
        // Replace "YourNextScene" with the name of your scene
        SceneManager.LoadScene("Together");
    }
}
