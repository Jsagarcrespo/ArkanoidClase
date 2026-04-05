
using UnityEngine;
using UnityEngine.SceneManagement;

public class Generador : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Vector2Int size;
    public Vector2 offset;

    public GameObject[] bloquesPrefabs;     
    public GameObject bloqueMetalPrefab;
    public GameObject bloqueDsiparoPrefab; 

    public GameObject panelBienvenida;
    public static bool primeraVez = true;

    public int minMetales = 2;
    public int maxMetales = 5;

    public int minBloqDisparo = 2;
    public int maxBloqDisparo = 5;

    private void Awake()
    {
        int totalBricks = size.x * size.y;

        
        int cantidadMetales = Random.Range(minMetales, maxMetales + 1);
        int cantDisparos = Random.Range(minBloqDisparo, maxBloqDisparo + 1);

        // Para no repetir posiciones
        int metalesColocados = 0;
        int disparosColocados = 0; 

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject prefabElegido;

                // Probabilidad de colocar metal (hasta el limite)
                if (metalesColocados < cantidadMetales && Random.value < 0.2f)
                {
                    prefabElegido = bloqueMetalPrefab;
                    metalesColocados++;
                } 
                else if(disparosColocados < cantDisparos && Random.value < 0.2f)
                {
                    prefabElegido = bloqueDsiparoPrefab;
                    disparosColocados++;
                }
                else
                {
                    prefabElegido = bloquesPrefabs[Random.Range(0, bloquesPrefabs.Length)];
                }

                GameObject newBrick = Instantiate(prefabElegido, transform);

                newBrick.transform.position = transform.position +
                    new Vector3((float)((size.x - 1) * .5f - i) * offset.x, j * offset.y, 0);
            }
        }
    }


    void Start()
    {
        if (primeraVez)
        {
            Time.timeScale = 0f;
            panelBienvenida.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            panelBienvenida.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
                     
    }

    public void EmpezarJuego()
    {
        primeraVez = false;
        panelBienvenida.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
