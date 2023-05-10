using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CollectBox : MonoBehaviour
{
    public AudioClip positiveSound;
    public AudioClip negativeSound;
    public bool isChecked;

    void Start()
    {
        isChecked = false;
    }

    void Update()
    {
        if(isChecked)
        {
            GetComponent<MeshRenderer>().material.color = Color.yellow;
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerDynamic.debugInfo = "keycode E"; Debug.Log(PlayerDynamic.debugInfo);

                int collectedBoxes = 0;
                while(collectedBoxes == 0) { collectedBoxes = Random.Range(-10, 11) * 40; }                  

                if(collectedBoxes > 0)
                {
                    AudioSource.PlayClipAtPoint(positiveSound, transform.position);
                }
                else if (collectedBoxes < 0)
                {
                    AudioSource.PlayClipAtPoint(negativeSound, transform.position);
                }
                
                PlayerDynamic.Coins += collectedBoxes;
                PlayerDynamic.collectedBoxes = collectedBoxes;
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
