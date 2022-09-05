using System.Collections.Generic;
using UnityGameKit.Runtime;
using ProcedureOwner = GameKit.Fsm.IFsm<GameKit.Procedure.IProcedureManager>;

public class ProcedureWorld : ProcedureBase
{
    private const float GameOverDelayedSeconds = 2f;
    private readonly Dictionary<GameMode, GameBase> m_Games = new Dictionary<GameMode, GameBase>();
    private GameBase m_CurrentGame = null;
    private bool m_GotoMenu = false;
    private float m_GotoMenuDelaySeconds = 0f;

    public override bool UseNativeDialog
    {
        get
        {
            return false;
        }
    }

    public void GotoMenu()
    {
        m_GotoMenu = true;
    }

    protected override void OnInit(ProcedureOwner procedureOwner)
    {
        base.OnInit(procedureOwner);
        m_Games.Add(GameMode.PlotMode, new PlotGame());
    }

    protected override void OnDestroy(ProcedureOwner procedureOwner)
    {
        base.OnDestroy(procedureOwner);

        // m_Games.Clear();
    }

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
        base.OnEnter(procedureOwner);

        m_GotoMenu = false;
        GameMode gameMode = (GameMode)procedureOwner.GetData<VarByte>("GameMode").Value;
        m_CurrentGame = m_Games[gameMode];
        m_CurrentGame.Initialize();
    }

    protected override void OnExit(ProcedureOwner procedureOwner, bool isShutdown)
    {
        if (m_CurrentGame != null)
        {
            m_CurrentGame.Shutdown();
            m_CurrentGame = null;
        }

        base.OnExit(procedureOwner, isShutdown);
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        if (m_CurrentGame != null && !m_CurrentGame.GameOver)
        {
            m_CurrentGame.Update(elapseSeconds, realElapseSeconds);
            return;
        }

        if (!m_GotoMenu)
        {
            m_GotoMenu = true;
            m_GotoMenuDelaySeconds = 0;
        }

        m_GotoMenuDelaySeconds += elapseSeconds;
        if (m_GotoMenuDelaySeconds >= GameOverDelayedSeconds)
        {
            procedureOwner.SetData<VarString>(ProcedureStateUtility.NEXT_SCENE_NAME, "GameMenu");
            ChangeState<ProcedureChangeScene>(procedureOwner);  
        }
    }
}

