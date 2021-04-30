using UnityEngine;
using System.Collections;
using UnityEngine.Playables;

public class Button : MonoBehaviour
{
    [SerializeField] PlayableDirector StartTimeline;


    /// ボタンをクリックした時の処理
    public void OnClick() {
        Debug.Log("Button click!");
        StartTimeline.Play();
    }
}