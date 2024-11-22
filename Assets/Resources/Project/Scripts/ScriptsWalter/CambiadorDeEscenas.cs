using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiadorDeEscenas : MonoBehaviour
{
    // Start is called before the first frame update
    public static CambiadorDeEscenas instance;

    private void Start()
    {
        instance = this;
    }

    public void Jugar() {

        SceneManager.LoadScene("ScenaNave", LoadSceneMode.Single);
    }

    public void VerDerrota() {
        SceneManager.LoadScene("EscenaDerrota", LoadSceneMode.Single);
    }

    public void VerVictoria() {
        SceneManager.LoadScene("EscenaVictoria", LoadSceneMode.Single);
    }

    public void VerMenu() {
        SceneManager.LoadScene("EscenaMenu", LoadSceneMode.Single);
    }

    public void Salir() { 
        Application.Quit();
    }
}
