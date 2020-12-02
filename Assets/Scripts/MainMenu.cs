using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject optionsMenuHolder;

    public UnityEngine.UI.Slider[] volumeSliders;
    public UnityEngine.UI.Toggle[] resolutionToggles;
    public int[] screenWidths;
    int activeScreenResIndex;



    public void Start()
    {
        activeScreenResIndex = PlayerPrefs.GetInt("screen res index");
        bool isFullscreen = (PlayerPrefs.GetInt("fullScreen") == 1) ? true:false;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }

    public void OptionsMenu()
    {

    }

    public void SetScreenResolution(int i)
    {
        if(resolutionToggles [i].isOn)
        {
            activeScreenResIndex = i;
            float aspectRatio = 16 / 9f;
            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / aspectRatio), false);
        }
    }

    public void SetFullScreen(bool isFullscreen)
    {
    for (int i = 0; i < resolutionToggles.Length; i++)
        {
            resolutionToggles [i].interactable = !isFullscreen;
        }

        if(isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution (maxResolution.width, maxResolution.height, true);
            PlayerPrefs.SetInt("screen res index", activeScreenResIndex);
            PlayerPrefs.Save();
        }
        else
        {
            SetScreenResolution (activeScreenResIndex);
        }
        PlayerPrefs.SetInt("fullscreen", ((isFullscreen) ? 1:0));
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float value)
    {

    }

    public void SetMusicVolume(float value)
    {

    }
}
