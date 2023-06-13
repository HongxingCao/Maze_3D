using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;

using UnityEngine.UIElements;
//using TMPro;

public class PlayerDynamic : MonoBehaviour
{
    public bool isDebug = false;
    public GameObject miniMap;

    //for Goal & Box
    public static int goalNum = 0;
    public static int boxNum = 0;

    //for MoodRating
    public static int isEvent = 0; //0: no event  1: rating imediately  2: rating 2s later
    public static int isRating = 0; //0: no rating  1: rating 
    public static float ratingTime = 0f;

    //for debug
    public static bool carryGoal = false;
    public static int Coins = 0;
    public static string debugInfo = "";

    GUIStyle style = new GUIStyle();

    private InputControls inputControls;
    void Awake()
    {
        inputControls = new InputControls();
        inputControls.Player.Quit.performed += QuitUI;
    }

    void OnEnable()
    {
        inputControls.Player.Enable();
    }
    void OnDisable()
    {
        inputControls.Player.Disable();
    }

    void Start()
    {
        InvokeRepeating("WritePlayerBehavior", 0f, 0.1f);
        InvokeRepeating("TimeRelevant", 5 * 60f, 5 * 60f);
        InvokeRepeating("EventRelevant", 0f, 1f);

        // Position the Text in the center of the Box
        style.alignment = TextAnchor.MiddleCenter;
        style.fontSize = 40;
    }

    void QuitUI(InputAction.CallbackContext ctx)
    {
        FirstPersonMovement.HaltUpdateMovement = true;

        GameObject QuitUI = Instantiate(Resources.Load("UIPrefab\\QuitGameUI", typeof(GameObject))) as GameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        LayerMask layerMask = LayerMask.GetMask("Pickable");

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 50, layerMask))
        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.yellow);

            GameObject obj = hit.collider.gameObject;
            if (obj != null)
            {
                if (obj.tag == "Box") 
                {
                    obj.GetComponent<CollectBox>().isChecked = true;
                }
                if (obj.tag == "Goal")
                {
                    obj.GetComponent<PickUpGoal>().isChecked = true;
                }
            }

            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 200, Color.white);
        }


        if (isEvent != 0)
        {
            FirstPersonMovement.HaltUpdateMovement = true;
        }

        miniMap.SetActive(isDebug);

    }

    void WritePlayerBehavior()
    {
        StreamWriter sw = new StreamWriter("movementFile" + SubjectMenu.subjectNumber + ".txt", true);
        sw.WriteLine("{0}\t{1}\t{2}", Time.timeSinceLevelLoad, transform.position.x, transform.position.z);
        sw.Close();
    }

    void TimeRelevant()
    {
        float fromLastTime = Time.timeSinceLevelLoad - ratingTime;

        PlayerDynamic.debugInfo = "TimeRelevant"; Debug.Log(PlayerDynamic.debugInfo);
        isEvent = 2;

        StreamWriter sw = new StreamWriter("movementFile" + SubjectMenu.subjectNumber + ".txt", true);
        sw.WriteLine("{0}\t{1}\t{2}\tTimeRelevant\tfromLastTime:{3}", Time.timeSinceLevelLoad, transform.position.x, transform.position.z, fromLastTime.ToString());
        sw.Close();
    }

    void EventRelevant()
    {
        if (isEvent != 0 && isRating == 0)
        {
            FirstPersonMovement.HaltUpdateMovement = true;

            float wait = 0f;
            if (isEvent == 2) wait = 1.5f;
            isEvent = 0;

            isRating = 1;
            StartCoroutine(MoodRating(wait));
        }
    }

    IEnumerator MoodRating(float wait)
    {
        yield return new WaitForSeconds(wait);

        GameObject moodRating = Instantiate(Resources.Load("UIPrefab\\2DMood", typeof(GameObject))) as GameObject;
        
    }

    void OnGUI()
    {
        GUI.skin.box.fontSize = 48;
        GUI.skin.label.fontSize = 36;
        GUI.skin.textArea.fontSize = 24;

        if(isDebug)
        {
            GUI.color = Color.white;
            GUILayout.TextArea("     Debug Information     ", GUILayout.Width(300));
            GUILayout.TextArea(" Subject No: " + SubjectMenu.subjectNumber);
            GUILayout.TextArea(" Boxes: " + boxNum);
            GUILayout.TextArea(" Goals: " + goalNum);
            GUILayout.TextArea(" Coins: " + Coins);
            GUILayout.TextArea(" Time: " + (int)(Time.timeSinceLevelLoad / 60f) + " min", GUILayout.Width(300));
            GUILayout.TextArea(" last time Rating: " + (int)(ratingTime / 60f) + " min");
            GUILayout.TextArea(debugInfo);

            if (carryGoal)
            {
                GUILayout.TextArea("Carrying the drug bottle");
            }
        }
    }
}
