using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    void Update()
    {
        transform.position = Camera.main.transform.position;
    }
}
