using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.UI;
using System.IO;

public class Player : MonoBehaviour
{
    [Header("---Bateria---")]
    //bateria
    public float timerBateria;
    public float bateria = 100;


    [Header("---SkinsAnims---")]
    public GameObject[] skins = new GameObject[3];
    public Animator animator;


    [Header("---Itens---")]
    //interacao com itens
    public GameObject item; //item que o player ta carregando
    public Transform posItem; //posicao que o item fica no player
    public Transform[] posicoesItens;
    public GameObject paiInicialDoItem;
    public Vector3 posItemInicial;
    public bool podePegar; //verificar se pode pegar item
    public bool podeDropar; //verificar se pode dropar item
    public bool peguei; //verificar se estou com algum item
    public bool dropei; //verificar se estou
    public bool podeGrudar; //alguma coisa pra evitar problema
    public float timer; //timer pra liberar pegar ou dropar item
    public float cooldownItens = 2; //cooldown pro timer acima


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
    Color originalColor; // Cor original do jogador
    bool isFlashing = false; // Flag para controlar o estado de piscar
    float flashDuration = 0.2f; // Duracao do piscar
    float flashTimer = 0f; // Timer para controlar o piscar
    public GameObject estrelas;
    public float timerAtordoamento;

    [Header("---Cheats---")]
    //game Cheats
    public bool cheat5 = false;

    [Header("---Temp---")]

    public int faseEuTo = 1;
    public int roundEuTo = 1;
    public bool toPeperoni;


    [Header("---Conquistas---")]
    public int contadorBaterParede;


    private void Awake()
    {

    }

    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/dadosPersonagens.txt"))
        {
            DadosPersonagens dadosPersonagens = new DadosPersonagens();
            string teste = File.ReadAllText(Application.persistentDataPath + "/dadosPersonagens.txt");
            dadosPersonagens = JsonUtility.FromJson<DadosPersonagens>(teste);
            atualizarSkin(dadosPersonagens.qualSkin);

        }
        //atualizarSkin(GameController.instance.skinTemp);


