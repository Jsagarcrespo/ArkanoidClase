using UnityEngine;
using TMPro;


// Centralizamos todo el estado del juego
// Todas la bolas van llamar a GameManger.Instance para actualizar puntaje y vidas.
// Me he desecho de las variables static, ahora todas las instancias usan la misma instancia
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    // Variables que dejan de depender de la barra o la bola
    // lo centralizamos los UI en GameManager
    // Mantenemos separdo la UI del comportamiento individual de los objetos
    // referencia a commit: a8d9f8d
    public TextMeshProUGUI puntoTxt;
    public GameObject[] vidasImage;
    public GameObject gameOverPanel;
    public GameObject victoriaPanel;

    // Innecesario el static porque llamamos a la funcion SumarPuntos
    // con lo que nuestros puntos se comparten automaticamente
    // refereincia a commit: c62cc96
    public int puntos = 0;
    public int vidas = 3;

    // Innecesario el static porque llamamos a la funcion RomperLadrillo
    // refereincia a commit: c62cc96
    public int cuentaLadrillo;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        cuentaLadrillo = GameObject.FindGameObjectsWithTag("Bloque").Length;
        ActualizarUI();
    }

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        puntoTxt.text = puntos.ToString("0000");
    }

    public void RomperLadrillo()
    {
        cuentaLadrillo--;

        if (cuentaLadrillo <= 0)
        {
            victoriaPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void PerderVida()
    {
        vidas--;

        if (vidas >= 0 && vidas < vidasImage.Length)
            vidasImage[vidas].SetActive(false);

        if (vidas <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void ActualizarUI()
    {
        puntoTxt.text = puntos.ToString("0000");
    }
}