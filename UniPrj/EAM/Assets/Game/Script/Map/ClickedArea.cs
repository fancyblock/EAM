using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickedArea : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_sprite;

    private Color m_color;
    private float m_timer;


    void Awake()
    {
        m_color = m_sprite.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_timer -= Time.deltaTime;

        m_timer = Mathf.Clamp01(m_timer);
        m_sprite.color = new Color(m_color.r, m_color.g, m_color.b, m_timer);

        if (m_timer < float.Epsilon)
            gameObject.SetActive(false);
    }


    public void ShowAt(float x, float y)
    {
        transform.localPosition = new Vector2(x, y);

        gameObject.SetActive(true);
        m_sprite.color = m_color;
        m_timer = 1.0f;
    }
}
