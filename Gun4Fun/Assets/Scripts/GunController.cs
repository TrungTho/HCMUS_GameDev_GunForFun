using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GunController : MonoBehaviour
{
    public AudioClip normalShoot;
    public AudioClip shotgunShoot;
    public AudioClip sniperShoot;
    public AudioClip audioReload;
    AudioSource audioSource;

    public GameObject bulletPrefab;
    public GameObject minePrefab;
    private int numOfMines = 3;
    public Transform firePoint;
    [SerializeField] private float power = ListOfWeapon.selectedWeapon.power;
    [Range(5f, 50f)] [SerializeField] private float howFar = ListOfWeapon.selectedWeapon.howFar;

    float rateOfFire = ListOfWeapon.selectedWeapon.rateOfFire;
    public int numberOfAmmo = ListOfWeapon.selectedWeapon.numberOfAmmo;
    float reloadTime = -1;

    public TextMeshProUGUI GunStats;

    private CapsuleCollider2D capsuleCollider2D;
    private PointEffector2D pointEffector2D;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        capsuleCollider2D = gameObject.GetComponent<CapsuleCollider2D>();
        pointEffector2D = gameObject.GetComponent<PointEffector2D>();
    }

    public void resetStats()
    {
        power = ListOfWeapon.selectedWeapon.power;
        howFar = ListOfWeapon.selectedWeapon.howFar;
        rateOfFire = ListOfWeapon.selectedWeapon.rateOfFire;
        reloadTime = -1;
        numberOfAmmo = ListOfWeapon.selectedWeapon.numberOfAmmo;
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
                    rateOfFire = ListOfWeapon.selectedWeapon.rateOfFire;
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
            if (ListOfWeapon.selectedWeapon.typeOfGun != 1) //drop gun, back to default gun
            {
                GetComponentInParent<PlayerController>().ChangeWeapon(true);
                numberOfAmmo = ListOfWeapon.selectedWeapon.numberOfAmmo;
                reloadTime = ListOfWeapon.selectedWeapon.reloadTime;
            }
            else
            {
                numberOfAmmo = ListOfWeapon.selectedWeapon.numberOfAmmo;
                reloadTime = ListOfWeapon.selectedWeapon.reloadTime;
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
        // if (ListOfWeapon.selectedWeapon.typeOfGun == 3)
        // {
        //     capsuleCollider2D.enabled = true;
        //     pointEffector2D.enabled = true;
        //     playSound(shotgunShoot);
        // }
        // else
        {
            //Create a bullet game object and launch
            GameObject m_bulletPrefab = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = m_bulletPrefab.GetComponent<Bullet>();
            bullet.Launch(power, howFar, firePoint.position);
            switch (ListOfWeapon.selectedWeapon.typeOfGun)
            {
                case 3: playSound(shotgunShoot); break;
                case 4: playSound(sniperShoot); break;
                default:
                    playSound(normalShoot); break;


            }
        }

        // capsuleCollider2D.enabled = false;
        // pointEffector2D.enabled = false;
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
