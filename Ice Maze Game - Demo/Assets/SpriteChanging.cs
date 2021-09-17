using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteChanging : MonoBehaviour
{
    private SpriteRenderer CurrentSprite;
    public Sprite NewSprite;
    // Start is called before the first frame update
    void Start()
    {
        CurrentSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            CurrentSprite.sprite = NewSprite;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            CurrentSprite.sprite = NewSprite;
        }
    }
}
