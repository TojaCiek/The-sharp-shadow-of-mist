using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI text;
    public int score;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void Update()
    {
        text.text = "Coins: " + score.ToString();
    }

    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = "Coins: " + score.ToString();
    }

   
}
