using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiHandler : MonoBehaviour
{

    public GameObject inputPanel;
    public GameObject gamePanel;
    public List<TMP_InputField> inputFields;
    public GameObject textPrefab;
    public Gun gun;
    public int playerCount = 4;
    

    public void onStartGameClick()
    {
        foreach (Transform child in gamePanel.transform)
        {
            if(child.CompareTag("NameText"))
                Destroy(child.gameObject);
        }

        gamePanel.SetActive(true);
        inputPanel.SetActive(false);
        List<string> names = new List<string>();
        foreach (TMP_InputField inputField in inputFields)
        {
            if(!string.IsNullOrEmpty(inputField.text))
                names.Add(inputField.text);
        }

        playerCount = names.Count;
        // Calculate angle step based on the number of non-empty inputs
        float angleStep = 360.0f / names.Count;
        float radius = 150; // Set radius for circle arrangement
        for (int i = 0; i < names.Count; i++)
        {
            // Calculate position in a circle
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

            // Create text element
            GameObject textObject = Instantiate(textPrefab, gamePanel.transform);
            textObject.GetComponent<TextMeshProUGUI>().text = names[i];
            textObject.transform.localPosition = position;
        }
    }

    public void ReopenInputMenu()
    {
        gun.StopSpinning();
        gamePanel.SetActive(false);
        inputPanel.SetActive(true);
    }
}
