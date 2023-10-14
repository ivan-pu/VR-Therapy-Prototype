using com.zibra.liquid.Manipulators;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [SerializeField] ZibraLiquidEmitter generalEmitter;
    [SerializeField] float geStartValue, geEndValue;
    [SerializeField] ZibraLiquidEmitter dedicatedEmitter;
    [SerializeField] float deStartValue, deEndValue;
    [SerializeField] GameObject waterMesh;
    [SerializeField] Material waterMaterial;
    [SerializeField] Color colorStartValue, colorEndValue;
    [SerializeField] ParticleSystem rainParticles;
    [SerializeField] float RPStartEmission, RPEndEmission;
    private float emissionCount;
    // Start is called before the first frame update
    void Start()
    {
        generalEmitter.VolumePerSimTime = geStartValue;
        dedicatedEmitter.VolumePerSimTime = deStartValue;
        waterMaterial = waterMesh.GetComponent<MeshRenderer>().material;
        waterMaterial.color = colorStartValue;
        DOTween.To(() => generalEmitter.VolumePerSimTime, x => generalEmitter.VolumePerSimTime = x, geEndValue, 60f);
        DOTween.To(() => dedicatedEmitter.VolumePerSimTime, x => dedicatedEmitter.VolumePerSimTime = x, deEndValue, 60f);
        waterMaterial.DOColor(colorEndValue, 45f);
        var emission = rainParticles.emission;
        emission.rateOverTime = RPStartEmission;
        emissionCount = RPStartEmission;
        DOTween.To(() => emissionCount, x => emissionCount = x, RPEndEmission, 60f);
    }

    // Update is called once per frame
    void Update()
    {
        var emission = rainParticles.emission;
        emission.rateOverTime = emissionCount;
    }
}
