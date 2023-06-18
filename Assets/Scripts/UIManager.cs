using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        if(showText.Length>0)
        {
            TMP_Text title = newUI.GetComponentInChildren<TMP_Text>();
            title.text = showText;
        }

        if (showTime > 0)
        {
            Destroy(newUI, showTime);
        }
    }
}
