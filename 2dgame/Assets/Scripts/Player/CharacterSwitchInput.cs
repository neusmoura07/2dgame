using UnityEngine;

public class CharacterSwitchInput : MonoBehaviour
{
    void Update()
    {
        // Troca de personagem com a tecla Q (customize conforme seu input)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.Instance.SwitchCharacter();
        }
    }
}