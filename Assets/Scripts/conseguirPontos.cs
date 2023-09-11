using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conseguirPontos : MonoBehaviour
{
    int prediosSobrando;

    void OnTriggerEnter(Collider other){
        Destroy(gameObject);
        prediosSobrando = GameObject.FindGameObjectsWithTag("predio").Length;
        pontuacao.pontos = 21 - prediosSobrando + 1;
        
    }
}
