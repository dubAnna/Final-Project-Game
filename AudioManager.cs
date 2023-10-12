/*Скрипт для керування аудіо в грі. 

Основні методи:
PlaySFX(int soundToPlay) - метод для відтворення звукових ефектів, зупиняє звук якщо він вже відтворюєтсья, 
встановлює випадковий пітч у діапазоні від 0.9 до 1.1 - тобто варіацію між висотою звуку під час програвання.

PlayLevelVictory() - призначений для відтворення музики у випадку завершення рівня.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource[] soundEffects;
    public AudioSource bgm, levelEndMusic;

    private void Awake()
    {

        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();
        soundEffects[soundToPlay].pitch = Random.Range(.9f, 1.1f);
        soundEffects[soundToPlay].Play();
    }
    public void PlayLevelVictory(){
        bgm.Stop();
        levelEndMusic.Play();
    }
}
