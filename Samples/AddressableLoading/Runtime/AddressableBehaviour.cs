using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;
using UnityEngine.ResourceManagement.AsyncOperations;

[System.Serializable]
public class AddressableBehaviour : PlayableBehaviour
{
    public AssetReferenceGameObject prefabReference;
    public Vector3 spawnPoint;
    public Vector3 rotationPoint;
    public double clipStart;
    public double clipEnd;
    public string clipName;
    public string trackName;
    public int clipKey;
    public bool saveTransform = true;
    public AddressableClip clipAsset;
    public GameObject parentTransform;
    public AddressableRegistery registery;

    private AsyncOperationHandle<GameObject> loadHandle;
    private bool shouldBeActive;

    const double TIME_EPSILON = 0.001;

    private AddressableRegistery GetRegistry()
    {
        if (registery != null) return registery;
        if (parentTransform == null) return null;

        registery = parentTransform.GetComponent<AddressableRegistery>();
        if (registery == null) registery = parentTransform.AddComponent<AddressableRegistery>();

        return registery;
    }

    public AddressableRegistery.Status GetStatus()
    {
        var reg = GetRegistry();
        if (reg == null) return null;
        return reg.Get(clipKey);
    }

    public override void OnBehaviourPlay(Playable playable, FrameData frameData)
    {
        shouldBeActive = true;

        var status = GetStatus();
        if (status == null || status.instance != null || status.isLoading) return;

        status.isLoading = true;
        LoadPrefab();
    }

    public override void OnBehaviourPause(Playable playable, FrameData frameData)
    {
        shouldBeActive = false;

        var status = GetStatus();
        if (status == null || status.instance == null)
            return;

        double t = currentTime(playable);
        bool inside =
            t > clipStart + TIME_EPSILON &&
            t < clipEnd - TIME_EPSILON;

        if (!inside)
        {
            ReleasePrefab();
        }
    }

    private double currentTime(Playable playable)
    {
        return playable.GetGraph().GetRootPlayable(0).GetTime();
    }

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {

    }


    public void LoadPrefab()
    {
        loadHandle = prefabReference.InstantiateAsync(
            spawnPoint,
            Quaternion.Euler(rotationPoint),
            parentTransform.transform
        );

        loadHandle.Completed += OnPrefabLoaded;
    }


    private void OnPrefabLoaded(AsyncOperationHandle<GameObject> handle)
    {
        var status = GetStatus();
        if (status != null) status.isLoading = false;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            if (!shouldBeActive)
            {
                prefabReference.ReleaseInstance(handle.Result);
                if (status != null) status.instance = null;
                return;
            }

            if (status != null) status.instance = handle.Result;
        }
        else
        {
            if (status != null) status.instance = null;
            Debug.LogError("Failed to load prefab.");
        }
    }


    public void ReleasePrefab()
    {
        var status = GetStatus();
        if (status!= null && status.instance != null)
        {
            if (saveTransform)
            {
                clipAsset.spawnPoint = status.instance.transform.position;
                clipAsset.rotationPoint = status.instance.transform.rotation.eulerAngles;
                spawnPoint = clipAsset.spawnPoint;
                rotationPoint = clipAsset.rotationPoint;
            }

            prefabReference.ReleaseInstance(status.instance);
            status.instance = null;

            GetRegistry().Clear(clipKey);
        }
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        ReleasePrefab();
    }
}


