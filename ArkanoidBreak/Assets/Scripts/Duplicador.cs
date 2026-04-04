using UnityEngine;

public class Duplicador : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    // Test para saber si el Duplicador que cae lo detecta la barra
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Barra"))
        {
            Debug.Log("Duplicador ha pasado por la barra"); 
        }
    }

}
