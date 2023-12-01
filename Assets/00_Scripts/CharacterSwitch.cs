using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [SerializeField] GameObject[] avatars;
    private int n = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < avatars.Length; i++)
        {
            avatars[i].SetActive(false);
        }
    }

    public void NextAvatar()
    {
        avatars[n].SetActive(false);
        if (n == avatars.Length - 1)
        {
            n = 0;
        }
        else n++;
        avatars[n].SetActive(true);
    }
}
