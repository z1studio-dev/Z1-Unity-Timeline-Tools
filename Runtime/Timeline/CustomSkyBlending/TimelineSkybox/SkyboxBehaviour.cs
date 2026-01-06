using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class SkyboxBehaviour : PlayableBehaviour
{
    [Tooltip("Any material that the Scene skybox shader understands " +
             "(e.g. Skybox/Cubemap, 6-Sided, HDRI, etc.).")]
    public Material skyboxMaterial;

    public bool updateEnvLight;
}