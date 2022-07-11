using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZniszczSie : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var coto = other.gameObject.layer;
        if (coto == 7)
        {
            Destroy(gameObject);
        }
    }
   
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }
}
