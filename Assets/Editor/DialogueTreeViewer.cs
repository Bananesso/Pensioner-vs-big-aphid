using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogueTreeViewer : EditorWindow
{
    private DialogueTree _dialogueTree;
    private Vector2 _scrollPos;
    private Dictionary<DialogueNode, Rect> _nodeRects = new Dictionary<DialogueNode, Rect>();

    [MenuItem("Tools/Dialogue Tree Viewer")]
    public static void ShowWindow()
    {
        GetWindow<DialogueTreeViewer>("Dialogue Tree Viewer");
    }

    private void OnGUI()
    {
        // Поле для выбора DialogueTree (как в вашем DialogueManager)
        _dialogueTree = (DialogueTree)EditorGUILayout.ObjectField("Dialogue Tree", _dialogueTree, typeof(DialogueTree), false);

        if (_dialogueTree == null || _dialogueTree.startNode == null)
        {
            EditorGUILayout.HelpBox("Assign a Dialogue Tree with a start node.", MessageType.Warning);
            return;
        }

        // Область с прокруткой
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

        // Отрисовка узлов и связей
        DrawNode(_dialogueTree.startNode, new Vector2(20, 20));

        EditorGUILayout.EndScrollView();
    }

    private void DrawNode(DialogueNode node, Vector2 position)
    {
        if (node == null) return;

        // Стиль для узла
        GUIStyle nodeStyle = new GUIStyle(GUI.skin.box);
        nodeStyle.padding = new RectOffset(10, 10, 10, 10);

        // Рассчитываем размер узла на основе текста
        Vector2 textSize = GUI.skin.label.CalcSize(new GUIContent(node.text));
        float width = Mathf.Max(200, textSize.x + 20);
        float height = 100 + (node.answers != null ? node.answers.Count * 30 : 0);

        Rect nodeRect = new Rect(position.x, position.y, width, height);
        _nodeRects[node] = nodeRect; // Запоминаем позицию узла для связей

        // Рисуем узел
        GUILayout.BeginArea(nodeRect, nodeStyle);
        EditorGUILayout.LabelField($"<b>{node.speakerName}</b>", new GUIStyle(GUI.skin.label) { richText = true });
        EditorGUILayout.LabelField(node.text, GUILayout.Height(40));

        // Рисуем ответы
        if (node.answers != null)
        {
            for (int i = 0; i < node.answers.Count; i++)
            {
                EditorGUILayout.LabelField($"? {node.answers[i]}");
            }
        }
        GUILayout.EndArea();

        // Рисуем связи к следующим узлам
        if (node.nextNodes != null)
        {
            for (int i = 0; i < node.nextNodes.Count; i++)
            {
                if (node.nextNodes[i] == null) continue;

                // Вычисляем позицию следующего узла (смещение для ветвления)
                Vector2 nextNodePos = new Vector2(position.x + width + 50, position.y + i * 150);
                DrawNode(node.nextNodes[i], nextNodePos);

                // Рисуем линию между узлами
                DrawNodeCurve(nodeRect, _nodeRects[node.nextNodes[i]]);
            }
        }
    }

    // Рисуем кривую между узлами
    private void DrawNodeCurve(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.gray, null, 2);
    }
}