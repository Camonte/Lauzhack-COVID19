using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneScript : MonoBehaviour
{
    public string AudioInput;
    private AudioSource audio;
    // Start recording with built-in Microphone and play the recorded audio right away
    void Start()
    {
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            
            //AudioInput = device;
        }
        audio = GetComponent<AudioSource>();
        int min;
        int max;

       
        

        audio.clip = Microphone.Start(AudioInput, true, 10, 44100);
        Microphone.GetDeviceCaps(AudioInput, out min,out max);
        Debug.Log("Start playing"+min+" "+max);
        audio.Play();
        audio.timeSamples = Microphone.GetPosition(AudioInput);
        
        //aud.timeSamples = 0;
        
    }
    void Update(){
        Debug.Log("Is Playing : " + Microphone.IsRecording(AudioInput));
        Debug.Log(" devicePosition:" + Microphone.GetPosition(AudioInput));
        Debug.Log("      audioTime:" + audio.time);
        audio.timeSamples = Microphone.GetPosition(AudioInput);
    }
}
