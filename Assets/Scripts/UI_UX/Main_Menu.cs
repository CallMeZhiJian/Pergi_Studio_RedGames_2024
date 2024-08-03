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

    public Animator transitionAnimation_left;
    public Animator transitionAnimation_right;

    public void LoadLevel()
    {
        transitionAnimation_left.SetTrigger("Transition");
        transitionAnimation_right.SetTrigger("Transition");
        StartCoroutine(LoadLevelWithDelay());
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

    private IEnumerator LoadLevelWithDelay()
    {
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Ervin_Posttesting");

        
    }

}
