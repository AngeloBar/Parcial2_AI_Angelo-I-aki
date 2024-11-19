using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackState : IState
{
    private FSM _fsm;
    private Enemy _enemy;

    public GoBackState(FSM fsm, Enemy enemy)
    {
        _fsm = fsm;
        _enemy = enemy;
    }

    public void OnEnter()
    {
        Debug.Log("Entre a GoBack.");
        _enemy.path = Pathfinding.instance.CalculateAStar(ManagerNodes.instance.GetMinNode(_enemy.transform.position), ManagerNodes.instance.GetMinNode(_enemy.patroll[_enemy.currentPatrolPoint].transform.position));
    }

    public void OnExit()
    {
        Debug.Log("Sali de GoBack.");
    }

    public void OnUpdate()
    {
        Debug.Log("Entoy en GoBack.");

        var Arrive = _enemy.GoBack();
        if (Arrive == true)
        {
            _fsm.ChangeState(FSM.AgentStates.Patrol);
        }
    }
}
