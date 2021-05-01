using UnityEngine;
using UnityEngine.UI;//これは忘れる
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverText;//試してないけどprefabにも出来る？ここでセット内容指定してたのかな
    [SerializeField] GameObject gameClearText;
    [SerializeField] PlayableDirector StartTimeline;//Startの時にTimelineを再生
    [SerializeField] PlayableDirector GameOverTimeline;
    [SerializeField] PlayableDirector GameClearTimeline;

    public enum GAME_STATUS
    {
        START,
        GAME,
        END,
    }

    public GAME_STATUS gameStatus = GAME_STATUS.START;

    void Start() 
    {
        //開始と同時に再生してみる
        StartTimeline.Play();
        //終わったので状態をGAMEにする
        gameStatus = GAME_STATUS.GAME;
    }

    public void GameOver() //ゲームオーバーな更新
    {
        gameOverText.SetActive(true);//SetActive使えるけどTimelineに入れても良い
        GameOverTimeline.Play();
        Time.timeScale = 0f;//動きとめてみた
        RestartScene();
    }

    public void GameClear() //ゲームクリアな更新
    {
        gameClearText.SetActive(true);
        GameClearTimeline.Play();
        Time.timeScale = 0f;
        RestartScene();
    }

    void RestartScene() 
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
    }

}
