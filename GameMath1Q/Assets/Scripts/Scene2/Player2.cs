using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    private PlayerInputActions actions;

    Vector2 CenterVec = Vector2.zero;
    Vector2 inputVec = Vector2.zero;
    float speed = 11f;
    float radian = 0f;

    public int hp;

    private void Awake()
    {
        CenterVec = transform.position;
        actions = new PlayerInputActions();
    }
    private void OnEnable()
    {
        InputInit();
        hp = 1;
    }

    private void OnDisable()
    {
        InputRelease();
    }

    private void Update()
    {
        radian += inputVec.x * speed * Time.deltaTime;

        float x = Mathf.Cos(radian);
        float y = Mathf.Sin(radian);

        transform.position = CenterVec + new Vector2(x, y);
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
