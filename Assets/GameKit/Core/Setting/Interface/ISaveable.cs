using System;
using System.Collections.Generic;

namespace GameKit.Setting
{
    public interface ISaveable
    {
        void OnLoad();
        void OnSave();
    }
}