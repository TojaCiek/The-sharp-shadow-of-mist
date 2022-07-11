using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LicznikPotions : MonoBehaviour
{
    public static LicznikPotions licznik;
    public TextMeshProUGUI text;
    public int potions;
    public int PotionValue = 1;
    void Start()
    {
        if (licznik == null)
        {
            licznik = this;
        }
    }

    public void Update()
    {
        text.text = "Potions: " + potions.ToString();
    }

    public void ChangeScore()
    {
        potions += PotionValue;
        text.text = "Potions: " + potions.ToString();
    }


}
