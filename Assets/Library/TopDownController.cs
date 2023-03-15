using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownController : MonoBehaviour
{
    public float walkSpeed;
    public float sprintSpeed;
    private float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    public PlayerControls playerControls;
    private Vector2 moveDirection;
    private InputAction move;
    private InputAction run;
    private InputAction fire;

    public Animator animator;

    public bool useFlipSprite = false;
    public bool isRunning = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveSpeed = walkSpeed;
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();

        run = playerControls.Player.Run;
        run.performed += Run_performed;
        run.Enable();

        //fire = playerControls.Player.Fire;
        //fire.Enable();
        //fire.performed += Fire;
    }

    private void Run_performed(InputAction.CallbackContext obj)
    {
        isRunning = !isRunning;
        animator.SetBool("Running", isRunning);

        moveSpeed = (isRunning ? sprintSpeed : walkSpeed);
    }

    private void OnDisable()
    {
        move.Disable();
        run.Disable();

        run.performed -= Run_performed;
        //fire.performed -= Fire;
        //fire.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //moveInput.x = Input.GetAxisRaw("Horizontal");
        //moveInput.y = Input.GetAxisRaw("Vertical");

        //moveInput.Normalize();

        //if (Input.GetKey(KeyCode.LeftShift))
        //{
        //    animator.SetBool("Running", true);
        //    moveSpeed = sprintSpeed;
        //}
        //else
        //{
        //    animator.SetBool("Running", false);
        //    moveSpeed = walkSpeed;
        //}

        moveDirection = move.ReadValue<Vector2>();

        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);

        if (moveDirection.x > 0)
        {
            animator.SetBool("FaceLeft", false);
            if (useFlipSprite)
            {
                transform.localScale = new Vector3(2, 2);
            }
        }

        if (moveDirection.x < 0)
        {
            animator.SetBool("FaceLeft", true);
            if (useFlipSprite)
            {
                transform.localScale = new Vector3(2, 2);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
