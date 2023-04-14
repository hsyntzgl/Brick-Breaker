using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    public List<GameObject> disabledBricks = new List<GameObject>();

    [SerializeField] private GameObject demoLevel;

    private int brickCount;
    private int defaultBrickCount;

    public int BrickCount
    {
        get => brickCount;
        set
        {
            if (value > 0)
            {
                brickCount = value;
            }
            else
            {
                Ball.instance.computerPlaying = false;
                brickCount = defaultBrickCount;
                ActiveAllBricks();
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
        defaultBrickCount = demoLevel.transform.childCount;
        brickCount = defaultBrickCount;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void ActiveAllBricks()
    {
        for (int i = 0; i < disabledBricks.Count; i++)
        {
            disabledBricks[i].SetActive(true);
        }
        disabledBricks.Clear();
    }
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
