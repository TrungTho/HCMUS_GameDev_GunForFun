using UnityEngine;

public class Bullet : MonoBehaviour {
    [Range(20f, 100f)] [SerializeField] private float speed = 20f;
    public Rigidbody2D rigidbody2d;
    private float strength;
    private float distance;
    private Vector3 startPoint;

    public void Launch(float power, float howFar, Vector3 pos) {
        //Launch bullet
        rigidbody2d.velocity = transform.right * speed;
        //Load power stat
        strength = power;
        distance = howFar;
        startPoint = pos;
    }

    void Update() {
        //Destroy bullet when going too far away
        if (Vector3.Distance(transform.position, startPoint) > distance) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        Rigidbody2D rgbd2d = col.GetComponent<Rigidbody2D>();
        if (rgbd2d != null) {
            //Push collided object back
            rgbd2d.AddForce(transform.right * strength, 0f);
            Destroy(gameObject);
        }
    }
}
