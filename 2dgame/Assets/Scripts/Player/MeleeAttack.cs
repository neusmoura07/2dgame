using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private int damage = 10;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private GameObject attackArea; // Arraste o AttackArea aqui

    private float lastAttackTime;
    private bool isAttacking = false;

    void Start()
    {
        attackArea.SetActive(false); // Garante que está desativado inicialmente
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsMeleeActive())
        {
            // Atualiza a direção do ataque baseada no movimento
            Vector2 direction = new Vector2(
                Input.GetAxisRaw("Horizontal"),
                Input.GetAxisRaw("Vertical")
            ).normalized;

            if (direction != Vector2.zero)
            {
                // Rotaciona o AttackArea para a direção do movimento
                attackArea.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            }

            // Verifica o input de ataque (ex: botão esquerdo do mouse)
            if (Input.GetButtonDown("Fire1") && Time.time > lastAttackTime + attackCooldown)
            {
                StartAttack();
                lastAttackTime = Time.time;
            }
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        attackArea.SetActive(true);
        Debug.Log("Ataque iniciado!");
        Invoke("EndAttack", 0.3f); // Desativa após 0.3 segundos
    }

    void EndAttack()
    {
        isAttacking = false;
        attackArea.SetActive(false);
        Debug.Log("Ataque finalizado!");
    }

    // Métodos públicos para acesso externo
    public bool IsAttacking() => isAttacking;
    public int GetDamage() => damage;
}