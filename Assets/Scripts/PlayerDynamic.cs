using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;

using UnityEngine.UIElements;
//using TMPro;

public class PlayerDynamic : MonoBehaviour
{
    public static PlayerDynamic Instance;

    [Header("Debug")]
    public bool isDebug = false;
    public GameObject miniMap;

    [Header("Rating")]
    public float ratingLag = 2 * 60f;
    
    public int LogCoins { get; set; } = 0;
    public string LogInfo { get; set; } = "";

    public bool isEvent { set; get; } = false;
    public bool isRating { set; get; } = false;
    public int goalNum { set; get; } = 0;
    public int boxNum { set; get; } = 0;
    public int droppingsNum { set; get; } = 0;
    public float ratingTime { set; get; } = 0f;
    public bool carryGoal { get; set; } = false;
    public GameObject checkedObj = null;

    public bool Halt { set; get; } = false;

    public InputControls inputControls;

    public void WriteBehavior(string BehaviorLine)
    {
        StreamWriter sw = new StreamWriter("movementFile" + SubjectMenu.subjectNumber + ".txt", true);
        sw.WriteLine(BehaviorLine);
        sw.Close();
    }

    void Awake()
    {
        Instance = this;
        inputControls = new InputControls();
        inputControls.Player.Enable();
        inputControls.Player.Quit.performed += QuitUI;
    }

    void Start()
    {
        InvokeRepeating("WritePlayerPosition", 0f, 0.1f);
        InvokeRepeating("MoodRating", 0f, 3f);

        UIManager.GlobalAccess.UIInstantiate("StartGameUI", "", 0.5f, -1f);
    }

    // Update is called once per frame
    void Update()
    {
        miniMap.SetActive(isDebug);

        LayerMask layerMask = LayerMask.GetMask("Pickable");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 50, layerMask))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);
            checkedObj = hit.collider.gameObject;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 200, Color.white);
            checkedObj = null;
        }

        if(isEvent)
        {
            StartCoroutine(ClearEvent(3f));
        }
    }
    IEnumerator ClearEvent(float wait)
    {
        yield return new WaitForSeconds(wait);
        isEvent = false;
    }

    void QuitUI(InputAction.CallbackContext ctx)
    {
        PlayerDynamic.Instance.Halt = true;
        UIManager.GlobalAccess.UIInstantiate("QuitGameUI", "", 0.5f, -1f);
    }

    void WritePlayerPosition()
    {
        WriteBehavior(string.Format("{0}\t{1}\t{2}", Time.timeSinceLevelLoad, transform.position.x, transform.position.z));
    }

    void MoodRating()
    {
        float fromLastTime = Time.timeSinceLevelLoad - ratingTime;
        if(fromLastTime > ratingLag && ! isEvent && ! isRating)
        {
            PlayerDynamic.Instance.LogInfo = "MoodRating"; Debug.Log(PlayerDynamic.Instance.LogInfo);

            isRating = true;
            PlayerDynamic.Instance.Halt = true;
            UIManager.GlobalAccess.UIInstantiate("2DMood", "", 0.5f, -1f);
           

            WriteBehavior(string.Format("{0}\t{1}\t{2}\tTimeRelevant\tfromLastTime:{3}", Time.timeSinceLevelLoad, transform.position.x, transform.position.z, fromLastTime.ToString()));
        }
    }

    //private GUIStyle style = new GUIStyle();
    void OnGUI()
    {
        // Position the Text in the center of the Box
        //style.alignment = TextAnchor.MiddleCenter;
        //style.fontSize = 40;

        GUI.skin.box.fontSize = 48;
        GUI.skin.label.fontSize = 36;
        GUI.skin.textArea.fontSize = 24;

        if (isDebug)
        {
            GUI.color = Color.white;
            GUILayout.TextArea("     Debug Information     ", GUILayout.Width(300));
            GUILayout.TextArea(" Subject No: " + SubjectMenu.subjectNumber);
            GUILayout.TextArea(" Boxes: " + boxNum);
            GUILayout.TextArea(" Droppings: " + droppingsNum);
            GUILayout.TextArea(" Goals: " + goalNum);
            GUILayout.TextArea(" Coins: " + LogCoins);
            GUILayout.TextArea(" Time: " + (int)(Time.timeSinceLevelLoad / 60f) + " min", GUILayout.Width(300));
            GUILayout.TextArea(" last time Rating: " + (int)(ratingTime / 60f) + " min");
            GUILayout.TextArea(LogInfo);

            if (carryGoal)
            {
                GUILayout.TextArea("Carrying the drug bottle");
            }
        }
    }

}
