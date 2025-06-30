using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SceneType
{
    Lobby,
    Stage
}

public class BackGroundMusic : MonoBehaviour
{
    [SerializeField]
    SceneType sceneType;

    void Start()
    {
        switch(sceneType)
        {
            case SceneType.Lobby:
                SoundManager.Instance.PlayerBackGroundSound(SoundEnum.TitleBackGround);
                break;

            case SceneType.Stage:
                SoundManager.Instance.PlayerBackGroundSound(SoundEnum.StageBackGround);
                break;
        }
    }
}
