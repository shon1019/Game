using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {

    public AudioClip[] audioClips;

    public enum EffectName
    {
        Dead,
        Collision,
        TimeUP,
        Champion,
        Limit
    }

    public void PlaySoundEffect(EffectName _effect,Vector3 _pos)
    {

        switch (_effect)
        {
            case EffectName.Dead:
                AudioSource.PlayClipAtPoint(audioClips[0], _pos);
                break;
            case EffectName.Collision:
                AudioSource.PlayClipAtPoint(audioClips[1], _pos);
                break;
            case EffectName.TimeUP:
                AudioSource.PlayClipAtPoint(audioClips[2], _pos);
                break;
            case EffectName.Champion:
                AudioSource.PlayClipAtPoint(audioClips[3], _pos);
                break;
            case EffectName.Limit:
                AudioSource.PlayClipAtPoint(audioClips[4], _pos);
                break;
        }
    }

    public void Stop(EffectName _effect)
    {

    }

    public void StopAll()
    {
        
    }
}
