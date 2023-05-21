using UnityEngine;

public class AttackState : BaseState
{
    public override void EnterState(EnemyController npc)
    {
        npc.animator.SetTrigger("Attack");
    }

    public override void UpdateState(EnemyController npc)
    {
        var heading = GameManager.Instance.playerRef.transform.position - npc.transform.position;
        if (heading.sqrMagnitude > npc.attackRange() * npc.attackRange()) npc.SwitchState(npc.runState);  
    }

    public override void ExitState(EnemyController npc)
    {
        
    }
}
