namespace UniGame.MultiScene.Runtime
{
    using System.Collections.Generic;
    using UniGame.MultiScene.Runtime;

    public interface IMultiSceneSettings
    {
        IReadOnlyList<MultiSceneAsset> Scenes { get; }

        MultiSceneAsset Find(string sceneName);
    }
}