using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;    

public class Bola : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocidad = 15f;

    Rigidbody2D RB;
    public GameObject duplisPrefab;


    private void Awake()
    {
        GameManager.Instance.RegistrarBola(this);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        RB.linearVelocity = Vector2.down * 10f;
            
        // Al tener un manejador ya no es necesario que la Bola.cs se encargue contar los ladrillos y actualizar la UI
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY)
        {
            if (GameManager.Instance.sacarBolasAct() <= 1) 
            {
                // No necesitamos condicion de contar vida para que salte el GameOver
                // Y lo hace la funcion PerderVida del GameManger

                GameManager.Instance.PerderVida();
                transform.position = Vector3.zero;
                RB.linearVelocity = Vector2.down * 10f;     
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

            GameManager.Instance.SumarPuntos(10);
            GameManager.Instance.RomperLadrillo();

            // probabilidad de que salga el duplicado en un 20%
            // Random.value para compararlo entre 0 y 1 
            // Si el valor es menor que la probabilidad, se instancia el prefab
            float probableDuplis = 0.2f; 
            if (Random.value < probableDuplis)
            {
                Instantiate(duplisPrefab, collision.transform.position, Quaternion.identity);
            }

        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.EliminarBola(this);
    }


}
