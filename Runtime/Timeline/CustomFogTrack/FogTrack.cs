using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[TrackColor(0.8f, 0.1f, 0.4f)]
[TrackClipType(typeof(FogClip))]
public class FogTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<FogMixerBehaviour>.Create(graph, inputCount);
    }
}