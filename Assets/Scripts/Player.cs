using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public float timerBateria;
    public float bateria  = 100;

    //interacao com itens
    public GameObject item;
    public bool podePegar;
    public bool podeDropar;
    public bool peguei;
    public bool dropei;
    public float timer;
    public float cooldownItens = 2;

    //movimentacao
    public float velocidade = 10;
    public float horizontal; //input pra computador
    public float vertical; //input pra computador
    public Rigidbody rb;
    //swaipe
    public Vector2 startTouchPos;
    public Vector2 endTouchPos;
    public Vector3 dir;

    void Start()
    {
        //pegar o rigidbody do proprio objeto  //obs: a gente pode colocar pelo inspector tambem
        rb.GetComponent<Rigidbody>();
    }


    void Update()
    {

        //logica pra coleta de itens
        // peguei o item
        // cooldown por 2 segundos
        // se esse cooldown chegar a 2, posso dropar
        // dropei o item
        // cooldown por 2 segundos
        // se esse cooldown chegar a 2 de novo, posso pegar
        timerBateria += Time.deltaTime;
        if (timerBateria > 2) 
        {
            bateria--;
            timerBateria = 0;
        }
        FimDaBateria();

        if (peguei == true)
        {
            timer += Time.deltaTime;

            if (timer >= cooldownItens)
            {
                podeDropar = true;
                timer = 0;
                peguei = false;
            }
        }

        if (dropei == true)
        {
            timer += Time.deltaTime;
            if (timer >= cooldownItens)
            {
                podePegar = true;
                timer = 0;
                dropei = false;
            }
        }

        //metodo pra mover no teclado
        /* 
         Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
         if (dir.x != 0) dir.z = 0;
         if (dir.magnitude != 0) {
             Quaternion olhandoPara = Quaternion.LookRotation(dir);
             transform.rotation = olhandoPara;
         }*/


        if (Input.touchCount > 0)
        {
            Touch firstTouch = Input.GetTouch(0);

            if (firstTouch.phase == TouchPhase.Began)
            {
                startTouchPos = firstTouch.position;

            }

            if (firstTouch.phase == TouchPhase.Ended)
            {
                endTouchPos = firstTouch.position;

                if (Mathf.Abs(endTouchPos.x - startTouchPos.x) > Mathf.Abs(endTouchPos.y - startTouchPos.y))
                {

                    if (startTouchPos.x > endTouchPos.x)
                    {
                        dir = new Vector3(-1, 0, 0);
                    }
                    else
                    {
                        dir = new Vector3(1, 0, 0);
                    }
                }
                else
                {
                    if (startTouchPos.y > endTouchPos.y)
                    {
                        dir = new Vector3(0, 0, -1);

                    }
                    else
                    {
                        dir = new Vector3(0, 0, 1);
                    }

                }

            }

            /* if (dir.x != 0)
             {
                 dir.z = 0;
             }*/
            if (dir.magnitude != 0)
            {
                Quaternion olhandoPara = Quaternion.LookRotation(dir);
                transform.rotation = olhandoPara;
            }
        }


    }

    private void FixedUpdate()
    {
        //movimentacao constante pra frente
        rb.AddForce(transform.forward * velocidade * 10f * Time.deltaTime, ForceMode.VelocityChange);


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            if (podePegar == true)
            {
                //aqui o item fica preso ao player
                item = other.gameObject;
                item.transform.parent = transform;
                item.transform.position = transform.position + new Vector3(0, 2f, 0);
                peguei = true;
                podePegar = false;
                bateria = bateria - 5;
                Debug.Log($"sua bateria esta em {bateria}%");

            }


        }

        if (other.CompareTag("CameraMata"))
        {

            GameController.instance.derrota();
        }

            if (other.CompareTag("Drop"))
        {
            if (podeDropar == true)
            {
                //aqui o item fica preso ao local de entrega
                item.transform.position = other.transform.position + new Vector3(0,1,0);
                item.transform.parent = other.transform;
                dropei = true;
                podeDropar = false;

            }


        }
        if (other.CompareTag("Bateria"))
        {
            
            bateria += 100; 
            if (bateria > 100)
            {
                bateria = 100;
            }
            Destroy(other.gameObject);
        }
    }
    public void FimDaBateria() 
    {
        if (bateria <= 0)
        {
            GameController.instance.derrota();
        }
    }
}
