using System.Collections.Generic;


namespace GameKit.EntityModule
{
    internal sealed partial class EntityManager : GameKitModule, IEntityManager
    {
        private sealed class EntityInfo : IReference
        {
            private IEntity m_Entity;
            private IEntity m_ParentEntity;
            private List<IEntity> m_ChildEntities;

            public EntityInfo()
            {
                m_Entity = null;
                m_ParentEntity = null;
                m_ChildEntities = new List<IEntity>();
            }
            #region Properties
            public IEntity Entity
            {
                get
                {
                    return m_Entity;
                }
            }
            public IEntity ParentEntity
            {
                get
                {
                    return m_ParentEntity;
                }
                set
                {
                    m_ParentEntity = value;
                }
            }

            public int ChildEntityCount
            {
                get
                {
                    return m_ChildEntities.Count;
                }
            }

            #endregion

            #region Public
            public IEntity GetChildEntity()
            {
                return m_ChildEntities.Count > 0 ? m_ChildEntities[0] : null;
            }

            public IEntity[] GetChildEntities()
            {
                return m_ChildEntities.ToArray();
            }

            public void GetChildEntities(List<IEntity> results)
            {
                if (results == null)
                {
                    throw new GameKitException("Results is invalid.");
                }

                results.Clear();
                foreach (IEntity childEntity in m_ChildEntities)
                {
                    results.Add(childEntity);
                }
            }

            public void AddChildEntity(IEntity childEntity)
            {
                if (m_ChildEntities.Contains(childEntity))
                {
                    throw new GameKitException("Can not add child entity which is already exist.");
                }

                m_ChildEntities.Add(childEntity);
            }

            public void RemoveChildEntity(IEntity childEntity)
            {
                if (!m_ChildEntities.Remove(childEntity))
                {
                    throw new GameKitException("Can not remove child entity which is not exist.");
                }
            }
            #endregion

            public static EntityInfo Create(IEntity entity)
            {
                if (entity == null)
                {
                    throw new GameKitException("Entity is invalid.");
                }

                EntityInfo entityInfo = ReferencePool.Acquire<EntityInfo>();
                entityInfo.m_Entity = entity;
                return entityInfo;
            }

            public void Clear()
            {
                m_Entity = null;
                m_ParentEntity = null;
                m_ChildEntities.Clear();
            }
        }
    }
}
