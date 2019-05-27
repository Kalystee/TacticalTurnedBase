using PNN.Entities;

public class Battleground_OnEntityDespawnedArgs
{
    public readonly Entity affectedEntity;

    public Battleground_OnEntityDespawnedArgs(Entity affectedEntity)
    {
        this.affectedEntity = affectedEntity;
    }
}
