using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Mood : MonoBehaviour
{
    float slidderValue;
    public void OnButtonPress()
    {
        PlayerDynamic.debugInfo = "Mood Rating: " + slidderValue;  Debug.Log(PlayerDynamic.debugInfo);

        StreamWriter sw = new StreamWriter("movementFile" + SubjectMenu.subjectNumber + ".txt", true);
        sw.WriteLine("{0}\t{1}\t{2}\tMoodRating\t{3}", Time.timeSinceLevelLoad, transform.position.x, transform.position.z, slidderValue);
        sw.Close();

        FirstPersonMovement.HaltUpdateMovement = false;
        PlayerDynamic.isRating = 0;
        PlayerDynamic.ratingTime = Time.timeSinceLevelLoad;

        GameObject moodRating = GameObject.FindWithTag("Mood");
        Destroy(moodRating);
    }

    public void OnSliderValue(float v)
    {
        slidderValue = v;
    }

}
