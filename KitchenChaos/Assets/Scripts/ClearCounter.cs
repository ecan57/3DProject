using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO kitchenObjectSO;
   
    public override void Interact(Player player)
    {
        if(!HasKitchenObject())
        {
            if(player.HasKitchenObject()) //elinde obje varsa
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);//playerin eline ald���(GetKitchenObject) kitchenobjeyi etkile�ime girdi�i countera b�rak(SetKitchenObjectParent(this))
            }
            else
            {

            }
        }
        else
        {
            if(player.HasKitchenObject())
            {

            }
            else 
            {
                GetKitchenObject().SetKitchenObjectParent(player); //elinde yoksa eline al.
            }
        }
    }
}
