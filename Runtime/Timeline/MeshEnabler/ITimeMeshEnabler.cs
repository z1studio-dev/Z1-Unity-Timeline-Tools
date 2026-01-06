using System;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[ExecuteInEditMode]
public class ITimeMeshEnabler : MonoBehaviour, ITimeControl
{
    private MeshRenderer[] _renderers;
    private SkinnedMeshRenderer[] _skinnedMeshRenderers;
    private void Awake()
    {
        if (GetComponentInChildren<MeshRenderer>())
        {
            _renderers = GetComponentsInChildren<MeshRenderer>();
        }

        if (GetComponentInChildren<SkinnedMeshRenderer>())
        {
            _skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        }
    }

    public void OnControlTimeStart()
    {
        if(_renderers != null)
        foreach (MeshRenderer renderer in _renderers)
        {
            renderer.enabled = true;
        }

        if(_skinnedMeshRenderers != null)
        foreach (SkinnedMeshRenderer renderer in _skinnedMeshRenderers)
        {
            renderer.enabled = true;
        }
    }

    public void OnControlTimeStop()
    {
        if(_renderers != null)
        foreach (MeshRenderer renderer in _renderers)
        {
            renderer.enabled = false;
            
        }
        
        if(_skinnedMeshRenderers != null)
        foreach (SkinnedMeshRenderer meshRenderer in _skinnedMeshRenderers)
        {
            meshRenderer.enabled = false;
        }
    }

    public void SetTime(double time)
    {
        // Debug.Log("CURRENT TIMELINE TIME: " + time);
    }
}