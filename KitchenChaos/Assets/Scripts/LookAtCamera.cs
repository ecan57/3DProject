using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{//Cameraya bak�� �ekilleri s�rekli d�z duranlar forward, camaraya g�re de�i�enler lookat
    private enum Mode
    {
        LookAt,
        LookAtInvert,
        CameraForward,
        CameraForwardInvert
    }

    [SerializeField] Mode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt:
                transform.LookAt(Camera.main.transform); //buradaki sar� renk lookat enumdakiyle ayn� de�il
                break;
            case Mode.LookAtInvert:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInvert:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
    }
}
