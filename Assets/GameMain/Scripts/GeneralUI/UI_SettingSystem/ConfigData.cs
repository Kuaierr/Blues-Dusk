using System;
using System.Collections.Generic;

public class ConfigData
{
    //Basic
    private CustomConfigOptionSet b_TextLanguage;
    private CustomConfigOptionSet b_AudioLanguage;
    private CustomConfigOptionSet b_AutoPlay;
    private CustomConfigOptionSet b_TextSpeed;

    private CustomConfigOptionSet b_HandleVibration;

    //Pixel
    private CustomConfigOptionSet p_ScreenResolution;
    private CustomConfigOptionSet p_FullScreen;

    //Visual
    private CustomConfigOptionSet v_EnvironmentFX;
    private CustomConfigOptionSet v_Antialiasing;
    private CustomConfigOptionSet v_VerticalSync;
    private CustomConfigOptionSet v_Noise;
    private CustomConfigOptionSet v_FrameLimit;

    //Audio
    private CustomConfigOptionSet a_GlobalVolume;
    private CustomConfigOptionSet a_EnvironmentVolume;
    private CustomConfigOptionSet a_DiscVolume;
    private CustomConfigOptionSet a_VoiceVolume;
    private CustomConfigOptionSet a_FxVolume;
    private CustomConfigOptionSet a_Mute;

    //预期是通过列表对应每个Sheet
    private List<CustomConfigOptionSet> _basicConfigOptions;
    private List<CustomConfigOptionSet> _pixelConfigOptions;
    private List<CustomConfigOptionSet> _visualConfigOptions;
    private List<CustomConfigOptionSet> _audioConfigOptions;

    private List<List<CustomConfigOptionSet>> _configOptions;

    public List<CustomConfigOptionSet> BasicConfigOptions
    {
        get
        {
            if (_basicConfigOptions == null)
            {
                _basicConfigOptions = new List<CustomConfigOptionSet>();
                _basicConfigOptions.Add(b_TextLanguage);
                _basicConfigOptions.Add(b_AudioLanguage);
                _basicConfigOptions.Add(b_AutoPlay);
                _basicConfigOptions.Add(b_TextSpeed);
                _basicConfigOptions.Add(b_HandleVibration);
            }

            return _basicConfigOptions;
        }
    }

    public List<CustomConfigOptionSet> PixelConfigOptions
    {
        get
        {
            if (_pixelConfigOptions == null)
            {
                _pixelConfigOptions = new List<CustomConfigOptionSet>();
                _pixelConfigOptions.Add(p_ScreenResolution);
                _pixelConfigOptions.Add(p_FullScreen);
            }

            return _pixelConfigOptions;
        }
    }

    public List<CustomConfigOptionSet> VisualConfigOptions
    {
        get
        {
            if (_visualConfigOptions == null)
            {
                _visualConfigOptions = new List<CustomConfigOptionSet>();
                _visualConfigOptions.Add(v_EnvironmentFX);
                _visualConfigOptions.Add(v_Antialiasing);
                _visualConfigOptions.Add(v_VerticalSync);
                _visualConfigOptions.Add(v_Noise);
                _visualConfigOptions.Add(v_FrameLimit);
            }

            return _visualConfigOptions;
        }
    }

    public List<CustomConfigOptionSet> AudioConfigOptions
    {
        get
        {
            if (_audioConfigOptions == null)
            {
                _audioConfigOptions = new List<CustomConfigOptionSet>();
                _audioConfigOptions.Add(a_GlobalVolume);
                _audioConfigOptions.Add(a_EnvironmentVolume);
                _audioConfigOptions.Add(a_VoiceVolume);
                _audioConfigOptions.Add(a_DiscVolume);
                _audioConfigOptions.Add(a_FxVolume);
                _audioConfigOptions.Add(a_Mute);
            }

            return _audioConfigOptions;
        }
    }

    public List<List<CustomConfigOptionSet>> ConfigOptions
    {
        get
        {
            if (_configOptions == null)
            {
                _configOptions = new List<List<CustomConfigOptionSet>>();
                _configOptions.Add(BasicConfigOptions);
                _configOptions.Add(PixelConfigOptions);
                _configOptions.Add(VisualConfigOptions);
                _configOptions.Add(AudioConfigOptions);
            }

            return _configOptions;
        }
    }

