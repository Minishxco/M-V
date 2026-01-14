using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speedX = 4f;
    public float speedY = 3.5f;
    public bool canPlayerMove;
    public GameObject minY;
    public GameObject maxY;

    private Vector3 minScale;
    public float minScalePlayer;

    private Vector3 maxScale;
    public float maxScalePlayer;

    public float smooth = 8f;

    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        minScale = new Vector3(minScalePlayer, minScalePlayer, 1f);
        maxScale = new Vector3(maxScalePlayer, maxScalePlayer, 1f);
    }

    void Update()
    {
        if (!canPlayerMove) return;
        ReadMovementInput();

        // ---- Movimiento ----
        Vector3 delta = new Vector3(movement.x * speedX, movement.y * speedY, 0f) * Time.deltaTime;

        transform.position += delta;

        float clampedY = Mathf.Clamp(transform.position.y, minY.transform.position.y, maxY.transform.position.y);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

        float t = Mathf.InverseLerp(minY.transform.position.y, maxY.transform.position.y, transform.position.y);
        Vector3 target = Vector3.Lerp(maxScale, minScale, t);
        transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime * smooth);

        UpdateAnimations();
        UpdateFlip();
    }

    void ReadMovementInput()
    {
        movement = Vector2.zero;

        // Arriba
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            movement.y = 1;

        // Abajo
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            movement.y = -1;

        // Izquierda
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            movement.x = -1;

        // Derecha
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            movement.x = 1;

        // Normalizar para evitar mayor velocidad en diagonal
        if (movement.sqrMagnitude > 1)
            movement.Normalize();
    }


    // ----------------------------
    // ANIMACIONES
    // ----------------------------
    void UpdateAnimations()
    {
        bool up = movement.y > 0;
        bool down = movement.y < 0;
        bool left = movement.x < 0;
        bool right = movement.x > 0;

        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
        animator.SetBool("LR", false);
        animator.SetBool("Idle", false);

        if (up) animator.SetBool("Up", true);
        else if (down) animator.SetBool("Down", true);
        else if (left || right) animator.SetBool("LR", true);
        else animator.SetBool("Idle", true);
    }

    void UpdateFlip()
    {
        if (movement.x < 0)
            spriteRenderer.flipX = true;
        else if (movement.x > 0)
            spriteRenderer.flipX = false;
    }

    public void SetPlayerMovement(bool estado)
    {
        canPlayerMove = estado;
    }
}
