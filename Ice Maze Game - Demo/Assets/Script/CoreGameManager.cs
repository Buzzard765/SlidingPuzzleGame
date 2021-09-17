using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoreGameManager : MonoBehaviour
{
    public Text LevelMonologue;
    public Animator TextFader;
    public Animator ScreenFader;

    public int gamelevel;
    public LevelSelector Data;
    public GameObject PlayField;
    public PlayerSlidingMovement PlayerCharacter;
    public bool GameStarted;
    public bool GameFinished;
    private bool Ending;

    // Start is called before the first frame update



    void Start()
    {
        Data = GameObject.Find("Level Selector").GetComponent<LevelSelector>();
        LevelMonologue = GameObject.Find("TextFader").GetComponent<Text>();
        ScreenFader = GameObject.Find("Fading").GetComponent<Animator>();
        TextFader = GameObject.Find("TextFader").GetComponent<Animator>();
        //TextFader.gameObject.SetActive(false);       
        PlayerCharacter = FindObjectOfType<PlayerSlidingMovement>();
        PlayField = GameObject.Find("Grid");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerCharacter.PlayerMonologueIntro.ToString());
        LevelMonologue.text = PlayerCharacter.PlayerMonologueIntro.ToString();
        Debug.Log(PlayerCharacter.PlayerMonologueIntro.ToString());
        //IntroEndingFader.FadeIn();
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine("Startgame");
        }
       // PlayField.SetActive(false);
        if (GameStarted == true) {
            
        }

        if (PlayerCharacter.GameFinished == true)
        {
            GameFinished = true;
            if (GameFinished == true)
            {
                AddLevel();
                StartCoroutine("FinishGame");
                if (Input.GetMouseButtonDown(0)) {
                    Data.SaveGame();
                    SceneManager.LoadScene(0);
                    Data.RefreshLevel();
                }
            }
        }

        if (PlayerCharacter.Ending == true)
        {
            Ending = true;
            if (Ending == true)
            {
                //AddLevel();
                StartCoroutine("FinishGame");
                if (Input.GetMouseButtonDown(0))
                {
                    Data.SaveGame();
                    SceneManager.LoadScene(13);
                    Data.RefreshLevel();
                }
            }
        }

    }

    public void AddLevel()
    {
        if (Data.levelReached < gamelevel)
        {
            Data.status[Data.levelReached] = 1;
            Data.levelReached = gamelevel;           
        }
    }

    private IEnumerator Startgame() {                 
        PlayField.SetActive(true);
        TextFader.SetTrigger("GameStart");
        ScreenFader.SetTrigger("GameStart");
        yield return new WaitForSeconds(2f);
        TextFader.gameObject.SetActive(false);
        ScreenFader.gameObject.SetActive(false);
        PlayerCharacter.GameStarted = true;
        GameStarted = true;
        //IntroEndingFader.FadeIn();       
    }

    private IEnumerator FinishGame() {
        TextFader.gameObject.SetActive(true);
        ScreenFader.gameObject.SetActive(true);
        LevelMonologue.text = PlayerCharacter.PlayerMonologueEnding.ToString();
        TextFader.SetTrigger("GameFinished");
        ScreenFader.SetTrigger("GameFinished");
        yield return null;
    }


}
