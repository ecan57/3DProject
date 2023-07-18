using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] CuttingCounter cuttingCounter;
    [SerializeField] Image bar;

    void Start()
    {
        cuttingCounter.OnProgressBarChanged += CuttingCounter_OnProgressBarChanged;
        bar.fillAmount = 0;
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    } 
    
    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void CuttingCounter_OnProgressBarChanged(object sender, CuttingCounter.OnProgressBarChangesEventArgs e)
    {
        bar.fillAmount = e.progressNormalized;

        if (e.progressNormalized == 0 || e.progressNormalized == 1)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }
}
