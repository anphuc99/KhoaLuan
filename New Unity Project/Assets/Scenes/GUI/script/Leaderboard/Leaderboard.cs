using Assets.script.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour
{
    public GameObject leaderboardItem;
    public GameObject content;
    public GameObject myRank;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getTopRank());
        StartCoroutine(getMyRank());
    }

    IEnumerator getTopRank()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URL.game_getTopRank))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                Debug.Log(json);
                PlayerClient[] playerClients = JsonHelper.FromJson<PlayerClient>(json);
                showTopRank(playerClients);
            }
        }
    }

    IEnumerator getMyRank()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URL.game_getMyRank + "/" + Global.playerClient.account_id))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                int myRank = int.Parse(json);
                showMyRank(myRank);
            }
        }
    }

    private void showTopRank(PlayerClient[] playerClients)
    {
        int i = 0;
        foreach (PlayerClient playerClient in playerClients)
        {
            GameObject leaderboardItem = Instantiate(this.leaderboardItem);
            LeaderboardItem leaderboardItem1 = leaderboardItem.GetComponent<LeaderboardItem>();
            leaderboardItem1.rank = i;
            leaderboardItem1.name = playerClient.name;
            leaderboardItem1.fans = playerClient.fans;
            leaderboardItem1.transform.parent = this.content.transform;
            leaderboardItem1.transform.localScale = Vector3.one;
            i++;
        }
    }

    private void showMyRank(int rank)
    {
        GameObject leaderboardItem = Instantiate(this.leaderboardItem);
        LeaderboardItem myRank = leaderboardItem.GetComponent<LeaderboardItem>();
        myRank.rank = rank;
        myRank.name = Global.playerClient.name;
        myRank.fans = Global.playerClient.fans;
        myRank.transform.parent = this.myRank.transform;
        myRank.transform.localScale = Vector3.one;
    }
}
