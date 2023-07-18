using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;
    
    [SerializeField] KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player); //counter objeyi direkt playerdan ald��� i�in player dedik
        }
    }
}
