using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed; //Event kullanýmý
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);//invoke ateþliyooruz
    }

    public Vector2 GetMovementVectorNormalize()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); // PlayerInputActions oluþturduðumuz için aþaðýdaki kodlara gerek kalmadý

        inputVector = inputVector.normalized; //ortalamasýný alýp düzenliyor iki tuþa bastýðýnda düzgn çalýþmasý için

        return inputVector;
    }
}
