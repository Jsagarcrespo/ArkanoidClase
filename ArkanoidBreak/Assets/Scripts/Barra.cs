using UnityEngine;
using UnityEngine.InputSystem;

public class Barra : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // VELOCIDAD A LA QUE QUEREMOS LA BARRA
    public float velocidadBarra = 65f;
    private Rigidbody2D RB; 
    
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

        RB.MovePosition(RB.position + new Vector2(movimientoX * velocidadBarra * Time.deltaTime, 0));
    }
}
