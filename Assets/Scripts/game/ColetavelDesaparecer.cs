using UnityEngine;

public class ColetavelDesaparecer : MonoBehaviour
{
    public float lifetime = 5f;         
    public float fadeDuration = 1f;     

    private SpriteRenderer sr;
    private float fadeTimer = 0f;
    private bool isFading = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Invoke(nameof(IniciarFade), lifetime);
    }

    void Update()
    {
        if (isFading)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeDuration);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

            if (fadeTimer >= fadeDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    void IniciarFade()
    {
        isFading = true;
    }
}