        init();

    }


    void Update()
    {
        if (GameController.instance.pausaInicial == false)
        {

            animacaoIdle();
            cheats();

            //verificar a todo momento se ele pode coletar ou nao algum item
            ColetaDrop();


            if (podeAndar == true)
            {
                movimentacao();
            }
            else
            {
                podeAndarTimer += Time.deltaTime;


                if (podeAndarTimer >= 1)
                {
                    estrelas.SetActive(false);
                    podeAndar = true;
                    podeAndarTimer = 0;

                    //desliga animacao de atordoamento
                    if (animator != null)
                    {
                        animator.SetBool("bAtordoado", false);

                    }
                }

            }




            //logica bateria
            diminuirBateria();

            // Atualizar o efeito de piscar ao atordoar
            UpdateFlash();
        }

    }

    private void FixedUpdate()
    {
        if (GameController.instance.pausaInicial == false)
        {
            //movimentacao constante pra frente local
            rb.AddForce(transform.forward * velocidade * 10f * Time.deltaTime, ForceMode.VelocityChange);
        }
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
            //se o player tiver segurando algum item e puder dropar
            if (item != null && podeDropar == true)
            {
                //audio
                audioSource.PlayOneShot(audios[0]);

                if (faseEuTo == 1)
                {
                    atualizarCesta();


                }
                else if (faseEuTo == 2)
                {

                    atualizarPizza();

                }
                else //faseEuTo == 3
                {

                    atualizarCaixa(collision.gameObject);

                }


                if (paiInicialDoItem != null && faseEuTo != 3)
                {
                    item.transform.parent = paiInicialDoItem.transform;
                    item = null;

                }

                //acessa o totem e ativa as funcionalidades dele
                Totem totemTemp;
                totemTemp = collision.gameObject.GetComponentInParent<Totem>();


                if (totemTemp.fase == 3)
                {
                    Pizza pizzaScript = GameObject.FindGameObjectWithTag("Pizza").GetComponent<Pizza>();
                    pizzaScript.esvaziar();
                }

                int fasetmp = faseEuTo;

                faseEuTo = totemTemp.fase;
                roundEuTo = totemTemp.round;
                toPeperoni = totemTemp.peperoni;


                if (faseEuTo != fasetmp)
                {
                    Debug.Log("TroqueiDeFase");



                }

                totemTemp.fazerOsTrem(); //ativar as funcionalidades do totem

                if (roundEuTo == 2)
                {
                    ScoreManager.instance.conquista[2] = true;
                    Debug.Log("Conquista2 obtida");
                    ScoreManager.instance.atualizarConquistas();
                }
                GameController.instance.atualizarFase();
                GameController.instance.atualizarRound();


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

            contadorBaterParede++;
            GameController.instance.addScore(-10);

            if (contadorBaterParede == 5)
            {
                Debug.Log("vc fez a conquista 1 aojekdo");
                ScoreManager.instance.conquista[1] = true;
                ScoreManager.instance.atualizarConquistas();
            }


            if (cheat5 == false)
            {
                baterParede();

            }
            else
            {
                Debug.Log("cheat imortalidade ligado");
            }
        }

        if (collision.gameObject.CompareTag("Forno") || collision.gameObject.CompareTag("MesaCorte"))
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


    }

    public void init()
    {
        // Pegar o Rigidbody do proprio objeto
        rb = GetComponent<Rigidbody>();
        // Pegar o componente Renderer do proprio objeto
        playerRenderer = GetComponentInChildren<Renderer>();
        // Salvar a cor original do jogador
        originalColor = playerRenderer.material.color;
        estrelas.SetActive(false);
    }

    public void atualizarSkin(int qual)
    {
        if (qual == 0)
        {
            skins[0].gameObject.SetActive(false);
            skins[1].gameObject.SetActive(true);
            skins[2].gameObject.SetActive(false);
            animator = skins[1].GetComponent<Animator>();
            posItem = posicoesItens[1];
        }
        else if (qual == 1)
        {
            skins[0].gameObject.SetActive(true);
            skins[1].gameObject.SetActive(false);
            skins[2].gameObject.SetActive(false);
            animator = skins[0].GetComponent<Animator>();
            posItem = posicoesItens[0];
        }
        else
        {
            skins[0].gameObject.SetActive(false);
            skins[1].gameObject.SetActive(false);
            skins[2].gameObject.SetActive(true);
            animator = skins[2].GetComponent<Animator>();
            posItem = posicoesItens[2];
        }
    }

    public void baterParede()
    {
        // Faz o jogador piscar de branco
        StartFlash();
        //audio atordoamento
        audioSource.PlayOneShot(audios[2]);
        //ligar estrelas de atordoamento
        estrelas.SetActive(true);

        //ativa animacao de atordoamento
        if (animator != null)
        {
            animator.SetBool("bAtordoado", true);
        }
        // Impede o jogador de andar temporariamente
        podeAndar = false;

        // Diminui a bateria ao colidir com a parede
        bateria -= 15;
    }

    public void atualizarCesta()
    {
        Cesta cestaScript = GameObject.FindGameObjectWithTag("Cesta").GetComponent<Cesta>();

        if (cestaScript != null)
        {
            string nomeItem = item.name;
            cestaScript.Preencher(nomeItem);

        }
    }
    public void atualizarPizza()
    {
        Pizza pizzaScript = GameObject.FindGameObjectWithTag("Pizza").GetComponent<Pizza>();

        if (pizzaScript != null)
        {
            string nomeItem = item.name;
            pizzaScript.Preencher(nomeItem);

        }
    }
    public void atualizarCaixa(GameObject collision)
    {
        CaixaPizza caixa = GameObject.FindGameObjectWithTag("CaixaPizza").GetComponent<CaixaPizza>();
        GameObject teste;
        teste = collision.gameObject.transform.GetChild(0).GetChild(0).gameObject;

        Collider colTemp;
        colTemp = caixa.GetComponentInChildren<Collider>();
        colTemp.enabled = false;
        //item.SetActive(false);

        colTemp.enabled = false;
        item.gameObject.transform.parent = teste.transform;
        caixa.desparentear();
        item = null;
    }


    public void animacaoIdle()
    {
        if (animator != null)
        {
            if (rb.velocity.magnitude > 1f)
            {

                animator.SetBool("bParado", false);
            }
            else
            {
                animator.SetBool("bParado", true);
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

                //se ao clicar na tela o movimento for muito curto:
                if (Mathf.Abs(endTouchPos.x - startTouchPos.x) <= 0.05f || Mathf.Abs(endTouchPos.y - startTouchPos.y) <= 0.05f)
                {

                    //droparItemNoChao();
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

            if (dir.magnitude != 0)
            {
                Quaternion olhandoPara = Quaternion.LookRotation(dir);
                transform.rotation = olhandoPara;
            }

        }
    }

    public void cheats()
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
    }

    public void diminuirBateria()
    {
        if (cheat5 == false)
        {

            timerBateria += Time.deltaTime;
            if (timerBateria > 0.4f)
            {
                bateria--;
                timerBateria = 0;
            }

            if (bateria <= 0 && bateria > -100)
            {
                FimDaBateria();
            }
        }
    }

    public void FimDaBateria()
    {
        audioSource.PlayOneShot(audios[1]);
        bateria = -100;
        GameController.instance.derrota();
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


    public void droparItem()
    {

        if (item != null)
        {


        }

    }


    public void droparItemNoChao()
    {
        //metodo desatualizado
        //dropar o item com um toque
        if (item != null)
        {
            item.transform.position = new Vector3(item.transform.position.x, posItemInicial.y, item.transform.position.z);
            item.transform.parent = paiInicialDoItem.transform;
            item = null;
            dropei = true;
            podeDropar = false;

        }

    }
}