using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEditor.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidade = 10;
    public float horizontal;
    public float vertical;
    public Rigidbody rb;
    public GameObject item;

    public bool podePegar;
    public bool podeDropar;
    public bool peguei;
    public bool dropei;
    public float timer;
    public float cooldownItens = 2;


    void Start()
    {
        rb.GetComponent<Rigidbody>();
    }


    void Update()
    {
        // peguei o item
        // cooldown por 2 segundos
        // se esse cooldown chegar a 2, posso dropar
        // dropei o item
        // cooldown por 2 segundos
        // se esse cooldown chegar a 2 de novo, posso pegar
        

        if(peguei == true) 
        {
            timer += Time.deltaTime;

            if(timer >= cooldownItens)
            {
                podeDropar = true;
                timer = 0;
                peguei = false;
            }
        }

        if(dropei == true)
        {
            timer += Time.deltaTime;
            if (timer >= cooldownItens)
            {
                podePegar = true;
                timer = 0;
                dropei = false;
            }
        }

       // Debug.Log(rb.velocity);

        if (horizontal == 0)
        {
            vertical = Input.GetAxisRaw("Vertical");

        }

        if (vertical == 0)
        {
            horizontal = Input.GetAxisRaw("Horizontal");

        }


        if (horizontal != 0 || vertical != 0)
        {

            Vector3 eixos = new Vector3(horizontal, 0, vertical);
            Quaternion olhandoPara = Quaternion.LookRotation(eixos);
            transform.rotation = olhandoPara;
        }


        //codigo pra jogar item pra frente //ignora
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (item != null)
            {
                item.transform.parent = null;

                if (transform.rotation.y > 0.5f && transform.rotation.y < 0.8f)
                {
                    item.transform.position = transform.position + new Vector3(4f, 0, 0);

                }
                else if (transform.rotation.y > -1f && transform.rotation.y < -0)
                {
                    item.transform.position = transform.position + new Vector3(-4f, 0, 0);
                }
               
                else if (transform.rotation.y == 0)
                {
                    item.transform.position = transform.position + new Vector3(0, 0, 4f);
                }
                else
                {
                    item.transform.position = transform.position + new Vector3(0, 0, -4f);
                }

                item = null;
            }
         
        }*/

    }

    private void FixedUpdate()
    {
        rb.AddForce(transform.forward * velocidade * 10f * Time.deltaTime, ForceMode.Impulse);


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            if(podePegar == true)
            {
                item = other.gameObject;
                item.transform.parent = transform;
                item.transform.position = transform.position + new Vector3(0, 2f, 0);
                peguei = true;
                podePegar = false;

            }
                

        }



        if (other.CompareTag("Drop"))
        {
            if (podeDropar == true)
            {
                item.transform.position = other.transform.position;
                item.transform.parent = null;
                dropei = true;
                podeDropar = false;

            }
           
       
        }

    }
}
