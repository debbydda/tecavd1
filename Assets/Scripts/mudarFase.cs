 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mudarFase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other){
        if (other.tag == "mudarCena"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (other.tag == "encerrar"){
            Application.Quit();
        }
    }
}
