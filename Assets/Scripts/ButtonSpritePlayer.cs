using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ButtonSpritePlayer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private int character;

    public Sprite characterA;
    public Sprite characterB;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        character = UserDataLoader.LoadCharacter();

        if (character == 1)
        {
            spriteRenderer.sprite = characterA;
        }
        else if (character == 2)
        {
            spriteRenderer.sprite = characterB;
        }
    }
}