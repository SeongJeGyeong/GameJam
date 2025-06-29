using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEnum
{
    BtnClick,
    BtnHover,
    Pause,
    Attack_Melee_Hand,
    Attack_Melee_Sword,
    Attack_Melee_Hammer,
    Attack_Magic_Fire,
    Attack_Magic_Ice,
    Attack_Magic_Electric,
    BulletHited_Fire,
    BulletHited_Ice,
    BulletHited_Electric,
    Hitted_Player,
    Hitted_Monster,
    Player_Move,
    Player_Jump,
    EquipChange,
    ChestOpen,
    StageClear,
    StageFail,
}
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;

    public List<AudioClip> AudioClips;
    [SerializeField]
    AudioSource audioSource;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static SoundManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public void PlaySound(SoundEnum sEnum)
    {
        audioSource.PlayOneShot(AudioClips[(int)sEnum]);
    }
}
