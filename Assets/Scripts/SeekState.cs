using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SeekState : IState
{
    private FSM _fsm;
    private Enemy _enemy;

    public SeekState(FSM fsm, Enemy enemy)
    {
        _fsm = fsm;
        _enemy = enemy;
    }
    public void OnEnter()
    {
        Debug.Log("Entre a Seek.");

        _enemy.playerFound = false;
        _enemy.path = Pathfinding.instance.CalculateAStar(ManagerNodes.instance.GetMinNode(_enemy.transform.position), ManagerNodes.instance.GetMinNode(_enemy.target.position));
    }
    public void OnExit()
    {
        Debug.Log("Sali de Seek.");
    }
    public void OnUpdate()
    {
        Debug.Log("Entoy en Seek.");

        var Arrive = _enemy.Seek();
        Debug.Log(Arrive);
        if (Arrive == true)
        {
            Debug.Log("Cambie a: " + Arrive);
            _fsm.ChangeState(FSM.AgentStates.GoBack);
        }

        if (_enemy.InView(_enemy.target))
        {
            _fsm.ChangeState(FSM.AgentStates.Chase);
        }
    }
}
