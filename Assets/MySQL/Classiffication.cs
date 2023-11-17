using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Timeline;
using UnityEngine;

public class Classiffication
{
    private string username; // Actual username
    private string map; // Actual map
    private string leg; // Actual leg
    private string stage; // Actual stage
    private TimeData time; // Time used to complete level, created as a TimeData
    private float damage; // Damage the car recived until level completed
    private float fuel; // Remaining fuel when level completed
    // Mosto common builder
    public Classiffication (string username, string map, string leg, string stage)
    {
        this.username = username;
        this.map = map;
        this.leg = leg;
        this.stage = stage;
        this.time = new TimeData();
        this.damage = 0;
        this.fuel = 0;
    }
    // Default builder
    public Classiffication()
    {
        this.username = "";
        this.map = "";
        this.leg = "";
        this.stage = "";
        this.time = new TimeData();
        this.damage = 0;
        this.fuel = 0;
    }
    // Method that saves actual classiffication
    public async Task<bool> SaveClassiffication()
    {
        bool success = false;
        // If classiffication does exists, it updates it. Inserts a new if not
        if (!await MySQLManager.InsertClassiffication(this))
        {
            Classiffication c = await MySQLManager.GetUserClassiffication(this.username, this.map, this.leg, this.stage);
            if (this.time.GetAllSeconds() > c.time.GetAllSeconds())
            {
                Debug.Log("Clasificacion anterior peor a la actual");
                success = await MySQLManager.UpdateClassiffication(this);
            }
        }
        else
            success = true;

        return success;
    }
    // Delete all classiffication of a specific user
    public async Task<bool> DeleteClassiffication(string username)
    {
        return await MySQLManager.DeleteClassiffication(username);
    }
    // This classiffication will transform into a spcefic classiffication from a single level
    public async Task<bool> GetUserClassiffication(string username, string map, string leg, string stage)
    {
        Classiffication c = await MySQLManager.GetUserClassiffication(username, map, leg, stage);
        this.SetUsername(username);
        this.SetMap(c.GetMap());
        this.SetLeg(c.GetLeg());
        this.SetStage(c.GetStage());
        float.TryParse(c.GetTime(), out float time);
        this.SetTime(time);
        this.SetDamage(c.GetDamage());
        this.SetFuel(c.GetFuel());
        return true;

    }
    // Returns a classiffication list with all classiffication from all levels and users
    public async Task<List<Classiffication>> GetAllClassiffication(string username)
    {
        return await MySQLManager.GetAllClassiffication(username);
    }
    // Geters and setters for all attributes
    public string GetUsername() { return this.username; }
    public void SetUsername(string username) { this.username = username; }
    public string GetMap() { return map; }
    public void SetMap(string map) { this.map = map; }
    public string GetLeg() { return leg; }
    public void SetLeg(string leg) {  this.leg = leg; }
    public string GetStage() { return stage; }
    public void SetStage(string stage) {  this.stage = stage; }
    public string GetTime() { return time.ToString(); }
    public void SetTime(float time) { this.time.UpdateSeconds(time); }
    public float GetDamage() { return damage; }
    public void SetDamage(float damage) {  this.damage = damage; }
    public float GetFuel() { return fuel; }
    public void SetFuel(float fuel) { this.fuel = fuel; }
}
