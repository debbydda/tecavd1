using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//codigo baseado em dave/gamedeveloper 

public class armaScript : MonoBehaviour
{
    //bala
    public GameObject bala;

    //força na bala
    public float forçaTiro;

    //info da arma
    public float tempoEntreTiros, tempoRecarregar, tempoEntreTirosConsc, spread;
    public int capacidadeArma, balasPorVez;
    public bool disparoContinuo;
    int balasSobrando, balasAtiradas;
    public string nomeArma;

    //bools sobre estado atual
    bool atirando, prontoParaAtirar, recarregando;

    //pov 
    public Camera fpsCamera;
    public Transform pontoDisparo;
    public TextMeshProUGUI displayCapacidade;

    public bool allowInvoke = true;

    private void Awake(){
        //garantir que não estamos zerados para poder atirar
        balasSobrando = capacidadeArma;
        prontoParaAtirar = true;
    }

    private void Update(){
        //receber input do jogador
        MyInput();

        //mostrar capacidade atual da arma
        if(displayCapacidade != null){
            displayCapacidade.SetText(balasSobrando/balasPorVez + "/" + capacidadeArma + "\n" + nomeArma);
        }
    }

    private void MyInput(){
        //saber se vamos atirar continuamente
        if(disparoContinuo) atirando = Input.GetKey(KeyCode.Mouse0);
        else atirando = Input.GetKeyDown(KeyCode.Mouse0);

        //recarregando
        if(Input.GetKeyDown(KeyCode.R) && balasSobrando < capacidadeArma && !recarregando)
            Reload();
 
        //atirando
        if(prontoParaAtirar && atirando && !recarregando && balasSobrando > 0){
            balasAtiradas = 0;
            Shoot();
        }
    }

    private void Shoot() {
        //se estamos atirando nao estamos mais prontos
        prontoParaAtirar = false;


        //ver se vamos atingir algo com raycast
        Ray ray = fpsCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100);

        //calcular spread para shotgun
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //calcular distancia normal para forças
        Vector3 distanciaNormal = targetPoint - pontoDisparo.position;

        //calcular distancia com spread para forças
        Vector3 distanciaSpread = distanciaNormal + new Vector3(x, y, 0);

        //spawn bala
        GameObject balaAtual = Instantiate(bala, pontoDisparo.position, Quaternion.identity);

        //girar bala na direção do tiro
        bala.transform.forward = distanciaSpread.normalized;

        //disparar bala pra frente com forças
        balaAtual.GetComponent<Rigidbody>().AddForce(distanciaSpread.normalized * forçaTiro, ForceMode.Impulse);
        
    
        balasSobrando--;
        balasAtiradas++;

        //repetir caso mais de uma bala por vez
        if (balasAtiradas < balasPorVez && balasSobrando > 0)
            Invoke("Shoot", tempoEntreTirosConsc);

        //invoke resetshot para continuar atirando
        if (allowInvoke)
        {
            Invoke("ResetShot", tempoEntreTiros);
            allowInvoke = false;
        }
    }   

    private void ResetShot(){
        //reset arma
        prontoParaAtirar = true;
        allowInvoke = true;
    }

    private void Reload(){
        //recarregar arma
        recarregando = true;
        Invoke("ReloadFinished", tempoRecarregar);
    }

    private void ReloadFinished(){
        //terminar recarega
        balasSobrando = capacidadeArma;
        recarregando = false;
    }
}
