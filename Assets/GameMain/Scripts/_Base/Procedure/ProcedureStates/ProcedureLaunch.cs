using System;
using GameKit.Setting;
using UnityGameKit.Runtime;
using ProcedureOwner = GameKit.Fsm.IFsm<GameKit.Procedure.IProcedureManager>;

public class ProcedureLaunch : ProcedureBase
{
    public override bool UseNativeDialog
    {
        get
        {
            return true;
        }
    }

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        // 语言配置：设置当前使用的语言，如果不设置，则默认使用操作系统语言
        InitLanguageSettings();

        // 变体配置：根据使用的语言，通知底层加载对应的资源变体
        InitCurrentVariant();

        // 声音配置：根据用户配置数据，设置即将使用的声音选项
        InitSoundSettings();
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        // 运行一帧即切换到 Splash 展示流程
        ChangeState<ProcedureSplash>(procedureOwner);
    }

    private void InitLanguageSettings()
    {

    }

    private void InitCurrentVariant()
    {

    }

    private void InitSoundSettings()
    {

    }
}

