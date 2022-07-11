using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Invisibility : MonoBehaviour
{
    public SpriteRenderer Sprite;
    private Color col;
    public float fireRate = 6F;
    private float nextFire = 0.0F;
    public GameObject InvisibilitySkill;
    void Start()
    {
        Sprite = GetComponent<SpriteRenderer>();
        col = Sprite.color;
        InvisibilitySkill.SetActive(true);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.X) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(CantSee());
            InvisibilitySkill.SetActive(false);
        }



    }
    IEnumerator CantSee()
    {
        gameObject.layer = LayerMask.NameToLayer("Invisible");
        
        col.a = .2f;
        Sprite.color = col;
        yield return new WaitForSeconds(5);
        gameObject.layer = LayerMask.NameToLayer("Player");
        
        col.a = 1;
        Sprite.color = col;
        yield return new WaitForSeconds(20);
        InvisibilitySkill.SetActive(true);
    }
}
