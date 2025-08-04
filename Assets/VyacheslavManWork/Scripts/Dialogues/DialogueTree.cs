using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Tree", menuName = "Dialogue/Tree")]
public class DialogueTree : ScriptableObject
{
    public DialogueNode startNode;
}