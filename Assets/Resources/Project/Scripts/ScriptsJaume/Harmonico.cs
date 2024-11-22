using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Harmonico : Minijuego
{
    private float ContadorVeces;
    public Image imagenBarra;
    public Image Harmonica;
    private bool VerificarSubir=true;
    // Start is called before the first frame update
    public override void IniciarMinijuego()
    {
        if (Mathf.Approximately(Harmonica.fillAmount, 1f))
        {
            VerificarSubir = true;
        }
        if (Harmonica.fillAmount <= 0.21f && Harmonica.fillAmount >= 0.19f && VerificarSubir)
        {
            ContadorVeces += 0.05f;
            imagenBarra.fillAmount += 0.05f;
            VerificarSubir = false;
        }
    }

    public override bool VerificarJuegoCompletado()
    {
        if(Mathf.Approximately(ContadorVeces, 1f))
        {
            return true;
        }
        return false;
    }
    public override void TerminarMinijuego()
    {
        ContadorVeces=0f;
        imagenBarra.fillAmount = 0f;    
    }
}
