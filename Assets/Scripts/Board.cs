using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    //set a 2-dimentional array of buildings
    private Building[,] _buildings = new Building[100, 100]; 

    public Vector3 CalculateGridPosition(Vector3 position)
    {
        //round the click position on x & z, and set the height of y on .25
        return new Vector3(Mathf.Round(position.x), .5f, Mathf.Round(position.z));
    }

    public void AddBuilding(Building building, Vector3 position)
    {
        //add buildings(based on x and z), then add a building(based on button clicked)
        _buildings[(int)position.x, (int)position.z] = Instantiate(building, position, Quaternion.identity);
    }

    public Building CheckForBuildingAtPosition(Vector3 position)
    {
        //check if position that clicked had a building already
        return _buildings[(int)position.x, (int)position.z];
    }

    public void RemoveBuilding(Vector3 position)
    {
        Destroy(_buildings[(int)position.x, (int)position.z].gameObject);
        _buildings[(int)position.x, (int)position.z] = null;
    }
}
