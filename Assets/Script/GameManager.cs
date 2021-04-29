using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject gameClearText;

    public void GameOver() 
    {
        gameOverText.SetActive(true);
    }

    public void GameClear() 
    {
        gameClearText.SetActive(true);
    }
}
