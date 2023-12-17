using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class DatabaseManager : MonoBehaviour
{
    [Serializable]
    private class Credentials
    {
        public string userDB;
        public string passwordDB;
        public string uriInsert;
        public string uriGet;
    }
    
    [Serializable]
    public class ResponseData
    {
        public string result;
        public UserData[] data;
    }

    [Serializable]
    public class UserData
    {
        public string name;
        public string password;
        public string age;
        public string sex;
    }
    
    [Header("Menu Manager")]
    public GameObject gManager;
    
    [HideInInspector] public string name;
    [HideInInspector] public string passwd;
    [HideInInspector] public string age;
    [HideInInspector] public string sex;
    
    private string _sUsername;
    private string _sPassword;
    private string _sUriInsert;
    private string _sUriGet;
    private string _sContentType = "application/json";
    private MenuManager _MenuManager;
    private GameManager _GameManager;
    private bool _isMenu = false;

    public string Name
    {
        get => name;
        set => name = value;
    }
    
    public string Passwd
    {
        get => passwd;
        set => passwd = value;
    }
    
    private void _FillUserData(ResponseData rd)
    {
        name = rd.data[0].name;
        passwd = rd.data[0].password;
        age = rd.data[0].age;
        sex = rd.data[0].sex;
    }

    string _CreateUserPostJSON(string table, string n, string p, string a, string s)
    {        
        string json = $@"{{
            ""username"":""{_sUsername}"",
            ""password"":""{_sPassword}"",
            ""table"":""{table}"",
            ""data"": {{
                ""name"": ""{n}"",
                ""password"": ""{p}"",
                ""age"": ""{a}"",
                ""sex"": ""{s}""
            }}
        }}";

        return json;
    }
    
    string _CreateUserGetJSON(string table, string name)
    {      
        string json = $@"{{
            ""username"":""{_sUsername}"",
            ""password"":""{_sPassword}"",
            ""table"":""{table}"",
            ""filter"": {{
                ""name"": ""{name}""
            }}
        }}";

        return json;
    }
    
    string _CreateMetricPostJSON(string table, string n, string l, string t, string f, string d)
    {        
        string json = $@"{{
            ""username"":""{_sUsername}"",
            ""password"":""{_sPassword}"",
            ""table"":""{table}"",
            ""data"": {{
                ""name"": ""{n}"",
                ""level"": ""{l}"",
                ""time"": ""{t}"",
                ""fuel"": ""{f}"",
                ""damage"": ""{d}""
            }}
        }}";

        return json;
    }
    
    private void _LoadCredentials()
    {
        string configPath = $"{Application.dataPath}\\Src\\Framework\\DBCredentials.json";

        if (File.Exists(configPath))
        {
            string configJson = File.ReadAllText(configPath);
            var config = JsonUtility.FromJson<Credentials>(configJson);

            _sUsername = config.userDB;
            _sPassword = config.passwordDB;
            _sUriInsert = config.uriInsert;
            _sUriGet = config.uriGet;
        }
        else Debug.LogError("Config file not found!");

        if (_isMenu) _MenuManager = gManager.GetComponent<MenuManager>();
        else _GameManager = gManager.GetComponent<GameManager>();
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name.Equals("Menu")) _isMenu = true;
        else _isMenu = false;
        _LoadCredentials();
    }
    
    public IEnumerator SendUserPostRequest(string table, string n, string p, string a, string s)
    {
        string data = _CreateUserPostJSON(table, n, p, a, s);
        
        using (UnityWebRequest www = UnityWebRequest.Post(_sUriInsert, data, _sContentType))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError("Err: " + www.error);
            else
            {
                if (www.responseCode == 200) Debug.Log("POST completed: " + www.downloadHandler.text);
                else Debug.LogError("Response err. Code: " + www.responseCode);
            }
        }
    }

    public IEnumerator SendUserGetRequest(string table, string name)
    {
        string data = _CreateUserGetJSON(table, name);
        
        using (UnityWebRequest www = UnityWebRequest.Post(_sUriGet, data, _sContentType))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Err: " + www.error);
            }
            else
            {
                if (www.responseCode == 200)
                {
                    Debug.Log("GET completed: " + www.downloadHandler.text);
                    
                    string  userData= www.downloadHandler.text;
                    ResponseData responseData = JsonUtility.FromJson<ResponseData>(userData);
                    if (responseData != null && responseData.data.Length > 0) _FillUserData(responseData);
                    else Debug.LogError("Response err. Data is null or empty");
                }
                else
                {
                    Debug.LogError("Response err. Code: " + www.responseCode);
                }
            }
            
            _MenuManager.Ready = true;
        }
    }
    
    public IEnumerator SendMetricPostRequest(string table, string n, string l, string t, string f, string d)
    {
        string data = _CreateMetricPostJSON(table, n, l, t, f, d);
        
        using (UnityWebRequest www = UnityWebRequest.Post(_sUriInsert, data, _sContentType))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
                Debug.LogError("Err: " + www.error);
            else
            {
                if (www.responseCode == 200) Debug.Log("POST completed: " + www.downloadHandler.text);
                else Debug.LogError("Response err. Code: " + www.responseCode);
            }
        }
    }
}
