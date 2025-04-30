using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class ConsoleErrorSimulator : MonoBehaviour
{
    [Header("TMP Settings")]
    public TMP_Text consoleText;
    public float scrollSpeed = 30f; // Скорость прокрутки
    public float lineDelay = 0.1f;  // Задержка между строками
    public int maxLines = 20;       // Макс. строк в консоли

    [Header("Error Pool")]
    public List<string> errorPool = new List<string>()
    {
        "TVSB//SYSTEM//CRITICAL_ERROR[0x7F3A9D2E]",
        ">> FATAL EXCEPTION IN CORE THREAD #4",
        ">> MEMORY VIOLATION AT 0x0048FF1C",
        ">> STACK OVERFLOW DETECTED",
        ">> SHADER PIPELINE CORRUPTION",
        "TVSB//ENGINE//RENDERER_FAILURE",
        ">> DX11_BUFFER_OVERWRITE (CODE: 0x887A0005)",
        ">> TEXTURE_ATLAS_CORRUPTED: \"MATERIALS/WORLD/TERRAIN_DIFFUSE.pak\"",
        ">> GPU DESYNC DETECTED - FORCING HARD RESET"
    };

    private Queue<string> activeLines = new Queue<string>();

    void Start()
    {
        consoleText.text = "";
        StartCoroutine(ErrorStreamRoutine());
    }

    IEnumerator ErrorStreamRoutine()
    {
        while (true)
        {
            // Добавляем случайную ошибку из пула
            string newError = errorPool[Random.Range(0, errorPool.Count)];
            AddErrorLine(newError);

            // Ждем перед следующей строкой
            yield return new WaitForSeconds(lineDelay);
        }
    }

    void AddErrorLine(string error)
    {
        // Добавляем новую строку в очередь
        activeLines.Enqueue(error);

        // Удаляем старые строки, если превышен лимит
        if (activeLines.Count > maxLines)
        {
            activeLines.Dequeue();
        }

        // Обновляем текст TMP
        consoleText.text = string.Join("\n", activeLines);

        // Прокрутка вниз (если нужно)
        consoleText.rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
    }

    // Для внешнего добавления ошибок (опционально)
    public void PushError(string customError)
    {
        AddErrorLine(customError);
    }
}