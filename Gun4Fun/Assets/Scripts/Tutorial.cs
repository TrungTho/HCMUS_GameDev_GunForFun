using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject nextGoal;
    [SerializeField] private int goalHit = 3;

    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerController player = col.GetComponent<PlayerController>();
        if (player != null)
        {
            gameObject.SetActive(false);
            nextGoal.SetActive(true);
        }

        Bullet bullet = col.GetComponent<Bullet>();
        if (bullet != null)
        {
            goalHit -= 1;
            if (goalHit == 0)
            {
                gameObject.SetActive(false);
                nextGoal.SetActive(true);
            }
        }
    }
}
