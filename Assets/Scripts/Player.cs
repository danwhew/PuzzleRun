using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("---Bateria---")]
    //bateria
    public float timerBateria;
    public float bateria = 100;

    [Header("---Itens---")]
    //interacao com itens
    public GameObject item;
    public Transform posItem;
    public GameObject paiInicialDoItem;
    public Vector3 posItemInicial;
    public bool podePegar;
    public bool podeDropar;
    public bool peguei;
    public bool dropei;
    public float timer;
    public float cooldownItens = 2;


    [Header("---Movimentacao---")]
    //movimentacao
    public Animator animator;
    public float velocidade = 10;
    public Rigidbody rb;
    public bool podeAndar = true;
    public float podeAndarTimer;
    //swaipe
    private Vector2 startTouchPos;
    private Vector2 endTouchPos;
    public Vector3 dir;

    [Header("---Audio---")]
    //audio
    public AudioSource audioSource;
    public AudioClip[] audios;

    [Header("---Atordoamento---")]
    //flash feedback
    Renderer playerRenderer;
    Color yellow;
    Color originalColor; // Cor original do jogador
    bool isFlashing = false; // Flag para controlar o estado de piscar
    float flashDuration = 0.2f; // Dura��o do piscar
    float flashTimer = 0f; // Timer para controlar o piscar
    public GameObject estrelas;

    [Header("---Atordoamento---")]
    //game Cheats
    public bool cheat5 = false;

    [Header("---Temp---")]

    public int faseEuTo = 1;
    public int roundEuTo = 1;
    public bool toPeperoni;

    public bool podeGrudar;



    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        // Pegar o Rigidbody do pr�prio objeto
        rb = GetComponent<Rigidbody>();
        // Pegar o componente Renderer do pr�prio objeto
        playerRenderer = GetComponentInChildren<Renderer>();
        // Salvar a cor original do jogador
        originalColor = playerRenderer.material.color;
        estrelas.SetActive(false);
    }


    void Update()
    {

        if (rb.velocity.magnitude > 1f)
        {

            animator.SetBool("bParado", false);
        }
        else
        {
            animator.SetBool("bParado", true);
        }


        // Ativando/desativando o cheat de bateria infinita quando cinco dedos tocarem na tela
        if (Input.touchCount >= 5)
        {
            if (cheat5 == false)
            {
                cheat5 = true;
                bateria = 100;
            }
            else
            {
                cheat5 = false;
            }

        }

        //verificar a todo momento se ele pode coletar ou nao algum item
        ColetaDrop();

        if (cheat5 == false)
        {
            if (podeAndar == false)
            {
                podeAndarTimer += Time.deltaTime;
                if (podeAndarTimer >= 1)
                {
                    estrelas.SetActive(false);
                    podeAndar = true;
                    podeAndarTimer = 0;
                    animator.SetBool("bAtordoado", false);
                }
            }
            if (podeAndar == true)
            {
                movimentacao();
            }

            //logica bateria
            timerBateria += Time.deltaTime;
            if (timerBateria > 0.4f)
            {
                bateria--;
                timerBateria = 0;
            }

        }
        else
        {
            bateria = 100;
            movimentacao();
        }

        FimDaBateria();

        // Atualizar o efeito de piscar, se necessario
        UpdateFlash();

    }

    private void FixedUpdate()
    {
        //movimentacao constante pra frente
        rb.AddForce(transform.forward * velocidade * 10f * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item") || other.CompareTag("ItemErrado"))
        {
            if (podePegar == true)
            {
                audioSource.PlayOneShot(audios[0]);

                if (other.transform.parent.parent != null)
                {
                    paiInicialDoItem = other.transform.parent.parent.gameObject;

                }
                //posItemInicial = other.transform.position;
                item = other.transform.parent.gameObject;
                item.transform.parent = this.gameObject.transform;
                item.transform.position = posItem.position;

                peguei = true;
                podePegar = false;

            }
        }



        if (other.CompareTag("CameraMata"))
        {
            audioSource.PlayOneShot(audios[1]);
            GameController.instance.derrota();
        }





        if (other.CompareTag("Bateria"))
        {
            
            audioSource.PlayOneShot(audios[0]);
            StartFlash();
            bateria += 60;
            if (bateria > 100)
            {
                bateria = 100;
            }
            other.transform.parent.gameObject.SetActive(false);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Drop")) //totem
        {
            //se o player tiver segurando algum item
            if (item != null && podeDropar == true)
            {


                //audio
                audioSource.PlayOneShot(audios[0]);

                //acessa a cesta


                if (faseEuTo == 1)
                {
                    Cesta cestaScript = GameObject.FindGameObjectWithTag("Cesta").GetComponent<Cesta>();

                    if (cestaScript != null)
                    {
                        string nomeItem;
                        nomeItem = item.name;

                        cestaScript.Preencher(nomeItem);

                    }


                }
                else
                {
                    Pizza pizzaScript = GameObject.FindGameObjectWithTag("Pizza").GetComponent<Pizza>();

                    if (pizzaScript != null)
                    {
                        string nomeItem;
                        nomeItem = item.name;

                        pizzaScript.Preencher(nomeItem);

                    }
                }




                if (paiInicialDoItem != null)
                {
                    item.transform.parent = paiInicialDoItem.transform;
                    item = null;

                }



                //acessa o totem e ativa as funcionalidades dele
                Totem totemTemp;
                totemTemp = collision.gameObject.GetComponentInParent<Totem>();

                faseEuTo = totemTemp.fase;
                roundEuTo = totemTemp.round;
                toPeperoni = totemTemp.peperoni;
                

                if(roundEuTo == 2)
                {
                    ScoreManager.instance.conquista[2] = true;
                    Debug.Log("Conquista2 obtida");
                    ScoreManager.instance.atualizarConquistas();
                }
                    GameController.instance.atualizarFase();
                    GameController.instance.atualizarRound();
                    

                totemTemp.fazerOsTrem();

                dropei = true;
                podeDropar = false;
                podeGrudar = true;

            }

        }


        if (collision.gameObject.CompareTag("TotemFinal"))
        {


            TotemFinal tmp;
            tmp = collision.gameObject.GetComponent<TotemFinal>();
            tmp.fazerOsTrem();

        }

        if (collision.gameObject.CompareTag("Parede"))
        {

            Debug.Log("parede");
            if (cheat5 == false)
            {
                // Faz o jogador piscar de branco
                StartFlash();
                audioSource.PlayOneShot(audios[2]);
                estrelas.SetActive(true);
                animator.SetBool("bAtordoado", true);

                // Impede o jogador de andar temporariamente
                podeAndar = false;

                // Diminui a bateria ao colidir com a parede
                bateria -= 15;

            }
            else
            {
                Debug.Log("cheat imortalidade ligado");
            }
        }

        if (collision.gameObject.CompareTag("Forno"))
        {



            if (item != null && podeGrudar == true)
            {
                item.transform.parent = null;
                item = null;
                dropei = true;
                podeDropar = false;
                podeGrudar = false;

            }


        }

        if (collision.gameObject.CompareTag("MesaCorte"))
        {

            if (item != null && podeGrudar == true)
            {

                dropei = true;
                podeDropar = false;
                item.transform.parent = null;
                item = null;
                podeGrudar = false;

            }

        }
    }

    public void movimentacao()
    {
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

                if (Mathf.Abs(endTouchPos.x - startTouchPos.x) <= 0.05f || Mathf.Abs(endTouchPos.y - startTouchPos.y) <= 0.05f)
                {
                    //dropar o item com um toque
                    /*if (item != null)
                    {
                        item.transform.position = new Vector3(item.transform.position.x, posItemInicial.y, item.transform.position.z);
                        item.transform.parent = paiInicialDoItem.transform;
                        item = null;
                        dropei = true;
                        podeDropar = false;

                    }*/

                    Debug.Log("mto curto");

                }
                else
                {
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
            }

            // isso evita que o player se mexa com o jogo pausado
            if (GameController.instance.pausado == false)
            {
                if (dir.magnitude != 0)
                {
                    Quaternion olhandoPara = Quaternion.LookRotation(dir);
                    transform.rotation = olhandoPara;
                }
            }
        }
    }

    public void FimDaBateria()
    {
        if (bateria <= 0 && bateria > -100)
        {
           
            audioSource.PlayOneShot(audios[1]);
            bateria = -100;
            GameController.instance.derrota();
        }
    }

    void ColetaDrop()
    {
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
    }

    // Funcao para comecar o efeito de piscar
    void StartFlash()
    {
        // Ativa o estado de piscar
        isFlashing = true;
        // Inicia o temporizador do piscar
        flashTimer = 0f;
        // Define a cor inicial do piscar como amarelo
        playerRenderer.material.color = Color.white;
    }

    // Funcao para atualizar o efeito de piscar
    void UpdateFlash()
    {
        if (isFlashing)
        {
            // Alterna entre a cor amarela e a cor original
            playerRenderer.material.color = Color.Lerp(Color.white, originalColor, Mathf.PingPong(flashTimer / flashDuration, 1f));

            // Atualiza o temporizador do piscar
            flashTimer += Time.deltaTime;

            // Verifica se o tempo de piscar acabou
            if (flashTimer >= flashDuration)
            {
                // Para o efeito de piscar
                StopFlash();
            }
        }
    }

    // Funcao para parar o efeito de piscar
    void StopFlash()
    {
        // Desativa o estado de piscar
        isFlashing = false;
        // Restaura a cor original do jogador
        playerRenderer.material.color = originalColor;
    }
}