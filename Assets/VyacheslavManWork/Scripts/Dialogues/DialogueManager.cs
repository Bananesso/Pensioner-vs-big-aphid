using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private DialogueTree _dialogueTree;

    [SerializeField] private TMP_Text _dialogueSpeaker;

    [SerializeField] private TMP_Text _dialogueText;

    [SerializeField] private List<Button> _answerButtons;

    [SerializeField] private PlayableDirector _cutscene;

    private DialogueNode _currentNode;
    void Start()
    {
        ShowNode(_dialogueTree.startNode);
    }

    public void ShowNextNode()
    {
        if (_currentNode.nextNodes != null && _currentNode.nextNodes.Count > 0)
        {
            _currentNode = _currentNode.nextNodes[0];
            ShowNode(_currentNode);
        }
        else
        {
            _cutscene.time = _cutscene.duration-1;
        }
    }

    public void ShowNode(DialogueNode node)
    {
        _dialogueSpeaker.text = node.speakerName + ":";

        _dialogueText.text = node.text;

        if (node.answers != null && node.answers.Count > 0)
        {
            for (int i = 0; i < node.answers.Count; i++)
            {
                _answerButtons[i].GetComponentInChildren<TMP_Text>().text = node.answers[i];
                _answerButtons[i].gameObject.SetActive(true);

                int answerIndex = i;

                _answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answerIndex));
            }
            _cutscene.Pause();
        }
        _currentNode = node;
    }

    private void OnAnswerSelected(int answerIndex)
    {
        if (_currentNode.nextNodes != null && _currentNode.nextNodes.Count > answerIndex)
        {
            if (_currentNode.playerPrefsKeys != null &&
                _currentNode.playerPrefsValues != null &&
                _currentNode.playerPrefsKeys.Count > answerIndex &&
                _currentNode.playerPrefsValues.Count > answerIndex)
            {
                //как бы, смысла в этом немного, но читается код легче, поэтому пусть будет так
                string key = _currentNode.playerPrefsKeys[answerIndex];
                int value = _currentNode.playerPrefsValues[answerIndex];
                PlayerPrefs.SetInt(key, value);
                PlayerPrefs.Save();
            }

            DialogueNode nextNode = _currentNode.nextNodes[answerIndex];

            foreach (Button button in _answerButtons)
            {
                button.gameObject.SetActive(false);
            }
            _cutscene.Resume();
            ShowNode(nextNode);
        }
    }
}