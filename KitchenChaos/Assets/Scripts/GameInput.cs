using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector2 GetMovementVectorNormalize()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>(); // PlayerInputActions olu�turdu�umuz i�in a�a��daki kodlara gerek kalmad�

        //Vector2 inputVector = new Vector2(0, 0);

        //if (Input.GetKey(KeyCode.W))
        //{
        //    inputVector.y = +1;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    inputVector.y = -1;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    inputVector.x = -1;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    inputVector.x = +1;
        //}

        inputVector = inputVector.normalized; //ortalamas�n� al�p d�zenliyor iki tu�a bast���nda d�zgn �al��mas� i�in

        return inputVector;
    }
}
