using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    PlayerController player;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 10)
        {
            Destroy(gameObject);
            //Debug.Log("heart touched");

            player = other.gameObject.GetComponent<PlayerController>();
            player.ChangeHealth(1);
        }
    }
}
