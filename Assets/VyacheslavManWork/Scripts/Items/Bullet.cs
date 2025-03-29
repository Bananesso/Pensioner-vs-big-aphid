using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 7);
    }
    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(gameObject);
    }
}
