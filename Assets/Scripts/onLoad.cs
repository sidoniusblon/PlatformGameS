using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class onLoad : MonoBehaviour
{
    [SerializeField] Button ContinueButton;
    [SerializeField] Transform LevelPanel;
    [SerializeField] GameObject Panel;
    bool panel;
    void Awake()
    {
        if (PlayerPrefs.GetInt("Level") >= 1) ContinueButton.interactable = true;
        else {
            ContinueButton.interactable = false;
            ContinueButton.gameObject.GetComponent<Image>().color = Color.black;
        
        }

        for (int i = 0; i <=PlayerPrefs.GetInt("Level"); i++)
        {
            LevelPanel.GetChild(i).GetComponent<Button>().interactable = true;
            LevelPanel.GetChild(i).GetComponent<Image>().color = Color.white;
        }
    }
  public  void StartLevel()
    {
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetInt("Score", 0);
        SceneManager.LoadScene("SampleScene");
    }
    public void LevelChanger(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }
    public void PanelActivity()
    {
        panel = !panel;
        Panel.SetActive(panel);
    }
    public void continueButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level")+1);
    }
    public void quit()
    {
        Application.Quit();
    }
  
}
