using UnityEngine;

public class InvisibleEnemy : MonoBehaviour {
    public float invisibleTime = 3;
    public float showTime = 2;

    private float countDown;
    private bool showing = true;
    private float transparent;
    private SpriteRenderer m_renderer;
    // Start is called before the first frame update
    void Start() {
        countDown = invisibleTime;
        m_renderer = GetComponent<SpriteRenderer>();
        FlipState();
    }

    // Update is called once per frame
    void Update() {
        if (showing) {
            Blinking();
        }
        if (countDown > 0) {
            countDown -= Time.deltaTime;
        }
        else {
            if (showing) {
                countDown = invisibleTime;
            }
            else {
                countDown = showTime;
            }
            FlipState();
        }
    }

    void FlipState() {
        float transparent;
        Color tColor;
        if (showing) transparent = 0;
        else transparent = 1;
        tColor = m_renderer.color;
        tColor.a = transparent;
        m_renderer.color = tColor;
        foreach (Transform child in transform) {
            SpriteRenderer sprite = child.gameObject.GetComponent<SpriteRenderer>();
            if (sprite != null) {
                tColor = sprite.color;
                tColor.a = transparent;
                sprite.color = tColor;
            }
        }
        showing = !showing;
    }

    void Blinking() {
        Color tColor;
        transparent = 1 - transparent;
        tColor = m_renderer.color;
        tColor.a = transparent;
        m_renderer.color = tColor;
        foreach (Transform child in transform) {
            SpriteRenderer sprite = child.gameObject.GetComponent<SpriteRenderer>();
            if (sprite != null) {
                tColor = sprite.color;
                tColor.a = transparent;
                sprite.color = tColor;
            }
        }
    }
}
