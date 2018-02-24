using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fadein : MonoBehaviour
{

    public float FadeInTime;


    private Image _fadePanel;
    private Color _currentColor;

    // Use this for initialization
    private void Start()
    {
        _fadePanel = GetComponent<Image>();
        _currentColor = _fadePanel.color;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.timeSinceLevelLoad < FadeInTime)
        {
            var alpha = Time.deltaTime / FadeInTime;
            _currentColor.a -= alpha;
            _fadePanel.color = _currentColor;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
