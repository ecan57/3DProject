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

        playerInputActions.Player.Interact.performed += Interact_performed; //Event kullan�m�
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);//invoke ate�liyooruz
    }

    public Vector2 GetMovementVectorNormalize()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); // PlayerInputActions olu�turdu�umuz i�in a�a��daki kodlara gerek kalmad�

        inputVector = inputVector.normalized; //ortalamas�n� al�p d�zenliyor iki tu�a bast���nda d�zgn �al��mas� i�in

        return inputVector;
    }
}
