using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trial", menuName = "AceAttorney GDW/Trial")]
public class TrialSO : ScriptableObject 
{
    public string trialName;
    public DialogueSO[] listOfDialogues;
    public List<EvidenceSO> listOfEvidence = new List<EvidenceSO>();
}