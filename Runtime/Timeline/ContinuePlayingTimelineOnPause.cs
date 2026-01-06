using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class ContinuePlayingTimelineOnPause : MonoBehaviour, ITimeControl
{
    [SerializeField] private PlayableDirector _playableDirector;
    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }
    public void OnControlTimeStart()
    {
    }

    public void OnControlTimeStop()
    {
        _playableDirector.Resume();
        //Debug.Log("STOP TIME");
    }

    public void SetTime(double time)
    {
    }
}
