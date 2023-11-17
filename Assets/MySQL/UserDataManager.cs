using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    private string username; // Current username logged

    public static UserDataManager Instance; // Self reference to be used in any scene
    // Called in the first frame
    private void Start()
    {
        username = "No sesion";
    }
    // Called when object this script is attached to is load in memory
    private void Awake()
    {
        if (Instance != null) // If this object does exists, copies are destroyed
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // The object this script is attached to is never destroyed when scene changes
    }
    // Method to set actual logged username
    public void SetUsername(string username) { this.username = username; }
    // Same as before, but setting username
    public string GetUsername() { return username; }
    // Method to regist this user, with a couple more data
    public async Task<bool> SignUpUser(string password, string sex, int age)
    {
        return await MySQLManager.RegisterUser(this.username, password, sex, age);
    }
    // Method to log in the user (check if user does exists in the database)
    public async Task<bool> LogInUser(string password)
    {
        return await MySQLManager.LoginUser(this.username, password);
    }
    // Delete this user
    public async Task<bool> DeleteUser()
    {
        string u = this.username;
        this.username = "No sesion";
        return await MySQLManager.DeleteUser(u);
    }
    // Returns a list of strings with all usernames in the database
    public async Task<List<string>> GetAllUsers()
    {
        return await MySQLManager.GetAllUsers();
    }
}
