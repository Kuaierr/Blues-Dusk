public interface IEntityManager
{
    void ShowEntity(int entityId, string entityAssetName, string entityGroupName, int priority = 0);
    void HideEntity(int entityId);
    bool HasEntity(int entityId);
    bool HasEntity(string entityAssetName);
    int GetChildEntityCount(int parentEntityId);
    IEntity GetParentEntity(int childEntityId);
    IEntity GetChildEntity(int parentEntityId);
    IEntity[] GetChildEntities(int parentEntityId);
    IEntity GetEntity(int entityId);
    IEntity GetEntity(string entityAssetName);
    IEntity[] GetEntities(string entityAssetName);
    IEntity[] GetAllEntities();
}