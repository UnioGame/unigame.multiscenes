namespace UniGame.MultiScene.Runtime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UniGame.MultiScene.Runtime;
    using UnityEngine;
    
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
#if UNITY_EDITOR
    using UniModules.Editor;
#endif
    

    [CreateAssetMenu(menuName = "MultiScene/Multi Scenes Collection", fileName = "Multi Scenes Collection")]
    public class MultiScenesAsset : ScriptableObject, IMultiSceneSettings
    {
        #region inspector

#if ODIN_INSPECTOR
        [ListDrawerSettings(ListElementLabelName = "@name")]
        [InlineEditor]
#endif

        public List<MultiSceneAsset> scenes = new();

        #endregion

        [NonSerialized]
        private List<MultiSceneAsset> _scenes;
        [NonSerialized]
        private MultiSceneAsset _emptyScene;
        
        public IReadOnlyList<MultiSceneAsset> Scenes => _scenes ?? CreateScenes();
        
        public MultiSceneAsset Find(string sceneName)
        {
            foreach (var sceneAsset in Scenes)
            {
                if (!sceneAsset.id.Equals(sceneName,StringComparison.OrdinalIgnoreCase))
                    continue;
                return sceneAsset;
            }

            _emptyScene ??= CreateInstance<MultiSceneAsset>();
            return _emptyScene;
        }

        private List<MultiSceneAsset> CreateScenes()
        {
            if(_scenes != null) return _scenes;
            _scenes = scenes.Select(Instantiate).ToList();
            return _scenes;
        }

#if UNITY_EDITOR

#if ODIN_INSPECTOR
        [Button]
#endif
        public void CollectScenes()
        {
            _scenes = null;
            var sceneAssets = AssetEditorTools.GetAssets<MultiSceneAsset>();
            scenes.Clear();
            scenes.AddRange(sceneAssets);
            this.SaveAsset();
        }
        
#endif
    }
}