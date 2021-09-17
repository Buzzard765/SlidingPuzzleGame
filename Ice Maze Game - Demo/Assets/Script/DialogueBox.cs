using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    //private Queue<string> Sentence = new Queue<string>();
    public NPCDialogue CurrNPC;

    //public DialogueManager AllDialogues;

    public GameObject DialoguePanel;
    public PlayerSlidingMovement PlayerStatus;
    //public Text NameText;
    public Text DialogueText;
    public int DialogueIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        PlayerStatus = GetComponent<PlayerSlidingMovement>();
        //DialoguePanel = GameObject.Find("Panel");
        // Sentence.Clear();
        CurrNPC = GetComponent<NPCDialogue>();
        DialogueText.text = "Click to Continue";
    }

    // Update is called once per frame
    void Update()
    {      
        //TurnOnDialogueBox(CharDialogue);
        Talk();
    }

    public void Talk() {

        if (CurrNPC == null) {
            return;
        }
               
        DialogueText.text = CurrNPC.CurrSentence.ToString();
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) && PlayerStatus.IsTalking == true)
        {
            bool IsTalking = CurrNPC.DisplayNextLine();
            if (IsTalking == false)
            {
                EndDialogue();
            }
            else {
                print(DialogueIndex);
                DialogueIndex++;
                if (DialogueIndex == CurrNPC.CharacterDialogues.DialogueLines.Length)
                    DialogueIndex--;
            }           
        }
    }
    
    public void TurnOnDialogueBox(CharacterObject character) {

       /* if (CharDialogue != null) {
            foreach (string boxSentence in CharDialogue.)
            {
                Sentence.Enqueue(boxSentence);
            }
        } */
        //DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        
        Debug.Log("Sentence " + CurrNPC.NPCSentences.Count);
        if (CurrNPC.NPCSentences.Count == 0)//DialogueIndex == CharDialogue.CharLines.Length
        {

            //CurrNPC.IsTalking = false;
            //EndDialogue();
            //DialogueIndex = 0;
            //EndDialogue();
            //TurnOnDialogueBox(CharDialogue);
            PlayerStatus.IsTalking = false;
            //PlayerStatus.PlayerRB.ve
            // close dialogue box
        }
        else {
            //string NextSentence = CurrNPC.NPCSentences.Dequeue();
            //DialogueText.text = NextSentence;
            //DialogueIndex++;
            PlayerStatus.IsTalking = true;
            //DialoguePanel.SetActive(true);
        }       
    }

    void EndDialogue() {
        DialoguePanel.SetActive(false);
        CurrNPC.DisplayNextLine();
    }
}
