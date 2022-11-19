using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.script.Player;
using UnityEngine.UI;

public class LeaderboardItem : MonoBehaviour
{
    public int rank;
    public string avatar;
    public int level;
    public string name;
    public int fans;

    [SerializeField]
    private Sprite[] bgRanks;

    [SerializeField]
    private Sprite[] cups;
    void Start()
    {
        if (rank == 0 || rank == 1 || rank == 2)
        {
            Image bg = transform.Find("bgItem").GetComponent<Image>();
            bg.sprite = this.bgRanks[rank];
            Image cup = transform.Find("rank/cup").GetComponent<Image>();
            cup.sprite = cups[rank];
            cup.gameObject.SetActive(true);
        }
        else
        {
            Image bg = transform.Find("bgItem").GetComponent<Image>();
            bg.sprite = this.bgRanks[3];
            Text rank = transform.Find("rank/rank").GetComponent<Text>();
            if (this.rank > 1000)
            {
                rank.text = "???";
            }
            else
            {
                rank.text = (this.rank + 1).ToString();
            }
            rank.gameObject.SetActive(true);
        }
        transform.Find("name").GetComponent<Text>().text = name;
        transform.Find("fans_count").GetComponent<Text>().text = fans.ToString();
        transform.Find("bgAvatar/level/Text").GetComponent<Text>().text = level.ToString();
    }
}
