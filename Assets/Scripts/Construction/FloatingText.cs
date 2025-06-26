using System.Collections;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private float _floatDistance = 1f;
    [SerializeField] private float _duration = 1f;

    private TMP_Text _text;
    private Color _startColor;
    private Vector3 _startPos;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _startColor = _text.color;
        _startPos = transform.localPosition;
    }

    private void OnEnable()
    {
        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;

        while (elapsed < _duration)
        {
            float t = elapsed / _duration;

            transform.localPosition = _startPos + Vector3.up * (_floatDistance * t);

            Color c = _startColor;
            c.a = Mathf.Lerp(1f, 0f, t);
            _text.color = c;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = _startPos + Vector3.up * _floatDistance;
        _text.color = new Color(_startColor.r, _startColor.g, _startColor.b, 0f);

        Destroy(gameObject);
    }
}

