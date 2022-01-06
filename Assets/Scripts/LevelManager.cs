using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public GameObject parent;
    public GameObject Collectable;
    public int score = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(canCreate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator canCreate()
    {
        yield return new WaitUntil(() => PlayerScript.Instance.collect);
        Instantiate(Collectable,new Vector3(Random.Range(2, 10f), 0.41f, Random.Range(-7.62f, 14.28f)),Quaternion.identity,parent.transform);
        PlayerScript.Instance.collect = false;
        score += 10;
        GameManager.Instance.scoreTextInGame.text = "SCORE : " + score.ToString();
        StartCoroutine(canCreate());
    }
}
