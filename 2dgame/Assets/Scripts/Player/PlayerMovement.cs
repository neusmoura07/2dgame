using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private float moveSpeed = 5f;
    
    private Rigidbody2D rb;
    private Vector2 movementInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Garante que a gravidade está desligada
    }

    void Update()
    {
        // Captura o input
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput = movementInput.normalized;

        // Rotaciona o quadrado para a direção do movimento
        if (movementInput != Vector2.zero)
        {
            // Mathf.Atan2(movementInput.y, movementInput.x) calcula o ângulo em radianos do vetor de movimento.
            // Mathf.Rad2Deg converte o resultado para graus.
            float angle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void FixedUpdate()
    {
        // Movimento sem física (usando MovePosition para suavidade)
        Vector2 targetPosition = rb.position + movementInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);
    }
}