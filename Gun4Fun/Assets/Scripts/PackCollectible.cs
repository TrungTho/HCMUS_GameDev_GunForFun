using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackCollectible : MonoBehaviour
{
    public AudioClip audioCollectPack;
    PlayerController player;
    EnemyBot enemy;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 10)
        {
            Destroy(gameObject);

            player = other.gameObject.GetComponent<PlayerController>();
            player.PlaySound(audioCollectPack);
            player.ChangeWeapon(false);
        }
        else
        if (other.gameObject.layer == 13)
        {
            Destroy(gameObject);

            enemy = other.gameObject.GetComponent<EnemyBot>();
            enemy.PlaySound(audioCollectPack);
            // enemy.ChangeWeapon(false);
        }
    }
}