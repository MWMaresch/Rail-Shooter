using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using System;
using System.Linq;

public class Menu : MonoBehaviour {

    public InputField fpsInputField;
    public AudioMixer audioMixer;
    public Dropdown resDropdown;
    public Dropdown internalResDropdown;
    public Dropdown screenModeDropdown;
    public Toggle yAxisInversion;
    public Toggle vSyncToggle;

    private List<Resolution> resolutions;

    private void UpdateWindowResolutions()
    {
        Debug.Log("updating resolutions");
        resDropdown.ClearOptions();
        resolutions = new List<Resolution>();
        foreach (Resolution res in Screen.resolutions.Where(resolution => resolution.refreshRate == 60))
        {
            //if (resolutions.IndexOf(res) == -1)
            resolutions.Add(res);
        }
        //these resolutions are not supported in exclusive fullscreen mode
        if (Screen.fullScreenMode != FullScreenMode.ExclusiveFullScreen)
        {
            Resolution x1 = new Resolution();
            x1.width = 480; x1.height = 270;
            Resolution x2 = new Resolution();
            x2.width = 960; x2.height = 540;
            Resolution x3 = new Resolution();
            x3.width = 1440; x3.height = 810;
            resolutions.Add(x1);
            resolutions.Add(x2);
            resolutions.Add(x3);
        }

        List<string> resOptions = new List<string>();

        int curResIndex = 0;
        for (int res = 0; res < resolutions.Count; res++)
        {
            string resOption = resolutions[res].width + "X" + resolutions[res].height;
            resOptions.Add(resOption);

            if (resolutions[res].width == Screen.currentResolution.width &&
                resolutions[res].height == Screen.currentResolution.height)
            {
                curResIndex = res;
            }
        }
        if (Screen.fullScreenMode != FullScreenMode.ExclusiveFullScreen)
        {
            resOptions[resOptions.Count - 1] = "X3 (1440X810)";
            resOptions[resOptions.Count - 2] = "X2 (960X540)";
            resOptions[resOptions.Count - 3] = "PERFECT (480X270)";
        }

        resDropdown.AddOptions(resOptions);
        resDropdown.value = curResIndex;
        resDropdown.RefreshShownValue();
    }

    private void Start()
    {
        UpdateWindowResolutions();

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

        UpdateScreenModeDropDown();
    }

    private void UpdateScreenModeDropDown()
    {
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
        UpdateScreenModeDropDown();
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
        UpdateWindowResolutions();
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
