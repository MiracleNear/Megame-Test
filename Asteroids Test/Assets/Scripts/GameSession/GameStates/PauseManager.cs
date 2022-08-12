using System;
using System.Collections.Generic;

namespace GameSession
{
    public class PauseManager : IPauseHandler, IDisposable
    {
        public bool IsPaused { get; private set; }
        
        private List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();

        private static PauseManager _instance;
        
        private PauseManager()
        {
            
        }

        public static PauseManager GetInstance()
        {
            if (_instance == null)
            {
                _instance = new PauseManager();
            }

            return _instance;
        }

        public void Register(IPauseHandler pauseHandler)
        {
            _pauseHandlers.Add(pauseHandler);
        }

        public void UnRegister(IPauseHandler pauseHandler)
        {
            _pauseHandlers.Remove(pauseHandler);
        }

        public void SetPause(bool isPause)
        {
            IsPaused = isPause;
            
            foreach (IPauseHandler pauseHandler in _pauseHandlers)
            {
                pauseHandler.SetPause(IsPaused);
            }
        }

        public void Dispose()
        {
            _pauseHandlers.Clear();
            _instance = null;
        }
    }
}