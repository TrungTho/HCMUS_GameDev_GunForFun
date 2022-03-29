using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LifeManager : MonoBehaviour
{
    public PlayerController playerController;
    public TextMeshProUGUI playerLifes;
    public TextMeshProUGUI gunStats;
    public GunController gunController;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerLifes.text = playerController.numberOfHealth.ToString();
        gunStats.text = ListOfWeapon.selectedWeapon.gunName + " " + gunController.numberOfAmmo;
    }

    public void PlayerAddLife(PlayerController player)
    {
        player.ChangeHealth(1);
    }

    public void PlayerSubstractLife(PlayerController player)
    {
        player.ChangeHealth(-1);
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
