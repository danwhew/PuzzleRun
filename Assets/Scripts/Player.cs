using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEditor.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 10;
    public float horizontal;
    public float vertical;

    public Rigidbody rb;

    void Start()
    {
        rb.GetComponent<Rigidbody>();  
    }


    void Update()
    {
       

        if(horizontal == 0)
        {
        vertical = Input.GetAxisRaw("Vertical");

        }

        if(vertical == 0)
        {
        horizontal = Input.GetAxisRaw("Horizontal");

        }


        if (horizontal != 0 || vertical != 0)
        {

        Vector3 eixos = new Vector3(horizontal, 0, vertical);
        Quaternion olhandoPara = Quaternion.LookRotation(eixos);
        transform.rotation = olhandoPara;
        }


        //transform.Translate(0,0,velocidade * Time.deltaTime, Space.Self);
        

    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * velocidade * 10f * Time.deltaTime, ForceMode.Impulse);
        

    }

}
