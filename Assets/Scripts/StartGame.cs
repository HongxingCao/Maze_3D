using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void OnDebug()
    {
        Debug.Log(" Debug Mode");
        PlayerDynamic.Instance.isDebug = true;
        Destroy(gameObject);
    }

    public void OnExplorer()
    {
        Debug.Log(" Explorer Mode");
        PlayerDynamic.Instance.isDebug = false;
        Destroy(gameObject);
    }

    public void OnGoalSetting()
    {
        Debug.Log(" Goal Setting Mode");
        PlayerDynamic.Instance.isDebug = false;
        Destroy(gameObject);
    }
}
