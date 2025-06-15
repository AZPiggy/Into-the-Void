using System.Collections;
using UnityEngine;
using TMPro;

public class InstructionManager : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI instructionText;           
    public string[] instructions;            
    public float displayTime = 3f;

    // things to activate
    public GameObject playgroundObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playgroundObjects.SetActive(false);
        StartCoroutine(ShowInstructions());
    }

    private IEnumerator ShowInstructions()
    {
        foreach (string line in instructions)
        {
            // show instructions one by one
            instructionText.text = line;
            yield return new WaitForSeconds(displayTime);
        }

        panel.SetActive(false);
        instructionText.enabled = false;
        // players can test in the playground
        if (playgroundObjects != null)
        {
            playgroundObjects.SetActive(true);
        }
    }
}
