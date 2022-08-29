// using GameKit.Localization;
using System;
using GameKit.Localization;
using GameKit.Setting;
using UnityGameKit.Runtime;
using GameKit.Localization;
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

        // 构建信息：发布版本时，把一些数据以 Json 的格式写入 Assets/GameMain/Configs/BuildInfo.txt，供游戏逻辑读取
        // GameKitCenter.BuiltinData.InitBuildInfo();

        // 语言配置：设置当前使用的语言，如果不设置，则默认使用操作系统语言
        InitLanguageSettings();

        // 变体配置：根据使用的语言，通知底层加载对应的资源变体
        InitCurrentVariant();

        // 声音配置：根据用户配置数据，设置即将使用的声音选项
        InitSoundSettings();

        // 默认字典：加载默认字典文件 Assets/GameMain/Configs/DefaultDictionary.xml
        // 此字典文件记录了资源更新前使用的各种语言的字符串，会随 App 一起发布，故不可更新
        // GameKitCenter.BuiltinData.InitDefaultDictionary();
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        // 运行一帧即切换到 Splash 展示流程
        ChangeState<ProcedureSplash>(procedureOwner);
    }

    private void InitLanguageSettings()
    {
        if (GameKitCenter.Core.EditorResourceMode && GameKitCenter.Core.EditorLanguage != Language.Unspecified)
        {
            // 编辑器资源模式直接使用 Inspector 上设置的语言
            return;
        }

        Language language = GameKitCenter.Localization.Language;
        if (GameKitCenter.Setting.HasSetting(Constant.Setting.Language))
        {
            try
            {
                string languageString = GameKitCenter.Setting.GetString(Constant.Setting.Language);
                language = (Language)Enum.Parse(typeof(Language), languageString);
            }
            catch
            {
            }
        }

        if (language != Language.English
            && language != Language.ChineseSimplified
            && language != Language.ChineseTraditional
            && language != Language.Korean)
        {
            // 若是暂不支持的语言，则使用英语
            language = Language.English;

            GameKitCenter.Setting.SetString(Constant.Setting.Language, language.ToString());
            GameKitCenter.Setting.Save();
        }

        GameKitCenter.Localization.Language = language;
        // Log.Info("Init language settings complete, current language is '{0}'.", language.ToString());
    }

    private void InitCurrentVariant()
    {
        if (GameKitCenter.Core.EditorResourceMode)
        {
            // 编辑器资源模式不使用 AssetBundle，也就没有变体了
            return;
        }

        // string currentVariant = null;
        // switch (GameKitCenter.Localization.Language)
        // {
        //     case Language.English:
        //         currentVariant = "en-us";
        //         break;

        //     case Language.ChineseSimplified:
        //         currentVariant = "zh-cn";
        //         break;

        //     case Language.ChineseTraditional:
        //         currentVariant = "zh-tw";
        //         break;

        //     case Language.Korean:
        //         currentVariant = "ko-kr";
        //         break;

        //     default:
        //         currentVariant = "zh-cn";
        //         break;
        // }

        // GameKitCenter.Resource.SetCurrentVariant(currentVariant);
        // Log.Info("Init current variant complete.");
    }

    private void InitSoundSettings()
    {
        // GameKitCenter.Sound.Mute("Music", GameKitCenter.Setting.GetBool(Constant.Setting.MusicMuted, false));
        // GameKitCenter.Sound.SetVolume("Music", GameKitCenter.Setting.GetFloat(Constant.Setting.MusicVolume, 0.3f));
        // GameKitCenter.Sound.Mute("Sound", GameKitCenter.Setting.GetBool(Constant.Setting.SoundMuted, false));
        // GameKitCenter.Sound.SetVolume("Sound", GameKitCenter.Setting.GetFloat(Constant.Setting.SoundVolume, 1f));
        // GameKitCenter.Sound.Mute("UISound", GameKitCenter.Setting.GetBool(Constant.Setting.UISoundMuted, false));
        // GameKitCenter.Sound.SetVolume("UISound", GameKitCenter.Setting.GetFloat(Constant.Setting.UISoundVolume, 1f));
        // Log.Info("Init sound settings complete.");
    }
}

