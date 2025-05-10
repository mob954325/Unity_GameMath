using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputActions actions;
    private Vector2 inputVec = Vector2.zero;
    private float speed = 5f;

    public int hp = 5;
    public float hitDelay = 0f;
    public float maxHitDelay => 1f;

    private void Awake()
    {
        actions = new PlayerInputActions();        
    }

    private void OnEnable()
    {
        InputInit();
        hp = 10;
    }

    private void OnDisable()
    {
        InputRelease();
    }

    void Update()
    {
        if(hitDelay >= 0f)
        {
            hitDelay -= Time.deltaTime;
        }

        transform.Translate(inputVec * speed * Time.deltaTime);
    }

    #region Input
    private void InputInit()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnMoveInput;
        actions.Player.Move.canceled += OnMoveInput;
    }

    private void InputRelease()
    {
        actions.Player.Move.canceled -= OnMoveInput;
        actions.Player.Move.performed -= OnMoveInput;
        actions.Player.Disable();
    }

    private void OnMoveInput(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        inputVec = obj.ReadValue<Vector2>();
    }

    #endregion
}
