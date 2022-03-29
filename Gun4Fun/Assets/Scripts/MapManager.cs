using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    //set of position to drop somethings
    public DroppingPoint droppingPoint1;
    public DroppingPoint droppingPoint2;
    public DroppingPoint droppingPoint3;
    //somethings that will be dropped 
    public PlayerController playerController;
    public EnemyBot enemy1;
    public EnemyBot enemy2;
    public GameObject healthCollectible;
    public GameObject packPrefab;
    public float timeToDrop = 10.0f;
    public float timeToDropHeart = 50.0f;
    //private Crate crateCollectible;
    //life manage to bind data to UI
    private LifeManager lifeManager;

    //menu for win or lose
    public GameObject winMenu;
    public GameObject loseMenu;

    // Start is called before the first frame update
    void Start()
    {
        //danger: scene can have more than one player/life stats
        // playerController = FindObjectOfType<PlayerController>();
        lifeManager = FindObjectOfType<LifeManager>();

        // Debug.Log(playerController.transform.position);
        // Debug.Log(droppingPoint1.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //auto drop pack
        if (timeToDrop < 0)
        {
            //guarantee that screen only have 1 heart at the same time
            if (FindObjectOfType<PackCollectible>() == null)
            {
                DropPack();
                timeToDrop = 10.0f;
            }
        }
        else
        {
            timeToDrop -= Time.deltaTime;
        }

        //auto drop heart
        if (timeToDropHeart < 0)
        {
            //guarantee that screen only have 1 heart at the same time
            if (FindObjectOfType<HealthCollectible>() == null)
            {
                DropHeart();
                timeToDrop = 50.0f;
            }
        }
        else
        {
            timeToDrop -= Time.deltaTime;
        }


        // Drop heart
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            //guarantee that screen only have 1 heart at the same time
            if (FindObjectOfType<HealthCollectible>() == null)
            {
                DropHeart();
            }
        }

        // Drop pack
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            //guarantee that screen only have 1 heart at the same time
            if (FindObjectOfType<PackCollectible>() == null)
            {
                DropPack();
            }
        }
    }

    private void DropPack()
    {
        GameObject packObject = Instantiate(packPrefab);
        packObject.transform.position = RandDroppingPoint().transform.position;
    }

    private void DropHeart()
    {
        GameObject heartObject = Instantiate(healthCollectible);
        heartObject.transform.position = RandDroppingPoint().transform.position;
    }

    public void RespawnPlayer()
    {
        // Debug.Log("Drop again~~~~~~" + RandDroppingPoint());
        //playerController.ChangeHealth(-1);
        playerController.transform.position = RandDroppingPoint().transform.position;

        lifeManager.PlayerSubstractLife(playerController);

        //reset gun to default
        playerController.ChangeWeapon(true);
    }

    public void RespawnEnemy(EnemyBot bot)
    {
        // Debug.Log("Drop again~~~~~~" + RandDroppingPoint());
        //playerController.ChangeHealth(-1);
        bot.transform.position = RandDroppingPoint().transform.position;

        lifeManager.BotSubstractLife(bot);
    }

    DroppingPoint RandDroppingPoint()
    {
        int pos = Random.Range(1, 4);
        switch (pos)
        {
            case 1: return droppingPoint1;
            case 2: return droppingPoint2;
            case 3: return droppingPoint3;
            default: return droppingPoint1;
        }
    }

    //characterType ==1 -> player, 0 -> enemy
    public void GameFinish(int characterType, EnemyBot bot)
    {
        if (characterType == 1)
        {
            if (playerController.numberOfHealth == 0)
            {
                loseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
        else
        {
            if ((enemy1.numberOfHealth == 0 && enemy2 == null) || (enemy1.numberOfHealth == 0 && enemy2.numberOfHealth == 0))
            {
                winMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
