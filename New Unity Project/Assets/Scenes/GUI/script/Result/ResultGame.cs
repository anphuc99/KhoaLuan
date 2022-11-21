using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Photon.Pun;

public class ResultGame : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Image Icon;
    public TextMeshProUGUI redScore;
    public TextMeshProUGUI blueScore;
    public TextMeshProUGUI exp;
    public TextMeshProUGUI fans;
    public GameObject[] playerTeamRed = new GameObject[3];
    public GameObject[] playerTeamBlue = new GameObject[3];

    public Sprite IconWin;
    public Sprite IconDraw;
    public Sprite IconLose;
    void Start()
    {
        StartCoroutine(getGameInfo());
    }

    IEnumerator getGameInfo()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URL.game_getGameInfo + "/" + Global.gameID))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                Json.GetGameInfo gameInfo = JsonUtility.FromJson<Json.GetGameInfo>(json);
                setInfoGame(gameInfo);
            }
        }
    }

    private void setInfoGame(Json.GetGameInfo gameInfo)
    {
        redScore.text = gameInfo.game.redScore.ToString();
        blueScore.text = gameInfo.game.blueScore.ToString();
        Json.GameInfo myGameInfo = gameInfo.gameInfo.First(x => x.playerID == Global.account.id);
        Icon.gameObject.SetActive(true);
        exp.gameObject.SetActive(true);
        fans.gameObject.SetActive(true);
        redScore.gameObject.SetActive(true);
        blueScore.gameObject.SetActive(true);
        title.gameObject.SetActive(true);
        if (myGameInfo.team == 0 && gameInfo.game.redScore > gameInfo.game.blueScore || myGameInfo.team == 1 && gameInfo.game.redScore < gameInfo.game.blueScore)
        {
            title.GetComponent<Languages>().key = "victory";
            Icon.sprite = IconWin;
            exp.text = "+80";
            fans.text = "+100";
        }
        else if (gameInfo.game.redScore == gameInfo.game.blueScore)
        {
            title.GetComponent<Languages>().key = "draw";
            Icon.sprite = IconDraw;
            exp.text = "+50";
            fans.text = "+0";
        }
        else
        {
            title.GetComponent<Languages>().key = "defeat";
            Icon.sprite = IconLose;
            exp.text = "+20";
            fans.text = "-50";
        }

        int indexRedTeam = 0;
        int indexBlueTeam = 0;
        foreach (Json.GameInfo info in gameInfo.gameInfo)
        {
            if (info.team == 0)
            {
                playerTeamRed[indexRedTeam].SetActive(true);
                playerTeamRed[indexRedTeam].transform.Find("name").GetComponent<TextMeshProUGUI>().text = info.name;
                playerTeamRed[indexRedTeam].transform.Find("Star/text").GetComponent<TextMeshProUGUI>().text = info.level;
                indexRedTeam++;
            }
            else if (info.team == 1)
            {
                playerTeamBlue[indexBlueTeam].SetActive(true);
                playerTeamBlue[indexBlueTeam].transform.Find("name").GetComponent<TextMeshProUGUI>().text = info.name;
                playerTeamBlue[indexBlueTeam].transform.Find("Star/text").GetComponent<TextMeshProUGUI>().text = info.level;
                indexBlueTeam++;
            }
        }
    }

    public void backToHistory()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        Event.emit(Events.showHistory, null);
    }
}
