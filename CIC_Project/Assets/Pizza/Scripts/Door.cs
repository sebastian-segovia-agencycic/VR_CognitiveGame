using System.Collections;
using System.Collections.Generic;
using Tilia.Interactions.Controllables.LinearDriver;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool canOpen = false, onlyOne = true;
    public RoomManager roomManager;
    public LinearDriveFacade controlable;

    public ConveyorMovement conveyorMovement;

    public void OpenDoor()
    {
        canOpen = CheckBools();
        if (canOpen)
            controlable.MoveToTargetValue = true;
        else
            controlable.MoveToTargetValue = false;
    }

    public void MoveConveyor()
    {
        canOpen = CheckBools();
        if (canOpen && onlyOne)
        {
            onlyOne = false;
            conveyorMovement.MoveToNextWaypoint();
        }
    }


    private bool CheckBools()
    {
        foreach (var element in roomManager.listBool)
        {
            if (element == false)
                return false;
        }
        return true;
    }
}
