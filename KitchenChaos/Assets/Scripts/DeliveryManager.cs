using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeComplated;
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; } //get: d��ar�dan verileri okuyay�m, private set: d�zenleyememyiyim
    [SerializeField] private RecipeListSO recipeListSO;

    [SerializeField] private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;

    private float waitingRecipeMax = 4f;

    private void Awake()
    {
        Instance = this; //singleton yapt�k b�yle de kullan�labilirmi�
        waitingRecipeSOList = new List<RecipeSO>(); //Listemizi olu�turduk
    }

    void Update()
    {//Gelen sipari�ler listeleniyor verdi�imiz s�relere g�re
        spawnRecipeTimer -= Time.deltaTime;

        if(spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if(waitingRecipeSOList.Count < waitingRecipeMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliveryRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentMatchesRecipe = true;

                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingradientFound = false;

                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingradientFound = true; 
                            break; //arad���m�z� bulunca d�ng�den ��k
                        }
                    }
                    if(!ingradientFound)
                    {
                        plateContentMatchesRecipe = false;
                    }
                }
                if(plateContentMatchesRecipe)
                {
                    //Debug.Log("Player Delivered the correct recipe.");
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeComplated?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        Debug.Log("Player did not delivered the correct recipe!");
    }
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
}
