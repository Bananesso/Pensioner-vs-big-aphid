using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Tooltip("Индекс сцены, на которую нужно перейти")]
    public int targetSceneIndex = 0;
    
    [Tooltip("Время в секундах перед переходом на другую сцену")]
    public float delayTime = 5f;

    private void Start()
    {
        // Запускаем корутину для задержки перед переходом
        StartCoroutine(ChangeSceneAfterDelay());
    }

    private System.Collections.IEnumerator ChangeSceneAfterDelay()
    {
        // Ждем указанное количество секунд
        yield return new WaitForSeconds(delayTime);
        
        // Переключаем сцену
        SceneManager.LoadScene(targetSceneIndex);
    }
}