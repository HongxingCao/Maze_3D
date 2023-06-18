using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class TriggerWell : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Entry")
            {
                //TODO
            }
           else if(gameObject.tag == "Corner")
            {
                GenerateMaze.updateType = 1;

            }
            else if(gameObject.tag == "GoalWell" && gameObject.transform.childCount > 0)
            {
                GameObject goal = gameObject.transform.GetChild(0).gameObject;
                //goal.transform.position = other.gameObject.transform.forward * 40f + other.gameObject.transform.position;
                goal.SetActive(true);

                GenerateMaze.updateType = 1;
            }
            else if(gameObject.tag == "BoxWell" && gameObject.transform.childCount > 0)
            {
                GameObject box = gameObject.transform.GetChild(0).gameObject;
                box.transform.position = other.gameObject.transform.forward * 40f + other.gameObject.transform.position;
                box.SetActive(true);
            }
            else if (gameObject.tag == "DroppingsWell" && gameObject.transform.childCount > 0)
            {
                GameObject droppings = gameObject.transform.GetChild(0).gameObject;
                droppings.transform.position = other.gameObject.transform.forward * 40f + other.gameObject.transform.position;
                droppings.SetActive(true);
            }

            PlayerDynamic.Instance.LogInfo = gameObject.tag; Debug.Log(PlayerDynamic.Instance.LogInfo);
            PlayerDynamic.Instance.WriteBehavior(string.Format("{0}\t{1}\t{2}\t{3}", Time.timeSinceLevelLoad, transform.position.x, transform.position.z, gameObject.tag));
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

            }
            else if (gameObject.tag == "GoalWell" && gameObject.transform.childCount > 0)
            {
                GameObject goal = gameObject.transform.GetChild(0).gameObject;
                goal.SetActive(false);
            }
            else if (gameObject.tag == "BoxWell" && gameObject.transform.childCount > 0)
            {

                GameObject box = gameObject.transform.GetChild(0).gameObject;
                box.SetActive(false);
            }
            else if (gameObject.tag == "DroppingsWell" && gameObject.transform.childCount > 0)
            {

                GameObject droppings = gameObject.transform.GetChild(0).gameObject;
                droppings.SetActive(false);
            }

        }
    }
}
