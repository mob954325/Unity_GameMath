using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_00_Projectile : TestBase
{
    public GameObject spawnPoint;
    public GameObject projectileSin;
    public GameObject projectileBar;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        Debug.Log("1: Spawn projectileSin");
        Instantiate(projectileSin, spawnPoint.transform.position, Quaternion.identity);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        Debug.Log("2: Spawn projectileBar");
        Instantiate(projectileBar, spawnPoint.transform.position, Quaternion.identity);
    }
}
