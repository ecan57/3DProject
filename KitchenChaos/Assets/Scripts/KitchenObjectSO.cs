using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "KitchenObject")] //assette sa� t�klay�p crate deyince KitchenObject ��k�yor object olu�turabiliyoruz
public class KitchenObjectSO : ScriptableObject //i�eriisnde veri tutabilen bile�en
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
