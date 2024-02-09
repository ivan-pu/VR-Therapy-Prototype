using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace SodaCraft
{
    public class SunFogRenderFeature : ScriptableRendererFeature
    {
        [System.Serializable]
        public class SunFogFeatureSettings
        {
            // we're free to put whatever we want here, public fields will be exposed in the inspector
            public bool IsEnabled = true;
            public RenderPassEvent WhenToInsert = RenderPassEvent.BeforeRenderingTransparents;
            public Texture ditherTexture;
        }

        // MUST be named "settings" (lowercase) to be shown in the Render Features inspector
        public SunFogFeatureSettings settings = new SunFogFeatureSettings();
        Material SunFogMaterial
        {
            get
            {
                if (sMat==null)
                {
                    Debug.Log("create sunFog material");
                    sMat=CoreUtils.CreateEngineMaterial("SodaCraft/SunFog");
                }
                return sMat;
            }
        }

        private Material sMat;
        RenderTargetHandle renderTextureHandle;
        SunFogRenderPass sunFogRenderPass;

        //RenderFeature的初始化方法，创建renderPass
        public override void Create()
        {
            sunFogRenderPass = new SunFogRenderPass(
                "SunFogPass",
                settings.WhenToInsert,
                SunFogMaterial,
                settings.ditherTexture
            );
        }

        // called every frame once per camera
        //这里的Render就是设置文件的那个renderer
        //renderingData是要渲染的各种数据
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (!settings.IsEnabled)
            {
                // we can do nothing this frame if we want
                return;
            }
            // Gather up and pass any extra information our pass will need.

            // In this case we're getting the camera's color buffer target
            //var cameraColorTargetHandle = renderer.cameraColorTargetHandle;
            sunFogRenderPass.Setup(renderer);

            // Ask the renderer to add our pass.
            // Could queue up multiple passes and/or pick passes to use
            renderer.EnqueuePass(sunFogRenderPass);
        }

        public void OnDestroy()
        {
            if (sMat)
            {
                Debug.Log("destroy sunFog material");
                Destroy(sMat);
            }
        }
    }
}