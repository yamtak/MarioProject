using UnityEngine;
using UnityEngine.UI;//����͖Y���
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverText;//�����ĂȂ�����prefab�ɂ��o����H�����ŃZ�b�g���e�w�肵�Ă��̂���
    [SerializeField] GameObject gameClearText;
    [SerializeField] PlayableDirector StartTimeline;//Start�̎���Timeline���Đ�
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
        //�J�n�Ɠ����ɍĐ����Ă݂�
        StartTimeline.Play();
        //�I������̂ŏ�Ԃ�GAME�ɂ���
        gameStatus = GAME_STATUS.GAME;
    }

    public void GameOver() //�Q�[���I�[�o�[�ȍX�V
    {
        gameOverText.SetActive(true);//SetActive�g���邯��Timeline�ɓ���Ă��ǂ�
        GameOverTimeline.Play();
        Time.timeScale = 0f;//�����Ƃ߂Ă݂�
        RestartScene();
    }

    public void GameClear() //�Q�[���N���A�ȍX�V
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
