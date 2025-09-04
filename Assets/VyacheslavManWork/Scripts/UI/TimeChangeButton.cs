using UnityEngine;

public class TimeChangeButton : MonoBehaviour
{
    public void TimeChange(float timeSpeed = 1)
    {
        Time.timeScale = timeSpeed;
    }
}