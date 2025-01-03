using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class WinCollision : MonoBehaviour
{
    public GameObject VictoryScreenParent; // Reference to the parent GameObject containing VictoryScreen
    public Text gameOverText;             // Reference to the Game Over text element
    public AudioSource audioSource;       // Audio source for playing sounds
    public AudioClip VictorySFX;          // Audio clip for the victory sound

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Play the victory sound
            PlayGameOverSound();

            // Destroy the player game object
            Destroy(collision.gameObject);

            // Enable the VictoryScreen parent object
            EnableVictoryScreen();

            // Start a coroutine to delay scene transition
            StartCoroutine(DelaySceneTransition());
        }
    }

    void EnableVictoryScreen()
    {
        // Ensure VictoryScreenParent is not null and activate it
        if (VictoryScreenParent != null)
        {
            VictoryScreenParent.SetActive(true);
        }

        // Update the victory text if needed
        if (gameOverText != null)
        {
            gameOverText.text = "Victory!";
            gameOverText.gameObject.SetActive(true);
        }
    }

    void PlayGameOverSound()
    {
        if (audioSource != null && VictorySFX != null)
        {
            audioSource.clip = VictorySFX;
            audioSource.Play();
        }
    }

    private IEnumerator DelaySceneTransition()
    {
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next scene
    }
}
