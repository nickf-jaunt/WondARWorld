using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController Instance { get; private set; }

    public GameObject targetDefinitionWindow;
    public Button setTargetButton;
    public Text globalStatusText;
    public GameObject limboDefinitionWindow;
    public GameObject detectingWindow;
    public GameObject scanButton;
	public GameObject scanningWindow;
    public GameObject foundWindow;

    private void Awake ()
    {
        if (Instance != null && Instance != this)
            GameObject.Destroy(Instance.gameObject);
        else
            Instance = this;
    }
}
