using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class TriggerWell : MonoBehaviour
{
    public int enterCount = 0;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enterCount++;

            if (gameObject.tag == "Entry")
            {
                //PlayerDynamic.isEvent = 1;
                //Destroy(gameObject);
            }
           else if(gameObject.tag == "Corner")
            {
                GenerateMaze.updateType = 1;
                if (enterCount % 2 == 0)
                {
                    //PlayerDynamic.isEvent = 1;//enter the same corner for the 2nd time
                }
            }
            else if(gameObject.tag == "GoalWell" && gameObject.transform.childCount > 0)
            {
                GameObject goal = gameObject.transform.GetChild(0).gameObject;
                //goal.transform.position = other.gameObject.transform.forward * 40f + other.gameObject.transform.position;
                goal.SetActive(true);
            }
            else if(gameObject.tag == "BoxWell" && gameObject.transform.childCount > 0)
            {
                GameObject box = gameObject.transform.GetChild(0).gameObject;
                box.transform.position = other.gameObject.transform.forward * 40f + other.gameObject.transform.position;
                box.SetActive(true);
            }

            StreamWriter sw = new StreamWriter("movementFile" + SubjectMenu.subjectNumber + ".txt", true);
            sw.WriteLine("{0}\t{1}\t{2}\t{3}\tenterCount:{4}", Time.timeSinceLevelLoad, transform.position.x, transform.position.z, gameObject.tag, enterCount.ToString());
            sw.Close();

            PlayerDynamic.debugInfo = gameObject.tag + " " + enterCount.ToString(); Debug.Log(PlayerDynamic.debugInfo);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Entry")
            {

            }
            else if (gameObject.tag == "Corner")
            {
                if (enterCount % 2 == 0)
                {
                    //PlayerDynamic.isEvent = 1;//leave the same corner for the 2nd time
                }
            }
            else if (gameObject.tag == "GoalWell" && gameObject.transform.childCount > 0)
            {
                /*GameObject goal = gameObject.transform.GetChild(0).gameObject;
                goal.SetActive(false);*/
            }
            else if (gameObject.tag == "BoxWell" && gameObject.transform.childCount > 0)
            {

                GameObject box = gameObject.transform.GetChild(0).gameObject;
                box.SetActive(false);
            }

        }
    }
}
