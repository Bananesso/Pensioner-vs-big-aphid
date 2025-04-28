using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListikiPodschet : MonoBehaviour
{
    public TMP_Text Materials;
    public TMP_Text Listiki;
    public int KolichestvoMaterialov;
    public int KolichestvoListikov = 25;

    private void FixedUpdate()
    {
        Materials.text = KolichestvoMaterialov.ToString();
        Listiki.text = KolichestvoListikov.ToString();
    }

    private void OnValidate()
    {
        Materials.text = KolichestvoMaterialov.ToString();
        Listiki.text = KolichestvoListikov.ToString();
    }
}