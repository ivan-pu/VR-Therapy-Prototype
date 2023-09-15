using com.zibra.liquid.Manipulators;
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
    // Start is called before the first frame update
    void Start()
    {
        generalEmitter.VolumePerSimTime = geStartValue;
        dedicatedEmitter.VolumePerSimTime = deStartValue;
        waterMaterial = waterMesh.GetComponent<MeshRenderer>().material;
        waterMaterial.color = colorStartValue;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
