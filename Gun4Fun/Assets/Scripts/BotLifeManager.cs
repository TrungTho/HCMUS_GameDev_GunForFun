using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BotLifeManager : MonoBehaviour
{
    public EnemyBot enemy;
    public TextMeshProUGUI playerLifes;
    public TextMeshProUGUI gunStats;
    public BotGunController gunController;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerLifes.text = enemy.numberOfHealth.ToString();
        gunStats.text = gunController.getWeaponName() + " " + gunController.numberOfAmmo;
    }

    public void BotAddLife(EnemyBot bot)
    {
        bot.ChangeHealth(1);
    }

    public void BotSubstractLife(EnemyBot bot)
    {
        bot.ChangeHealth(-1);
    }

}
