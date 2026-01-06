using UnityEngine;
using UnityEngine.Playables;

public class FogMixerBehaviour : PlayableBehaviour
{
    private Color defaultColor;
    private float defaultDensity;
    private FogMode defaultMode;
    private bool defaultCaptured = false;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (!defaultCaptured)
        {
            defaultColor = RenderSettings.fogColor;
            defaultDensity = RenderSettings.fogDensity;
            defaultMode = RenderSettings.fogMode;
            defaultCaptured = true;
        }

        int inputCount = playable.GetInputCount();

        Color blendedColor = Color.black;
        float blendedDensity = 0f;
        float totalWeight = 0f;
        FogMode lastMode = FogMode.Exponential;

        for (int i = 0; i < inputCount; i++)
        {
            float weight = playable.GetInputWeight(i);
            ScriptPlayable<FogBehaviour> inputPlayable = (ScriptPlayable<FogBehaviour>)playable.GetInput(i);
            FogBehaviour input = inputPlayable.GetBehaviour();

            blendedColor += input.fogColor * weight;
            blendedDensity += input.fogDensity * weight;
            totalWeight += weight;

            if (weight > 0f)
                lastMode = input.fogMode; // use mode of the most dominant clip
        }

        // If there's weight, blend. Otherwise, restore original
        if (totalWeight > 0f)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = blendedColor;
            RenderSettings.fogDensity = blendedDensity;
            RenderSettings.fogMode = lastMode;
        }
        else
        {
            RenderSettings.fogColor = defaultColor;
            RenderSettings.fogDensity = defaultDensity;
            RenderSettings.fogMode = defaultMode;
        }
    }
/*
    public override void OnGraphStop(Playable playable)
    {
        // Restore defaults when timeline stops
        RenderSettings.fogColor = defaultColor;
        RenderSettings.fogDensity = defaultDensity;
        RenderSettings.fogMode = defaultMode;
        defaultCaptured = false;
    }
    */
}
