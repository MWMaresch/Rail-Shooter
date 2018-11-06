using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;



public class Menu : MonoBehaviour {

    public InputField fpsInputField;
    public AudioMixer audioMixer;
    public Dropdown resDropdown;
    public Dropdown internalResDropdown;
    public Dropdown screenModeDropdown;
    public Toggle yAxisInversion;
    public Toggle vSyncToggle;

    private Resolution[] resolutions;


    private void Start()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();

        List<string> resOptions = new List<string>();

        int curResIndex = 0;
        for(int res = 0; res < resolutions.Length; res++)
        {
            string resOption = resolutions[res].width + " x " + resolutions[res].height;
            resOptions.Add(resOption);

            if (resolutions[res].width == Screen.currentResolution.width &&
                resolutions[res].height == Screen.currentResolution.height)
            {
                curResIndex = res;
            }
        }
        
        resDropdown.AddOptions(resOptions);
        resDropdown.value = curResIndex;
        resDropdown.RefreshShownValue();

        if (GlobalOptions.InternalWidth == 480)
            internalResDropdown.value = 0;
        else if (GlobalOptions.InternalWidth == 960)
            internalResDropdown.value = 1;
        else
            internalResDropdown.value = 2;
        internalResDropdown.RefreshShownValue();

        yAxisInversion.isOn = GlobalOptions.InvertYAxis;

        fpsInputField.text = Application.targetFrameRate.ToString();
        if (QualitySettings.vSyncCount == 1)
            vSyncToggle.isOn = true;
        else
            vSyncToggle.isOn = false;

        if (Screen.fullScreen)
        {
            if (Screen.fullScreenMode == FullScreenMode.ExclusiveFullScreen)
                screenModeDropdown.value = 0;
            else
                screenModeDropdown.value = 1;
        }
        else
            screenModeDropdown.value = 2;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetVolume(float v)
    {
        audioMixer.SetFloat("Volume", v);
    }
    public void SetVSync(bool vSync)
    {
        if (vSync)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
        Debug.Log("vsync turned to " + QualitySettings.vSyncCount);
    }
    public void SetFramerate(string Sfps)
    {
        try { 
            int fps = Convert.ToInt32(Sfps);
            if (fps < 10)
            {
                fps = -1;
                //fpsInputField.text = "Unlimited";
            }
            Application.targetFrameRate = fps;
        }
        catch
        {
            Application.targetFrameRate = 120;
        }
    }

    public void SetResolution(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetScreenMode(int mode)
    {
        if (mode == 0)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreen = true;
        }
        else if (mode == 1)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
        }
        else if (mode == 2)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = false;
        }
    }

    public void SetInternalResolution(int index)
    {
        if (index == 0)
        {
            GlobalOptions.InternalWidth = 480;
            GlobalOptions.InternalHeight = 270;
        }
        else if (index == 1)
        {
            GlobalOptions.InternalWidth = 960;
            GlobalOptions.InternalHeight = 540;
        }
        else if (index == 2)
        {
            GlobalOptions.InternalWidth = 1440;
            GlobalOptions.InternalHeight = 810;
        }
    }

    public void SetYAxisInversion(bool val)
    {
        GlobalOptions.InvertYAxis = val;
    }
}
