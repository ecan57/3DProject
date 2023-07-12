using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Object", menuName = "KitchenObject")] //assette sað týklayýp crate deyince KitchenObject çýkýyor object oluþturabiliyoruz
public class KitchenObjectSO : ScriptableObject //içeriisnde veri tutabilen bileþen
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
