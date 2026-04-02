using UnityEngine;

public class Bola : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocidad = 25f;

    Rigidbody2D RB; 
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY)
        {
            transform.position = Vector3.zero;
            RB.linearVelocity = Vector3.zero;
        }

        
    }
}
