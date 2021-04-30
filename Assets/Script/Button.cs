using UnityEngine;
using System.Collections;
using UnityEngine.Playables;

public class Button : MonoBehaviour
{
    [SerializeField] PlayableDirector StartTimeline;


    /// �{�^�����N���b�N�������̏���
    public void OnClick() {
        Debug.Log("Button click!");
        StartTimeline.Play();
    }
}