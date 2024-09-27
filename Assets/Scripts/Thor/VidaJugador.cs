using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class VidaJugador : MonoBehaviour
{
    [SerializeField] private float maximoVida;
    [SerializeField] private float vida;
    [SerializeField] private BarraDeVida barraDeVida;

    private void Start()
    {
        vida = maximoVida;
        barraDeVida.InicializarBarraDeVida(vida);
    }

    public void TomarDa�o(int da�o)
    {
        vida -= da�o;
        barraDeVida.CambiarVidaActual(vida);

        if (vida <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

