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
    public GameObject victoriaPanel;
    int cuentaLadrillo; 
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        RB.linearVelocity = Vector2.down * 10f;
        cuentaLadrillo = GameObject.FindGameObjectsWithTag("Bloque").Length;

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
   

}
