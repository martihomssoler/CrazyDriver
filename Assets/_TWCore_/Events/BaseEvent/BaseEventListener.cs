using UnityEngine;
using UnityEngine.Events;

namespace TWCore.Events
{
    public abstract class BaseEventListener<T> : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public BaseEvent<T> Event;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent<T> Response;

        private void OnEnable()
        {
            Event.RegisterListener(this);
        }

        private void OnDisable()
        {
            Event.UnregisterListener(this);
        }

        public void OnEventRaised(T value)
        {
            Response.Invoke(value);
        }
    }
}