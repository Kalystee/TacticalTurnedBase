using PNN.Entities;

public class Battleground_OnEntitySpawnedArgs
{
    public readonly Entity affectedEntity;

    public Battleground_OnEntitySpawnedArgs(Entity affectedEntity)
    {
        this.affectedEntity = affectedEntity;
    }
}
