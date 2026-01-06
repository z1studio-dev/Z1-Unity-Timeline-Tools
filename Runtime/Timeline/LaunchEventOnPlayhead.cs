using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class LaunchEventOnPlayhead : MonoBehaviour, ITimeControl
{
    public UnityEvent onPlayheadReached;
    public UnityEvent onPlayheadLeft;

    public void OnControlTimeStart()
    {
        onPlayheadReached.Invoke();
    }

    public void OnControlTimeStop()
    {
        onPlayheadLeft.Invoke();
    }

    public void SetTime(double time)
    {
    }
    
}
