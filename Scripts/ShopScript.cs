using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public Button HealthPotionButton;
    public Button DoubleJumpButton;
    public Button ShurikenFuryButton;
    public Button InvisibilityButton;
    public ShurikenFury ShurikenScript;
    public Invisibility Invisible;
    public bool buyDoubleJump= false;
    private bool isShurikenActive = false;
    private bool isInvisibilityActive = false;
    public weapon Weapon;


    void Start()
    {
        ScoreManager coins = GetComponent<ScoreManager>();
        ShurikenScript = GetComponent<ShurikenFury>();
        Invisible = GetComponent<Invisibility>();

        Button button1 = HealthPotionButton.GetComponent<Button>();
        button1.onClick.AddListener(Check1);

        void Check1()
        {
            if ( coins.score >= 15)
            {
                AddPotion();
                coins.score -= 15;
                LicznikPotions.licznik.ChangeScore();
            }
            else Debug.Log("Masz za malo kasy!");
        }

        Button button2 = DoubleJumpButton.GetComponent<Button>();
        button2.onClick.AddListener(Check2);

        void Check2()
        {
            if (coins.score >= 50 && buyDoubleJump == false)
            {
                buyDoubleJump = true;
                coins.score -= 50;
                
            }
            else if (buyDoubleJump == false)
            {
                Debug.Log("Masz za malo kasy!");
            }
        }

        Button button3 = ShurikenFuryButton.GetComponent<Button>();
        button3.onClick.AddListener(Check3);

 
        void Check3()
        {
            if (coins.score >= 150 && isShurikenActive == false)
            {
                ActivateShurikenFury();
                coins.score -= 150;
            }
            else Debug.Log("Masz za malo kasy!");

        }

        Button button4 = InvisibilityButton.GetComponent<Button>();
        button4.onClick.AddListener(Check4);

        void Check4()
        {
            if (coins.score >= 200 && isInvisibilityActive == false)
            {
                ActivateInvisibility();
                coins.score -= 200;
            }
            else Debug.Log("Masz za malo kasy!");
        }
    }



    void AddPotion()
    {
        Debug.Log("You have clicked the button1!");
    }


    void ActivateShurikenFury()
    {
        ShurikenScript.enabled = true;
        isShurikenActive = true;
}
    void ActivateInvisibility()
    {
        Invisible.enabled = true;
        isInvisibilityActive = true;
    }
}
