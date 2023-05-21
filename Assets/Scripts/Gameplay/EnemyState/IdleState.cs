using UnityEngine;

public class IdleState : BaseState
{
    public override void EnterState(EnemyController npc)
    {
        npc.animator.SetBool("isWaiting",true);
    }

    public override void UpdateState(EnemyController npc)
    {
        var heading = GameManager.Instance.playerRef.transform.position - npc.transform.position;
        if (heading.sqrMagnitude <= npc.rangeToSee() * npc.rangeToSee()) npc.SwitchState(npc.runState);
    }

    public override void ExitState(EnemyController npc)
    {
        npc.animator.SetBool("isWaiting",false);
    }
}
