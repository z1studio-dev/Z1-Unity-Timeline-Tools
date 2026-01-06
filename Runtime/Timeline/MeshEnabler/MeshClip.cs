using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class MeshClip : PlayableAsset
{
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<MeshBehavior>.Create(graph);
        MeshBehavior mb = playable.GetBehaviour();
        
        mb.renderer = meshRenderer;
        mb.skinnedMeshRenderer = skinnedMeshRenderer;
        
        return playable;
    }
}