    public ConfigData()
    {
        _basicConfigOptions = null;
        _pixelConfigOptions = null;
        _visualConfigOptions = null;
        _audioConfigOptions = null;
        _configOptions = null;

        b_TextLanguage = new CustomConfigOptionSet(new List<string>()
        { "SC", "TC", "JP", "EN" }, new List<string>()
        { "简体中文", "繁体中文", "日语", "英语" }, 0, "游戏语言");
        b_AudioLanguage = new CustomConfigOptionSet(new List<string>()
        { "EN", "JP" }, new List<string>()
        { "英文", "日语" }, 0, "语音语言");
        b_AutoPlay = new CustomConfigOptionSet(new List<string>()
        { "OFF", "ON" }, new List<string>()
        { "禁用", "启用" }, 0, "文本自动播放");
        b_TextSpeed = new CustomConfigOptionSet(new List<string>()
        { "SLOW", "NORMAL", "FAST" }, new List<string>()
        { "慢速", "普通", "快速" }, 1, "文本速度");
        b_HandleVibration = new CustomConfigOptionSet(new List<string>()
        { "OFF", "ON" }, new List<string>()
        { "禁用", "启用" }, 1, "手柄震动");

        p_ScreenResolution = new CustomConfigOptionSet(
            new List<string>()
            { "1176*664", "1280*720", "1360*768", "1600*900", "1920*1080", "2560*1440" },
            new List<string>()
            { "1176*664", "1280*720", "1360*768", "1600*900", "1920*1080", "2560*1440" },
            4, "屏幕分辨率");
        p_FullScreen = new CustomConfigOptionSet(new List<string>()
        { "OFF", "ON" }, new List<string>()
        { "禁用", "启用" }, 1, "全屏");

        v_EnvironmentFX = new CustomConfigOptionSet(new List<string>()
        { "LOW", "MID", "HIGH" }, new List<string>()
        { "低", "中", "高" }, 2, "环境FX");
        v_Antialiasing = new CustomConfigOptionSet(new List<string>()
        { "OFF", "ON" }, new List<string>()
        { "禁用", "启用" }, 1, "抗锯齿");
        v_VerticalSync = new CustomConfigOptionSet(new List<string>()
        { "OFF", "ON" }, new List<string>()
        { "禁用", "启用" }, 1, "垂直同步");
        v_Noise = new CustomConfigOptionSet(new List<string>()
        { "OFF", "ON" }, new List<string>()
        { "禁用", "启用" }, 1, "噪点");
        v_FrameLimit = new CustomConfigOptionSet(new List<string>()
        { "OFF", "ON" }, new List<string>()
        { "禁用", "启用" }, 1, "限制帧率");

        a_GlobalVolume = new CustomConfigOptionSet(new List<string>()
            { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" },
            new List<string>()
            { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" }, 10, "全局音量");
        a_EnvironmentVolume= new CustomConfigOptionSet(new List<string>()
            { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" },
            new List<string>()
            { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" }, 10, "环境音量");
        a_DiscVolume= new CustomConfigOptionSet(new List<string>()
            { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" },
            new List<string>()
            { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" }, 10, "唱片机音量");
        a_VoiceVolume= new CustomConfigOptionSet(new List<string>()
            { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" },
            new List<string>()
            { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" }, 10, "语音音量");
        a_FxVolume= new CustomConfigOptionSet(new List<string>()
            { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" },
            new List<string>()
            { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11" }, 10, "音效音量");
        a_Mute = new CustomConfigOptionSet(new List<string>()
            { "GLOBAL_ON", "GLOBAL_MUTE", "MUSIC_MUTE", "VOICE_MUTE" },
            new List<string>()
            { "全局播放", "全局静音", "音乐静音", "语音静音" }, 1, "后台静音");
    }

    //获取总配置
    public Dictionary<string, string> GetResult()
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        foreach (CustomConfigOptionSet set in BasicConfigOptions)
        {
            result.Add(set.ConfigName, set.CurrentOptionKey);
        }

        return result;
    }
}

public class CustomConfigOptionSet
{
    private List<CustomConfigUnit> _units = new List<CustomConfigUnit>();
    private int _defaultIndex = 0;
    public int CurrentIndex { get; private set; }
    public string ConfigName { get; private set; }

    public CustomConfigOptionSet(List<string> keys, List<string> chinese, int defaultIndex, string name)
    {
        for (int i = 0; i < keys.Count; i++)
            _units.Add(new CustomConfigUnit(keys[i], chinese[i]));

        CurrentIndex = defaultIndex;
        _defaultIndex = defaultIndex;
        ConfigName = name;
    }

    public string CurrentOptionKey => _units[CurrentIndex].Key;
    public string CurrentOptionName => _units[CurrentIndex].Chinese;

    public string Add()
    {
        int result = CurrentIndex + 1;
        if (result >= _units.Count)
            result = 0;
        CurrentIndex = result;
        return _units[CurrentIndex].Key;
    }

    public string Minus()
    {
        int result = CurrentIndex - 1;
        if (result < 0)
            result = _units.Count - 1;
        CurrentIndex = result;
        return _units[CurrentIndex].Key;
    }
}

public class CustomConfigUnit
{
    /// <summary>
    /// 起配置作用
    /// </summary>
    public string Key { get; private set; }

    /// <summary>
    /// UI上显示的内容
    /// </summary>
    public string Chinese { get; private set; }

    public CustomConfigUnit(string key, string chinese)
    {
        Key = key;
        Chinese = chinese;
    }

    public string GetText()
    {
        return Chinese;
    }
}