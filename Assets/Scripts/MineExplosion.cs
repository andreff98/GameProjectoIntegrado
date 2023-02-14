using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MineExplosion : MonoBehaviour
{
    private ParticleSystem explosion;
    private AudioSource explosionSound;
    public GameObject efeitosVisuais;
    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        explosion = efeitosVisuais.GetComponent<ParticleSystem>();
        explosionSound = efeitosVisuais.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerDog") || other.gameObject.CompareTag("Player"))
        {
            ExplosionDetection();
        }
    }

    private void ExplosionDetection()
    {
        explosion.Play();
        explosionSound.Play();
        GameObject.Find("Dog").GetComponent<AudioSource>().Stop();

        Invoke("PauseGame", 1);
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        menu.SetActive(true);

        Invoke("ReloadScene", 2);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}