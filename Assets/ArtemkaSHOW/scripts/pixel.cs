using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIDissolveEffect : MonoBehaviour
{
    [Header("Dissolve Settings")]
    [SerializeField] private Texture2D noiseTexture;
    [SerializeField] private float dissolveDuration = 1.5f;
    [SerializeField] private float pixelSize = 50f;
    [SerializeField] private Color edgeColor = Color.white;
    [SerializeField] private float edgeWidth = 0.1f;

    private Material dissolveMaterial;
    private Image image;
    private float dissolveAmount = 0f;
    private bool isDissolving = false;

    private void Awake()
    {
        image = GetComponent<Image>();

        // Создаем новый материал на основе шейдера
        dissolveMaterial = new Material(Shader.Find("UI/Dissolve"));
        image.material = dissolveMaterial;

        // Устанавливаем параметры материала
        dissolveMaterial.SetTexture("_NoiseTex", noiseTexture);
        dissolveMaterial.SetFloat("_PixelSize", pixelSize);
        dissolveMaterial.SetColor("_EdgeColor", edgeColor);
        dissolveMaterial.SetFloat("_EdgeWidth", edgeWidth);
    }

    public void StartDissolve()
    {
        if (!isDissolving)
        {
            isDissolving = true;
            dissolveAmount = 0f;
            InvokeRepeating("UpdateDissolve", 0f, 0.016f); // ~60 FPS
        }
    }

    private void UpdateDissolve()
    {
        dissolveAmount += Time.deltaTime / dissolveDuration;
        dissolveMaterial.SetFloat("_DissolveAmount", dissolveAmount);

        if (dissolveAmount >= 1f)
        {
            CancelInvoke("UpdateDissolve");
            isDissolving = false;
            gameObject.SetActive(false); // Отключаем объект после завершения
        }
    }

    private void OnDestroy()
    {
        if (dissolveMaterial != null)
        {
            Destroy(dissolveMaterial);
        }
    }
}