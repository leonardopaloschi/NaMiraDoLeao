using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void RestartGame()
    {
        Time.timeScale = 1f;
        GameObject bgMusicGame = GameObject.FindGameObjectWithTag("BackgroundGameMusic");
        if (bgMusicGame != null)
        {
            AudioSource bgMusic = bgMusicGame.GetComponent<AudioSource>();
            if (bgMusic != null)
            {
                bgMusic.Pause();
            }
        }
        UIManager.Instance.ResetStats();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
