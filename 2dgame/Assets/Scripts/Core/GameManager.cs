using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    // Singleton para acesso global
    public static GameManager Instance { get; private set; }

    // Eventos para comunicação entre sistemas
    public UnityEvent OnCharacterSwitched { get; private set; } = new UnityEvent();
    public UnityEvent<int> OnCoinsChanged { get; private set; } = new UnityEvent<int>();
    public UnityEvent<float> OnHealthChanged { get; private set; } = new UnityEvent<float>();

    [Header("Configurações do Jogador")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private GameObject meleeCharacter; // Arraste o prefab/objeto do personagem melee aqui
    [SerializeField] private GameObject rangedCharacter; // Arraste o prefab/objeto do personagem ranged aqui

    private float currentHealth;
    private int coins;
    private bool isMeleeActive = true;

    void Awake()
    {
        // Configura o Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Inicializa valores
        currentHealth = maxHealth;
        meleeCharacter.SetActive(true);
        rangedCharacter.SetActive(false);
    }

    // ==== Controle de Personagem ====
    public void SwitchCharacter()
    {
        isMeleeActive = !isMeleeActive;
        meleeCharacter.SetActive(isMeleeActive);
        rangedCharacter.SetActive(!isMeleeActive);
        OnCharacterSwitched.Invoke(); // Notifica outros sistemas (ex: UI)
    }

    // ==== Sistema de Moedas ====
    public void AddCoins(int amount)
    {
        coins += amount;
        OnCoinsChanged.Invoke(coins); // Atualiza a UI
    }

    public bool RemoveCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            OnCoinsChanged.Invoke(coins);
            return true; // Compra bem-sucedida
        }
        return false; // Moedas insuficientes
    }

    // ==== Sistema de Vida ====
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        OnHealthChanged.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            // Lógica de morte (ex: recarregar cena)
            Debug.Log("Game Over!");
        }
    }

    public void Heal(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        OnHealthChanged.Invoke(currentHealth);
    }

    // ==== Controle de Pausa ====
    public void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
        // Adicione aqui lógica adicional (ex: desativar inputs)
    }

    // ==== Getters para outros sistemas ====
    public bool IsMeleeActive() => isMeleeActive;
    public int GetCoins() => coins;
    public float GetHealth() => currentHealth;
}