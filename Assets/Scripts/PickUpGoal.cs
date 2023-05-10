using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PickUpGoal : MonoBehaviour
{
    public AudioClip PickUpSound;
    //public static bool isCollected;
    public bool isChecked;

    void Awake()
    {
        //isCollected = false;
        isChecked = false;
    }

    void Update()
    {
        if (isChecked)
        {
            GetComponent<MeshRenderer>().material.color = Color.yellow;

            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerDynamic.debugInfo = "keycode E"; Debug.Log(PlayerDynamic.debugInfo);

                int collectBoxes = Random.Range(-10, 10) * 40;

                AudioSource.PlayClipAtPoint(PickUpSound, transform.position);

                PlayerDynamic.getGoal = true; PlayerDynamic.carryGoal = true;
                PlayerDynamic.isEvent = 2;

                //Destroy(gameObject);
                GameObject player = GameObject.FindWithTag("Player");
                transform.parent = player.transform;
                transform.localPosition = new Vector3(5, 6, 6); //Camera.main.ScreenToViewportPoint(Input.mousePosition);
                transform.localScale = new Vector3(1, 1, 1);
                transform.GetChild(0).gameObject.SetActive(false);
                gameObject.name = "goal";


                StreamWriter sw = new StreamWriter("movementFile" + SubjectMenu.subjectNumber + ".txt", true);
                sw.WriteLine("{0}\t{1}\t{2}\tPickUpGoal", Time.timeSinceLevelLoad, transform.position.x, transform.position.z);
                sw.Close();

            }            
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        isChecked = false;
    }
}
