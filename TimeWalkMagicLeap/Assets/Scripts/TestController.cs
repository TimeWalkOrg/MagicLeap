using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;

public class TestController : MonoBehaviour {

    #region Public Variables
    public MLInputController _controller; 
    public GameObject Info;
    public GameObject MainCamera;
    public GameObject Object01;
    public GameObject Object02;
    public GameObject Object03;
    public GameObject Object04;
    public GameObject Object05;
    public List<GameObject> allObjects;
    public int objectIndex = 0;
    #endregion

    #region Private Variables
    private Vignette _vignette;
    private Instructions _instructions; 
    private bool _infoMode = true;
    private bool _bumperUp = false;
    private bool _homeButtonUp = false;
    private float _triggerThreshold = 0.2f;
    private bool _triggerPressed;
    #endregion

    #region Unity Methods
    void Awake() {
        MLInput.Start();
    }
        
    void Start() {
        _controller = MLInput.GetController(MLInput.Hand.Left);
        MLInput.OnControllerButtonUp += OnButtonUp;

        _vignette = MainCamera.GetComponentInChildren<Vignette>();
        _instructions = Info.GetComponentInChildren<Instructions>();

        setInfoState(true);
    }

    void OnDestroy() {
        MLInput.Stop();
    }

    void LateUpdate() {
        if (_infoMode) {
            checkTrigger();
        }
        else {
            checkBumper();
            checkTrigger();
        }
        checkHomeButton();
        resetFlags();
    }
    #endregion

    #region Private Methods
    void setInfoState(bool state) {
        _infoMode = state;
        if (_infoMode) {
            Object01.SetActive(false);
            Object02.SetActive(false);
            Object03.SetActive(false);
            Object04.SetActive(false);
            Object05.SetActive(false);
            for(int i = 0; i < allObjects.Count; i++)
            {
                allObjects[i].SetActive(false);
            }
            _instructions.NextPage(true);
            _vignette.Reset();
        }
        else if (Object01.activeSelf)
            {
                Object01.SetActive(false);
                Object02.SetActive(true);
                Object03.SetActive(false);
                Object04.SetActive(false);
                Object05.SetActive(false);
        }
        else if (Object02.activeSelf)
            {
                Object01.SetActive(false);
                Object02.SetActive(false);
                Object03.SetActive(true);
                Object04.SetActive(false);
                Object05.SetActive(false);
        }
        else if (Object03.activeSelf)
        {
            Object01.SetActive(false);
            Object02.SetActive(false);
            Object03.SetActive(false);
            Object04.SetActive(true);
            Object05.SetActive(false);
        }
        else if (Object04.activeSelf)
        {
            Object01.SetActive(false);
            Object02.SetActive(false);
            Object03.SetActive(false);
            Object04.SetActive(false);
            Object05.SetActive(true);
        }
        else
            {
                Object01.SetActive(true);
                Object02.SetActive(false);
                Object03.SetActive(false);
            }
        //_vignette.ToggleVignetteState();
        _vignette.Reset();
    }
    private void resetFlags() {
        _homeButtonUp = false;
        _bumperUp = false;
    }
    private void checkHomeButton() {
        if (_homeButtonUp) {
            Application.Quit();
            // setInfoState(true);
        }
    }
    private void checkBumper() {
        if (_bumperUp) {
            _vignette.ToggleVignetteState();
            _controller.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.Bump, MLInputControllerFeedbackIntensity.High);
        }
    }

    private void checkTrigger() {
        if (_controller.TriggerValue < _triggerThreshold) {
            _triggerPressed = false;
        }
        else {
            if (_triggerPressed == false) {
                if (!_instructions.NextPage()) {
                    setInfoState(false);
                }
                _triggerPressed = true;
            }
        }
    }
    #endregion

    #region Events    
    void OnButtonUp(byte controller_id, MLInputControllerButton button) {
        if (button == MLInputControllerButton.Bumper) {  
            _bumperUp = true;
        }
        else if (button == MLInputControllerButton.HomeTap) {          
            _homeButtonUp = true;
        }
    }
    #endregion
}


