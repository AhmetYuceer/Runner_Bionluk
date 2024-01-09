using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject restartPanel;
    [SerializeField] private TextMeshProUGUI scoreText;

    private PlayerController playerController;

    //Score
    private int currentScore;
    [SerializeField] private int scoreAmount = 1;

    //Sound
    private AudioSource audioSource;
    [SerializeField] private AudioClip pickClip;
    [SerializeField] private AudioClip crashClip;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = PlayerController.Instance;
        currentScore = 0;
        startPanel.SetActive(true);
        restartPanel.SetActive(false);
        audioSource.Play();
    }

    //Sound 
    public void crashSound()
    {
        audioSource.PlayOneShot(crashClip);
    }

    //Score 
    public void AddScore(GameObject strawberry)
    {
        currentScore += scoreAmount;
        audioSource.PlayOneShot(pickClip);
        scoreText.text = currentScore.ToString();
        Destroy(strawberry);
    }

    // UI

    public void OpenRestartPanel()
    {
        restartPanel.SetActive(true);
    }

    public void StartGame()
    {
        playerController.StartingMoveing();
        startPanel.SetActive(false);
    }  

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}