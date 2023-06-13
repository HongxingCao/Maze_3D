using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
//using System.Security.Cryptography;
using System;
using UnityEngine.InputSystem;

public class Home : MonoBehaviour
{
    public AudioClip CongratsSound;
    public bool isPlayerHome;

    private InputControls inputControls;

    void Awake()
    {
        inputControls = new InputControls();
        inputControls.Player.Putdown.performed += Putdown;
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
        isPlayerHome = false;
    }

    void Putdown(InputAction.CallbackContext ctx)
    {
        if (isPlayerHome && PlayerDynamic.carryGoal)
        {
            //if (Input.GetKeyDown(KeyCode.Q))
            {
                PlayerDynamic.debugInfo = "Home Putdown"; Debug.Log(PlayerDynamic.debugInfo);

                AudioSource.PlayClipAtPoint(CongratsSound, transform.position);
                UIManager.GlobalAccess.UIInstantiate("VictoryUI", "", 0f, 3f);

                PlayerDynamic.goalNum++;
                PlayerDynamic.carryGoal = false;
                PlayerDynamic.isEvent = 2;


                GameObject player = GameObject.FindWithTag("Player");
                GameObject goal = player.transform.Find("goal").gameObject;
                goal.transform.parent = transform;
                //float pos_x = (float)(Math.Pow((-1), ((int)PlayerDynamic.goalNum % 2)) * ((int)PlayerDynamic.goalNum / 2) * 1.1f);
                goal.transform.localPosition = calLocalPos(PlayerDynamic.goalNum);//new Vector3(pos_x, 1, 1.1f); 
                goal.transform.localScale = new Vector3(10 / 14f, 10 / 1f, 10 / 11f);
                goal.transform.GetChild(0).gameObject.SetActive(true);
                goal.layer = LayerMask.NameToLayer("Default");


                GenerateMaze.updateType = 2;

                StreamWriter sw = new StreamWriter("movementFile" + SubjectMenu.subjectNumber + ".txt", true);
                sw.WriteLine("{0}\t{1}\t{2}\tOffloadGoal\tgoalNum:{3}", Time.timeSinceLevelLoad, transform.position.x, transform.position.z, PlayerDynamic.goalNum);
                sw.Close();


            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerHome = true;

            StreamWriter sw = new StreamWriter("movementFile" + SubjectMenu.subjectNumber + ".txt", true);
            sw.WriteLine("{0}\t{1}\t{2}\tComeBackHome", Time.timeSinceLevelLoad, transform.position.x, transform.position.z);
            sw.Close();

            PlayerDynamic.debugInfo = "Come back home!"; Debug.Log(PlayerDynamic.debugInfo);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerHome = false;

            StreamWriter sw = new StreamWriter("movementFile" + SubjectMenu.subjectNumber + ".txt", true);
            sw.WriteLine("{0}\t{1}\t{2}\tLeaveHome", Time.timeSinceLevelLoad, transform.position.x, transform.position.z);
            sw.Close();

            PlayerDynamic.debugInfo = "Leave home!"; Debug.Log(PlayerDynamic.debugInfo);
        }
    }


    private Vector3 calLocalPos(int n)
    {
        int number =25;
        float z_shift = -1;
        float x_shift = 0;
        float y_shift = 3;
        float y_lag = 6;

        int ind_y = (n - 1) / number;
        n = (n - 1) % number + 1;

        float r = (float)(4.5f - (ind_y * 0.4)) / 2 ;

        float angle = (float)(Math.Pow(-1, ((int)n % 2)) * ((int)n / 2) * (360 / number));
        float radian = (angle / 180) * Mathf.PI;
        float pos_z = r * Mathf.Cos(radian);
        float pos_x = r * Mathf.Sin(radian);

        return new Vector3(x_shift + pos_x, y_shift + y_lag * ind_y, z_shift + pos_z);
    }

}
