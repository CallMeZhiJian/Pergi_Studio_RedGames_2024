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

    public Animator dailyPanelAnimation;
    private bool isDailyOpen = false;

    public Animator transitionAnimation_left;
    public Animator transitionAnimation_right;

    private void Start()
    {
        OpenDailyPanel();
    }

    public void LoadLevel()
    {
        Audio_MainMenu.instance.PlaySFX(0);
        transitionAnimation_left.SetTrigger("Transition");
        transitionAnimation_right.SetTrigger("Transition");
        StartCoroutine(LoadLevelWithDelay());
    }

    public void OpenSettings()
    {
        Audio_MainMenu.instance.PlaySFX(0);
        isSettingOpen = !isSettingOpen;
        settingsPanelAnimation.SetBool("OpenSettings", isSettingOpen);
    }

    public void OpenCredits()
    {
        Audio_MainMenu.instance.PlaySFX(0);
        isCreditOpen = !isCreditOpen;
        creditsPanelAnimation.SetBool("OpenCredits", isCreditOpen);
    }

    private IEnumerator LoadLevelWithDelay()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("KW_testing");

    }

    public void OpenDailyPanel()
    {
        Audio_MainMenu.instance.PlaySFX(0);
        isDailyOpen = !isDailyOpen;
        dailyPanelAnimation.SetBool("OpenDaily", isDailyOpen);
    }

}
