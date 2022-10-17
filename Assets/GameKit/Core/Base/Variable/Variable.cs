using System;

namespace GameKit
{
    public abstract class Variable : IReference
    {
        public Variable()
        {
            
        }

        public abstract Type Type
        {
            get;
        }

        public abstract object GetValue();

        public abstract void SetValue(object value);

        public abstract void Clear();
    }
}
