using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //Movement speed
    [SerializeField] private float speed = 4.0f;
    //Jump strength
    [SerializeField] private float jumpForce = 400.0f;
    //Tag of ground to check for collision
    [SerializeField] private string groundTag;
    //How fast does player change direction
    [Range(0, .3f)] [SerializeField] private float smoothTime = 0.15f;
    //Layer contain ground
    [SerializeField] private LayerMask groundLayer;
    //Object to check for ground
    [SerializeField] private Transform groundCheck;


    [Header("Events")]
    [Space]
    //Events occur when player landing
    public UnityEvent OnLandEvent;


    private Rigidbody2D rgbd2d;
    private Animator animate;

    public KeyCode jumpBtn = KeyCode.UpArrow;

    ///--------------------drop down event
    public KeyCode downBtn = KeyCode.DownArrow;
    public float delayTime;

    ///-------------------Health, pickup event
    public int numberOfHealth = 5;

    //-------------------- Movement
    //To check if player facing right
    private bool facingRight = true;
    public float direction { get { if (facingRight) { return 1f; } return -1f; } }
    //To check if player is on ground
    private bool grounded = true;
    //To check if player can perform double jump
    private bool canDoubleJump = true;

    //Radius of circle to check for ground
    private float groundedRadius = 0.2f;

    //support vector for movement
    private Vector3 targetVelocity; //target to reach
    private Vector3 currentVelocity = Vector3.zero; //current velocity in function progress

    //customize-----------------
    [SerializeField] bool hasControl;
    public static PlayerController localPlayer;
    static Color playerColor;
    SpriteRenderer playerSprite;

    static Sprite playerHat;
    public SpriteRenderer hatHolder;

    void Start()
    {

    }

    void Awake()
    {
        if (hasControl)
        {
            localPlayer = this;
        }

        playerSprite = gameObject.GetComponent<SpriteRenderer>();
        if (playerColor == Color.clear)
        {
            playerColor = Color.white;
        }
        playerSprite.color = playerColor;

        if (playerHat != null)
        {
            hatHolder.sprite = playerHat;
        }

        rgbd2d = GetComponent<Rigidbody2D>();
        animate = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            gameObject.GetComponentInChildren<GunController>().Fire();
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            gameObject.GetComponentInChildren<GunController>().ThrowMine();
        }

        //========movement===========

        //Get input and generate targetVelocity
        float horizontal = Input.GetAxis("Horizontal");
        targetVelocity = new Vector2(horizontal * speed, rgbd2d.velocity.y);

        animate.SetFloat("Horizontal", Mathf.Abs(horizontal));

        //Determine player facing direction
        if (horizontal > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontal < 0 && facingRight)
        {
            Flip();
        }

        //Jumping
        if (Input.GetKeyDown(jumpBtn))
        {
            if (grounded)
            {
                grounded = false;
                rgbd2d.AddForce(new Vector2(0f, jumpForce));
                animate.SetBool("IsJumping", true);
            }
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                // rgbd2d.AddForce(new Vector2(0f, jumpForce));
                rgbd2d.velocity = Vector2.up * 8f;
            }
        }

        //Drop platform 
        if (grounded && Input.GetKeyUp(downBtn))
        {
            gameObject.layer = 9; //change layer of this character to go through platform
            delayTime = 0.4f; //delay time for charater go through the platform completely
        }

        if (delayTime > 0 && gameObject.layer == 9)
        {
            delayTime -= Time.deltaTime;
            Debug.Log("countdown " + delayTime);
        }

        if (delayTime <= 0 && gameObject.layer == 9)
        {
            gameObject.layer = 10; //give back real layer of character
            Debug.Log("Back!!!");
        }

    }

    //Change player position
    void FixedUpdate()
    {
        rgbd2d.velocity = Vector3.SmoothDamp(rgbd2d.velocity, targetVelocity, ref currentVelocity, smoothTime);

        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                canDoubleJump = true;
                if (!wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    public void OnLanding()
    {
        animate.SetBool("IsJumping", false);
    }

    //Flipping player facing direction
    private void Flip()
    {
        facingRight = !facingRight;

        //Rotate the player's vertical axis by 180 degree.
        transform.Rotate(0f, 180f, 0f);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            numberOfHealth--;
            Debug.Log(numberOfHealth);
            gameObject.GetComponentInChildren<GunController>().resetMine();
        }
        else
        {
            numberOfHealth++;
            Debug.Log(numberOfHealth);
        }
    }

    public void ChangeWeapon(bool isDefault)
    {
        int typeOfGun;
        int numberOfGun;

        if (isDefault == false)
        {
            //random and get data to gun
            typeOfGun = Random.Range(2, 5);
            switch (typeOfGun)
            {
                case 1: numberOfGun = Random.Range(1, 16); break;
                case 2: numberOfGun = Random.Range(1, 14); break;
                case 3: numberOfGun = Random.Range(1, 12); break;
                case 4: numberOfGun = Random.Range(1, 8); break;
                default: numberOfGun = 1; break;
            }
        }
        else //back to default gun, do not random
        {
            typeOfGun = 1;
            numberOfGun = 1;
        }

        string spriteAddress = "Assets/Sprites/Weapons/Guns/" + getGunAdress(typeOfGun, numberOfGun) + ".png";
        Debug.Log("change to:" + typeOfGun.ToString() + "---" + numberOfGun.ToString());
        // Debug.Log(ListOfWeapon.selectedWeapon);
        gameObject.GetComponentInChildren<GunController>().changeSprite(spriteAddress);
    }

    string getGunAdress(int typeOfGun, int numberOfGun)
    {
        switch (typeOfGun)
        {
            case 1: ListOfWeapon.selectedWeapon = ListOfWeapon.handguns[numberOfGun - 1]; return "handguns/" + ListOfWeapon.handguns[numberOfGun - 1].gunName;
            case 2: ListOfWeapon.selectedWeapon = ListOfWeapon.rifles[numberOfGun - 1]; return "rifles/" + ListOfWeapon.rifles[numberOfGun - 1].gunName;
            case 3: ListOfWeapon.selectedWeapon = ListOfWeapon.shotguns[numberOfGun - 1]; return "shotguns/" + ListOfWeapon.shotguns[numberOfGun - 1].gunName;
            case 4: ListOfWeapon.selectedWeapon = ListOfWeapon.snipers[numberOfGun - 1]; return "snipers/" + ListOfWeapon.snipers[numberOfGun - 1].gunName;
            default: Debug.Log("err rand"); ListOfWeapon.selectedWeapon = ListOfWeapon.handguns[0]; return "handguns/" + ListOfWeapon.handguns[0].gunName;
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        gameObject.GetComponentInChildren<GunController>().playSound(audioClip);
    }

    public void setColor(Color newColor)
    {
        playerColor = newColor;
        if (playerSprite != null)
        {
            playerSprite.color = playerColor;
        }
    }

    public void setHat(Sprite newHat)
    {
        playerHat = newHat;
        hatHolder.sprite = playerHat;
    }
}
