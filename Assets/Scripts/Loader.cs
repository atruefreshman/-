
using UnityEngine.SceneManagement;

public static class Loader 
{
    public enum Scene 
    {
        MainMenuScene,
        LoadingScene,
        GameScene,
    }

    public static Scene targetScene;

    public static void Load(Scene targetScene) 
    {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void LoaderCallBack()    //进入加载场景一帧才会加载新场景
    {
        SceneManager.LoadScene(targetScene.ToString());
    }
}
