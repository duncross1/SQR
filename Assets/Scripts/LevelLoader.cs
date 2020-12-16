using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transistion;
    public Animator nextLevelTransistion;

    public float restartTransitionTime = 1f;
    public float nextLevelTransitionTime = 1f;

    public void loadNextLevel()
    {
        //transistion = nextLevelTransitions[0];
        StartCoroutine(NextLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void loadGivenLevel(int levelIndexIn)
    {
        StartCoroutine(NextLevel(levelIndexIn));
    }

    public void loadCurrentLevel()
    {
        StartCoroutine(reloadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator reloadLevel(int levelIndex)
    {
        //Play animation
        transistion.SetTrigger("Start");
        //Wait
        yield return new WaitForSeconds(restartTransitionTime);
        //LoadScene
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator NextLevel(int levelIndex)
    {
        //Play animation
        nextLevelTransistion.SetTrigger("Start");
        //Wait
        yield return new WaitForSeconds(nextLevelTransitionTime);
        //LoadScene
        SceneManager.LoadScene(levelIndex);
    }
}