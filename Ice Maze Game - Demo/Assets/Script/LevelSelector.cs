using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour {

    
    public int levelReached;
    public int[] status;
    //public float MasterVolumeValue, BGMValue, SFXValue;
    //public LevelSelector ThisObject;
    //public CoreGameManager Data;
    
    //public int[] LvlRank;
    //public bool[] levelStatus;
    //public Sprite RankA, RankB, RankC, RankD, Locked;
    //public Text Highscore;

    /*private void Awake()
    {
        if (ThisObject != null)
        {
            Destroy(gameObject);
        }
        else
        {
            ThisObject = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }*/

    // Use this for initialization
    void Start() {

        try
        {
            LoadGame();
        } catch { }
        RefreshLevel();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void RefreshLevel()
    {
        /*for (int i = 0; i < unlockedlevels.Length; i++)
        {
            unlockedlevels[i].interactable = false;
            //Ranking[i].gameObject.SetActive(false);
        }

        for (int i = 0; i <= levelReached; i++)
        {
            unlockedlevels[i].interactable = true;
            Debug.Log(i);
            /*if (LvlRank[i] != 0)
            {
                Ranking[i].gameObject.SetActive(true);
                if (LvlRank[i] == 1)
                {
                    Ranking[i].sprite = RankD;
                }
                else if (LvlRank[i] == 2)
                {
                    Ranking[i].sprite = RankC;
                }
                else if (LvlRank[i] == 3)
                {
                    Ranking[i].sprite = RankB;
                }
                else if (LvlRank[i] == 4)
                {
                    Ranking[i].sprite = RankA;
                }
            }
            else
            {
                Ranking[i].gameObject.SetActive(false);
            }
        }

        /*try
        {
            for (int i = levelReached + 1; i < Ranking.Length; i++)
            {
                Ranking[i].gameObject.SetActive(true);
                Ranking[i].sprite = Locked;
            }
        } catch {
            Debug.Log("in");
        }*/
    }

    public void SaveGame() {
        SaveSystem.SaveGame(this);
        Debug.Log("saving");
    }

    /*public void AddLevel()
    {
        /*if (levelReached < levelNumber)
        {
            levelReached = levelNumber;
        }
        
            levelReached = Data.gamelevel;
        }
    }*/

    public void LoadGame() {
        ProgressData data = SaveSystem.LoadGame();
        levelReached = data.LevelReached;
        for (int i = 0; i <data.status.Length; i++)
        {
            status[i] = data.status[i];
        }
    }
    
    
}
