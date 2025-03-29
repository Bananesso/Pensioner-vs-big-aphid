using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListikiPodschet : MonoBehaviour
{
    public TMP_Text Listiki;
    public int KolichestvoListikov = 25;

    private void FixedUpdate()
    {
        Listiki.text = KolichestvoListikov.ToString();
    }
}