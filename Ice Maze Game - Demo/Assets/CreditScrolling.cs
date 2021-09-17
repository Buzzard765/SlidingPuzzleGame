using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditScrolling : MonoBehaviour
{
    [TextArea(5, 5)]
    public List<string> CreditSequence = new List<string>();
    public Text CreditsText;
    int TextIndex;

    // Start is called before the first frame update
    void Start()
    {
        CreditsText = GameObject.Find("CreditsText").GetComponent<Text>();
        TextIndex = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        CreditsText.text = CreditSequence[TextIndex].ToString();
        if (Input.GetMouseButtonDown(0)) {
            sequence();
        }        
        Debug.Log(TextIndex);
        Debug.Log(CreditSequence.Count);
    }

    public void sequence() {
        if (TextIndex < CreditSequence.Count-1)
        {           
            CreditsText.text = CreditSequence[TextIndex].ToString();
            TextIndex++;
        }
        else if (TextIndex == CreditSequence.Count -1)
        {
            Debug.Log("backtoMainMenu");
            SceneManager.LoadScene(0);
        }       
    }
}
