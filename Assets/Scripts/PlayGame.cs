using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void playGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void instructionTostartPage(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
    public void StartToInstructionScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void EndToStartScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
    public void EndToPlayScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
    public void SuccessToPlayScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }
    public void SuccessToMainMenu(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }
    
    public void Quit(){
        Application.Quit();
    }
}