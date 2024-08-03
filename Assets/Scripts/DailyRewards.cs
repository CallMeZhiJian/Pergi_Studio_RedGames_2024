using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DailyRewards : MonoBehaviour
{
    private string dateCache;

    [SerializeField] private RewardDetail[] rewardDetail;

    private int dayTemp;

    private void Start()
    {     
        ChangeRewardIcon();

        GetDateNow();

        CheckIfNextDay();

        UpdateDailyRewards();
    }

    private void ChangeRewardIcon()
    {
        for (int i = 0; i < rewardDetail.Length; i++)
        {
            rewardDetail[i].rewardImage.sprite = rewardDetail[i].rewardSprite;
        }
    }

    private void UpdateDailyRewards()
    {
        dayTemp = PlayerPrefs.GetInt("Day");

        //For deactivate
        for (int i = 0; i < dayTemp; i++)
        {
            rewardDetail[i].deactive.SetActive(true);
        }

        if (PlayerPrefs.GetInt("CheckIn" + rewardDetail[dayTemp].day) == 1)
        {
            rewardDetail[dayTemp].checkedIn.SetActive(true);
            rewardDetail[dayTemp].deactive.SetActive(true);
        }
        else
        {
            //For active
            rewardDetail[dayTemp].active.SetActive(true);
        }
    }

    public void GetRewards()
    {
        Audio_MainMenu.instance.PlaySFX(0);

        if (rewardDetail[dayTemp].active.activeInHierarchy)
        {
            
            rewardDetail[dayTemp].active.SetActive(false);
            rewardDetail[dayTemp].checkedIn.SetActive(true);
            rewardDetail[dayTemp].deactive.SetActive(true);
            PlayerPrefs.SetInt("CheckIn" + rewardDetail[dayTemp].day, 1);

            // Get Actual Date of Getting Reward
            CachedRewardDate();

            //Give Rewards
            Debug.Log("Rewarded");
        }
    }

    private void GetDateNow()
    {
        string[] date = System.DateTime.Now.ToString().Split(' ');

        dateCache = date[0];
    }

    private void CachedRewardDate()
    {
        string[] date = System.DateTime.Now.ToString().Split(' ');

        PlayerPrefs.SetString("GetRewardTime", date[0]);
    }

    private void CheckIfNextDay()
    {
        string[] dateNow = dateCache.Split('/');
        string[] dateLastReward = PlayerPrefs.GetString("GetRewardTime", dateCache).Split('/');

        if (int.Parse(dateNow[2]) > int.Parse(dateLastReward[2]))
        {
            PlayerPrefs.SetInt("Day", dayTemp + 1);
        }
        else
        {
            if (int.Parse(dateNow[0]) > int.Parse(dateLastReward[0]))
            {
                PlayerPrefs.SetInt("Day", dayTemp + 1);
            }
            else
            {
                if (int.Parse(dateNow[1]) > int.Parse(dateLastReward[1]))
                {
                    PlayerPrefs.SetInt("Day", dayTemp + 1);
                }
            }
        }

        if (PlayerPrefs.GetInt("Day") > 6)
        {
            ResetDayCount();
        }
    }

    public void ResetDayCount()
    {
        PlayerPrefs.SetInt("Day", 0);

        for (int i = 0; i < rewardDetail.Length; i++)
        {
            PlayerPrefs.SetInt("CheckIn" + rewardDetail[i].day, 0);

            rewardDetail[i].deactive.SetActive(false);
            rewardDetail[i].active.SetActive(false);
            rewardDetail[i].checkedIn.SetActive(false);
        }

        UpdateDailyRewards();
    }

    //For Testing ONLY!!!
    public void NextDay()
    {
        PlayerPrefs.SetInt("Day", dayTemp + 1);

        UpdateDailyRewards();
    }

    public void ResetAll()
    {
        ResetDayCount();

        PlayerPrefs.DeleteAll();
    } 
}

[System.Serializable]
public class RewardDetail
{
    public int day;
    public int NoOfCoins;
    [HideInInspector] public bool isCheckedIn;

    [Header("UI Reference")]
    public Sprite rewardSprite;
    public Image rewardImage;
    public GameObject active;
    public GameObject deactive;
    public GameObject checkedIn;
}