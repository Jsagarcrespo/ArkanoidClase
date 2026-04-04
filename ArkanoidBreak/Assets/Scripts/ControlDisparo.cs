using UnityEngine;

public class ControlDisparo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    
        if (collision.gameObject.CompareTag("Bloque"))
        {
            GameManager.Instance.SumarPuntos(10);
            GameManager.Instance.RomperLadrillo();

            Destroy(collision.gameObject);

            Destroy(gameObject);
        }

        else if (collision.gameObject.CompareTag("Pared") || collision.gameObject.CompareTag("Bola") || collision.gameObject.CompareTag("BloqueMetal"))
        {
            Destroy(gameObject);
        }
    }

}
