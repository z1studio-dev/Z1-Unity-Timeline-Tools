using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class LaunchTimelineOnPlayhead : MonoBehaviour, ITimeControl
{
    [SerializeField] private PlayableDirector _playableDirector;
    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }
    public void OnControlTimeStart()
    {
        _playableDirector.Play();
    }

    public void OnControlTimeStop()
    {
        //Debug.Log("STOP TIME");
    }

    public void SetTime(double time)
    {
    }
    
}
