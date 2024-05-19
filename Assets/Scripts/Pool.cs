using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Pool : MonoBehaviour
{

    public static Pool poolerInstance;

    public GameObject tileBase;
    GameObject tmp;

    public List<GameObject> listaTilesBases  = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {


        poolerInstance = this;
        for (int i = 0; i < 6; i++)
        {

            tmp = Instantiate(tileBase);
            tmp.SetActive(false);
            listaTilesBases.Add(tmp);

        }

    }


    public GameObject getTileBase()
    {
        for (int i = 0; i < 6; i++)
        {

            if (!listaTilesBases[i].activeInHierarchy)
            {
                return listaTilesBases[i];
            }

        }
        return null;
    }

}
