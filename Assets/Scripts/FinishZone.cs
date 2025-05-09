using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishZone : MonoBehaviour
{
    public float pullSpeed = 5f;
    public float ascendTime = 2f;
    public float fadeDuration = 1f;
    public string nextSceneName = "EndScene";

    private bool pulling = false;
    private Rigidbody2D playerRb;
    private GameObject player;
    private CanvasGroup fadeGroup;

    void Start()
    {
        // Find the fade panel in the scene
        GameObject fadePanel = GameObject.Find("FadePanel");
        if (fadePanel != null)
        {
            fadeGroup = fadePanel.GetComponent<CanvasGroup>();
            fadeGroup.alpha = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!pulling && other.CompareTag("Player"))
        {
            pulling = true;
            player = other.gameObject;
            playerRb = player.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
                playerRb.gravityScale = 0;
                playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            StartCoroutine(AscendThenFade());
        }
    }

    System.Collections.IEnumerator AscendThenFade()
    {
        float timer = 0f;

        while (timer < ascendTime)
        {
            if (player != null)
            {
                player.transform.position += Vector3.up * pullSpeed * Time.deltaTime;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Start fade
        if (fadeGroup != null)
        {
            float fadeTimer = 0f;
            while (fadeTimer < fadeDuration)
            {
                fadeTimer += Time.deltaTime;
                fadeGroup.alpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeDuration);
                yield return null;
            }
        }

        // Load next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
