using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    public float speed = 2f;
    public float lifetime = 6f;
    public float fadeDuration = 1f;
    public GameObject player;

    private Transform target; 
    private SpriteRenderer sr;
    private float fadeTimer;
    private bool isFading;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        Invoke(nameof(StartFade), lifetime);
        player = GameObject.FindWithTag("Player");
        StartCoroutine(CSetTarget());

    }

    public void SetTarget(Transform t) {  target = t; }

    IEnumerator CSetTarget()
    {
        if (player == null) { GameObject.FindWithTag("Player"); }
        while (true)
        {
            target = player.transform;
            yield return new WaitForSeconds(1.0f);
        }
    }

    void Update()
    {
        if (target != null && !isFading)
        {
            Vector2 direction = ((Vector2)target.position - (Vector2)transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);
        }

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

    void StartFade()
    {
        isFading = true;
    }
}
