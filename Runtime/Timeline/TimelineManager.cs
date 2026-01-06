using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Assertions;

public class TimelineManager : MonoBehaviour
{
    [SerializeField] PlayableDirector _timeline;
    
    private static TimelineManager instance;
    public static TimelineManager Instance => instance;
    public bool autoStart = true;
    private void Awake()
    {
        Assert.IsNotNull(_timeline);
        instance = this;
    }

    private void Start()
    {
        if(autoStart)
        _timeline.Play();
    }
    
    
    public void PlayTimeline()
    {
        _timeline.Resume();
    }

    public void StopTimeline()
    {
        _timeline.Pause();
    }

    public void GoToFrame(int frame)        
    {
        _timeline.time = ConvertFrameToTime(frame);
        _timeline.Evaluate();
        _timeline.Resume();
    }

    public void TimelineEvaluate()
    {
       // _timeline.RebuildGraph();
        _timeline.Evaluate();   
    }

    public void GoToMarker(string markerTitle)
    {
        TimelineAsset timelineAsset = _timeline.playableAsset as TimelineAsset;
        foreach (var marker in timelineAsset.markerTrack.GetMarkers())
        {
            if (marker is NotesMarker notesMarker && notesMarker.title == markerTitle)
            {
                _timeline.time = notesMarker.time;
                _timeline.Evaluate();
                _timeline.Resume();
                return;
            }
        }
    }
    public double ConvertFrameToTime(int frame)
    {
        TimelineAsset timelineAsset = _timeline.playableAsset as TimelineAsset;
        return frame / timelineAsset.editorSettings.frameRate;
    }
}
