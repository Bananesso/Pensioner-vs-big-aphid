using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform _loookAtThis;
    void FixedUpdate()
    {
        this.transform.LookAt(_loookAtThis);
    }
}