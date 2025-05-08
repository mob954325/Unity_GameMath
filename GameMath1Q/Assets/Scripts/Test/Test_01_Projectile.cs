using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_01_Projectile : TestBase
{
    public GameObject spawnPoint;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Debug.Log("1: Spawn projectileSin");
        GameObject projectile = ObjectManager.Instance.SpawnProjectile(ProjectileType.Sin);
        projectile.transform.position = spawnPoint.transform.position;
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Debug.Log("2: Spawn projectileLerp");
        GameObject projectile = ObjectManager.Instance.SpawnProjectile(ProjectileType.Lerp);
        projectile.transform.position = spawnPoint.transform.position;
    }

    protected override void OnTest3(InputAction.CallbackContext context)
    {
        Debug.Log("3: Spawn projectilePolar");
        GameObject projectile = ObjectManager.Instance.SpawnProjectile(ProjectileType.Spiral);
        projectile.transform.position = spawnPoint.transform.position;
    }

    protected override void OnTest4(InputAction.CallbackContext context)
    {
        Debug.Log("4: Spawn projectileEight");
        GameObject projectile = ObjectManager.Instance.SpawnProjectile(ProjectileType.Eight);
        projectile.transform.position = spawnPoint.transform.position;
    }
}
