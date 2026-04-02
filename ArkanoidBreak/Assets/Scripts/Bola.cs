using UnityEngine;

public class Bola : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocidad = 15f;

    Rigidbody2D RB; 
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        RB.linearVelocity = Vector2.down * 10f;

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY)
        {
            transform.position = Vector3.zero;
            RB.linearVelocity = Vector2.down * 10f;
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
        }
    }

}
