using com.zibra.liquid.Manipulators;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [SerializeField] float transitionTime;
    [SerializeField] PinchChangeController pinchChangeController;
    [Header("Water Emitter")]
    [SerializeField] ZibraLiquidEmitter generalEmitter;
    [SerializeField] float geStartValue, geEndValue;
    // [SerializeField] ZibraLiquidEmitter dedicatedEmitter;
    // [SerializeField] float deStartValue, deEndValue;
    [Header("Water Color")]
    [SerializeField] GameObject waterMesh;
    [SerializeField] Material waterMaterial;
    [SerializeField] Color colorStartValue, colorEndValue;
    [Header("BG Rain Particle Count")]
    [SerializeField] ParticleSystem rainParticles;
    [SerializeField] float RPStartEmission, RPEndEmission;
    private float emissionCount;
    // Start is called before the first frame update
    void Start()
    {
        generalEmitter.VolumePerSimTime = geStartValue;
        DOTween.To(() => generalEmitter.VolumePerSimTime, x => generalEmitter.VolumePerSimTime = x, geEndValue, transitionTime);
        // dedicatedEmitter.VolumePerSimTime = deStartValue;
        // DOTween.To(() => dedicatedEmitter.VolumePerSimTime, x => dedicatedEmitter.VolumePerSimTime = x, deEndValue, transitionTime);

        waterMaterial = waterMesh.GetComponent<MeshRenderer>().material;
        waterMaterial.color = colorStartValue;
        waterMaterial.DOColor(colorEndValue, transitionTime);

        var emission = rainParticles.emission;
        emission.rateOverTime = RPStartEmission;
        emissionCount = RPStartEmission;
        DOTween.To(() => emissionCount, x => emissionCount = x, RPEndEmission, transitionTime);

        StartCoroutine(SetCanPinchTrue());
    }

    // Update is called once per frame
    void Update()
    {
        if (pinchChangeController.canPinch == false)
        {
            var emission = rainParticles.emission;
            emission.rateOverTime = emissionCount;
        }
    }

    IEnumerator SetCanPinchTrue()
    {
        yield return new WaitForSeconds(transitionTime);
        pinchChangeController.canPinch = true;
    }
}
