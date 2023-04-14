using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public static int level;

    public List<GameObject> disabledBricks = new List<GameObject>();

    [SerializeField] private GameObject[] levels;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private Animator restartLevelAnimator;
    [SerializeField] private TextMeshProUGUI animationTextGUI;
    [SerializeField] private TextMeshProUGUI lifeTextGUI;   

    private int brickCount;
    private int life = 3;

    private float timer;

    private bool isGameStarted;
    private bool isGameOver = false;
    private bool isGameRestarting = false;

    private readonly float animationTime = 3f;
    private readonly float countDown = 10f;

    private int Life
    {
        get => life;
        set
        {
            life = value;

            lifeTextGUI.SetText(" x {0}",life);
        }
    }

    public bool IsGameStarted
    {
        get => isGameStarted;
        set => isGameStarted = value;
    }
    public int BrickCount
    {
        get => brickCount;
        set
        {
            timer = countDown;
            if (value > 0)
            {
                brickCount = value;
            }
            else
            {
                isGameStarted = false;
                LevelSuccess();
            }
        }
    }
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        brickCount = GetBrickCount();
        StartCoroutine(StartAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameStarted)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                restartLevelAnimator.SetTrigger("Remind");
                timer = countDown;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }
    private void GetNewBall()
    {
        IsGameStarted = false;
    }
    public void BallOutOfScreen()
    {
        if (--Life > 0)
        {
            Invoke("GetNewBall", 1f);
        }
        else
        {
            GameOver();
        }
    }
    private void RestartLevel()
    {
        isGameStarted = false;
        isGameRestarting = true;
        animationTextGUI.SetText("LEVEL IS RESTARTING");
        StartCoroutine(EndAnimation());
    }
    private void SetLevel()
    {
        ResetBricks();
        if (!isGameRestarting)
        {
            levels[level++].SetActive(false);
            levels[level].SetActive(true);
        }
        brickCount = GetBrickCount();
    }
    private void ResetBricks()
    {
        for (int i = 0; i < disabledBricks.Count; i++)
        {
            disabledBricks[i].SetActive(true);
        }
        disabledBricks.Clear();
    }

    private void LevelSuccess()
    {
        if (level + 1 < levels.Length)
        {
            animationTextGUI.SetText("LEVEL SUCCEEDED");
            StartCoroutine(EndAnimation());
        }
        else
        {
            isGameOver = true;
            animationTextGUI.SetText("CONGRATULATIONS<br>YOU FINISH THE GAME !");
            StartCoroutine(EndAnimation());
        }
    }
    private void GameOver()
    {
        isGameOver = true;
        animationTextGUI.SetText("YOU LOST<br>GAME OVER");
        StartCoroutine(EndAnimation());
    }
    private IEnumerator StartAnimation()
    {
        animationTextGUI.SetText("LEVEL {0}", level + 1);
        transitionAnimator.SetTrigger("Start");

        yield return new WaitForSeconds(animationTime);
    }
    private IEnumerator EndAnimation()
    {
        Ball.instance.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transitionAnimator.SetTrigger("End");
        yield return new WaitForSeconds(animationTime);

        if (isGameOver)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SetLevel();
            StartCoroutine(StartAnimation());
        }
    }
    private int GetBrickCount()
    {
        string levelName = "Level " + (level + 1).ToString();
        return GameObject.Find(levelName).transform.childCount;
    }
}
