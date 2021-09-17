using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


[CreateAssetMenu(fileName = "Character Statistic", menuName ="ScriptableObject")]
public class CharacterObject : ScriptableObject
{
    public string Name;
    [TextArea(2, 5)]
    public string[] DialogueLines;
    public Queue<string> LineSequence = new Queue<string>();

    // Start is called before the first frame update
    void Start()
    {
        //LineSequence.Clear();
        //EnqueueDialogues(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnqueueDialogues()
    {
        /*foreach (string Sentences in DialogueLines) {
            LineSequence.Enqueue(Sentences);
        }*/
    }

    public void DisplayNextLine()
    {
       /* Debug.Log("Sentence " + LineSequence.Count);
        if (LineSequence.Count == 0)//DialogueIndex == CharDialogue.CharLines.Length
        {           
            EnqueueDialogues();            
        }
        else
        {
            string NextSentence = LineSequence.Dequeue();          
        }*/

    }
}
