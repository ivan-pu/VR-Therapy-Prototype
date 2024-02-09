using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace SodaCraft
{
    class SunFogRenderPass : ScriptableRenderPass
    {
        // used to label this pass in Unity's Frame Debug utility
        string profilerTag;
        Material SunFogMaterial;
        private Texture ditherNoiseTexture;
        ScriptableRenderer renderer;
        private RenderTargetIdentifier tempTexture;

        private int tempRtID;

        //构造函数，用于初始化，在RenderFeature的Create函数中进行调用
        public SunFogRenderPass(string profilerTag,
            RenderPassEvent renderPassEvent, Material sunFogMaterial, Texture ditherNoiseTexture)
        {
            this.profilerTag = profilerTag;
            this.renderPassEvent = renderPassEvent;
            this.SunFogMaterial = sunFogMaterial;
            this.ditherNoiseTexture = ditherNoiseTexture;
        }

        // This isn't part of the ScriptableRenderPass class and is our own addition.
        // For this custom pass we need the camera's color target, so that gets passed in.
        public void Setup(ScriptableRenderer renderer)
        {
            //this.cameraColorTargetHandle = cameraColorTargetHandle;
            this.renderer = renderer;
            tempRtID = Shader.PropertyToID("fullRT1");
        }

        // called each frame before Execute, use it to set up things the pass will need
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            // create a temporary render texture that matches the camera

            cmd.GetTemporaryRT(tempRtID, cameraTextureDescriptor, FilterMode.Bilinear);
            tempTexture = new RenderTargetIdentifier(tempRtID);
            ConfigureTarget(tempTexture);
        }

        // Execute is called for every eligible camera every frame. It's not called at the moment that
        // rendering is actually taking place, so don't directly execute rendering commands here.
        // Instead use the methods on ScriptableRenderContext to set up instructions.
        // RenderingData provides a bunch of (not very well documented) information about the scene
        // and what's being rendered.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var stack = VolumeManager.instance.stack;
            var shadowVolume = stack.GetComponent<SunFog>();
            if (!shadowVolume.IsActive())
            {
                return;
            }

            // fetch a command buffer to use
            CommandBuffer cmd = CommandBufferPool.Get(profilerTag);
            cmd.Clear();
            // the actual content of our custom render pass!
            // we apply our material while blitting to a temporary texture
            //Debug.Log("Blit"+materialToBlit.name);
            cmd.SetGlobalFloat("_FogStartDistance",shadowVolume.fogStartDistance.value);
            cmd.SetGlobalFloat("_Density", shadowVolume.fogDensity.value);
            cmd.SetGlobalColor("_SunFogColor", shadowVolume.sunFogColor.value);
            cmd.SetGlobalFloat("_SunPower", shadowVolume.sunPower.value);
            cmd.SetGlobalColor("_SkyFogColor", shadowVolume.skyFogColor.value);
            cmd.SetGlobalColor("_EquatorFogColor", shadowVolume.equatorFogColor.value);
            cmd.SetGlobalFloat("_EquatorPower", shadowVolume.equatorSharpPower.value);
            cmd.SetGlobalFloat("_Power", shadowVolume.fogStrength.value);
            float minFogHeight = Mathf.Min(shadowVolume.endFogHeight.value, shadowVolume.startFogHeight.value);
            float maxFogHeight = Mathf.Max(shadowVolume.endFogHeight.value, shadowVolume.startFogHeight.value);
            cmd.SetGlobalFloat("_MaxFogHeight", maxFogHeight);
            cmd.SetGlobalFloat("_MinFogHeight", minFogHeight);
            cmd.SetGlobalFloat("_HeightDensityPower", shadowVolume.heightDensityPower.value);
            cmd.SetGlobalFloat("_Thickness", shadowVolume.thickness.value);
           
            switch (shadowVolume.sunColorMode.value)
            {
                case SunColorMode.Normal:
                    cmd.EnableShaderKeyword("SUNFOG_BLEND_NORMAL");
                    cmd.DisableShaderKeyword("SUNFOG_BLEND_ADDITIVE");
                    break;
                case SunColorMode.Additive:
                    cmd.DisableShaderKeyword("SUNFOG_BLEND_NORMAL");
                    cmd.EnableShaderKeyword("SUNFOG_BLEND_ADDITIVE");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (ditherNoiseTexture)
            {
                cmd.SetGlobalTexture("_DitherNoise", ditherNoiseTexture);
            }
    
            RenderTargetIdentifier cameraRT = renderer.cameraColorTarget;
            //RTHandle cameraRT = renderer.cameraColorTargetHandle;
            cmd.Blit(cameraRT, tempTexture, SunFogMaterial, 0);
            cmd.Blit(tempTexture, cameraRT);
            // don't forget to tell ScriptableRenderContext to actually execute the commands
            context.ExecuteCommandBuffer(cmd);

            // tidy up after ourselves
            cmd.Clear();
            CommandBufferPool.Release(cmd);
        }

        // called after Execute, use it to clean up anything allocated in Configure
        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(tempRtID);
        }
    }
}