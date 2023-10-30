using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ITS GONNA BE REDONE, SO DONT PAY ATTENTION TO THIS CODE
public class MenuSystemComponent : MonoBehaviour
{
    public GameObject mainMenu;
    public Text sesionText;
    public Text menuErrorLog;
    public GameObject logIn;
    public InputField logInInputUsername;
    public InputField logInInputPassword;
    public Text logInErrorLog;
    public GameObject signUp;
    public InputField signUpInputUsername;
    public InputField signUpInputPassword;
    public Text signUpErrorLog;
    public User user;
    public List<User> userList;
    
    private string filePath;

    private void ReadFile()
    {
        try
        {
            StreamReader sr = new StreamReader(filePath);
            string text = sr.ReadToEnd();
            sr.Close();
            var splitUser = new string[] { "\r\n", "\r", "\n" };
            var splitAttr = new string[] { "\t" };
            string[] users = text.Split(splitUser, System.StringSplitOptions.None);

            for (int i = 0; i < users.Length; i++)
            {
                string[] userAttr = users[i].Split(splitAttr, System.StringSplitOptions.None);
                User userN = new User(userAttr[0], userAttr[1]);
                userList.Add(userN);
            }
        }
        catch (Exception e) { }
    }

    public void EnterGame()
    {
        User userN = new User();
        if (user != null)
        {
            SceneManager.LoadScene("Prefs");
        }
        else
        {
            menuErrorLog.text = "Unable to enter without a user logged";
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LogIn()
    {
        logInErrorLog.text = "";
        logInInputUsername.text = "";
        logInInputPassword.text = "";
        mainMenu.SetActive(false);
        logIn.SetActive(true);
    }

    public void LogInSubmit()
    {
        if (logInInputUsername.text == "" || logInInputPassword.text == "")
            logInErrorLog.text = "Please, enter username and password";
        else
        {
            User userN = new User(logInInputUsername.text, logInInputPassword.text);

            bool foundUsername = false;
            int index = -1;

            for (int i = 0; i < userList.Count && !foundUsername; i++)
                if (userN.GetUsername() == userList[i].GetUsername())
                {
                    foundUsername = true;
                    index = i;
                }
                    
            if (!foundUsername)
                logInErrorLog.text = "No user found with that username. Enter a valid username or sign up";
            else
            {
                if (userN.GetPassword() == userList[index].GetPassword())
                {
                    user = userN;
                    Back();
                }
            }
        }
    }

    public void Back()
    {
        menuErrorLog.text = "";
        logIn.SetActive(false);
        signUp.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SignUp()
    {
        signUpErrorLog.text = "";
        signUpInputPassword.text = "";
        signUpInputUsername.text = "";
        signUp.SetActive(true);
        logIn.SetActive(false);
    }

    public void SignUpSubmit()
    {
        if (signUpInputUsername.text == "" || signUpInputPassword.text == "")
            signUpErrorLog.text = "Please, enter username and password";
        else
        {
            User userN = new User(signUpInputUsername.text, signUpInputPassword.text);

            bool foundUsername = false;

            for (int i = 0; i < userList.Count && !foundUsername; i++)
                if (userN.GetUsername() == userList[i].GetUsername())
                {
                    foundUsername = true;
                }

            if (foundUsername)
                logInErrorLog.text = "User already exists. User something different or log in";
            else
            {
                userList.Add(userN);
                UpdateDB();
                user = userN;
                Back();
            }
        }
    }

    private void UpdateDB()
    {
        try
        {
            StreamWriter writer = new StreamWriter(filePath);
            for (int i = 0; i < userList.Count; i++)
                writer.WriteLine(userList[i].GetUsername() + "\t" + userList[i].GetPassword());
            writer.Close();
        }
        catch (Exception e) { }
    }
    
    void Awake()
    {
        Application.targetFrameRate = 60;
        
        filePath = "Assets/Src/Dev/UserDB.txt";
        menuErrorLog.text = "";
        logInErrorLog.text = "";
        signUpErrorLog.text = "";
        userList = new List<User>();
        ReadFile();
        mainMenu.SetActive(true);
        logIn.SetActive(false);
        signUp.SetActive(false);
    }
    
    void Update()
    {
        if (user == null)
            sesionText.text = "Log In";
        else
            sesionText.text = user.GetUsername();
    }
    

}
