using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private void Awake() { Instance = this; }

    public delegate void ChangeCurrentLevel();
    public event ChangeCurrentLevel CurrentLevelChanger;

    private Move movementSc;

    private Animator _levelChangeAnim;
    private void Start()
    {
        _levelChangeAnim = GameObject.Find("LevelChangeAnim").GetComponent<Animator>();
        movementSc = GameObject.FindObjectOfType<Move>();
        CurrentLevelChanger += ChangeLevel; 
    }

    private void ChangeLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (SceneManager.GetActiveScene().buildIndex == 1) 
        { 
            SceneManager.LoadScene(nextLevelIndex); 
        }

        if (nextLevelIndex != 0) 
        {  
            Debug.Log("YOU FINISHED THE GAME!"); 
        }

    }

    public void InvokeCurrentLevelChanger()
    {
        movementSc.speed = 0;
        StartCoroutine(ChangeLevelAnim());
    }

    IEnumerator ChangeLevelAnim() 
    {
        _levelChangeAnim.SetTrigger("BlackIn");
        yield return new WaitForSeconds(1.5f);
        CurrentLevelChanger.Invoke();
    }
}
