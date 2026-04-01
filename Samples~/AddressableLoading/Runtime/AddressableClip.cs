using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;

public class AddressableClip : PlayableAsset
{
    public AssetReferenceGameObject prefabReference;
    public bool autoSaveTransform = true;
    public ExposedReference<GameObject> parentTransform;
    [HideInInspector] public Vector3 spawnPoint;
    [HideInInspector] public Vector3 rotationPoint;
    [HideInInspector] public double clipStart;
    [HideInInspector] public double clipEnd;
    [HideInInspector] public string clipName;
    [HideInInspector] public string trackName;
    [HideInInspector] public int clipKey;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<AddressableBehaviour>.Create(graph);
        var behaviour = playable.GetBehaviour();
        behaviour.prefabReference = prefabReference;
        behaviour.spawnPoint = spawnPoint;
        behaviour.rotationPoint = rotationPoint;
        behaviour.saveTransform = autoSaveTransform;
        behaviour.clipStart = clipStart;
        behaviour.clipEnd = clipEnd;
        behaviour.clipName = clipName;
        behaviour.trackName = trackName;
        behaviour.parentTransform = parentTransform.Resolve(graph.GetResolver());
        behaviour.clipAsset = this;
        behaviour.clipKey = clipKey;

        return playable;
    }
}
