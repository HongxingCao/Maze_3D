using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity_x = 0.06f;
    public float sensitivity_y = 0.001f;
    public float smoothing = 1.5f;
    public bool canSpin = true;
    public bool IsSpinning { get; private set; }
    public float spinSpeed = 20;

    Vector2 velocity;
    Vector2 frameVelocity;

    private InputControls inputControls;
    void Awake()
    {
        inputControls = new InputControls();
        inputControls.Player.Enable();
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        //Cursor.lockState = CursorLockMode.Locked;

        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Update()
    {
        if (PlayerDynamic.Instance.Halt)
            return;

        //IsRunning = canRun && Input.GetKey(KeyCode.LeftAlt);
        float altKey = inputControls.Player.Spin.ReadValue<float>();
        IsSpinning = canSpin && (altKey > 0.2f);
        float sensitivity_factor = IsSpinning ? spinSpeed : 1;

        // Get smooth velocity.
        //float lookX = Input.GetAxisRaw("Mouse X");
        //float lookY = Input.GetAxisRaw("Mouse Y");
        Vector2 look = inputControls.Player.Look.ReadValue<Vector2>();
        Vector2 rawFrameVelocity = Vector2.Scale(look, sensitivity_factor * new Vector2(sensitivity_x, sensitivity_y));
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }
}
