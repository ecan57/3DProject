using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Sound
    public static Action OnRecipeSuccess;
    public static Func<int> OnRecipeFailed;
    #endregion
}
