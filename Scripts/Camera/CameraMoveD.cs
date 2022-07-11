using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveD : MonoBehaviour
{
    public Transform sterownikKamery;
    public Transform kamera;
    public Transform gracz;
    public Transform poprzedni;
    public int ileMaSkoczycX;
    public int ileMaSkoczycY;
    public float ileMaBycX = 25f;
    public float ileMaBycY = 18f;
    public SpriteRenderer tlojaskiniowe;
    public Transform tlojaskinioweT;
    public Vector3 ruszjaskinie;

    Vector3 pos;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            pos = gracz.transform.position;
            sterownikKamery.transform.position += new Vector3(ileMaSkoczycX, ileMaSkoczycY, 0);
            pos = new Vector3((sterownikKamery.transform.position.x - ileMaBycX), (sterownikKamery.transform.position.y + ileMaBycY), -10);
            kamera.position = pos;
            poprzedni.transform.position += new Vector3(10, 0, 0);
            transform.position += new Vector3(-10, 0, 0);
            sterownikKamery.GetComponent<PodazajZaGraczem>().blokadaKameryX = ileMaBycX;
            sterownikKamery.GetComponent<PodazajZaGraczem>().blokadaKameryY = ileMaBycY;
            if (tlojaskiniowe.enabled == false)
            tlojaskiniowe.enabled = true;
            else if (tlojaskiniowe.enabled == true)
            tlojaskiniowe.enabled = false;
            ruszjaskinie = new Vector3(kamera.transform.position.x, kamera.transform.position.y, tlojaskinioweT.transform.position.z);
            tlojaskinioweT.position = ruszjaskinie;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

}
