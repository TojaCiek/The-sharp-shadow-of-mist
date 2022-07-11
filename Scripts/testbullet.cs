using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class testbullet : MonoBehaviour {
 
 [SerializeField] private AudioClip wybuchsound;
     [SerializeField] GameObject explosionPrefab;
     [SerializeField] float speed;
     float finalSpeed;
     GameObject player;
     Vector3 currentPlayerPos;
     
     void Start()
     {
         player = GameObject.FindGameObjectWithTag("Player");
         if(player)
             currentPlayerPos = player.transform.position;
         finalSpeed = speed * Time.deltaTime;
     }
 
     void Update()
     {
         transform.LookAt(currentPlayerPos);
         transform.position = Vector3.MoveTowards(transform.position, currentPlayerPos, finalSpeed);
         if (transform.position == currentPlayerPos)
         {
             Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                         SoundManager.instance.PlaySound(wybuchsound);
             Destroy(gameObject);
         }
     }
 }
