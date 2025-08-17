using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Node", menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    [TextArea(3, 10)] public string text;
    [TextArea(3, 10)] public List<string> answers;
    public string speakerName;
    public List<DialogueNode> nextNodes;

    [Header("Для репутации")]
    public List<string> playerPrefsKeys;
    public List<int> playerPrefsValues;

    [Header("Изображение говорящего")]
    public Sprite SpeakerImage;
}