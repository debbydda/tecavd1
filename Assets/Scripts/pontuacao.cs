using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pontuacao : MonoBehaviour
{
    public GameObject pTexto;
    public GameObject cubo;
    public static int pontos;

    // Update is called once per frame
    void Update()
    {
        pTexto.GetComponent<TMPro.TextMeshProUGUI>().text = "Predios destruidos: " + pontos;

        if(pontos >= 21){
            cubo.active = true;
            pontos = 0;
        }
    }
}
