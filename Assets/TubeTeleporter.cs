using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeTeleporter : MonoBehaviour
{
    private Joystick joystick;
    private GameObject joystickContainer;
    private GameObject Mario;

    public float teleportX, teleportY;
    public float cameraX, cameraY;
    //false - телепорт вниз (анимация вхождения вниз), true - вверх (анимация вхождения влево - вверх)
    public bool teleportType = false;

    private void Start()
    {
        joystickContainer = GameObject.Find("Main Canvas").transform.Find("Fixed Joystick").gameObject;
        Mario = GameObject.Find("Mario").gameObject;
        joystick = joystickContainer.GetComponent<Joystick>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var downPressed = joystick.Vertical;
        //телепорт возможен!
        if(downPressed < -0.8f && Mario.GetComponent<PlayerMovement>().isGrounded)
        {
            Mario.GetComponent<AudioSource>().PlayOneShot(SoundLibrary.instance.pipe_travel);
            //воспроизводим анимацию перехода
            AudioSource src = Camera.main.GetComponent<AudioSource>();
            src.Stop();
            if (!teleportType)
                src.PlayOneShot(SoundLibrary.instance.secret_theme);
            else
                src.PlayOneShot(SoundLibrary.instance.main_theme);
            //телепортируем Марио в определенную локацию
            Mario.transform.position = new Vector3(teleportX, teleportY, 0);
            GameObject.Find("Main Camera").transform.position = new Vector3(cameraX, cameraY, -10);
        }
    }
}
