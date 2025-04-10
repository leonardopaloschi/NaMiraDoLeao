using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    AudioSource audio;
    GameObject gameOverUI;
    private UIManager uiManager;


    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        gameOverUI = GameObject.FindWithTag("GameOverUI");
        gameOverUI.SetActive(false);
        uiManager = FindObjectOfType<UIManager>();

    }

    void FixedUpdate()
    {
        if (isDead) return;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.MovePosition(rb.position + movement.normalized * Time.fixedDeltaTime * speed);

        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(0.15f, 0.15f, 1f);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(-0.15f, 0.15f, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (isDead) return;

        if (c.CompareTag("Coletavel"))
        {
            audio.Play();
            Destroy(c.gameObject);
            FindFirstObjectByType<TriangleSpawner>()?.OnTriangleCollected();
            uiManager.AddCoin();

        }
        else if (c.CompareTag("Inimigo"))
        {
            isDead = true;
            AudioSource roar = c.GetComponent<AudioSource>();
            if (roar != null)
            {
                roar.Play();
            }
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        uiManager.ShowFinalStats();

        GameObject bgMusicGame = GameObject.FindGameObjectWithTag("BackgroundGameMusic");
        if (bgMusicGame != null)
        {
            AudioSource bgMusic = bgMusicGame.GetComponent<AudioSource>();
            if (bgMusic != null)
            {
                bgMusic.Pause();
            }
        }

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

    }
}
