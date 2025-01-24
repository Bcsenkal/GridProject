
using UnityEngine;

namespace Managers
{
    public sealed class EventManager : Singleton<EventManager>
    {
        
#region Level Status

        public event System.Action<bool> ONLevelEnd;
        public event System.Action ONLevelStart;
        
        public void OnONLevelStart(){
            ONLevelStart?.Invoke();
        }
        public void OnONLevelEnd(bool isSuccess)
        {
            ONLevelEnd?.Invoke(isSuccess);
        }

#endregion

#region Input

        public event System.Action<Vector2> OnMouseDown;
        public event System.Action<Vector2> OnMouseUp;

        public void ONOnMouseDown(Vector2 position)
        {
            OnMouseDown?.Invoke(position);
        }

        public void ONOnMouseUp(Vector2 position)
        {
            OnMouseUp?.Invoke(position);
        }

#endregion


        //remove listeners from all of the events here
        public void NextLevelReset()
        {
            ONLevelStart= null;
            ONLevelEnd = null;
        }


        private void OnApplicationQuit() {
            NextLevelReset();
        }
    }
}
