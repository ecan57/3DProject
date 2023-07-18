using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressBarChangesEventArgs> OnProgressBarChanged;
    public class OnProgressBarChangesEventArgs : EventArgs
    {
        public float progressNormalized;
    }

    [SerializeField] CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject()) //elinde obje varsa
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))//tezgaha koymadan önce obje kesilebilir mi deðil mi bunu kontrol ediyor HasRecipe metodu bool olduðu için true ya da false döndürecek
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);//playerin eline aldýðý(GetKitchenObject) kitchenobjeyi etkileþime girdiði countera býrak(SetKitchenObjectParent(this))
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressBarChanged?.Invoke(this, new OnProgressBarChangesEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                }
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
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            cuttingProgress++;

            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            OnProgressBarChanged?.Invoke(this, new OnProgressBarChangesEventArgs
            {
                progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
            });

            if(cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());

                GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            }
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);

        if (cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else 
        { 
            return null; 
        }
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }
}
