using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
        [SerializeField] private AudioClip monetka;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(monetka);
            ScoreManager.instance.ChangeScore(coinValue);
         
        }
    }
}
