using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    #region Singleton
    public static SoundLibrary instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Sounds instance error!");
            return;
        }
        instance = this;
    }
    #endregion

    //звуки
    public AudioClip death, brick, flag, jump, 
        mushroom, pipe_travel, coin, enemy;

    //музыка
    public AudioClip gameover, main_theme, mushroom_appear,
        secret_theme, success;
}
