using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text _dayText;
    [SerializeField]
    private Text _cityText;
    private City _city;
    
    void Start()
    {
        _city = GetComponent<City>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDayCount()
    {
        _dayText.text = string.Format("Day {0}", _city.Day);
    }

    public void UpdateCityInfo()
    {
        _cityText.text = string.Format
            ("Cash: {0} (+{1})\nPopulation: {2}/{3}\nFood: {4}\nJobs: {5}/{6}",
            _city.Cash, _city.JobsCurrent * 2, (int)_city.PopulationCurrent, (int)_city.PopulationCeiling,
            (int)_city.Food, _city.JobsCurrent, _city.JobsCeiling);
    }
}
