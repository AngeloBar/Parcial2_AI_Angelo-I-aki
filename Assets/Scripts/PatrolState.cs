using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private FSM _fsm;
    private Enemy _enemy;

    public PatrolState(FSM fsm, Enemy enemy)
    {
        _fsm = fsm;
        _enemy = enemy;
    }

    public void OnEnter()
    {
        Debug.Log("Entre a Patrol.");
    }

    public void OnExit()
    {
        Debug.Log("Sali de Patrol.");
    }

    public void OnUpdate()
    {
        Debug.Log("Entoy en Patrol.");

        _enemy.Patrol();

        if (_enemy.InView(_enemy.target))
        {
            _fsm.ChangeState(FSM.AgentStates.Chase);
        }

        if (_enemy.playerFound)
        {
            _fsm.ChangeState(FSM.AgentStates.Seek);
        }
    }
}
