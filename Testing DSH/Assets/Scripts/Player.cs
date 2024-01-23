using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string name;
    private float score;

    public Player(string name, float score)
    {
        this.name = name;
        this.score = score;
    }

    public string getName()
    {
        return this.name;
    }
    public float getScore()
    {
        return this.score;
    }

    public void setName(string name)
    {
        this.name = name;
    }
    public void setScore(float score)
    { 
        this.score = score;
    }
}
