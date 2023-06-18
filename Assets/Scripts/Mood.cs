using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Mood : MonoBehaviour
{
    float slidderValue;
    public void OnButtonPress()
    {
        PlayerDynamic.Instance.LogInfo = "Mood Rating: " + slidderValue;  Debug.Log(PlayerDynamic.Instance.LogInfo);
        PlayerDynamic.Instance.WriteBehavior(string.Format("{0}\t{1}\t{2}\tMoodRating\t{3}", Time.timeSinceLevelLoad, transform.position.x, transform.position.z, slidderValue));

        PlayerDynamic.Instance.Halt = false;
        PlayerDynamic.Instance.isRating = false;
        PlayerDynamic.Instance.ratingTime = Time.timeSinceLevelLoad;

        GameObject moodRating = GameObject.FindWithTag("Mood");
        Destroy(moodRating);
    }

    public void OnSliderValue(float v)
    {
        slidderValue = v;
    }

}
