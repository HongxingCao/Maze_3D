using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.InputSystem;

public class Dropping : MonoBehaviour
{
    public AudioClip positiveSound;
    public GameObject[] positiveEffects;
    public AudioClip negativeSound;
    public GameObject[] negativeEffects;

    void OnEnable()
    {
        GetComponent<MeshRenderer>().material.color = Color.yellow;

        PlayerDynamic.Instance.LogInfo = "Dropping Enable"; Debug.Log(PlayerDynamic.Instance.LogInfo);

        int Coins = 0;
        while (Coins == 0) { Coins = Random.Range(-10, 11) * 40; }
        int absCoins = System.Math.Abs(Coins);

        if (Coins > 0)
        {
            AudioSource.PlayClipAtPoint(positiveSound, transform.position);
            for (int i = 0; i < positiveEffects.Length; i++)
            {
                GameObject newParticleEffect = GameObject.Instantiate(positiveEffects[i], transform.position, positiveEffects[i].transform.rotation) as GameObject;
                Destroy(newParticleEffect, 5);
            }
            UIManager.GlobalAccess.UIInstantiate("GetRewardUI", "���" + absCoins.ToString() + "Ԫ", 0.5f, 2f);
        }
        else if (Coins < 0)
        {
            AudioSource.PlayClipAtPoint(negativeSound, transform.position);
            for (int i = 0; i < negativeEffects.Length; i++)
            {
                GameObject newParticleEffect = GameObject.Instantiate(negativeEffects[i], transform.position, negativeEffects[i].transform.rotation) as GameObject;
                Destroy(newParticleEffect, 5);
            }
            UIManager.GlobalAccess.UIInstantiate("BePunishedUI", "��ʧ" + absCoins.ToString() + "Ԫ", 0.5f, 2f);
        }

        PlayerDynamic.Instance.LogCoins += Coins;
        PlayerDynamic.Instance.isEvent = true;

        PlayerDynamic.Instance.WriteBehavior(string.Format("{0}\t{1}\t{2}\tdroppings\tcoins:{3}", Time.timeSinceLevelLoad, transform.position.x, transform.position.z, Coins));

        Destroy(gameObject.transform.parent.gameObject);
        //Destroy(gameObject);
    }
}
