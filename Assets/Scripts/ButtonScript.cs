using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class StartGameButton : MonoBehaviour
{
    public Light2D buttonLight;
    public float glowDuration = 0.2f;

    public void StartGame()
    {
        StartCoroutine(ShineThenStart());
    }

    IEnumerator ShineThenStart()
    {
        if (buttonLight != null)
        {
            buttonLight.enabled = true;
            yield return new WaitForSeconds(glowDuration);
        }

        SceneManager.LoadScene("TutorialLevel");
    }
}
