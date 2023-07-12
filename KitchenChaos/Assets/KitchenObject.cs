using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectSO kitchenObjectSO; //counterler�n �zerinde spwn olan so lar�n ne oldu�u belli olsun
    [SerializeField] ClearCounter clearCounter;

    public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

    public void SetClearCounter(ClearCounter clearCounter) 
    { 
        if(this.clearCounter != null)
        {
            this.clearCounter.ClearKitchenObject();
        }

        this.clearCounter = clearCounter;

        if(clearCounter.HasKitchenObject())
        {
            Debug.Log("Counter alresdy has a KitchenObject!");
        }

        clearCounter.SetKitchenObject(this);

        transform.parent =clearCounter.GetKtchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public ClearCounter GetClearCounter() { return this.clearCounter; }
}
