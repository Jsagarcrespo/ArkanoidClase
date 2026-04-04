using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Barra : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // VELOCIDAD A LA QUE QUEREMOS LA BARRA
    public float velocidadBarra = 65f;
    private Rigidbody2D RB;

    private float anchoBarra;

    public GameObject disparo;
    private readonly float fuerza = 15f;


    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        RB.bodyType = RigidbodyType2D.Kinematic;
        RB.gravityScale = 0f;
        RB.freezeRotation = true;
        GameManager.Instance.DesactivaDisparo(); 

    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float movimientoX = 0f;
        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            movimientoX = -1f;
        else if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            movimientoX = 1f;

        // para moverlo horizontalmente
        Vector2 nuevaPos = RB.position + new Vector2(movimientoX * velocidadBarra * Time.deltaTime, 0f);

        // Para que no salga de nuestra camara
        float distHorizontal = Camera.main.orthographicSize * Screen.width / Screen.height;

        // Volvemos a meter la barra si se nos sale de la camara
        if (nuevaPos.x - anchoBarra > distHorizontal)
            nuevaPos.x = -distHorizontal + anchoBarra;
        else if ( nuevaPos.x + anchoBarra < - distHorizontal) 
            nuevaPos.x = distHorizontal - anchoBarra;


            RB.MovePosition(nuevaPos);

        // Disparo
        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            Disparar();
        }
    }

    void Disparar()
    {
        // Hacemos copias del prefab del disparo y las lanzamos
        GameObject nuevaBola = Instantiate(disparo, transform.position, transform.rotation);

        Rigidbody2D d = nuevaBola.GetComponent<Rigidbody2D>();

        d.gravityScale = 0;
        d.transform.Translate(Vector2.up * 0.7f); 
        d.AddForce(Vector2.up * fuerza, ForceMode2D.Impulse);

        // ya no necesitamos actualizar la ui ni contar ladrrilos
        // referencia a commit: a8d9f8d

    }
}
