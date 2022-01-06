using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject level;
    public Text scoreTextInGame;
    public Text scoreTextFinish;
    public bool levelFinish = false;

    public GameObject MainMenu;
    public GameObject InGame;
    public GameObject Finish;
    public GameObject cam;

    private GameObject activeLevel;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelManager.Instance != null)
        {
            if (!levelFinish && LevelManager.Instance.score >= 100)
            {
                InGame.SetActive(false);
                Finish.SetActive(true);
                scoreTextFinish.text = "SCORE : " + LevelManager.Instance.score.ToString();
                levelFinish = true;

            }
        }
        
    }

    public void LevelStartForMainMenu()
    {
        activeLevel = Instantiate(level, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void LevelStartForFinishMenu()
    {
        cam.transform.localPosition = new Vector3(7.2f, 8, -16.5f);
        Destroy(activeLevel);
        activeLevel = Instantiate(level, new Vector3(0, 0, 0), Quaternion.identity);
        scoreTextFinish.text = "SCORE : 0";
        scoreTextInGame.text = "SCORE : 0";
        levelFinish = false;

    }
}
