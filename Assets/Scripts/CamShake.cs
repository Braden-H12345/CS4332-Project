using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    [SerializeField] Transform _cam;
    [SerializeField] float _shakeDur = 0f;
    [SerializeField] float shakeAmount = .5f;

    [SerializeField] GameObject _objectToTrack;


    private Health Health;
    private Vector3 _startPos;
    // Start is called before the first frame update
    void Start()
    {
        Health = _objectToTrack.GetComponent<Health>();
        OnEnable();
    }

    private void Awake()
    {
        _startPos = _cam.localPosition;
    }

    private void OnEnable()
    {
        Health.PlayerDamaged += CameraShake;
    }

    private void OnDisable()
    {
        Health.PlayerDamaged -= CameraShake;
    }
    // Update is called once per frame
    void Update()
    {

    }

    void CameraShake(int damage)
    {
        StartCoroutine(Shake(_shakeDur, shakeAmount));
    }

    public IEnumerator Shake(float duration, float amount)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * amount;
            float y = Random.Range(-1f, 1f) * amount;
            float z = Random.Range(-1f, 1f) * amount;

            _cam.localPosition = new Vector3(x, y, z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        _cam.localPosition = _startPos;
    }
}
