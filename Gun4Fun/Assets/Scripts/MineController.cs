using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{

    public AudioClip clipExplose;
    AudioSource audioSource;
    float delayTime = 2.0f;
    float readyExploseTime = 1.0f;
    float exploseTime = 0.5f;

    //2 component to perform explosion
    CircleCollider2D circleCollider;
    PointEffector2D pointEffector;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        circleCollider = gameObject.GetComponent<CircleCollider2D>();
        pointEffector = gameObject.GetComponent<PointEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (delayTime > 0)
        {
            delayTime -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("isReadyExplose", true);
            if (readyExploseTime > 0)
            {
                readyExploseTime -= Time.deltaTime;
            }
            else
            {
                animator.SetBool("isExplose", true);
                if (exploseTime > 0)
                {
                    exploseTime -= Time.deltaTime;
                    playSound(clipExplose);
                    circleCollider.enabled = true;
                    pointEffector.enabled = true;
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void playSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
