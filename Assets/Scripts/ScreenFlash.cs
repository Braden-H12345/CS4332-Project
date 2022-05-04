using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScreenFlash : MonoBehaviour
{
    Image _image = null;
    Coroutine _currentFlashRoutine = null;

    [SerializeField] GameObject _objectToTrack;
    [SerializeField] float _duration;
    [SerializeField] float _maxAlpha;
    private Health Health;
    private void OnEnable()
    {
        Health.PlayerDamaged += ColorFlash;
    }

    private void OnDisable()
    {
        Health.PlayerDamaged -= ColorFlash;
    }
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        Health = _objectToTrack.GetComponent<Health>();
        OnEnable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ColorFlash(int damage)
    {
        StartFlash(_duration, _maxAlpha, Color.red);
    }

    void StartFlash(float secondsFlash, float maxAlpha, Color newColor)
    {
        _image.color = newColor;

        maxAlpha = Mathf.Clamp(maxAlpha, 0, 1);
        if (_currentFlashRoutine != null)
        {
            StopCoroutine(_currentFlashRoutine);
        }

        _currentFlashRoutine = StartCoroutine(Flash(secondsFlash, maxAlpha));
    }

    IEnumerator Flash(float secondsFlash, float maxAlpha)
    {
        float flashIn = secondsFlash / 2;

        for (float i = 0; i <= flashIn; i += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(0, maxAlpha, i / flashIn);
            _image.color = colorThisFrame;
            yield return null;
        }

        float flashOut = secondsFlash / 2;
        for (float i = 0; i <= flashOut; i += Time.deltaTime)
        {
            Color colorThisFrame = _image.color;
            colorThisFrame.a = Mathf.Lerp(maxAlpha, 0, i / flashOut);
            _image.color = colorThisFrame;
            yield return null;
        }

        _image.color = new Color32(0, 0, 0, 0);
    }

}
