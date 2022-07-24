using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public int Cash { get; set; }
    public int Day {get; set; }
    public float PopulationCurrent { get; set; }
    public float PopulationCeiling { get; set; }
    public int JobsCurrent { get; set; }
    public int JobsCeiling { get; set; }
    public float Food { get; set; }

    public int[] buildingCounts = new int[4];
    private UIController _uiController;

    void Start()
    {
        _uiController = GetComponent<UIController>();
        Cash = 50;
    }
    
    public void EndTurn()
    {
        Day++;
        CalculatePopulation();
        CalculateJobs();
        CalculateFood();
        CalculateCash();

        Debug.Log("Day ended.");
        _uiController.UpdateCityInfo();
        _uiController.UpdateDayCount();
        Debug.LogFormat
            ("Jobs: {0}/{1}, Cash: {2}, Pop: {3}/{4}, Food: {5}", 
            JobsCurrent, JobsCeiling, Cash, PopulationCurrent, PopulationCeiling, Food);
    }
    
    void CalculateJobs()
    {
        JobsCeiling = buildingCounts[3] * 10;
        JobsCurrent = Mathf.Min((int)PopulationCurrent, JobsCeiling);
    }

    public void DepositCash(int cash)
    {
        Cash += cash;
    }
    
    void CalculateCash()
    {
        Cash += JobsCurrent * 2;
    }

    void CalculateFood()
    {
        Food += buildingCounts[2] * 4;
    }

    void CalculatePopulation()
    {
        PopulationCeiling = buildingCounts[1] * 5;

        //calculate current population only when food is enough and have not reach the max yet
        if (Food >= PopulationCurrent && PopulationCurrent < PopulationCeiling)  
        {
            Food -= PopulationCurrent * .25f; //every 4 population take 1 food
            //the more food, more population increase, every 4 food increase 1 population
            PopulationCurrent = Mathf.Min(PopulationCurrent += Food * .25f, PopulationCeiling); 
        }
        else if(Food < PopulationCurrent)
        {
            //half amount of lacking food would be the decrease value of population
            PopulationCurrent -= (PopulationCurrent - Food) * .5f;
        }
    }
}
