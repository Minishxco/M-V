using UnityEngine;

public class YSort : MonoBehaviour
{
    public Transform player;
    private SpriteRenderer playerRenderer;

    public float yPosition;

    public int playerFrontOrder = 3;
    public int playerBackOrder = 0;

    void Start()
    {
        if (player != null)
            playerRenderer = player.GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (player == null || playerRenderer == null) return;

        if (player.position.y < yPosition)
        {
            playerRenderer.sortingOrder = playerFrontOrder;
        }
        else
        {
            playerRenderer.sortingOrder = playerBackOrder;
        }
    }
}