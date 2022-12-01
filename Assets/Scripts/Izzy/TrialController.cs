using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TrialController : MonoBehaviour
{
    [SerializeField] private DialogueSO currentTrial;
    [SerializeField] private DialogueSO[] allDialogues;
    [SerializeField] private int maxPenalties = 5;

    [SerializeField] private TextMeshProUGUI penaltiesText;

    public int currentPenalties = 5;
    private DialogueManager dialogueManager;
    private CrossExamination crossExamination;

    private PlayerInput playerInput;

    private void Start() {
        dialogueManager = FindObjectOfType<DialogueManager>();
        crossExamination = FindObjectOfType<CrossExamination>();

        UpdateText();

        playerInput = GameObject.FindWithTag("Controller Manager").GetComponent<PlayerInput>();
        playerInput.SwitchCurrentActionMap("Textbox");
    }

    public void StartTrial() {
        currentPenalties = maxPenalties;
        UpdateText();
        
        dialogueManager.StartText(currentTrial);
    }

    public void UpdateText()
    {
        penaltiesText.text = $"{currentPenalties}/{maxPenalties}";
    }

    public void IncreaseIncorrects() {
        currentPenalties--;
        UpdateText();

        if (currentPenalties <= 0) {
            print("AGHHHHH!!!!");
        }
    }

    public DialogueSO[] ReturnAllDialogues() {
        return allDialogues;
    }
}