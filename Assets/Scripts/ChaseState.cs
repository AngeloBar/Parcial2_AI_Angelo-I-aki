using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChaseState : IState
{
    private FSM _fsm;
    private Enemy _enemy;

    public ChaseState(FSM fsm, Enemy enemy)
    {
        _fsm = fsm;
        _enemy = enemy;
    }

    public void OnEnter()
    {
        Debug.Log("Entre a Chase.");
        _enemy.Alert();
    }
    public void OnExit()
    {
        Debug.Log("Sali de Chase.");
    }

    public void OnUpdate()
    {
        Debug.Log("Entoy en Chase.");

        Vector3 lookAtDirection = (_enemy.target.position - _enemy.transform.position).normalized;
        _enemy.Movement(lookAtDirection);

        if (_enemy.InView(_enemy.target) == false)
        {
            _fsm.ChangeState(FSM.AgentStates.GoBack);
        }
    }
}
