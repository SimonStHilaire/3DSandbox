using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Speaker : MonoBehaviour
{
    AudioSource Audio;

    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public bool IsPLaying()
    {
        return (Audio && Audio.isPlaying && !Audio.mute);
    }

    /*
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(IsPLaying())
            Gizmos.DrawIcon(transform.position, "music_icon_on");
        else
            Gizmos.DrawIcon(transform.position, "music_icon_off");

        if (Audio)
            Gizmos.DrawWireSphere(transform.position, Audio.maxDistance);
    }
#endif //UNITY_EDITOR
*/
}
