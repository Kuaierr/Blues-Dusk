public static class DialogStateUtility
{
    // 对话第一次启动
    public const string FIRST_TALKING = "FIRST_TALKING";
    // Dialog Animating State的下一个状态
    public const string CACHED_AFTER_ANIMATING_STATE = "CACHED_AFTER_ANIMATING_STATE";
    // Dialog Animating State使用的Animator
    public const string CACHED_ANIMATOR = "CACHED_ANIMATOR";
    // Dialog Animating State Animator 触发的Trigger名
    public const string CACHED_ANIMATOR_TRIGGER_NAME = "CACHED_ANIMATOR_TRIGGER_NAME";
    // 由 Dice Dialog Reseting 和 Dialog Talking 向 Dialog Choosing 传入
    public const string IS_DICE_CHOOSING = "IS_DICE_CHOOSING";
}