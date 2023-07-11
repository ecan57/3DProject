using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] ClearCounter clearCounter;
    [SerializeField] GameObject visualGameObject;
    void Start()
    {
        Player.Instance.OnSelectedCounterChangeEvent += Instance_OnSelectedCounterChangeEvent;

    }

    private void Instance_OnSelectedCounterChangeEvent(object sender, Player.OnSelectedCounterChangeEventArg e)
    {
        if(e.selectedCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
