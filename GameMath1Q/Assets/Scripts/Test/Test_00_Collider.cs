using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_00_Collider : TestBase
{
    public ObjectManager manager;
    public GameObject squarPrefab;
    public Transform spawnPoint;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        manager.AddObjectList(Instantiate(squarPrefab));
    }
}