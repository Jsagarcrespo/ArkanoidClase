using System.Collections.Generic;
using TMPro;
using UnityEngine;


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
    public GameObject gameOverPanel;
    public GameObject victoriaPanel;
    public GameObject[] ImagenesVida; 

    // Innecesario el static porque llamamos a la funcion SumarPuntos
    // con lo que nuestros puntos se comparten automaticamente
    // refereincia a commit: c62cc96
    public int puntos = 0;
    public int vidas = 3;

    // Innecesario el static porque llamamos a la funcion RomperLadrillo
    // refereincia a commit: c62cc96
    public int cuentaLadrillo;

    public Rigidbody2D bolaPrefab;
    public List<Bola> bolasActivas = new List<Bola>();

    public float fuerzaBola = 10f; 

    public bool disparoActivo = false;
    public float duracionDisparo = 6f; 

    void Awake()
    {
        Instance = this;

        Screen.SetResolution(800, 600, false);

    }

    void Start()
    {
        // Para que me cargue los puntos que voy almacenando entre los niveles
        puntos = ControlEscena.puntosTotales; 

        cuentaLadrillo = GameObject.FindGameObjectsWithTag("Bloque").Length;
        ActualizarUI();

        DesactivaDisparo(); 
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

        if (vidas >= 0 && vidas < ImagenesVida.Length)
            ImagenesVida[vidas].SetActive(false);

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


    public void RegistrarBola(Bola ball)
    {
        bolasActivas.Add(ball);
    }

    public void EliminarBola(Bola ball)
    {
        bolasActivas.Remove(ball);
    }

    public int sacarBolasAct()
    {
        for (int i = bolasActivas.Count - 1; i >= 0; i--)
        {
            if (bolasActivas[i] == null)
            {
                bolasActivas.RemoveAt(i);
            }
        }
        return bolasActivas.Count;
    }
        
        
    public void DuplicarBolas()
    {
        // Limpiar antes de duplicar
        sacarBolasAct(); 

        // con esto copiamos la lista que tenemos
        foreach (Bola b in new List<Bola>(bolasActivas)) 
        {
            Rigidbody2D nuevaBola = Instantiate(
                bolaPrefab,
                b.transform.position,
                Quaternion.identity
            );

            Rigidbody2D rb = nuevaBola.GetComponent<Rigidbody2D>();
            Vector2 fuerza = new Vector2(0.7f, 1f).normalized * fuerzaBola;
            rb.AddForce(fuerza, ForceMode2D.Impulse);

            // No es neceario que lo registremos aqui
            // RegistrarBola(nuevaBola.GetComponent<Bola>());
        }

        Debug.Log("Duplicador ha pasado por la barra");

    }


    public void ActivarDisparo()
    {
        disparoActivo = true;
        Invoke("DesactivaDisparo", duracionDisparo); 
    }


    public void DesactivaDisparo()
    {
        disparoActivo = false;
    }


    }