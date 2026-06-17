using UnityEngine;

public class ForceLandscape : MonoBehaviour
{
    public enum SceneOrientation
    {
        Portrait,
        Landscape
    }

    [Header("Orientation to apply for this scene")]
    public SceneOrientation targetOrientation = SceneOrientation.Portrait;

    void Start()
    {
        SetOrientation(targetOrientation);
    }

    void OnDestroy()
    {
        // Always reset to Portrait when leaving the scene
        SetOrientation(SceneOrientation.Portrait);
    }

    private void SetOrientation(SceneOrientation orientation)
    {
        switch (orientation)
        {
            case SceneOrientation.Portrait:
                Screen.orientation = ScreenOrientation.Portrait;
                break;
            case SceneOrientation.Landscape:
                Screen.orientation = ScreenOrientation.LandscapeLeft;
                break;
        }
    }
}
