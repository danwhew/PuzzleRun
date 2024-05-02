using System.Collections;
using System.Collections.Generic;
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
    public bool podePegar;
    public bool podeDropar;
    public bool peguei;
    public bool dropei;
    public float timer;
    public float cooldownItens = 2;

    [Header("---Movimentacao---")]
    //movimentacao
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
    float flashDuration = 0.2f; // Duração do piscar
    float flashTimer = 0f; // Timer para controlar o piscar

    [Header("---Atordoamento---")]
    //game Cheats
    public bool cheat5 = false;


    void Start()
    {
        // Pegar o Rigidbody do próprio objeto
        rb = GetComponent<Rigidbody>();
        // Pegar o componente Renderer do próprio objeto
        playerRenderer = GetComponent<Renderer>();
        // Salvar a cor original do jogador
        originalColor = playerRenderer.material.color;
    }


    void Update()
    {


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
                    podeAndar = true;
                    podeAndarTimer = 0;
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

        // Atualizar o efeito de piscar, se necessário
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

                //aqui o item fica preso ao player
                item = other.gameObject;
                item.transform.parent = transform;
                item.transform.position = transform.position + new Vector3(0, 2f, 0);
                peguei = true;
                podePegar = false;
                
            }
        }

        if (other.CompareTag("CameraMata"))
        {
            audioSource.PlayOneShot(audios[1]);
            GameController.instance.derrota();
        }

        if (other.CompareTag("Drop") )
        {
            if (podeDropar == true)
            {
                audioSource.PlayOneShot(audios[0]);

                //aqui o item fica preso ao local de entrega
                item.transform.position = other.transform.position + new Vector3(0, 0, 0);
                item.transform.parent = other.transform;
                dropei = true;
                podeDropar = false;
            }
        }

        if (other.CompareTag("Forno"))
        {
            dropei = true;
            podeDropar = false;


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
            Destroy(other.gameObject);
        }

        if (other.CompareTag("NovaFase"))
        {
            GameController.instance.atualizarFase();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Parede"))
        {
            if (cheat5 == false)
            {
                // Faz o jogador piscar de branco
                StartFlash();

                // Impede o jogador de andar temporariamente
                podeAndar = false;

                // Diminui a bateria ao colidir com a parede
                bateria -= 20;

            }
            else
            {
                Debug.Log("cheat imortalidade ligado");
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

    // Função para comecar o efeito de piscar
    void StartFlash()
    {
        // Ativa o estado de piscar
        isFlashing = true;
        // Inicia o temporizador do piscar
        flashTimer = 0f;
        // Define a cor inicial do piscar como amarelo
        playerRenderer.material.color = Color.white;
    }

    // Função para atualizar o efeito de piscar
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

    // Função para parar o efeito de piscar
    void StopFlash()
    {
        // Desativa o estado de piscar
        isFlashing = false;
        // Restaura a cor original do jogador
        playerRenderer.material.color = originalColor;
    }
}