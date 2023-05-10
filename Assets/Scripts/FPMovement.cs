using System.Collections.Generic;
using UnityEngine;

public class FPMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();


	public static bool HaltUpdateMovement = false;
	private float buttonRotation = 0f;
    public float RotationRatchet = 45.0f;

	void Awake()
    {

    }

	void Update()
    {
		if (HaltUpdateMovement)
			return;


		//Use keys to ratchet rotation
		if (Input.GetKeyDown(KeyCode.Q))
			buttonRotation -= RotationRatchet;

		if (Input.GetKeyDown(KeyCode.E))
			buttonRotation += RotationRatchet;

		Vector3 euler = Vector3.zero;
		euler.y += buttonRotation;
		transform.rotation = Quaternion.Euler(euler);

		// Update IsRunning from input.
		IsRunning = canRun && Input.GetKey(runningKey);

		// Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

		// Apply movement.
		GetComponent<Rigidbody>().velocity = transform.rotation * new Vector3(targetVelocity.x, GetComponent<Rigidbody>().velocity.y, targetVelocity.y);
	}

/*    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        GetComponent<Rigidbody>().velocity = transform.rotation * new Vector3(targetVelocity.x, GetComponent<Rigidbody>().velocity.y, targetVelocity.y);
    }*/

}