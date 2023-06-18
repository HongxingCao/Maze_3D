using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.InputSystem;

public class Home : MonoBehaviour
{
    public AudioClip CongratsSound;
    public bool isPlayerHome;

    void Awake()
    {
        isPlayerHome = false;
    }

    void Start()
    {
        PlayerDynamic.Instance.inputControls.Player.Putdown.performed += Putdown;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerHome = true;

            PlayerDynamic.Instance.LogInfo = "Come back home!"; Debug.Log(PlayerDynamic.Instance.LogInfo);
            PlayerDynamic.Instance.WriteBehavior(string.Format("{0}\t{1}\t{2}\tComeBackHome", Time.timeSinceLevelLoad, transform.position.x, transform.position.z));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayerHome = false;

            PlayerDynamic.Instance.LogInfo = "Leave home!"; Debug.Log(PlayerDynamic.Instance.LogInfo);
            PlayerDynamic.Instance.WriteBehavior(string.Format("{0}\t{1}\t{2}\tLeaveHome", Time.timeSinceLevelLoad, transform.position.x, transform.position.z));
        }
    }

    void Putdown(InputAction.CallbackContext ctx)
    {
        if (isPlayerHome && PlayerDynamic.Instance.carryGoal)
        {

            PlayerDynamic.Instance.LogInfo = "Home Putdown"; Debug.Log(PlayerDynamic.Instance.LogInfo);

            AudioSource.PlayClipAtPoint(CongratsSound, transform.position);
            UIManager.GlobalAccess.UIInstantiate("VictoryUI", "", 0f, 3f);

            PlayerDynamic.Instance.goalNum++;
            PlayerDynamic.Instance.carryGoal = false;
            PlayerDynamic.Instance.isEvent = true;


            GameObject player = GameObject.FindWithTag("Player");
            GameObject goal = player.transform.Find("goal").gameObject;
            goal.transform.parent = transform;
            //float pos_x = (float)(Math.Pow((-1), ((int)PlayerDynamic.Instance.goalNum % 2)) * ((int)PlayerDynamic.Instance.goalNum / 2) * 1.1f);
            goal.transform.localPosition = calLocalPos(PlayerDynamic.Instance.goalNum);//new Vector3(pos_x, 1, 1.1f); 
            goal.transform.localScale = new Vector3(10 / 14f, 10 / 1f, 10 / 11f);
            goal.transform.GetChild(0).gameObject.SetActive(true);

            GenerateMaze.updateType = 2;
            PlayerDynamic.Instance.WriteBehavior(string.Format("{0}\t{1}\t{2}\tOffloadGoal\tgoalNum:{3}", Time.timeSinceLevelLoad, transform.position.x, transform.position.z, PlayerDynamic.Instance.goalNum));
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
