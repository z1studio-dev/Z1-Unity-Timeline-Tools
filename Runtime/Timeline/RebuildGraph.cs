using UnityEngine;
using UnityEngine.Playables;
using System.Collections.Generic;

public class RebuildGraph : MonoBehaviour
{
    public PlayableDirector playableDirectorsToRebuild;

    public PlayableDirector masterTimeline;
    public void Rebuild()
    {

        masterTimeline.Stop();
        playableDirectorsToRebuild.Stop();
        playableDirectorsToRebuild.RebuildGraph();
        playableDirectorsToRebuild.Resume();
        masterTimeline.Resume();
        
    }
    
}
