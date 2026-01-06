using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[TrackColor(0.2f, 0.7f, 0.1f)]
[TrackClipType(typeof(MeshClip))]
public class MeshTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<MeshMixerBehavior>.Create(graph, inputCount);
    }
}
