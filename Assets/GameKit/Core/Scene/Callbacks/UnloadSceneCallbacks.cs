namespace GameKit.Scene
{
    public sealed class UnloadSceneCallbacks
    {
        private readonly UnloadSceneSuccessCallback m_UnloadSceneSuccessCallback;
        private readonly UnloadSceneFailureCallback m_UnloadSceneFailureCallback;

        public UnloadSceneCallbacks(UnloadSceneSuccessCallback unloadSceneSuccessCallback) : this(unloadSceneSuccessCallback, null)
        {
        }

        public UnloadSceneCallbacks(UnloadSceneSuccessCallback unloadSceneSuccessCallback, UnloadSceneFailureCallback unloadSceneFailureCallback)
        {
            if (unloadSceneSuccessCallback == null)
            {
                throw new GameKitException("Unload scene success callback is invalid.");
            }

            m_UnloadSceneSuccessCallback = unloadSceneSuccessCallback;
            m_UnloadSceneFailureCallback = unloadSceneFailureCallback;
        }

        public UnloadSceneSuccessCallback UnloadSceneSuccessCallback
        {
            get
            {
                return m_UnloadSceneSuccessCallback;
            }
        }

        public UnloadSceneFailureCallback UnloadSceneFailureCallback
        {
            get
            {
                return m_UnloadSceneFailureCallback;
            }
        }
    }
}
