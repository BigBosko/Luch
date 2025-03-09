using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingPlayer : UsableItem
{
    [Header("References")]
    [SerializeField] private AudioSource audioSource;
    
    private bool isOn = false;

    protected override void Start()
    {
        base.Start();
        audioSource.playOnAwake = false;
    }


    public override void Use()
    {
        TogglePlayer();
    }

    private void TogglePlayer()
    {
        if (isOn)
        {
            isOn = false;
            audioSource.Stop();
        }
        else
        {
            isOn = true;    
            audioSource.Play();
        }
    }

}
