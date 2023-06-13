using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class test : MonoBehaviour
{
	public GameObject[] ParticleEffects;
	public AudioSource EffectAudio;
	// Start is called before the first frame update

	private InputControls inputControls;
	void Awake()
	{
		inputControls = new InputControls();
		inputControls.Player.Pickup.performed += SpawnParticleEffect;
	}

	void OnEnable()
    {
		inputControls.Player.Enable();
	}
	void OnDisable()
    {
		inputControls.Player.Disable();
	}

	private Vector3 spawnPosition = Vector3.zero;
	public void SpawnParticleEffect(InputAction.CallbackContext ctx)
	{
		spawnPosition = new Vector3(1000, 1, 600);
		for(int i=0; i< ParticleEffects.Length; i++)
        {
			GameObject effect = GameObject.Instantiate(ParticleEffects[i], spawnPosition, ParticleEffects[i].transform.rotation) as GameObject;
			Destroy(effect, 5);
		}
		EffectAudio.Play();
	}
}
