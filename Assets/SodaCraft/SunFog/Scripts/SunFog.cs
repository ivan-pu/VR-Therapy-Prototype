using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using BoolParameter = UnityEngine.Rendering.BoolParameter;
using ColorParameter = UnityEngine.Rendering.ColorParameter;
using FloatParameter = UnityEngine.Rendering.FloatParameter;

namespace SodaCraft
{
    public enum SunColorMode
    {
        Normal,
        Additive
    }
    [Serializable]
    public sealed class SunColorModeParameter : VolumeParameter<SunColorMode> { public SunColorModeParameter(SunColorMode value, bool overrideState = false) : base(value, overrideState) { } }
    // volume路径
    [Serializable, VolumeComponentMenu("SodaCraft/SunFog")]
    public class SunFog : VolumeComponent, IPostProcessComponent
    {
        // 暴露出来的参数
        public BoolParameter enable = new BoolParameter(false);
        public MinFloatParameter fogStartDistance = new MinFloatParameter(0f,0f);
        public FloatParameter fogDensity=new FloatParameter(0.002f) ;
        public MinFloatParameter fogStrength = new MinFloatParameter(2f,0f);
        public SunColorModeParameter sunColorMode = new SunColorModeParameter(SunColorMode.Additive);
        public ColorParameter sunFogColor = new ColorParameter(Color.yellow, true, false, false);
        public MinFloatParameter sunPower = new MinFloatParameter(16f, 0.1f);
        public ColorParameter skyFogColor = new ColorParameter(Color.blue, true, false, false);
        public ColorParameter equatorFogColor = new ColorParameter(Color.cyan, true, false, false);
        public MinFloatParameter equatorSharpPower = new MinFloatParameter(3f,0.1f);
        public FloatParameter startFogHeight = new FloatParameter(0f);
        public FloatParameter endFogHeight = new FloatParameter(1200f);
        public MinFloatParameter heightDensityPower = new MinFloatParameter(3f,0.1f);
        public FloatParameter thickness=new FloatParameter(1200f);
       
        //public FloatParameter darkerFar = new FloatParameter(100f);
        public bool IsActive()
        {
            return enable.value;
        }
        public bool IsTileCompatible()
        {
            return false;
        }
        
    }
}