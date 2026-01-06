using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class FogBehaviour : PlayableBehaviour
{
    public Color fogColor = Color.gray;
    public float fogDensity = 0.01f;
    public FogMode fogMode = FogMode.Exponential;
}