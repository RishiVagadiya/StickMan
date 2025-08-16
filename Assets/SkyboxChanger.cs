using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxes; // 0 = Level1, 1 = Level2, ...

    public void SetSkybox(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < skyboxes.Length)
        {
            RenderSettings.skybox = skyboxes[levelIndex];
            DynamicGI.UpdateEnvironment(); // Optional: अगर lighting update करनी हो
        }
    }
}
