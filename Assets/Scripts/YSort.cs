using UnityEngine;

public class YSort : MonoBehaviour
{
    public Transform player;
    private SpriteRenderer playerRenderer;

    public bool useThreeZones = false;

    public float yPositionFront;
    public float yPositionBack;

    public int playerFrontOrder = 3;
    public int playerMiddleOrder = 1;
    public int playerBackOrder = 0;

    void Start()
    {
        if (player != null)
            playerRenderer = player.GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        if (player == null || playerRenderer == null) return;

        float playerY = player.position.y;

        if (useThreeZones)
        {
            if (playerY < yPositionFront)
            {
                playerRenderer.sortingOrder = playerFrontOrder;
            }
            else if (playerY < yPositionBack)
            {
                playerRenderer.sortingOrder = playerMiddleOrder;
            }
            else
            {
                playerRenderer.sortingOrder = playerBackOrder;
            }
        }
        else
        {
            if (playerY < yPositionFront)
            {
                playerRenderer.sortingOrder = playerFrontOrder;
            }
            else
            {
                playerRenderer.sortingOrder = playerBackOrder;
            }
        }
    }
}