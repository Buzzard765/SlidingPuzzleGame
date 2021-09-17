using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectionUI : MonoBehaviour
{

    public List<GameObject> UIGroup = new List<GameObject>();
    int index;
    public Button PrevBtn, NextBtn;
    // Start is called before the first frame update
    void Start()
    {
        UIGroupDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UIGroupDisplay()
    {
        foreach (GameObject GroupDisplay in UIGroup)
        {
            GroupDisplay.SetActive(false);
        }
        UIGroup[index].SetActive(true);
        PrevBtn.gameObject.SetActive(false);
        NextBtn.gameObject.SetActive(true);
    }

    public void Next()
    {
        UIGroup[index].SetActive(false);
        index++;
        if (index == UIGroup.Count - 1)
        {
            NextBtn.gameObject.SetActive(false);
            PrevBtn.gameObject.SetActive(true);
        }
        else
        {
            PrevBtn.gameObject.SetActive(true);
            NextBtn.gameObject.SetActive(true);
        }
        UIGroup[index].SetActive(true);
    }

    public void Previous()
    {
        UIGroup[index].SetActive(false);
        index--;
        if (index == 0)
        {
            PrevBtn.gameObject.SetActive(false);
            NextBtn.gameObject.SetActive(true);
        }
        else
        {
            NextBtn.gameObject.SetActive(true);
            PrevBtn.gameObject.SetActive(true);
        }
        UIGroup[index].SetActive(true);
    }
}
