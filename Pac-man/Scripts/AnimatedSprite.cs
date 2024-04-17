using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite[] sprites;
    public float animationTime = 0.25f;
    public int animationFrame;
    public bool loop;
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        InvokeRepeating(nameof(NextAnimationFrame), animationTime, animationTime);
    }

    private void NextAnimationFrame()
    {
        animationFrame++;
        
        if(animationFrame >= sprites.Length && loop)
        {
            animationFrame = 0;
        }

        if (animationFrame >= 0 && animationFrame < sprites.Length)
        {
            spriteRenderer.sprite = sprites[animationFrame];
        } 
    }

    public void RestartAnimation()
    {
        animationFrame = 0;
    }
}
