using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHandler : MonoBehaviour
{
    [SerializeField]
    private City _city;
    [SerializeField]
    private UIController _uiController;
    [SerializeField]
    private Building[] _buildings;
    [SerializeField]
    private Board _board;
    private Building selectedBuilding;

    public void EnableBuilder(int building)
    {
        selectedBuilding = _buildings[building];
        Debug.Log("Selected building: " + selectedBuilding.buildingName);
    }

    void Update()
    {
        //check if mouse clicked and building has selected
        if(Input.GetMouseButton (0) && Input.GetKey(KeyCode.LeftShift) && selectedBuilding != null)
        {
            InteractWithBoard(0);
        }
        else if(Input.GetMouseButtonDown(0) && selectedBuilding != null)
        {
            InteractWithBoard(0);
        }

        if(Input.GetMouseButtonDown(1))
        {
            InteractWithBoard(1);
        }
    }

    void InteractWithBoard(int action)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            //send the position of mouse click to Board script
            Vector3 gridPosition = _board.CalculateGridPosition(hit.point);

            //check if mouse click blocked by UI buttons
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) 
            {
                //false means have vacancy
                if (action == 0 && _board.CheckForBuildingAtPosition(gridPosition) == null) //left click on empty position
                {
                    if (_city.Cash >= selectedBuilding.cost) //check if have enough cash
                    {
                        _city.DepositCash(-selectedBuilding.cost); //buliding cost
                        _uiController.UpdateCityInfo(); //update city info
                        _city.buildingCounts[selectedBuilding.id]++; //add counts of building
                        _board.AddBuilding(selectedBuilding, gridPosition); //placed building
                    }
                }
                else if(action == 1 && _board.CheckForBuildingAtPosition(gridPosition) != null) //right click on existing building
                {
                    _city.DepositCash(_board.CheckForBuildingAtPosition(gridPosition).cost / 2); //return half cost of building to cash
                    _city.buildingCounts[_board.CheckForBuildingAtPosition(gridPosition).id] -= 1; //decrease 1 from selected building counts
                    _board.RemoveBuilding(gridPosition); //remove selected building
                    _uiController.UpdateCityInfo();
                }
            } 
        }
    }
}
