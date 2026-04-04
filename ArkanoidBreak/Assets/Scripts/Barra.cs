using UnityEngine;
using UnityEngine.InputSystem;

public class Barra : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // VELOCIDAD A LA QUE QUEREMOS LA BARRA
    public float velocidadBarra = 65f;
    private Rigidbody2D RB;

    private float anchoBarra;

    public Rigidbody2D bola;
    private readonly float fuerza = 5f;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        RB.bodyType = RigidbodyType2D.Kinematic;
        RB.gravityScale = 0f;
        RB.freezeRotation = true;


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
        Rigidbody2D d = (Rigidbody2D)Instantiate(bola, transform.position, transform.rotation);

        // Desactivar la gravedad para este objeto, si no, ¡se cae!
        d.gravityScale = 0;

        // Posición de partida, en la punta de la nave
        d.transform.Translate(Vector2.up * 0.7f);

        // Lanzarlo
        d.AddForce(Vector2.up * fuerza, ForceMode2D.Impulse);
    }
}
