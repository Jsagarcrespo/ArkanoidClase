using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;    

public class Bola : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocidad = 15f;

    Rigidbody2D RB;

    int puntos = 0;
    int vidas = 3;


    public TextMeshProUGUI puntoTxt;
    public GameObject[] vidasImage;

    public GameObject gameOverPanel; 
    

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
        }
    }


    void GameOver()
    {
        Debug.Log("Game Over"); 
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; 
        Destroy(gameObject);
    }
   

}
