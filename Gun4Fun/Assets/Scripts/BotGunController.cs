using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class BotGunController : MonoBehaviour
{
    public AudioClip audioShoot;
    public AudioClip audioReload;
    AudioSource audioSource;

    public int typeOfGun = 1;
    public int numOfgun = 2;
    private Weapon originalWeapon;

    public GameObject bulletPrefab;
    public GameObject minePrefab;
    private int numOfMines = 3;
    public Transform firePoint;
    [SerializeField] private float power;
    [Range(5f, 50f)] [SerializeField] private float howFar;

    float rateOfFire;
    public int numberOfAmmo;
    float reloadTime = -1;

    public TextMeshProUGUI GunStats;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        originalWeapon = ListOfWeapon.getGun(typeOfGun, numOfgun);
        // Debug.Log("enemy weapon" + weapon.gunName);
        resetStats();
    }

    void resetStats()
    {
        power = originalWeapon.power;
        howFar = originalWeapon.howFar;
        rateOfFire = originalWeapon.rateOfFire;
        reloadTime = -1;
        numberOfAmmo = originalWeapon.numberOfAmmo;
    }

    public string getWeaponName()
    {
        return originalWeapon.gunName;
    }
    public void Fire()
    {
        if (numberOfAmmo > 0)
        {
            // Debug.Log("fire" + reloadTime);

            if (reloadTime < 0)
            {
                // Shoot
                if (rateOfFire <= 0)
                {
                    Shoot();
                    rateOfFire = originalWeapon.rateOfFire;
                    numberOfAmmo--;
                }
                else
                {
                    rateOfFire -= Time.deltaTime;
                }
            }
            else
            {
                reloadTime -= Time.deltaTime;
            }
        }
        else //out of armo
        {
            playSound(audioReload);
            // if (weapon.typeOfGun != 1) //drop gun, back to default gun
            // {
            //     GetComponentInParent<EnemyBot>().ChangeWeapon(true);
            //     numberOfAmmo = weapon.numberOfAmmo;
            //     reloadTime = weapon.reloadTime;
            // }
            // else
            {
                numberOfAmmo = originalWeapon.numberOfAmmo;
                reloadTime = originalWeapon.reloadTime;
            }
        }
    }

    public void ThrowMine()
    {
        if (numOfMines > 0)
        {
            DropMine();
            numOfMines--;
        }
    }

    private void DropMine()
    {
        //Create a mine game object and drop
        GameObject m_mine = Instantiate(minePrefab, firePoint.position, firePoint.rotation);
    }

    void Shoot()
    {
        //Create a bullet game object and launch
        GameObject m_bulletPrefab = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = m_bulletPrefab.GetComponent<Bullet>();
        bullet.Launch(power, howFar, firePoint.position);
        playSound(audioShoot);
        // animator.SetTrigger("Launch");
    }

    public void playSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    //------------------- change weapon sprite
    public void changeSprite(string spriteAddress)
    {
        // Debug.Log("gun change sprite func");
        AsyncOperationHandle<Sprite[]> spriteHandle = Addressables.LoadAssetAsync<Sprite[]>(spriteAddress);
        spriteHandle.Completed += LoadSpritesWhenReady; //async function

        //reset stat in uibar to new stats
        resetStats();
    }

    void LoadSpritesWhenReady(AsyncOperationHandle<Sprite[]> handleToCheck)
    {
        if (handleToCheck.Status == AsyncOperationStatus.Succeeded)
        {
            //spriteArray = handleToCheck.Result;
            gameObject.GetComponent<SpriteRenderer>().sprite = handleToCheck.Result[0];
        }
    }

    public void resetMine()
    {
        numOfMines = 3;
    }
}
