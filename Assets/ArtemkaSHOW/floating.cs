using UnityEngine;

public class SmoothWobble : MonoBehaviour
{
    [Header("Position Wobble")]
    public float verticalAmplitude = 0.2f;    
    public float verticalFrequency = 1f;      

    [Header("Rotation Wobble")]
    public float tiltAngle = 15f;             
    public float tiltSpeed = 2f;              
    public Vector3 tiltAxis = Vector3.forward; 
    public Vector3 swayAxis = Vector3.right;   

    [Header("Randomness")]
    public float randomness = 0.3f;           
    public float randomSeed;                  
    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        randomSeed = Random.Range(0f, 100f); 
    }

    void Update()
    {

        float verticalOffset = Mathf.Sin(Time.time * verticalFrequency + randomSeed) * verticalAmplitude;
        float tiltOffset = Mathf.Sin(Time.time * tiltSpeed + randomSeed * 2) * tiltAngle;
        float swayOffset = Mathf.Cos(Time.time * tiltSpeed * 0.7f + randomSeed) * tiltAngle * 0.5f;


        transform.position = startPosition + Vector3.up * verticalOffset;


        Quaternion tiltRotation = Quaternion.AngleAxis(tiltOffset, tiltAxis);
        Quaternion swayRotation = Quaternion.AngleAxis(swayOffset, swayAxis);

       
        transform.rotation = startRotation * tiltRotation * swayRotation;
    }


}
