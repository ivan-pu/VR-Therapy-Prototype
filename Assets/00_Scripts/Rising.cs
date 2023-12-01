using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rising : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(16f, 45f);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
