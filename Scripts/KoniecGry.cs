using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class KoniecGry : MonoBehaviour
{
    public GameObject canvas;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            canvas.SetActive(true);
            
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        }
}