using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    bool isWalking;

    [SerializeField] GameInput gameInput;
   
    void Start()
    {
        
    }

    void Update()
    {
       HandleMovement();
        HandleInteractions();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalize();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); //yukar�da iki eksenli y�n� verdik sonra burada �� eksene �evirdik tan�mlamalr�n kar��l���n� verdik

        float moveDistance = moveSpeed * Time.deltaTime; //
        float playerRadius = 0.7f; //playerim�z�n �evresindeki kaps�l�n boyutu
        float playerHeight = 2f; //y�ksekli�i playerdaki

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                //cast te layer da kullan�labilir
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
                else
                {

                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
            isWalking = moveDir != Vector3.zero;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);//scriptin ba�l� oldu�u objenin transformu //slerp -- smoot olarak
        }
        //transform.position += moveDir * moveSpeed * Time.deltaTime; //hemen �stteki ife yazd���m�z i�in gerek kalmad�
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalize();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float interactDistance = 2f;

        if(Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance)) //��kan sonucun ��kt�s�n� al�yoruz raycasta �arpan�n transformu
        {
            Debug.Log(raycastHit.transform);
        }
        else
        {
            Debug.Log("-"); //Cast ler �ok �nemli
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

}
