using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public GameObject pineCharacterPrefab;
    public GameObject raccCharacterPrefab;

    public Button pineButton;
    public Button raccButton;
    public Button continueButton;
    public Text playerSelectText;

    private int currentPlayer = 1;
    private bool player1Picked = false;
    private bool player2Picked = false;

    private GameObject player1Character;

    void Start()
    {
        pineButton.Select(); // sets focus for any controllers
        continueButton.interactable = false;
        UpdateUIForPlayer();
    }

    public void OnSelectPine()
    {
        AssignCharacter(pineCharacterPrefab);
    }

    public void OnSelectRacc()
    {
        AssignCharacter(raccCharacterPrefab);
    }

    void AssignCharacter(GameObject character)
    {
        if (currentPlayer == 1)
        {
            JoinPlayersManager.Instance.player1Prefab = character;
            player1Character = character;
            player1Picked = true;
        }
        else if (currentPlayer == 2)
        {
            JoinPlayersManager.Instance.player2Prefab = character;
            player2Picked = true;
        }

        continueButton.interactable = true;
    }

    public void OnContinue()
    {
        if (currentPlayer == 1 && player1Picked)
        {
            currentPlayer = 2;
            player1Picked = false;
            continueButton.interactable = false;

            // Disable the button for the selected character to prevent duplication
            if (player1Character == pineCharacterPrefab)
            {
                pineButton.interactable = false;
            }
            else if (player1Character == raccCharacterPrefab)
            {
                raccButton.interactable = false;
            }

            UpdateUIForPlayer();
        }
        else if (currentPlayer == 2 && player2Picked)
        {
            SceneManager.LoadScene("LilyTestScene");
        }
    }

    void UpdateUIForPlayer()
    {
        playerSelectText.text = $"Player {currentPlayer}, choose your character and then click 'Ready'!";
        if (pineButton.interactable)
            pineButton.Select();
        else
            raccButton.Select();
    }
}
