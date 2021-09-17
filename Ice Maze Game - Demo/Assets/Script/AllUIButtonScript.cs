using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AllUIButtonScript : MonoBehaviour
{
    public LevelSelector Data;
    
    [System.Serializable]
    public class LevelStatus {
        public Button unlockedlevels;
        public Sprite Blank, CompletePic, IncompletePic;
    }

    public List<LevelStatus> LevelButtons = new List<LevelStatus>();

    //public Button[] unlockedlevels;
    
    //public Sprite Blank, CompletePic, IncompletePic;
    public Slider[] SoundSlider;

    public GameObject ScreenDim;
    public bool pause;
    public GameObject Guide;
    public AudioSource ButtonClick;
    public AudioClip ClickEnter, BookFold, ClickBack;

    //public GameObject RestartPanel;

   
    // Start is called before the first frame update
    void Start()
    {
        Data = GameObject.Find("Level Selector").GetComponent<LevelSelector>();
        try
        {
            Data.LoadGame();
        }
        catch { }
        RefreshLevelButtons();       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshLevelButtons()
    {
        for (int i = 0; i < LevelButtons.Count; i++)
        {               
            //Ranking[i].gameObject.SetActive(false);
            LevelButtons[i].unlockedlevels.interactable = false;
            LevelButtons[i].unlockedlevels.image.sprite = null;
        }
       
        for (int i = 0; i <= Data.levelReached; i++)
        {
            if (i == LevelButtons.Count) {
                break;
            }
                LevelButtons[i].unlockedlevels.interactable = true;
            //unlockedlevels[i].interactable = true;
            Debug.Log(i);            
            if (Data.status[i] == 1)
            {
                LevelButtons[i].unlockedlevels.image.sprite = LevelButtons[i].CompletePic;
            }              
            else if (Data.status[i] == 0)
            {
                LevelButtons[i].unlockedlevels.image.sprite = LevelButtons[i].IncompletePic;
            } 
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void PauseButton(GameObject PausePanel)
    {
        pause = !pause;
        if (pause)
        {
            Time.timeScale = 0;
            //ScreenDim.SetActive(true);
            PausePanel.SetActive(true);
        }
        else if (!pause)
        {
            Time.timeScale = 1;
            // ScreenDim.SetActive(true);
            PausePanel.SetActive(false);
        }
    }

    public void MuteButton()
    {
        if (PlayerPrefs.GetInt("Muted", 0) == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
            //AudioListener.pause = true;
            AudioListener.volume = 0;
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
            //AudioListener.pause = false;
            AudioListener.volume = 1;
        }
    }

    public void confirmQuit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        //Destroy(levelSelector.gameObject);
        Destroy(Data.transform.parent.gameObject);        
    }

    public void Back(GameObject SectionPanel) {
        SectionPanel.SetActive(false);
        ButtonClick.PlayOneShot(ClickBack);
    }

    public void EnterSection(GameObject SectionPanel) {
        SectionPanel.SetActive(true);
        ButtonClick.PlayOneShot(ClickEnter);
    }

    public void enterLevel(int SceneLevel) {
        SceneManager.LoadSceneAsync(SceneLevel);
        ButtonClick.PlayOneShot(ClickEnter);
    }

    public void ToggleSound(float value) {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
