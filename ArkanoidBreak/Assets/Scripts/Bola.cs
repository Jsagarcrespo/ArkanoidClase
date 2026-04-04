using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;    

public class Bola : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocidad = 15f;

    Rigidbody2D RB;

    /// <summary>
    /// Contador global de puntos
    ///  Con static todas las instancias de las bolas comparten este valor
    ///  cada vez que se rompe un ladrillo ira sumando en el contador
    ///  De este modo varias bolas suman correctamente el puntaje total, 
    ///  y no cada bola tiene su contado independiente
    /// </summary>
    
    public static int puntos = 0;
    int vidas = 3;


    public TextMeshProUGUI puntoTxt;
    public GameObject[] vidasImage;

    public GameObject gameOverPanel;
    public GameObject victoriaPanel;

    /// <summary>
    /// Contador global de ladrillos
    /// Con static todas las instancias de las bolas comparten este valor
    /// cada vez que se rompe un ladrillo ira restando en el contador
    /// Al llegar a cero significa que todos han sido destruidos
    /// y mostraremos el panel de Victoria
    /// </summary>
    public static int cuentaLadrillo;

    public static int bolasEnJuego = 0;


    private void Awake()
    {
        bolasEnJuego++;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        RB.linearVelocity = Vector2.down * 10f;


        /// <summary>
        /// Iniciliza el contador global de ladrillos al comienzo
        /// Se hace por si a caso, por si el contador queda en 0 de partidas anteriores
        /// Se consigue el numero de ladrillos mediante su etiqueta
        /// </summary>
        if (cuentaLadrillo == 0)
        {
            cuentaLadrillo = GameObject.FindGameObjectsWithTag("Bloque").Length;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY)
        {
            if (bolasEnJuego <= 1) 
            { 
                if (vidas <= 0)
                {
                    GameOver();
                }
                else
                {
                    transform.position = Vector3.zero;
                    RB.linearVelocity = Vector2.down * 10f;
                    vidas--;
                    vidasImage[vidas].SetActive(false);
                }
            }
            else
            {
                Destroy(gameObject);
            }

        }   

        if (RB.linearVelocity.magnitude > maxVelocidad)
        {
            RB.linearVelocity = Vector3.ClampMagnitude(RB.linearVelocity, maxVelocidad);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bloque"))
        {
            Destroy(collision.gameObject);
            puntos+= 10;
            puntoTxt.text = puntos.ToString("0000");
            cuentaLadrillo--; 
            if(cuentaLadrillo <= 0)
            {
                victoriaPanel.SetActive(true);
                Time.timeScale = 0;
            }

        }
    }


    void GameOver()
    {
        Debug.Log("Game Over"); 
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; 
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        bolasEnJuego--; 
    }


}
