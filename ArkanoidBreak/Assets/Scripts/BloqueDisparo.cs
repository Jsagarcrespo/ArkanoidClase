using UnityEngine;

public class BloqueDisparo : MonoBehaviour
{
    public GameObject iconoDisparoPrefab;

    private void OnDestroy()
    {
        if (iconoDisparoPrefab != null)
        {
            Instantiate(iconoDisparoPrefab, transform.position, Quaternion.identity);
        }
    }
}