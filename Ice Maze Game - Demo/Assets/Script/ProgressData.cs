using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressData
{
        
    public int LevelReached;
    public int[] status;    

    public ProgressData(LevelSelector levelSelector)
    {
        status = new int[levelSelector.status.Length];
        LevelReached = levelSelector.levelReached;

        for (int i = 0; i < levelSelector.status.Length; i++)
        {
            status[i] = levelSelector.status[i];
        }
    }
}
