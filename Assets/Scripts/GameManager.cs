using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private List<PropsDetails> propsDetails;

    private int tilesCount;

    private int coinNum;

    private bool isGameOver;

    private LevelManager levelManager;

    [Header("UI Properties")]
    [SerializeField] private Image[] tileImages;
    [SerializeField] private TextMeshProUGUI coinText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levelManager = GetComponent<LevelManager>();

        propsDetails = new List<PropsDetails>();

        isGameOver = false;

        coinText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    void SortTiles(PropsDetails newDetails)
    {
        for (int i = 0; i < propsDetails.Count; i++)
        {
            if (newDetails.ID == propsDetails[i].ID)
            {
                propsDetails.Insert(propsDetails.IndexOf(propsDetails[i]) + 1, newDetails);

                break;
            }
        }

        if(tilesCount == propsDetails.Count)
        {
            propsDetails.Add(newDetails);
            tilesCount++;
        }
        else
        {
            tilesCount++;
        }
    }

    void ClearTiles(int t1, int t2, int t3)
    {
        propsDetails.RemoveAt(t3);
        propsDetails.RemoveAt(t2);
        propsDetails.RemoveAt(t1);

        tilesCount = propsDetails.Count;

        //coinNum++;

        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);

        if (coinText != null)
        {
            coinText.text = PlayerPrefs.GetInt("Coins").ToString();
        }
    }

    void CheckMatchingTiles()
    {
        for (int i = 0; i < propsDetails.Count; i++)
        {
            if(propsDetails.Count >= i + 3)
            {
                if (propsDetails[i].ID == propsDetails[i + 1].ID)
                {
                    if (propsDetails[i + 1].ID == propsDetails[i + 2].ID)
                    {
                        ClearTiles(propsDetails.IndexOf(propsDetails[i]),
                            propsDetails.IndexOf(propsDetails[i + 1]),
                            propsDetails.IndexOf(propsDetails[i + 2]));
                        break;
                    }
                }
            }
            else
            {
                break;
            }
        }
    }

    void CheckTilesCount()
    {
        if(tilesCount >= 7)
        {
            if(levelManager.revived)
            {
                levelManager.GameOver();

                isGameOver = true;

            }
            else
            {
                levelManager.ReviveRequest();
            }
            

            //PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + coinNum);
        }
    }

    public void GetPropsDetails(Details details)    
    {
        PropsDetails newDetails = new PropsDetails();

        newDetails.name = details.propName;
        newDetails.ID = details.iD;
        newDetails.sprite = details.sprite != null ? details.sprite : null;

        if (propsDetails.Count == 0)
        {
            propsDetails.Add(newDetails);
            tilesCount++;
        }
        else
        {
            SortTiles(newDetails);
        }

        StartCoroutine(DelayClearTiles());

        
    }

    void UpdateTileBox()
    {
        for (int i = 0; i < propsDetails.Count; i++)
        {
            tileImages[i].sprite = propsDetails[i].sprite;
        }

        for(int i = propsDetails.Count; i < tileImages.Length; i++)
        {
            tileImages[i].sprite = null;
        }
    }

    public void ReviveClearTiles()
    {
        propsDetails.RemoveAt(6);
        propsDetails.RemoveAt(5);
        propsDetails.RemoveAt(4);

        tilesCount = propsDetails.Count;

        UpdateTileBox();
    }

    IEnumerator DelayClearTiles()
    {
        yield return new WaitForSeconds(0.5f);

        if (propsDetails.Count >= 3)
        {
            CheckMatchingTiles();
        }

        CheckTilesCount();

        if (!isGameOver)
        {
            UpdateTileBox();
        }
        
    }
}



[System.Serializable]
public class PropsDetails
{
    public string name;
    public int ID;
    public Sprite sprite;
    public Image tileImage;
}
