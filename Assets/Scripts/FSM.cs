using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM
{
    public enum AgentStates
    {
        Patrol,
        Seek,
        Chase,
        GoBack
    }
    private Dictionary<AgentStates, IState> _allStates=new Dictionary<AgentStates, IState>();
    private IState _currentState;
    public void AddState(AgentStates newState,IState state)
    {
        if (_allStates.ContainsKey(newState))
            return;
        _allStates.Add(newState, state);
    }
    public void ArtificialUpdate()
    {
        if (_currentState != null)
        {
            _currentState.OnUpdate();
        }
    }
    public void ChangeState(AgentStates newState)
    {
        if (_allStates.ContainsKey(newState))
        {
            if(_currentState!=null)
                _currentState.OnExit();
            _currentState = _allStates[newState];
            _currentState.OnEnter();
        }
    }
}
