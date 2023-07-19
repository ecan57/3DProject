using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    [SerializeField] GameObject stoveOnGameObject, particlesGameObject;

    void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChangeEventArgs e)
    {
        bool showVisual = e.state == StoveCounter.State.Burned || e.state == StoveCounter.State.Idle;
        stoveOnGameObject.SetActive(!showVisual); ;
        particlesGameObject.SetActive(!showVisual);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
