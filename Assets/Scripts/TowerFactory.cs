﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;
    [SerializeField] Transform towerParentTransform;

    Queue<Tower> TowerQueue = new Queue<Tower>();

    public void AddTower(Waypoint baseWaypoint)
    {
        print(TowerQueue.Count);
        int numTowers = TowerQueue.Count;

        if (numTowers < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        var newTower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        newTower.transform.parent = towerParentTransform;
        baseWaypoint.isPlaceable = false;

        newTower.baseWaypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        TowerQueue.Enqueue(newTower);
    }

    private void MoveExistingTower(Waypoint newBaseWaypoint)
    {
        var oldTower = TowerQueue.Dequeue();

        oldTower.baseWaypoint.isPlaceable = true;
        newBaseWaypoint.isPlaceable = false;

        oldTower.baseWaypoint = newBaseWaypoint;

        oldTower.transform.position = newBaseWaypoint.transform.position;

        TowerQueue.Enqueue(oldTower);
    }
}
