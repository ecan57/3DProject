using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject()) //elinde obje varsa
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);//playerin eline aldýðý(GetKitchenObject) kitchenobjeyi etkileþime girdiði countera býrak(SetKitchenObjectParent(this))
            }
            else
            {

            }
        }
        else
        {
            if (player.HasKitchenObject())
            {

            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player); //elinde yoksa eline al.
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if(!HasKitchenObject())
        {

        }
    }
}
