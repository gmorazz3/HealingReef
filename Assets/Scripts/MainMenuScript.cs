using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public AudioClip waterBubbleSound;
    public AudioClip hoverMenuSound;
    public AudioSource audioSource; // Drag manually in Inspector

    public void PlayGame()
    {
        StartCoroutine(PlayAndLoad());
    }

    IEnumerator PlayAndLoad()
    {
        audioSource.PlayOneShot(waterBubbleSound);
        yield return new WaitForSeconds(waterBubbleSound.length);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void PlayHoverSound()
    {
        audioSource.PlayOneShot(hoverMenuSound);
    }



    public void QuitGame()
    {
        StartCoroutine(PlaySoundAndQuit());
    }

    IEnumerator PlaySoundAndQuit()
    {
        audioSource.PlayOneShot(waterBubbleSound);

        // Wait just a short time for the pop sound
        yield return new WaitForSeconds(0.2f);

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

