using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerJsonController : MonoBehaviour
{
    public string jsonURL;

    // Start is called before the first frame update
    public void displayData()
    {
        StartCoroutine(getData());
    }

    IEnumerator getData()
    {
        UnityWebRequest request = UnityWebRequest.Get(jsonURL);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        } else
        {
            // get JSON
            List<PlayerJsonData> PlayerInfoList = JsonConvert.DeserializeObject<List<PlayerJsonData>>(request.downloadHandler.text);

            // display all players from JSON
            GameObject.Find("PlayerInfo").GetComponent<UnityEngine.UI.Text>().text = "Player Info:";
            foreach (PlayerJsonData playerinfo in PlayerInfoList)
            {
                var props = playerinfo.GetType().GetFields();
                var sb = new System.Text.StringBuilder();
                foreach (var p in props)
                    sb.AppendLine(p.Name + ": " + p.GetValue(playerinfo));

                sb.ToString();
                GameObject.Find("PlayerInfo").GetComponent<UnityEngine.UI.Text>().text += "\n" + sb;
            }
        }
    }
}
