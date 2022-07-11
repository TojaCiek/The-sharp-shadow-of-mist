using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;

public class CHEST : MonoBehaviour 
{ 

public Transform player; 
public Animator animator;
private float distToPlayer;

    public float interactRange = 2f; 
    

void Update() 
     {
        distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer <= interactRange)
        {
            TestRange();
        }

        else animator.SetBool("InRange", false);
    }
void TestRange() 
    {
        animator.SetBool("InRange", true);

    }

}