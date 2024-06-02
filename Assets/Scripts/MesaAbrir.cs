using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MesaAbrir : MonoBehaviour
{
    public float contador;
    public bool podeContar;
    public bool temItemPraAbrir;

    
    public GameObject item;

    public Transform posItemAbrir;
    public Transform posItemAberto;
    public GameObject totem;

    public Animator totemAnimator;
    public Animator roloAnimator;
    public Animator itemAnimator;

    public bool jaAbriu = false;

    public Slider slider;


    private void OnEnable()
    {
        itemAnimator.SetTrigger("tReset");
        itemAnimator.speed = 1;
        podeContar = false;
        jaAbriu = false;
        contador = 0;
        slider.value = 0;

        temItemPraAbrir = false;
        item.transform.GetChild(1).gameObject.SetActive(true);
        item.transform.GetChild(2).gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (!jaAbriu)
        {
            slider.value = contador;

            if (contador >= 2f)
            {
                
                roloAnimator.SetBool("bAbrindo", false);

                item.transform.GetChild(1).gameObject.SetActive(false);
                item.transform.GetChild(2).gameObject.SetActive(true);

                totem.SetActive(true);
                totemAnimator.Play("TotemEntrance");

                item.transform.position = posItemAberto.position;
                contador = 0;
                podeContar = false;
                jaAbriu = true;
                temItemPraAbrir = false;
            }

        }


    }



    private void OnTriggerEnter(Collider other)
    {
        if (jaAbriu == false)
        {
            if (other.CompareTag("Item"))
            {

                item = other.transform.parent.gameObject;
                item.transform.position = posItemAbrir.position;
                item.transform.parent = transform;
                temItemPraAbrir = true;
                podeContar = true;
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.instance.audioSource.time = 0;
            GameController.instance.audioSource.Play();
            GameController.instance.audioSource.PlayOneShot(GameController.instance.audios[0]);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (temItemPraAbrir)
            {

                
                contador += Time.deltaTime;
                itemAnimator.speed = 1;
                itemAnimator.SetBool("bAbrindo",true);
                roloAnimator.SetBool("bAbrindo", true);

            }

        }
       
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            GameController.instance.audioSource.Stop();
           itemAnimator.speed = 0;
            roloAnimator.SetBool("bAbrindo", false);
        }
    }

}
