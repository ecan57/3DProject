using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{ //ctrl+r+r kelimeyi seçince scripteki hepsini deðiþtiriyor.
    public static Player Instance { get; private set; } // dýþarýdan instance olarak çekip içinden verdiðimiz verileri dýþardan deðiþtirilemez olduðu söylüyor. Get ile dýþarýdan çekebiliyoruz ama set private olduðu için deðiþtirmeye izin vermiyor

    public event EventHandler<OnSelectedCounterChangeEventArg> OnSelectedCounterChangeEvent;
    public class OnSelectedCounterChangeEventArg : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    bool isWalking;

    private Vector3 lastInteractions;

    private ClearCounter selectedCounter;

    [SerializeField] GameInput gameInput;

    [SerializeField] LayerMask countersLayerMask;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one Player Instance");
        }
        Instance = this;
    }
    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        //event manager oluþturup içine üyeler eklersek oluþan bleþenleri de public sttic yaparsak tanýmlamalara da gerek kalmaz
    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) //object sender kendsini göndereceðini ifade ediyor çümkü ilk yerde this demiþtik
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact();
        }

        //Vector2 inputVector = gameInput.GetMovementVectorNormalize();

        //Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //if (moveDir != Vector3.zero)
        //{
        //    lastInteractions = moveDir;
        //}

        //float interactDistance = 2f;

        ///*if(Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance))*/ //çýkan sonucun çýktýsýný alýyoruz raycasta çarpanýn transformu
        //if (Physics.Raycast(transform.position, lastInteractions, out RaycastHit raycastHit, interactDistance, countersLayerMask)) //movedir yerine lastinteraction yazptýk
        //{
        //    if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
        //    {
        //        clearCounter.Interact();
        //        //Debug.Log("Interact");
        //    }
        //    //Debug.Log(raycastHit.transform); //bu ilk yazdýðýmýzdý
        //    //raycast in eski kullanýmý çalýþma sayfasýnda yer alýyor
        //}
        //else
        //{
        //    Debug.Log("-"); //Cast ler çok önemli
        //}
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalize();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); //yukarýda iki eksenli yönü verdik sonra burada üç eksene çevirdik tanýmlamalrýn karþýlýðýný verdik

        float moveDistance = moveSpeed * Time.deltaTime; //mesafe ölçüsü verdik
        float playerRadius = 0.7f; //playerimýzýn çevresindeki kapsülün boyutu
        float playerHeight = 2f; //yüksekliði playerdaki

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
                //cast te layer da kullanýlabilir
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
            transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);//scriptin baðlý olduðu objenin transformu //slerp -- smoot olarak
        }
        //transform.position += moveDir * moveSpeed * Time.deltaTime; //hemen üstteki ife yazdýðýmýz için gerek kalmadý
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalize();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            lastInteractions = moveDir;
        }

        float interactDistance = 2f;

        /*if(Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance))*/ //çýkan sonucun çýktýsýný alýyoruz raycasta çarpanýn transformu
        if (Physics.Raycast(transform.position, lastInteractions, out RaycastHit raycastHit, interactDistance, countersLayerMask)) //movedir yerine lastinteraction yazptýk
        {
            if(raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                if(clearCounter != selectedCounter)
                {
                    SetSelectedCounter(clearCounter);
                    //selectedCounter = clearCounter;
                    //OnSelectedCounterChangeEvent?.Invoke(this, new OnSelectedCounterChangeEventArg
                    //{
                    //    selectedCounter = selectedCounter
                    //});
                }
                //Debug.Log("Interact");
            }
            else
            {
                SetSelectedCounter(null);
                //selectedCounter = null;
                //OnSelectedCounterChangeEvent?.Invoke(this, new OnSelectedCounterChangeEventArg
                //{
                //    selectedCounter = selectedCounter
                //});
            }
            //Debug.Log(raycastHit.transform); //bu ilk yazdýðýmýzdý
            //raycast in eski kullanýmý çalýþma sayfasýnda yer alýyor
        }
        else
        {
            SetSelectedCounter(null);
            //selectedCounter = null;
            //OnSelectedCounterChangeEvent?.Invoke(this, new OnSelectedCounterChangeEventArg
            //{
            //    selectedCounter = selectedCounter
            //});
            //Debug.Log("-"); //Cast ler çok önemli
        }
    }

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChangeEvent?.Invoke(this, new OnSelectedCounterChangeEventArg
        {
            selectedCounter = selectedCounter
        });
    }

    public bool IsWalking()
    {
        return isWalking;
    }

}
