using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public class State
    {
        public Transition[] transitions;
        public Action UpdateAction;
        public Action EntryAction;
        public Action ExitAction;
    }
    public class Transition
    {
        public Func<bool> IsTriggered;
        public State targetState;
        public Action action;
    }
    public State[] states;    
    public State initialState;
    public State currentState;
    private Action actions;
    public void Init()
    {
        currentState = initialState;
    }
    public Action UpdateAndGetAction()
    {
        Transition triggeredTransition = null;
        foreach(var transition in currentState.transitions)
        {
            if(transition.IsTriggered())
            {
                triggeredTransition = transition;
                break;
            }
        }
        if (triggeredTransition != null)
        {
            var targetState = triggeredTransition.targetState;
            actions = currentState.ExitAction;
            actions += triggeredTransition.action;
            actions += targetState.EntryAction;
            currentState = targetState;
            return actions;
        }
        else
        {
            return currentState.UpdateAction;
        }
    }
}
