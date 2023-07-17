using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour, IKitchenObjectParent
{ //ctrl+r+r kelimeyi se�ince scripteki hepsini de�i�tiriyor.
    public static Player Instance { get; private set; } // d��ar�dan instance olarak �ekip i�inden verdi�imiz verileri d��ardan de�i�tirilemez oldu�u s�yl�yor. Get ile d��ar�dan �ekebiliyoruz ama set private oldu�u i�in de�i�tirmeye izin vermiyor

    public event EventHandler<OnSelectedCounterChangeEventArg> OnSelectedCounterChangeEvent;
    public class OnSelectedCounterChangeEventArg : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    bool isWalking;

    private Vector3 lastInteractions;

    private BaseCounter selectedCounter;

    [SerializeField] LayerMask countersLayerMask;

    [SerializeField] GameInput gameInput;

    [SerializeField] Transform kitchenObjectHoldPoint;

    [SerializeField] KitchenObject kitchenObject;

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
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
        //event manager olu�turup i�ine �yeler eklersek olu�an ble�enleri de public sttic yaparsak tan�mlamalara da gerek kalmaz
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e) //object sender kendsini g�nderece�ini ifade ediyor ��mk� ilk yerde this demi�tik
    {
        if(selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalize();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); //yukar�da iki eksenli y�n� verdik sonra burada �� eksene �evirdik tan�mlamalr�n kar��l���n� verdik

        float moveDistance = moveSpeed * Time.deltaTime; //mesafe �l��s� verdik
        float playerRadius = 0.7f; //playerim�z�n �evresindeki kaps�l�n boyutu
        float playerHeight = 2f; //y�ksekli�i playerdaki

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
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

        /*if(Physics.Raycast(transform.position, moveDir, out RaycastHit raycastHit, interactDistance))*/ //��kan sonucun ��kt�s�n� al�yoruz raycasta �arpan�n transformu
        if (Physics.Raycast(transform.position, lastInteractions, out RaycastHit raycastHit, interactDistance, countersLayerMask)) //movedir yerine lastinteraction yazpt�k
        {
            if(raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if(baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
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

    public Transform GetKtchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
