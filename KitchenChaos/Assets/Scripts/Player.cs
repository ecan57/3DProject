using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;
    
    void Start()
    {
        
    }

    void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);

        if(Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if(Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        if(Input.GetKey(KeyCode.D))
        {
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized; //ortalamasýný alýp düzenliyor iki tuþa bastýðýnda düzgn çalýþmasý için

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y); //yukarýda iki eksenli yönü verdik sonra burada üç eksene çevirdik tanýmlamalrýn karþýlýðýný verdik
        
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        transform.forward = Vector3.Slerp(transform.forward, moveDir, rotateSpeed * Time.deltaTime);//scriptin baðlý olduðu objenin transformu //slerp -- smoot olarak
    }
}
