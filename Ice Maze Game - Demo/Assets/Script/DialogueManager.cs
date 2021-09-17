using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Queue<CharacterObject> CharLines = new Queue<CharacterObject>();

    [System.Serializable]
    public class CharBox
    {
        public Image CharImage;
        public string name;
        public string[] line;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
