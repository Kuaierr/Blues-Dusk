using System;
using System.Collections.Generic;

public class ConfigData
{
    //Basic
    private CustomConfigOptionSet _textLanguage;
    private CustomConfigOptionSet _audioLanguage;
    private CustomConfigOptionSet _autoPlay;
    private CustomConfigOptionSet _textSpeed;
    private CustomConfigOptionSet _handleVibration;

    //预期是通过列表对应每个Sheet
    private List<CustomConfigOptionSet> _basicConfigOptions;

    public List<CustomConfigOptionSet> BasicConfigOptions
    {
        get
        {
            if (_basicConfigOptions == null)
            {
                _basicConfigOptions = new List<CustomConfigOptionSet>();
                _basicConfigOptions.Add(_textLanguage);
                _basicConfigOptions.Add(_audioLanguage);
                _basicConfigOptions.Add(_autoPlay);
                _basicConfigOptions.Add(_textSpeed);
                _basicConfigOptions.Add(_handleVibration);
            }

            return _basicConfigOptions;
        }
    }

    public ConfigData()
    {
        _basicConfigOptions = null;

        _textLanguage = new CustomConfigOptionSet(new List<string>()
        { "SC", "TC", "JP", "EN" }, new List<string>()
        { "简体中文", "繁体中文", "日语", "英语" }, 0, "游戏语言");
        _audioLanguage = new CustomConfigOptionSet(new List<string>()
        { "EN", "JP" }, new List<string>()
        { "英文", "日语" }, 0, "语音语言");
        _autoPlay = new CustomConfigOptionSet(new List<string>()
        { "OFF", "ON" }, new List<string>()
        { "禁用", "启用" }, 0, "文本自动播放");
        _textSpeed = new CustomConfigOptionSet(new List<string>()
        { "SLOW", "NORMAL", "FAST" }, new List<string>()
        { "慢速", "普通", "快速" }, 1, "文本速度");
        _handleVibration = new CustomConfigOptionSet(new List<string>()
        { "OFF", "ON" }, new List<string>()
        { "禁用", "启用" }, 1, "手柄震动");
    }

    //获取总配置
    public Dictionary<string, string> GetResult()
    {
        Dictionary<string, string> result = new Dictionary<string, string>();
        foreach (CustomConfigOptionSet set in BasicConfigOptions)
        {
            result.Add(set.ConfigName,set.CurrentOptionKey);
        }

        return result;
    }
}

/*public enum TextLanguageType
{
    SC = 0,
    TC = 1,
    JP = 2,
    EN = 3
}

public enum AudioLanguageType
{
    EN = 0,
    JP = 1
}

public enum AutoPlay
{
    OFF = 0,
    ON = 1
}

public enum TextSpeed
{
    SLOW = 0,
    NORMAL = 1,
    FAST = 2
}

public enum HandleVibration
{
    ON = 0,
    OFF = 1
}*/

public class CustomConfigOptionSet
{
    private List<CustomConfigUnit> _units = new List<CustomConfigUnit>();
    public int CurrentIndex { get; private set; }
    public string ConfigName { get; private set; }

    public CustomConfigOptionSet(List<string> keys, List<string> chinese, int defaultIndex, string name)
    {
        for (int i = 0; i < keys.Count; i++)
            _units.Add(new CustomConfigUnit(keys[i], chinese[i]));

        CurrentIndex = defaultIndex;
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
    /// TODO 可以用静态常量，以统一字符串规格
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