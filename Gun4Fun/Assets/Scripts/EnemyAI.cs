using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    [SerializeField] private Transform groundCheck;
    private float groundedRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    public GameObject Holding;

    public Transform target;
    public float speed = 200f;
    public float nextWayPointDistance = 2f;
    public float jumpPower = 200f;
    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;
    bool isGrounded = true;
    bool canDoubleJump = true;
    public float jumpCheckOffset = 0.1f;
    public float jumpNodeHeightRequirement = 0.8f;

    public float shootingHeightRange = 0.5f;
    bool facingLeft = true;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
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

    // Update is called once per frame
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

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        Vector2 targetVector = (Vector2)target.position - rb.position;
        /*Debug.Log("Direction" + targetVector);*/
        if (targetVector.x < 0 && !facingLeft) {
            Flip();
        }
        else if (targetVector.x >= 0 && facingLeft) {
            Flip();
        }

        if (distance < nextWayPointDistance) {
            currentWayPoint++;
        }

        /*float maxDistance = GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset;
        isGrounded = Physics2D.Raycast(transform.position + GetComponent<Collider2D>().bounds.extents, -Vector3.up, jumpCheckOffset);
        Debug.DrawRay(transform.position + GetComponent<Collider2D>().bounds.extents, -Vector3.up * jumpCheckOffset, color: Color.red);
        */
        Debug.Log(isGrounded);
        /*Debug.Log(GetComponent<Collider2D>().bounds.extents.y);*/


        /*bool wasGrounded = isGrounded;


        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++) {
            if (colliders[i].gameObject != gameObject) {
                isGrounded = true;
                canDoubleJump = true;
                if (!wasGrounded) {
                    *//*OnLandEvent.Invoke();*//*
                }
            }
        }*/

        /*if ((direction.y > jumpNodeHeightRequirement) && isGrounded) {
            rb.AddForce(Vector2.up * jumpPower);
            isGrounded = false;
        }
        else if ((direction.y > jumpNodeHeightRequirement) && canDoubleJump) {
            rb.AddForce(Vector2.up * jumpPower);
            canDoubleJump = false;
        }*/
    }

    void Update() {
        if ((transform.position.y <= target.position.y + shootingHeightRange) &&
                (transform.position.y >= target.position.y - shootingHeightRange)) {
            shoot();
        }
    }

    void Flip() {
        facingLeft = !facingLeft;
        transform.Rotate(0f, 180f, 0f);
    }

    void shoot() {
        GunController gun = Holding.GetComponent<GunController>();
        gun.Fire();
    }
}
