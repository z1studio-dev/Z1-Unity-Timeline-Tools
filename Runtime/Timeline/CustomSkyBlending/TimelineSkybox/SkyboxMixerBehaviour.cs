using UnityEngine;
using UnityEngine.Playables;
[ExecuteAlways]
public class SkyboxMixerBehaviour : PlayableBehaviour
{
    Material _blendMat; // The INSTANCED material that uses the “Skybox/Blended Cubemap” shader

    static readonly int ID_CubeA = Shader.PropertyToID("_CubeA");
    static readonly int ID_CubeB = Shader.PropertyToID("_CubeB");
    static readonly int ID_Blend = Shader.PropertyToID("_Blend");
    static readonly int ID_Tint = Shader.PropertyToID("_Tint");

    static readonly int ID_Exposure = Shader.PropertyToID("_Exposure");

    static readonly int ID_RotA = Shader.PropertyToID("_RotationA");
    static readonly int ID_RotB = Shader.PropertyToID("_RotationB");
    
    static readonly string kShaderName = "Skybox/Blended Cubemap";


    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (_blendMat == null)
        {
            Material current = RenderSettings.skybox;

            // If the current skybox is already our blended shader, reuse it.
            if (current != null && current.shader != null &&
                current.shader.name == kShaderName)
            {
                _blendMat = current;
            }
            else if (current != null)
            {
                _blendMat = Object.Instantiate(current);
                _blendMat.name = "BlendedSkybox";
                _blendMat.hideFlags = HideFlags.HideAndDontSave; // keep it unsaved & invisible
                RenderSettings.skybox = _blendMat;
            }
        }

        if (_blendMat == null) return;

        Cubemap cubeA = null, cubeB = null;
        float rotA = 0f, rotB = 0f;
        Color tint = Color.black;
        float exposure = 0f;
        float blendWeightB = 0f;
        bool updateEnvLight = true;
        
        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; ++i)
        {
            float w = playable.GetInputWeight(i);
            if (w <= 0f) continue;

            var behaviour = ((ScriptPlayable<SkyboxBehaviour>)playable.GetInput(i)).GetBehaviour();
            var mat = behaviour.skyboxMaterial;
            updateEnvLight = behaviour.updateEnvLight;
            if (mat == null) continue;

            var cubemap = mat.GetTexture("_Tex") as Cubemap;
            float rot = mat.GetFloat("_Rotation");

            if (cubeA == null)
            {
                cubeA = cubemap;
                rotA = rot;
            }
            else
            {
                cubeB = cubemap;
                rotB = rot;
                blendWeightB = w; // weight of “B” sample
            }

            tint += mat.GetColor("_Tint") * w;
            exposure += mat.GetFloat("_Exposure") * w;
        }

        // Fallbacks so A/B always both valid -----------------------------------
        if (cubeA == null && cubeB != null)
        {
            cubeA = cubeB;
            rotA = rotB;
        }

        if (cubeB == null)
        {
            cubeB = cubeA;
            rotB = rotA;
        }

        // Push to shader -------------------------------------------------------
        _blendMat.SetTexture(ID_CubeA, cubeA);
        _blendMat.SetTexture(ID_CubeB, cubeB);
        _blendMat.SetFloat(ID_Blend, blendWeightB);
        _blendMat.SetColor(ID_Tint, tint);
        _blendMat.SetFloat(ID_Exposure, exposure);
        _blendMat.SetFloat(ID_RotA, rotA);
        _blendMat.SetFloat(ID_RotB, rotB);

        if(updateEnvLight)
            UpdateEnvironmentIfNeeded(blendWeightB);
    }
    
    const int FramesPerUpdate = 12;      // tweak per target HW
    float lastBlend = -1f;              // mixer-scope field

    void UpdateEnvironmentIfNeeded(float blendWeight)
    {
        // significant change or time slice reached?
        bool changed = Mathf.Abs(blendWeight - lastBlend) > 0.01f;
        bool timeSlice = (Time.renderedFrameCount % FramesPerUpdate) == 0;
        if (changed && timeSlice)
        {
            DynamicGI.UpdateEnvironment();
            lastBlend = blendWeight;
           // Debug.Log($"Skybox blend updated: {blendWeight}, lastBlend: {lastBlend}");
        }
    }
}