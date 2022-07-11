using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopOpener : MonoBehaviour
{
    public GameObject Shop_UI;
    public GameObject Weapon;

    public void OpenShop()
    {
        if(Shop_UI != null)
        {
            bool isActive = Shop_UI.activeSelf;

            Shop_UI.SetActive(!isActive);
        }

        if (Weapon != null)
        {
            bool isActive = Weapon.activeSelf;

            Weapon.SetActive(!isActive);
        }
    }

}
