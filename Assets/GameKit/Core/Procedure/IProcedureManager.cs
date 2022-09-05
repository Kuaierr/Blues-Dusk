using System;
using GameKit.Fsm;

namespace GameKit.Procedure
{
    public interface IProcedureManager
    {
        ProcedureBase CurrentProcedure { get; }
        float CurrentProcedureTime { get; }
        void Initialize(IFsmManager fsmManager, params ProcedureBase[] procedures);
        void StartProcedure<T>() where T : ProcedureBase;
        void StartProcedure(Type procedureType);
        bool HasProcedure<T>() where T : ProcedureBase;
        bool HasProcedure(Type procedureType);
        ProcedureBase GetProcedure<T>() where T : ProcedureBase;
        ProcedureBase GetProcedure(Type procedureType);
        void LoadScene<T>(string sceneName) where T : ProcedureBase;
        void LoadScene(string sceneName, Type procedureType);
        void SetData<T>(string dataName) where T : Variable;
        void SetData(string dataName, Type dataType);
    }
}
