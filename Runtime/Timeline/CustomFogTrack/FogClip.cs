using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class FogClip : PlayableAsset
{
    public Color fogColor = Color.gray;
    public float fogDensity = 0.01f;
    public FogMode fogMode = FogMode.Exponential;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<FogBehaviour>.Create(graph);

        FogBehaviour behaviour = playable.GetBehaviour();
        behaviour.fogColor = fogColor;
        behaviour.fogDensity = fogDensity;
        behaviour.fogMode = fogMode;

        return playable;
    }
}