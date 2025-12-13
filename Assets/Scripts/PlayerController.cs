using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;

    public GameObject minY;
    public GameObject maxY;

    private Vector3 minScale;
    public float minScalePlayer;

    private Vector3 maxScale;
    public float maxScalePlayer;

    public float smooth = 8f;

    private Vector2 movement;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        minScale = new Vector3(minScalePlayer, minScalePlayer, 1f);
        maxScale = new Vector3(maxScalePlayer, maxScalePlayer, 1f);
    }

    void Update()
    {
        ReadMovementInput();

        // ---- Movimiento ----
        Vector3 delta = new Vector3(movement.x, movement.y, 0) * moveSpeed * Time.deltaTime;
        transform.position += delta;

        float clampedY = Mathf.Clamp(transform.position.y, minY.transform.position.y, maxY.transform.position.y);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

        float t = Mathf.InverseLerp(minY.transform.position.y, maxY.transform.position.y, transform.position.y);
        Vector3 target = Vector3.Lerp(maxScale, minScale, t);
        transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime * smooth);

        UpdateAnimations();
    }

    void ReadMovementInput()
    {
        movement = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
            movement.y = 1;

        if (Input.GetKey(KeyCode.DownArrow))
            movement.y = -1;

        if (Input.GetKey(KeyCode.LeftArrow))
            movement.x = -1;

        if (Input.GetKey(KeyCode.RightArrow))
            movement.x = 1;

        // Evita movimiento diagonal más rápido
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
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetBool("Idle", false);

        if (up) animator.SetBool("Up", true);
        else if (down) animator.SetBool("Down", true);
        else if (left) animator.SetBool("Left", true);
        else if (right) animator.SetBool("Right", true);
        else animator.SetBool("Idle", true);
    }
}

