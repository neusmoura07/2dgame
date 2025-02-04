using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se colidiu com o AttackArea
        if (collision.gameObject.CompareTag("AttackArea"))
        {
            Debug.Log("Inimigo colidiu com o AttackArea!");

            // Verifica se o ataque está ativo
            MeleeAttack meleeAttack = collision.gameObject.GetComponent<MeleeAttack>();
            if (meleeAttack != null && meleeAttack.IsAttacking())
            {
                // Aplica dano ao inimigo
                TakeDamage(meleeAttack.GetDamage());
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Inimigo levou " + damage + " de dano. Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Inimigo morreu!");
        Destroy(gameObject);
    }
}