using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    private float time;
    private string name;
    private string description;
    private float points;

    public Task(float time, string name, string description, float points)
    {
        this.time = time;
        this.name = name;
        this.description = description;
        this.points = points;
    }   

    public float Complete()
    {
        return points; //TODO: Point Calculation
    }

    public float getTime()
    { return time; }
    public string getName() { return name; }
    public string getDescription() { return description; }
    public float getPoints() { return points; }

    public void setTime(float time)
    {
        this.time = time;
    }
    public void setName(string name)
    {
        this.name = name;
    }
    public void setDescription(string description)
    { this.description = description;
    }
    public void setPoints(float points)
    {
        this.points = points;
    }
}
