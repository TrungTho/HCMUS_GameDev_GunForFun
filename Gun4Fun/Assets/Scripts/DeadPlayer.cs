using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPlayer : MonoBehaviour
{
    public MapManager mapManager;

    // Start is called before the first frame update
    void Start()
    {
        mapManager = FindObjectOfType<MapManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //character felt to valley
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            mapManager.RespawnPlayer();
            mapManager.GameFinish(1, null);
        }
        else
        {
            mapManager.RespawnEnemy(other.GetComponentInParent<EnemyBot>());
            mapManager.GameFinish(0, other.GetComponentInParent<EnemyBot>());
        }
    }
}
