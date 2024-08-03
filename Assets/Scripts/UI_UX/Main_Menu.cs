using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public Animator settingsPanelAnimation;
    private bool isSettingOpen = false;

    public Animator creditsPanelAnimation;
    private bool isCreditOpen = false;

    public void LoadLevel()
    {
        SceneManager.LoadScene("SampledScene");
    }

    public void OpenSettings()
    {
        isSettingOpen = !isSettingOpen;
        settingsPanelAnimation.SetBool("OpenSettings", isSettingOpen);
    }

    public void OpenCredits()
    {
        isCreditOpen = !isCreditOpen;
        creditsPanelAnimation.SetBool("OpenCredits", isCreditOpen);
    }

}
