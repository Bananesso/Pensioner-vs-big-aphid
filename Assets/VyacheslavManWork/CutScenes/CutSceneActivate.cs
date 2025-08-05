using UnityEngine;
using UnityEngine.Playables;

public class CutSceneActivate : MonoBehaviour, IInteractWithObj
{
    [SerializeField] private GameObject _cutSceneObject;
    public void Interact()
    {
        PlayableDirector cutScene = _cutSceneObject.GetComponent<PlayableDirector>();
        {
            cutScene.Play();
        }
    }
}