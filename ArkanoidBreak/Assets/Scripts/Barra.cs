using UnityEngine;
using UnityEngine.InputSystem;

public class Barra : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // VELOCIDAD A LA QUE QUEREMOS LA BARRA
    public float velocidadBarra = 65f;
    private Rigidbody2D RB;

    private float anchoBarra;

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
    }
}
