using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Color Fader;
    public UIType AllUI;
    public Image image;
    public Text text;
    public enum UIType { AllImage, AllText}
    float FadeTime;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (AllUI == UIType.AllText)
            FadeIn(text.color);
        else if (AllUI == UIType.AllImage) {
            FadeIn(image.color);
        }
    }

    public IEnumerator FadeIn(Color color) {
        FadeTime = 1f;
        //float FadeTime;
        print("test");
        Fader = new Color(Fader.r, Fader.g, Fader.b, 1);
        while ( FadeTime >0) {
            FadeTime -= Time.deltaTime;
            Fader = new Color(1f, 1f, 1f, FadeTime);
            yield return null;
        }       
    }
    public IEnumerator FadeOut()
    {
        print("test2");
        FadeTime = 0f;
        //float FadeTime;
        while (FadeTime <1)
        {
            FadeTime += Time.deltaTime;
            Fader= new Color(0f, 0f, 0f, FadeTime);
            yield return new WaitForSeconds(1f);
            
        }
        
    }
}
