using Pathfinding;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBot : MonoBehaviour {
    //Movement speed
    [SerializeField] private float speed = 4.0f;
    //Jump strength
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

    private Animator animate;

    public float delayTime;

    ///-------------------Health, pickup event
    public int numberOfHealth = 5;

    //-------------------- Movement
    //To check if player facing right
    //To check if player is on ground
    private bool grounded = true;

    //Radius of circle to check for ground
    private float groundedRadius = 0.1f;

    //support vector for movement
    private Vector2 direction;
    private Vector3 targetVelocity; //target to reach
    private Vector3 currentVelocity = Vector3.zero; //current velocity in function progress

    Seeker seeker;
    Rigidbody2D rb;

    public Transform target;
    public float nextWayPointDistance = 2f;
    Path path;
    int currentWayPoint = 0;
    public float jumpCheckOffset = 0.1f;
    public float jumpNodeHeightRequirement = 0.8f;
    private bool reachedEndOfPath;
    private float dodgePeriod = 2f;
    private float counter;

    public float shootingHeightRange = 0.5f;
    bool facingRight = true;


    void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        counter = dodgePeriod;
        InvokeRepeating("UpdatePath", 0f, .5f);
        animate = GetComponent<Animator>();
    }

    void UpdatePath() {
        if (seeker.IsDone()) {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWayPoint = 0;
        }
    }

    void Update() {
        if ((transform.position.y <= target.position.y + shootingHeightRange) &&
        (transform.position.y >= target.position.y - shootingHeightRange)) {
            gameObject.GetComponentInChildren<BotGunController>().Fire();
            //OnPathComplete(path);
            if (counter >= 0) {
                counter -= Time.deltaTime;
            }
            else {
                if (grounded) {
                    grounded = false;
                    rb.velocity = Vector2.up * 10f;
                    animate.SetBool("IsJumping", true);

                }
                counter = dodgePeriod;
            }
        }
        //========movement===========
        direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;

        Vector2 targetVector = (Vector2)target.position - rb.position;
        //Determine player facing direction
        if (targetVector.x < 0 && facingRight) {
            Flip();
        }
        else if (targetVector.x >= 0 && !facingRight) {
            Flip();
        }


    }

    //Change player position
    void FixedUpdate() {
        if (path == null) {
            return;
        }

        if (currentWayPoint >= path.vectorPath.Count) {
            reachedEndOfPath = true;
            return;
        }
        else {
            reachedEndOfPath = false;
        }

        targetVelocity = new Vector2(direction.x * speed, rb.velocity.y);
        animate.SetFloat("Horizontal", Mathf.Abs(direction.x));
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);

        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject) {
                grounded = true;
                if (!wasGrounded) {
                    OnLandEvent.Invoke();
                }
            }
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);


        //Jumping
        if (direction.y > jumpNodeHeightRequirement) {
            if (grounded) {
                grounded = false;
                rb.velocity = Vector2.up * 10f;
                animate.SetBool("IsJumping", true);
            }
        }

        if (distance < nextWayPointDistance) {
            currentWayPoint++;
        }
    }

    public void OnLanding() {
        animate.SetBool("IsJumping", false);
    }

    //Flipping player facing direction
    private void Flip() {
        facingRight = !facingRight;

        //Rotate the player's vertical axis by 180 degree.
        transform.Rotate(0f, 180f, 0f);
    }

    public void ChangeHealth(int amount) {
        if (amount < 0) {
            numberOfHealth--;
        }
        else {
            numberOfHealth++;
        }
    }

    // public void ChangeWeapon(bool isDefault)
    // {
    //     int typeOfGun;
    //     int numberOfGun;

    //     if (isDefault == false)
    //     {
    //         //random and get data to gun
    //         typeOfGun = Random.Range(2, 5);
    //         numberOfGun = Random.Range(1, 3); // increase later, max maybe 10??
    //     }
    //     else //back to default gun, do not random
    //     {
    //         typeOfGun = 1;
    //         numberOfGun = 1;
    //     }

    //     string spriteAddress = "Assets/Sprites/Weapons/Guns/" + getGunAdress(typeOfGun, numberOfGun) + ".png";
    //     gameObject.GetComponentInChildren<GunController>().changeSprite(spriteAddress);
    // }

    // string getGunAdress(int typeOfGun, int numberOfGun)
    // {
    //     switch (typeOfGun)
    //     {
    //         case 1: ListOfWeapon.botSelectedWeapon = ListOfWeapon.handguns[numberOfGun - 1]; return "handguns/" + ListOfWeapon.handguns[numberOfGun - 1].gunName;
    //         case 2: ListOfWeapon.botSelectedWeapon = ListOfWeapon.rifles[numberOfGun - 1]; return "rifles/" + ListOfWeapon.rifles[numberOfGun - 1].gunName;
    //         case 3: ListOfWeapon.botSelectedWeapon = ListOfWeapon.shotguns[numberOfGun - 1]; return "shotguns/" + ListOfWeapon.shotguns[numberOfGun - 1].gunName;
    //         case 4: ListOfWeapon.botSelectedWeapon = ListOfWeapon.snipers[numberOfGun - 1]; return "snipers/" + ListOfWeapon.snipers[numberOfGun - 1].gunName;
    //         default: Debug.Log("err rand"); ListOfWeapon.botSelectedWeapon = ListOfWeapon.handguns[0]; return "handguns/" + ListOfWeapon.handguns[0].gunName;
    //     }
    // }

    public void PlaySound(AudioClip audioClip) {
        gameObject.GetComponentInChildren<GunController>().playSound(audioClip);
    }
}
