using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM
{
    StateMachine stateMachine;
    StateMachine.State chasingState;
    StateMachine.State rangeAttackState;
    StateMachine.State runAwayState;
    StateMachine.State meleeAttackState;

    private float bowRange, dangerRange, meleeRange;
    public EnemyManager enemyManager;
    public float PlayerDistance { get; set; } 
    public void Init(float _bowRange, float _dangerRange, float _meleeRange, EnemyManager _enemyManager)
    {
        bowRange = _bowRange;
        dangerRange = _dangerRange;
        meleeRange = _meleeRange;
        enemyManager = _enemyManager;

        chasingState = new StateMachine.State();
        rangeAttackState = new StateMachine.State();
        runAwayState = new StateMachine.State();
        meleeAttackState = new StateMachine.State();
        stateMachine = new StateMachine()
        {
            states = new StateMachine.State[]
            {
                CreateChasingState(), CreateRangeAttackState(), CreateRunAwayState(), CreateMeleeAttackState()
            },initialState = chasingState
            
        };
        stateMachine.Init();
        CreateChasingStateTransition();
        CreateRangeAttackStateTransition();
        CreateRunAayStateTransition();
        CreateMeleeStateTransition();
    }
    #region create State
    private StateMachine.State CreateChasingState()
    {
        chasingState = new StateMachine.State()
        {
            EntryAction = () => {
                
            },
            UpdateAction = () => {
                enemyManager.UpdateChasing();
                //Debug.Log("Update Chasing");
            },
            ExitAction = () => {
                enemyManager.StopChasing();
                //Debug.Log("Stop Chasing");
            }
        };
        return chasingState;
    }
    private StateMachine.State CreateRangeAttackState()
    {
        rangeAttackState = new StateMachine.State()
        {
            EntryAction = () => { },
            UpdateAction = () => {
                enemyManager.UpdateAttacking();
                //Debug.Log("Update Action rangeAttackState"); 
            },
            ExitAction = () => { }
        };
        return rangeAttackState;
    }
    private StateMachine.State CreateRunAwayState()
    {
        runAwayState = new StateMachine.State()
        {
            EntryAction = () => { },
            UpdateAction = () => {
                enemyManager.UpdateAttacking();
                //Debug.Log("Update Action runAwayState"); 
            },
            ExitAction = () => { }
        };
        return runAwayState;
    }
    private StateMachine.State CreateMeleeAttackState()
    {
        meleeAttackState = new StateMachine.State()
        {
            EntryAction = () => { },
            UpdateAction = () => {
                enemyManager.UpdateAttacking();
                //Debug.Log("Update Action meleeAttackState");
            },
            ExitAction = () => { }
        };
        return meleeAttackState;
    }
    #endregion
    #region Create Transition
    private void CreateChasingStateTransition()
    {
        chasingState.transitions = new StateMachine.Transition[]
        {
            new StateMachine.Transition()
            {
                targetState = rangeAttackState,
                IsTriggered = () => PlayerDistance <= bowRange - 0.1f
            }
        };
    }
    private void CreateRangeAttackStateTransition()
    {
        rangeAttackState.transitions = new StateMachine.Transition[]
        {
            new StateMachine.Transition()
            {
                targetState = chasingState,
                IsTriggered = () => PlayerDistance > bowRange
            },
            new StateMachine.Transition()
            {
                targetState = runAwayState,
                IsTriggered = () => PlayerDistance <= dangerRange - 0.1f
            }
        };
    }
    private void CreateRunAayStateTransition()
    {
        runAwayState.transitions = new StateMachine.Transition[]
        {
            new StateMachine.Transition()
            {
                targetState = rangeAttackState,
                IsTriggered = () => PlayerDistance > dangerRange
            },
            new StateMachine.Transition()
            {
                targetState = meleeAttackState,
                IsTriggered = () => PlayerDistance <= meleeRange -0.1f
            }
        };
    }
    private void CreateMeleeStateTransition()
    {
        meleeAttackState.transitions = new StateMachine.Transition[]
        {
            new StateMachine.Transition()
            {
                targetState = runAwayState,
                IsTriggered = () => PlayerDistance > meleeRange
            }
        };
    }
    #endregion
    public void Update()
    {
        //Debug.Log("Dic:" + PlayerDistance + "---Range:" + meleeRange);
        var _action = stateMachine.UpdateAndGetAction();
        _action.Invoke();
    }
}
