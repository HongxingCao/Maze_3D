using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager GlobalAccess;

    void Awake()
    {
        GlobalAccess = this;
    }

    public void UIInstantiate(string UIname, string showText, float waitTime, float showTime)
    {
        StartCoroutine(ShowUI(UIname, showText, waitTime, showTime));
    }

    IEnumerator ShowUI(string UIname, string showText, float waitTime, float showTime)
    {
        yield return new WaitForSeconds(waitTime);

        GameObject newUI = Instantiate(Resources.Load("UIPrefab\\" + UIname, typeof(GameObject))) as GameObject;
        Destroy(newUI, showTime);
    }
}
