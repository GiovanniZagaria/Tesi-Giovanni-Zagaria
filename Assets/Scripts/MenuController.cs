using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static bool isPaused;
    public static bool isLoad = false;

    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 0.5f;

    [Header("Graphics Setting")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private TMP_Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    [Space(10)]
    [SerializeField] private Toggle fullScreenToogle;

    private bool _isFullscreen;
    private float _brightnessLevel;

    [Header("Confirmation")]
    [SerializeField] private GameObject comfirmationPrompt = null;

    [Header("Levels To Load")]
    public string _newGameLevel;
    private string pathToFile;
    [SerializeField] private GameObject noSavedGameDialog = null;
    [SerializeField] private GameObject LoadGameDialog = null;
    [SerializeField] private List<TMP_Text> SaveText = null;

    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void NewGameDialogYes()
    {
        isLoad = false;
        SceneManager.LoadScene(_newGameLevel);
    }

    public void LoadGameDialogYes()
    {
        if (File.Exists(Application.persistentDataPath + "/datiInputField0.json") || File.Exists(Application.persistentDataPath + "/datiInputField1.json") || File.Exists(Application.persistentDataPath + "/datiInputField2.json"))
        {

            for (int i = 0; i < 3; i++)
            {
                pathToFile = Application.persistentDataPath + "/datiInputField" + i + ".json";
                if (File.Exists(pathToFile))
                {
                    string datiJson = File.ReadAllText(pathToFile);
                    SaveData dati = JsonUtility.FromJson<SaveData>(datiJson);

                    SaveText[i].text = dati.date;
                }
            }
            LoadGameDialog.SetActive(true);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0").Replace(',', '.');
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public void ResetButton(string menutype)
    {
        if (menutype == "Graphics")
        {
            brightnessSlider.value = defaultBrightness;
            brightnessTextValue.text = defaultBrightness.ToString("0.0").Replace(',', '.');

            fullScreenToogle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }
        if (menutype == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0").Replace(',', '.');
            VolumeApply();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        brightnessTextValue.text = brightness.ToString("0.0").Replace(',', '.');
    }

    public void SetFullscreen(bool isFullscreen)
    {
        _isFullscreen = isFullscreen;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        PlayerPrefs.SetInt("masterFullscreen", _isFullscreen ? 1 : 0);
        Screen.fullScreen = _isFullscreen;

        StartCoroutine(ConfirmationBox());
    }

    public void ActivateMenu()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void DeactivateMenu()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    public void SetSaveToLoad(int slot)
    {
        if (File.Exists(Application.persistentDataPath + "/datiInputField" + slot + ".json"))
        {
            isLoad = true;
            SaveLoad.saveSlot = slot;
            SceneManager.LoadScene(_newGameLevel);
        }
    }
}
