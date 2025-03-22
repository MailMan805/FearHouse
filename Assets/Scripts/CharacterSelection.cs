using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public Image[] characterImages;  // UI Image array representing each character
    private int currentPlayer = 0;   // Track which player is selecting (1 or 2)
    private int selectedCharacter = 0;  // Track which character is selected for the current player

    public void UpdateSelection(int playerId, int direction)
    {
        // Ensure the current player is within bounds
        if (playerId == currentPlayer)
        {
            selectedCharacter = (selectedCharacter + direction + characterImages.Length) % characterImages.Length;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < characterImages.Length; i++)
        {
            characterImages[i].color = i == selectedCharacter ? Color.green : Color.white;
        }
    }
}