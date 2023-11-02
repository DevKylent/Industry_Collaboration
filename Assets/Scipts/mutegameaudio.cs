using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mutegameaudio : MonoBehaviour
{

    /// esto es para aplicarle la abilidad de poner mute al juego y quitalre el mute al juego para  que alla sonido en el juego 
    public void muteaudioTuggle(bool muted)
    {
        if (muted)
        {
            AudioListener.volume = 0;// con esto seria que se para el sonido 
        }
        else
        {
            AudioListener.volume = 1;// con esto seria para que alla sonido 
        }
    }
}
