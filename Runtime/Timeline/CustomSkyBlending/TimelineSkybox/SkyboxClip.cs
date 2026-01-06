using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class SkyboxClip : PlayableAsset, ITimelineClipAsset
{
    public SkyboxBehaviour template = new SkyboxBehaviour();

    public ClipCaps clipCaps => ClipCaps.Blending;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<SkyboxBehaviour>.Create(graph, template);
    }
}