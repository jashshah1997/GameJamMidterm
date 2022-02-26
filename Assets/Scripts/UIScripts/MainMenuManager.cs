using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    private Button m_start_game_button;
    private Button m_instructions_button;
    private Button m_credits_button;
    private Button m_back_button;
    private GameObject m_title_label;

    private GameObject m_instructions_text;
    private GameObject m_instructions_title;

    private GameObject m_credits_text;
    private GameObject m_credits_title;

    // Start is called before the first frame update
    void Start()
    {
        m_start_game_button = GameObject.Find("StartGameButton").GetComponent<Button>();
        m_back_button = GameObject.Find("BackButton").GetComponent<Button>();
        m_instructions_button = GameObject.Find("InstructionsButton").GetComponent<Button>();
        m_credits_button = GameObject.Find("CreditsButton").GetComponent<Button>();
        
        m_title_label = GameObject.Find("TitleLabel");
        m_instructions_text = GameObject.Find("InstructionsText");
        m_instructions_title = GameObject.Find("InstructionsTitle");

        m_credits_text = GameObject.Find("CreditsText");
        m_credits_title = GameObject.Find("CreditsTitle");

        m_start_game_button.onClick.AddListener(OnStartButtonClicked);
        m_back_button.onClick.AddListener(OnBackButtonClicked);
        m_instructions_button.onClick.AddListener(OnInstructionsButtonClicked);
        m_credits_button.onClick.AddListener(OnCreditsButtonClicked);

        ToggleInstructionsMenu(false);
        ToggleCreditsMenu(false);
    }

    private void OnStartButtonClicked()
    {
        Debug.Log("Start clicked");
        SceneManager.LoadScene("SampleScene");
    }

    private void OnInstructionsButtonClicked()
    {
        ToggleMainMenu(false);
        ToggleInstructionsMenu(true);
    }
    private void OnCreditsButtonClicked()
    {
        ToggleMainMenu(false);
        ToggleCreditsMenu(true);
    }

    private void OnBackButtonClicked()
    {
        ToggleMainMenu(true);
        ToggleInstructionsMenu(false);
        ToggleCreditsMenu(false);
    }

    private void ToggleMainMenu(bool toggle)
    {
        m_title_label.SetActive(toggle);
        m_start_game_button.gameObject.SetActive(toggle);
        m_instructions_button.gameObject.SetActive(toggle);
        m_credits_button.gameObject.SetActive(toggle);
    }

    private void ToggleInstructionsMenu(bool toggle)
    {
        m_instructions_text.SetActive(toggle);
        m_instructions_title.SetActive(toggle);
        m_back_button.gameObject.SetActive(toggle);
    }

    private void ToggleCreditsMenu(bool toggle)
    {
        m_credits_text.SetActive(toggle);
        m_credits_title.SetActive(toggle);
        m_back_button.gameObject.SetActive(toggle);
    }
}
