using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCDialogue : MonoBehaviour
{
    
    public CharacterObject CharacterDialogues;
    public string NPCName;
    public Queue<string> NPCSentences = new Queue<string>();
    public string CurrSentence;
    public bool IsTalking = false;

    //public string NPCSentences;
    // Start is called before the first frame update

    private void Awake()
    {
        //NPCName = CharacterDialogues.Name;
    }
    void Start()
    {
        NPCName = CharacterDialogues.Name;
        //CurrSentence = ;
        //NPCSentences = CharacterDialogues.DialogueLines.ToString();
        EnqueueDialogues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnqueueDialogues()
    {
        if(IsTalking == false) { 
            foreach (string Sentences in CharacterDialogues.DialogueLines)
            {
                NPCSentences.Enqueue(Sentences);
            }
        }
        else {
            DisplayNextLine();
        }       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player Character") {
            IsTalking = true;
        }
    }

    public bool DisplayNextLine()
    {
        //print(NPCSentences.Count);
        Debug.Log("Sentence " + NPCSentences.Count);
        if (NPCSentences.Count == 0)//DialogueIndex == CharDialogue.CharLines.Length
        {          
            print("i'm done talking");
            //IsTalking = false;
            //EnqueueDialogues();            
            gameObject.SetActive(false);
            return false;
        }
        else
        {
            CurrSentence = NPCSentences.Dequeue();
            return true;
        }

    }


}
