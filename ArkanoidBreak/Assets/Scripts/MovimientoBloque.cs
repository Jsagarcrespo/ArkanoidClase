using UnityEngine;

public class MovimientoBloque : MonoBehaviour
{
    public bool moverse = true;

    public float velocidad = 2f;

    public float limiteIzquierda = -6f;
    public float limiteDerecha = 6f;

    // nos referimos en la direccion que se vana a mover
    // 1 para derecha y -1 para izquierda
    private int direccion = 1; 


    void Update()
    {
        if (!moverse) return;

        
        transform.position += Vector3.right * direccion * velocidad * Time.deltaTime;

        // si llega al limite que le marque cambia de direccion
        if (transform.position.x >= limiteDerecha)
            direccion = -1;
        else if (transform.position.x <= limiteIzquierda)
            direccion = 1;
    }
}