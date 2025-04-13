using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfdestruct : MonoBehaviour
{
    [SerializeField] float timer = 3f;
    void Start()
    {
        Destroy(gameObject,timer);
    }

   
}
