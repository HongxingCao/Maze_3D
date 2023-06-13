using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;

public class PickUpGoal : MonoBehaviour
{
    public AudioClip PickUpSound;
    public GameObject[] PickupEffects;
    //public static bool isCollected;
    public bool isChecked;

    private InputControls inputControls;

    void Awake()
    {
        //isCollected = false;
        isChecked = false;

        inputControls = new InputControls();
        inputControls.Player.Pickup.performed += Pickup;
    }

    void OnEnable()
    {
        inputControls.Player.Enable();
    }
    void OnDisable()
    {
        inputControls.Player.Disable();
    }

    public void Pickup(InputAction.CallbackContext ctx)
    {
        if (isChecked && !PlayerDynamic.carryGoal)
        {
            GetComponent<MeshRenderer>().material.color = Color.yellow;

            //if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerDynamic.debugInfo = "Goal Pickup"; Debug.Log(PlayerDynamic.debugInfo);

                int collectBoxes = Random.Range(-10, 10) * 40;

                AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
                for (int i=0; i< PickupEffects.Length;i++)
                {
                    GameObject newParticleEffect = GameObject.Instantiate(PickupEffects[i], transform.position, PickupEffects[i].transform.rotation) as GameObject;
                    Destroy(newParticleEffect, 5);
                }

                UIManager.GlobalAccess.UIInstantiate("VictoryUI", "", 0.5f, 2f);
                
                PlayerDynamic.carryGoal = true;
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
