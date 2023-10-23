using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    private string username;
    private string password;

    public User()
    {
        username = "No sesion";
        password = "password";
    }

    public User(string username, string password)
    {
        this.username = username;
        this.password = password;
    }

    public string GetUsername()
    {
        return username;
    }

    public string GetPassword()
    {
        return password;
    }
}
