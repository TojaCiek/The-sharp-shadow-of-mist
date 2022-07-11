using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodazajZaGraczem : MonoBehaviour
{
    public Transform player;
    public new Transform camera;
    public float blokadaKameryX ;
    public float blokadaKameryY ;
    public float odlegloscOdGraczaX;
    public float odlegloscOdGraczaY;
    float playerX, playerY;
    Vector3 pos;
    void Start()
    {
       
    }
     void Update()
    {
        pos = camera.transform.position;
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;
        odlegloscOdGraczaX= transform.position.x - player.transform.position.x;
        odlegloscOdGraczaY= transform.position.y - player.transform.position.y;
        
        if ((Mathf.Abs(odlegloscOdGraczaX)) < blokadaKameryX)
        {
            pos.x = playerX;
        }
        if ((Mathf.Abs(odlegloscOdGraczaY)) < blokadaKameryY)
        {
            pos.y = playerY;    
        }
        camera.transform.position = pos;
  


    }
}
