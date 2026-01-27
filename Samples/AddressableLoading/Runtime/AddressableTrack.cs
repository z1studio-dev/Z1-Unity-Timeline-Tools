using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


[TrackColor(0.855f, 0.8623f, 0.87f)]
[TrackClipType(typeof(AddressableClip))]
public class AddressableTrack : TrackAsset
{

    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {


        foreach (var clip in GetClips())
        {
            AddressableClip addressableClip = clip.asset as AddressableClip;
            if (addressableClip != null)
            {
                addressableClip.clipStart = clip.start;
                addressableClip.clipEnd = clip.end;
                addressableClip.clipName = clip.displayName;
                addressableClip.trackName = this.name;
                addressableClip.clipKey = clip.GetHashCode();
            }
        }

        return ScriptPlayable<AddressableBehaviour>.Create(graph, inputCount);
    }
}
