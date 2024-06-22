using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaPizza : MonoBehaviour
{
    public void desparentear()
    {
        StartCoroutine(desparentearCoroutine());
    }

    public IEnumerator desparentearCoroutine()
    {
        yield return new WaitForSecondsRealtime(3f);
        transform.parent = null;
    }
}
