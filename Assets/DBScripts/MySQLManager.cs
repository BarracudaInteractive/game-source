using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class MySQLManager
{   
    // Specify locat rute, or internet link to the data base, and port where database is listening
    public static readonly string SERVER_URL = "localhost:80/PHP";
    // Method to register user in the database
    public static async Task<bool> RegisterUser(string nickname, string password, string sex, int age)
    {
        // Specify php file to use
        string REGISTER_USER_URL = $"{SERVER_URL}/registerUser.php";
        // Send specific data to the php file
        return (await SendPostRequest(REGISTER_USER_URL, new Dictionary<string, string>()
        {
            { "nickname", nickname },
            { "password", password },
            { "sex", sex },
            { "age", age.ToString() }
        }, false, false)).success;
    }
    // The rest of methods do exactly the same as the one before, so no coments were written
    public static async Task<bool> LoginUser(string nickname, string password)
    {
        string LOGIN_USER_URL = $"{SERVER_URL}/login.php";

        return (await SendPostRequest(LOGIN_USER_URL, new Dictionary<string, string>()
        {
            { "nickname", nickname },
            { "password", password }
        }, true, false)).success;
    }

    public static async Task<bool> DeleteUser(string nickname)
    {
        string DELETE_USER_URL = $"{SERVER_URL}/deleteUser.php";

        return (await SendPostRequest(DELETE_USER_URL, new Dictionary<string, string>()
        {
            { "nickname", nickname }
        }, false, false)).success;
    }

    public static async Task<List<string>> GetAllUsers()
    {
        string DELETE_USER_URL = $"{SERVER_URL}/getAllUsers.php";

        return (await SendPostRequest(DELETE_USER_URL, new Dictionary<string, string>(), true, false)).userList;
    }

    public static async Task<bool> InsertClassiffication(Classiffication classiffication)
    {
        string INSERT_CLASSIFFICATION_URL = $"{SERVER_URL}/insertClassiffication.php";

        return (await SendPostRequest(INSERT_CLASSIFFICATION_URL, new Dictionary<string, string>()
        {
            { "nickname", classiffication.GetUsername() },
            { "map", classiffication.GetMap() },
            { "leg", classiffication.GetLeg() },
            { "stage", classiffication.GetStage() },
            { "time", classiffication.GetTime().ToString() },
            { "damage", classiffication.GetDamage().ToString() },
            { "fuel", classiffication.GetFuel().ToString() },
        }, false, false)).success;
    }

    public static async Task<bool> UpdateClassiffication(Classiffication classiffication)
    {
        string UPDATE_CLASSIFFICATION_URL = $"{SERVER_URL}/updateClassiffication.php";

        return (await SendPostRequest(UPDATE_CLASSIFFICATION_URL, new Dictionary<string, string>()
        {
            { "nickname", classiffication.GetUsername() },
            { "map", classiffication.GetMap() },
            { "leg", classiffication.GetLeg() },
            { "stage", classiffication.GetStage() },
            { "time", classiffication.GetTime().ToString() },
            { "damage", classiffication.GetDamage().ToString() },
            { "fuel", classiffication.GetFuel().ToString() },
        }, false, false)).success;
    }

    public static async Task<Classiffication> GetUserClassiffication(string username, string map, string leg, string stage)
    {
        string GET_CLASSIFFICATION_URL = $"{SERVER_URL}/getUserClassiffication.php";

        return (await SendPostRequest(GET_CLASSIFFICATION_URL, new Dictionary<string, string>()
        {
            { "nickname", username },
            { "map", map },
            { "leg", leg },
            { "stage", stage }
        }, false, true)).info[0];
    }

    public static async Task<List<Classiffication>> GetAllClassiffication(string username)
    {
        string GET_CLASSIFFICATION_URL = $"{SERVER_URL}/getAllClassiffication.php";

        return (await SendPostRequest(GET_CLASSIFFICATION_URL, new Dictionary<string, string>()
        {
            { "nickname", username }
        }, false, true)).info;
    }

    public static async Task<bool> DeleteClassiffication(string username)
    {
        string DELETE_CLASSIFFICATION_URL = $"{SERVER_URL}/deleteClassiffication.php";

        return (await SendPostRequest(DELETE_CLASSIFFICATION_URL, new Dictionary<string, string>()
        {
            { "username", username }
        }, false, false)).success;
    }
    // Method that connects to the database via php file and waits for an answer
    static async Task<(bool success, List<string> userList, List<Classiffication> info)> SendPostRequest(string url, Dictionary<string, string> data, bool users, bool classiffication)
    {
        // A Web Request is created to talk to the database
        using (UnityWebRequest req = UnityWebRequest.Post(url, data))
        {
            // Web Request is sended
            req.SendWebRequest();
            // The method waits until request is finished
            while (!req.isDone)
            {
                await Task.Delay(100);
            }
            // Void classes are created for return
            List<Classiffication> c = new List<Classiffication>();
            List<string> userList = new List<string>();
            // Method checks if an error ocurred
            if (req.error != null || req.downloadHandler.text == "1" || req.downloadHandler.text == "2" ||
                req.downloadHandler.text == "30")
            {
                return (false, userList, c); // If error, false is returned
            }
            else
            {
                if (users) // If Method needs username from data base, it will be created
                    userList = CreateUserList(req.downloadHandler.text);

                if (classiffication) // If method needs classiffication in return, it will be created
                    c = CreateClassiffication(req.downloadHandler.text);
                return (true, userList, c); // Method returns true, and user and classiffication list if necesary
            }            
        }
    }
    // Method that separate a string into string array via spaces or tabs
    static string[] SplitString(string data, bool tab)
    {
        if (tab)
        {
            return data.Split("\t");
        }
        else
        {
            return data.Split(" ");
        }
    }
    // Method that creates classiffication list with the return text php file sends
    static List<Classiffication> CreateClassiffication(string data)
    {
        // A new list is created
        List<Classiffication> list = new List<Classiffication>();
        // Different classiffications are separated by tabs, and classiffication attributes by spaces
        string[] c = SplitString(data, true);
        // For each classiffication, a new classiffication is created and added to the list
        for (int i = 0; i < c.Length - 1; i++)
        {
            string[] cData = SplitString(c[i], false);
            Classiffication classiffication = new Classiffication();
            classiffication.SetUsername(cData[0]);
            classiffication.SetMap(cData[1]);
            classiffication.SetLeg(cData[2]);
            classiffication.SetStage(cData[3]);
            float.TryParse(cData[4], out float time);
            classiffication.SetTime(time);
            float.TryParse(cData[5], out float damage);
            classiffication.SetDamage(damage);
            float.TryParse(cData[6], out float fuel);
            classiffication.SetFuel(fuel);

            list.Add(classiffication);
        }

        return list; // Classiffication list is returned
    }
    // Method that returns string list with usernames
    static List<string> CreateUserList(string data)
    {
        // A new list is created
        List<string> userList = new List<string>();
        // Usernames are separatedby tabs
        string[] c = SplitString(data, true);
        // For each username, it is added to the list
        for (int i = 0; i < c.Length; i++)
        {
            userList.Add(c[i]);
        }

        return userList; // The list is returned
    }
}