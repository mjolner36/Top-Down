using UnityEngine;

public class RunState : BaseState
{
    public override void EnterState(EnemyController npc)
    {
        npc.animator.SetTrigger("Run");
    }

    public override void UpdateState(EnemyController npc)
    {
        npc.agent.SetDestination(GameManager.Instance.playerRef.transform.position);
        var heading = GameManager.Instance.playerRef.transform.position - npc.transform.position;
        if (heading.sqrMagnitude > npc.rangeToSee() * npc.rangeToSee())
        {
            npc.SwitchState(npc.idleState);
        }
        else if (heading.sqrMagnitude <= npc.attackRange() * npc.attackRange())
        {
            npc.SwitchState(npc.attackState);
            
        }
       
        
    }

    public override void ExitState(EnemyController npc)
    {
        
    }
}
