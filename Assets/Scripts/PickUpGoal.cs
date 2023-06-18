using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;

public class PickUpGoal : MonoBehaviour
{
    public AudioClip PickUpSound;
    public GameObject[] PickupEffects;

    void Start()
    {
        PlayerDynamic.Instance.inputControls.Player.Pickup.performed += Pickup;
    }

    public void Pickup(InputAction.CallbackContext ctx)
    {
        if (PlayerDynamic.Instance.checkedObj == gameObject && !PlayerDynamic.Instance.carryGoal)
        {
            GetComponent<MeshRenderer>().material.color = Color.yellow;

            PlayerDynamic.Instance.LogInfo = "Goal Pickup"; Debug.Log(PlayerDynamic.Instance.LogInfo);

            AudioSource.PlayClipAtPoint(PickUpSound, transform.position);
            for (int i=0; i< PickupEffects.Length;i++)
            {
                GameObject newParticleEffect = GameObject.Instantiate(PickupEffects[i], transform.position, PickupEffects[i].transform.rotation) as GameObject;
                Destroy(newParticleEffect, 5);
            }

            UIManager.GlobalAccess.UIInstantiate("GetGoalUI", "", 0.5f, 2f);
                
            PlayerDynamic.Instance.carryGoal = true;
            PlayerDynamic.Instance.isEvent = true;

            
            GameObject goalwell = gameObject.transform.parent.gameObject;
            GameObject player = GameObject.FindWithTag("Player");
            transform.parent = player.transform;//.Find("RefArrow");
            transform.localPosition = new Vector3(5, 6, 6); //Camera.main.ScreenToViewportPoint(Input.mousePosition);
            transform.localScale = new Vector3(1, 1, 1);
            transform.GetChild(0).gameObject.SetActive(false);
            gameObject.name = "goal";
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.layer = LayerMask.NameToLayer("Default");

            PlayerDynamic.Instance.WriteBehavior(string.Format("{0}\t{1}\t{2}\tPickUpGoal", Time.timeSinceLevelLoad, transform.position.x, transform.position.z));
            PlayerDynamic.Instance.inputControls.Player.Pickup.performed -= Pickup;
            Destroy(goalwell);

        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }
}
