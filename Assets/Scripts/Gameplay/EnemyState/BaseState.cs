public abstract class BaseState 
{
    public abstract void EnterState(EnemyController npc);
    
    public abstract void UpdateState(EnemyController npc);
    public abstract void ExitState(EnemyController npc);
}
