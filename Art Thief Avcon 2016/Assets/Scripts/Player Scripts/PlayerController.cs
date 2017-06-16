using UnityEngine;
using System.Collections;
using InControl;
public class PlayerController : MonoBehaviour {

    public int playerNumber = 1;
    [Space(5)]
    public float walkSpeed;
    public float jumpForce;
    [Space(5)]
    public Material platformColor;
    public Vector2 platformPosition;
    public GameObject playerIcon;
    public float airControl;
    public float delayUntilPlatform;
    public Animator anim;

    [Range(-1, 1)]
    public int direction;
    Rigidbody2D rb;
    Animator animator;
    bool grounded;
    bool painting;
    float paintTimer = 0;
    BoxCollider2D col;
    float zScale;
    bool canCreatePlatform = true;
    float xMovement;
    public float paintCooldown = 0;

    InputDevice device;

    PlayerIcon icon;

	// Use this for initialization
	void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        zScale = Mathf.Abs(transform.localScale.z);
        icon = playerIcon.GetComponent<PlayerIcon>();
        icon.SetPlayerNumber(playerNumber);
        device = InputManager.Devices[playerNumber - 1];

        if (direction == 0)
            direction = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        paintCooldown += Time.deltaTime;

        if (!Pauser.paused)
        {
            CheckGround();

            JumpandMovement();

            Platforms();
        }
        else
        {
            IconInputs();
        }

        //Animation paramaters
        // animator.SetFloat("Speed", Mathf.Abs(xMovement));
        // animator.SetBool("Grounded", grounded);
        // animator.speed = Mathf.Abs(xMovement);
        //Vector3 ls = transform.localScale;
        //transform.localScale = new Vector3(ls.x, ls.y, direction * zScale);

        if (grounded && direction != 0 && !WinDebug.gameOver)
        transform.rotation = Quaternion.Euler(0, direction * 91, 0);

        if (grounded)
        {
            if (Mathf.Abs(xMovement) > 0.8f)
            {
                anim.SetBool("IsRunning", true);
                anim.SetBool("IsWalking", false);
            }
            else if (xMovement != 0)
            {
                anim.SetBool("IsWalking", true);
                anim.SetBool("IsRunning", false);
            }
            else
            {
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsRunning", false);
            }
        }


    }

    void JumpandMovement()
    {
        if (grounded)
        {
            animator.SetBool("IsJumping", false);
        }
        //animator.SetBool("IsGrounded", grounded);
        //Jumping
        float jump;
        if (grounded && device.Action1.WasPressed)
        {
            anim.SetBool("IsJumping", true);
            //grounded = false;
            jump = jumpForce;
        }
        else jump = rb.velocity.y;

        

        animator.SetBool("IsGrounded", grounded);

        //Movement
        if (Mathf.Abs(device.LeftStickX.Value) > 0.2f)
            xMovement = device.LeftStickX.Value;
        else
            xMovement = 0;

        if (grounded)
        {
            if (xMovement > 0.2f)
                direction = 1;
            else if (xMovement < -0.2f)
                direction = -1;
            rb.velocity = new Vector2(xMovement * walkSpeed, jump);
        }
        else //floaty jumps
        {
            float vx = rb.velocity.x;
            if (Mathf.Abs(vx) > 0.2f)
                rb.velocity = new Vector2(vx, jump);
            if (Mathf.Abs(vx) > walkSpeed) //Don't go over maxSpeed
            {
                if (vx < 0.2f)
                    rb.velocity = new Vector2(-walkSpeed, jump);
                if (vx > 0.2f)
                    rb.velocity = new Vector2(walkSpeed, jump);
            }
            rb.AddForce(new Vector2((xMovement * walkSpeed) * airControl, 0));
        }
    }

    void CheckGround()
    {
        //Grounded test
        grounded = false;
        RaycastHit2D[] allHits;
        //allHits = Physics2D.RaycastAll(transform.position, new Vector3(0, -1, 0), col.bounds.extents.y * 1.5f); //create an array of all things hit
        allHits = Physics2D.BoxCastAll(transform.position, new Vector3(col.bounds.extents.x * 1.5f, col.bounds.extents.y, col.bounds.extents.z), 0, new Vector3(0, -1, 0), col.bounds.extents.y * 0.1f);
        Debug.Log("Objects under player: " + allHits.Length);
        foreach (var hit in allHits)
        {
            if (rb.velocity.y <= 0.1f && hit.transform.position != transform.position &&
                hit.transform.GetComponent<Rigidbody2D>() != null) //if collided with something other than self
            {
                grounded = true;
                break;
            }
        }
    }

    void Platforms()
    {
        //Create platform
        if ((canCreatePlatform && device.LeftTrigger.WasPressed || device.RightTrigger.WasPressed) && !painting && paintCooldown >= 1.5f)
        {
            painting = true;
            paintCooldown = 0f;
        }

        if (painting)
        {
            paintTimer += Time.deltaTime;
        }

        if (paintTimer >= delayUntilPlatform && painting)
        { 
            animator.SetBool("IsPainting", false);
            paintTimer = 0;
            painting = false;
            GameObject plat = Instantiate(Resources.Load("Platform"), transform.position +
                    new Vector3(platformPosition.x * direction, platformPosition.y, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
            plat.GetComponent<Renderer>().material = platformColor;
            canCreatePlatform = false;
        }
        
        animator.SetBool("IsPainting", painting);

        if (!device.LeftTrigger.IsPressed && !device.RightTrigger.IsPressed && !painting)
            canCreatePlatform = true;
    }

    void IconInputs()
    {
        if (device.RightTrigger.Value > 0.9f
            && device.Action1.WasPressed)
        {
            icon.TakePicture();
        }
        else if (device.RightTrigger.Value > 0.9f
            && device.Action2.WasPressed
            || Input.GetKey(playerNumber.ToString()))
        {
            icon.SetToDefault();
        }
        
        else if (device.RightTrigger.Value > 0.9f
            && device.Action4.WasPressed)
        {
            icon.SetReady(true);
        }
        
        else if (device.RightTrigger.Value > 0.9f
            && device.Action3.WasPressed)
        {
            icon.SetToDefault();
            icon.SetReady(false);
        }
    }

    public void SetAsLoser()
    {
        anim.SetBool("IsLoser", true);
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsPainting", false);
        Debug.Log("Player " + playerNumber + "loses");
    }

    public void SetAsWinner()
    {
        anim.SetBool("IsWinner", true);
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsWalking", false);
        anim.SetBool("IsJumping", false);
        anim.SetBool("IsPainting", false);
        Debug.Log("Player " + playerNumber + " wins");
    }
}
