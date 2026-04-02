using UnityEngine;

public class Bola : MonoBehaviour
{
    public float minY = -5.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY)
        {
            transform.position = Vector3.zero;
        }
    }
}
