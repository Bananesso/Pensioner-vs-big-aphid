using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RandomSceneLoader : MonoBehaviour
{
    [System.Serializable]
    public class SceneProbability
    {
        public string sceneName;
        [Range(0, 100)] public float probability;
    }

    [SerializeField] private Button loadButton;
    [SerializeField] private SceneProbability[] sceneProbabilities;

    private void OnEnable()
    {
        if (loadButton != null)
        {
            loadButton.onClick.AddListener(LoadRandomScene);
        }
    }

    private void OnDisable()
    {
        if (loadButton != null)
        {
            loadButton.onClick.RemoveListener(LoadRandomScene);
        }
    }

    private void LoadRandomScene()
    {
        // Calculate total probability
        float totalProbability = 0f;
        foreach (var scene in sceneProbabilities)
        {
            totalProbability += scene.probability;
        }

        // Normalize probabilities if they don't sum to 100
        if (totalProbability != 100f)
        {
            Debug.LogWarning("Probabilities don't sum to 100%. Normalizing...");
            for (int i = 0; i < sceneProbabilities.Length; i++)
            {
                sceneProbabilities[i].probability = (sceneProbabilities[i].probability / totalProbability) * 100f;
            }
        }

        // Generate random value
        float randomValue = Random.Range(0f, 100f);
        float cumulativeProbability = 0f;

        // Determine which scene to load based on probability
        foreach (var scene in sceneProbabilities)
        {
            cumulativeProbability += scene.probability;
            if (randomValue <= cumulativeProbability)
            {
                if (!string.IsNullOrEmpty(scene.sceneName))
                {
                    Debug.Log($"Loading scene: {scene.sceneName} (Random value: {randomValue}, Probability: {scene.probability}%)");
                    SceneManager.LoadScene(scene.sceneName);
                }
                else
                {
                    Debug.LogError("Scene name is empty!");
                }
                return;
            }
        }

        Debug.LogError("No scene was selected! Check your probability settings.");
    }
}