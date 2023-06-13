using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;

public class CollectBox : MonoBehaviour
{
    public AudioClip positiveSound;
    public GameObject[] positiveEffects;
    public AudioClip negativeSound;
    public GameObject[] negativeEffects;
    public bool isChecked;

    private InputControls inputControls;

    void Awake()
    {
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

    void Pickup(InputAction.CallbackContext ctx)
    {

        if (isChecked)
        {
            GetComponent<MeshRenderer>().material.color = Color.yellow;

            //if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerDynamic.debugInfo = "Box Pickup"; Debug.Log(PlayerDynamic.debugInfo);

                int collectedBoxes = 0;
                while (collectedBoxes == 0) { collectedBoxes = Random.Range(-10, 11) * 40; }

                if (collectedBoxes > 0)
                {
                    AudioSource.PlayClipAtPoint(positiveSound, transform.position);
                    for (int i = 0; i < positiveEffects.Length; i++)
                    {
                        GameObject newParticleEffect = GameObject.Instantiate(positiveEffects[i], transform.position, positiveEffects[i].transform.rotation) as GameObject;
                        Destroy(newParticleEffect, 5);
                    }
                    UIManager.GlobalAccess.UIInstantiate("GetRewardUI", "", 0.5f, 2f);
                }
                else if (collectedBoxes < 0)
                {
                    AudioSource.PlayClipAtPoint(negativeSound, transform.position);
                    for (int i = 0; i < negativeEffects.Length; i++)
                    {
                        GameObject newParticleEffect = GameObject.Instantiate(negativeEffects[i], transform.position, negativeEffects[i].transform.rotation) as GameObject;
                        Destroy(newParticleEffect, 5);
                    }
                    UIManager.GlobalAccess.UIInstantiate("BePunishedUI", "", 0.5f, 2f);
                }
               

                PlayerDynamic.Coins += collectedBoxes;
                PlayerDynamic.isEvent = 2;

                StreamWriter sw = new StreamWriter("movementFile" + SubjectMenu.subjectNumber + ".txt", true);
                sw.WriteLine("{0}\t{1}\t{2}\tcollectBox\tcoins:{3}", Time.timeSinceLevelLoad, transform.position.x, transform.position.z, collectedBoxes);
                sw.Close();

                Destroy(gameObject);
                Destroy(gameObject.transform.parent.gameObject);

            }
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }
        isChecked = false;
    }

}
