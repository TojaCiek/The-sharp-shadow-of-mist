using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveL : MonoBehaviour
{
    public Transform sterownikKamery;
    public Transform kamera;
    public Transform gracz;
    public Transform prawo;
    public Transform otoczenie;
    public int ileMaSkoczyc;
    public float ileMaBycX = 25f;
    public float ileMaBycY = 18f;
    
    Vector3 pos;
    Vector3 pos1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            pos = gracz.transform.position;
            sterownikKamery.transform.position += new Vector3(-ileMaSkoczyc, 0, 0);
            pos = new Vector3((sterownikKamery.transform.position.x + ileMaBycX), gracz.transform.position.y, -10);
            kamera.position = pos;
            pos1 = new Vector3((sterownikKamery.transform.position.x -60), otoczenie.transform.position.y, otoczenie.transform.position.z);
            otoczenie.transform.position = pos1;
            prawo.transform.position += new Vector3(0, 10, 0);
            transform.position += new Vector3(0, -10, 0);
            sterownikKamery.GetComponent<PodazajZaGraczem>().blokadaKameryX = ileMaBycX;
            sterownikKamery.GetComponent<PodazajZaGraczem>().blokadaKameryY = ileMaBycY;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

}
