namespace GameKit.Scene
{
    public sealed class LoadSceneCallbacks
    {
        private readonly LoadSceneSuccessCallback m_LoadSceneSuccessCallback;
        private readonly LoadSceneFailureCallback m_LoadSceneFailureCallback;

        public LoadSceneCallbacks(LoadSceneSuccessCallback loadSceneSuccessCallback) : this(loadSceneSuccessCallback, null)
        {

        }


        public LoadSceneCallbacks(LoadSceneSuccessCallback loadSceneSuccessCallback, LoadSceneFailureCallback loadSceneFailureCallback)
        {
            if (loadSceneSuccessCallback == null)
            {
                throw new GameKitException("Load scene success callback is invalid.");
            }

            m_LoadSceneSuccessCallback = loadSceneSuccessCallback;
            m_LoadSceneFailureCallback = loadSceneFailureCallback;
        }

        public LoadSceneSuccessCallback LoadSceneSuccessCallback
        {
            get
            {
                return m_LoadSceneSuccessCallback;
            }
        }

        public LoadSceneFailureCallback LoadSceneFailureCallback
        {
            get
            {
                return m_LoadSceneFailureCallback;
            }
        }
    }
}
