using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneByIndex : MonoBehaviour
{
    public float delayTime = 3f;  // Через сколько секунд переключить сцену
    public int sceneIndex = 1;    // Индекс сцены в Build Settings (0, 1, 2...)

    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    System.Collections.IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneIndex);  // Загрузка по индексу
    }
}