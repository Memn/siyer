using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Archery : MonoBehaviour
{

    public enum SpriteType { Landscape, target, Arrow, BowAndHandsPulled, BowAndHandsReady, BowAndHandsReleased };

    [SerializeField]
    private SpriteAtlas atlas;

    private SpriteRenderer spriteRenderer;
    public SpriteType currentSprite;

    private SpriteType lastSprite;


    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = atlas.GetSprite(currentSprite.ToString());
		lastSprite = currentSprite;
    }

    void Update()
    {
        ChangeSprite();
    }

    public void ChangeSprite()
    {
		if (currentSprite != lastSprite)
        {
            spriteRenderer.sprite = atlas.GetSprite(currentSprite.ToString());
            lastSprite = currentSprite;
        }

    }

}
