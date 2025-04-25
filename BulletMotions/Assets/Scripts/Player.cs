using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;

    private float bulletSpwanTimer = 0f;
    private float maxBulletSpawnTime = 0.3f;

    void Update()
    {
        bulletSpwanTimer += Time.deltaTime;

        if(bullet != null)
        {
            if(bulletSpwanTimer > maxBulletSpawnTime)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                bulletSpwanTimer = 0f;
            }    
        }
    }
}
