using UnityEngine;

public class PowerUpDisparo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Eliminamos el objeto si se sale de la pantalla
        if (transform.position.y < -5.5f)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Barra"))
        {
            GameManager.Instance.ActivarDisparo();
            Destroy(gameObject);
        }
    }
}
