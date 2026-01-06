using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackColor(0.2f, 0.4f, 0.8f)]
[TrackClipType(typeof(SkyboxClip))]
public class SkyboxTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph,
                                              GameObject go,
                                              int inputCount)
    {
        return ScriptPlayable<SkyboxMixerBehaviour>.Create(graph, inputCount);
    }
}
