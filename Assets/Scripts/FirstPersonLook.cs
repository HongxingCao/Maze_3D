using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity_x = 0.6f;
    public float sensitivity_y = 0.01f;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;

    private PlayerInputControls inputControls;
    void Awake()
    {

        inputControls = new PlayerInputControls();
        inputControls.PlayerAction.Enable();
    }

    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (FirstPersonMovement.HaltUpdateMovement)
            return;

        // Get smooth velocity.
        //float lookX = Input.GetAxisRaw("Mouse X");
        //float lookY = Input.GetAxisRaw("Mouse Y");
        Vector2 look = inputControls.PlayerAction.Look.ReadValue<Vector2>();
        Vector2 rawFrameVelocity = Vector2.Scale(look, new Vector2(sensitivity_x, sensitivity_y));
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }
}
