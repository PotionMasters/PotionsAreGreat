
using UnityEngine;

public class RandomSprite : MonoBehaviour {

    [SerializeField]
    private Sprite[] sprites;

    private SpriteRenderer spriteRenderer;

    void Start ()
    {
        RandomAppearance();

    }

    private void RandomAppearance()//we might want to find a way to make it so the same sprite isn't chosen twice, basically reorder then do a for i++ loop
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        int chooseRandomSprite = Random.Range(0, sprites.Length);

        spriteRenderer.sprite = sprites[chooseRandomSprite];
    }

}
