using UnityEngine;

public class TimeChangeButton : MonoBehaviour
{
    [SerializeField] private float TimeChangeFloat;
    public void TimeChange()
    {
        Time.timeScale = TimeChangeFloat;
    }
}