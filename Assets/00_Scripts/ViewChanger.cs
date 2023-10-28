using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.Input;

public class ViewChanger : MonoBehaviour
{
    [SerializeField] private bool inside = false;
    [SerializeField] GameObject regular, bigscale;

    [SerializeField] private Hand leftHand, rightHand;
    [SerializeField] private bool isPinchingBothHands = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leftHand.GetIndexFingerIsPinching() && rightHand.GetIndexFingerIsPinching() && inside)
        {
            OnViewChange();
        }
    }

    public void OnViewChange()
    {
        if (inside)
        {
            // Switch back
            regular.SetActive(true);
            bigscale.SetActive(false);
            inside = false;
        } else
        {
            regular.SetActive(false);
            bigscale.SetActive(true);
            inside = true;
        }
    }
}
