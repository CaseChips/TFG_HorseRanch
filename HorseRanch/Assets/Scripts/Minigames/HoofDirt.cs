using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 

public class HoofDirt : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Scrubbing Settings")]
    public float dirtHealth = 100f;
    public float scrubPower = 1700f; 

    private bool isHovering = false;
    private float startingHealth;
    private Image dirtImage;

    void Start()
    {
        dirtImage = GetComponent<Image>();
        startingHealth = dirtHealth;
    }

    public void OnPointerEnter(PointerEventData eventData) { isHovering = true; }
    public void OnPointerExit(PointerEventData eventData) { isHovering = false; }

    void Update()
    {
        if (isHovering && Input.GetMouseButton(0))
        {
            float mouseSpeed = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")).magnitude;

            if (mouseSpeed > 0.1f)
            {
                dirtHealth -= scrubPower * mouseSpeed * Time.unscaledDeltaTime;

                Color c = dirtImage.color;
                c.a = dirtHealth / startingHealth; 
                dirtImage.color = c;

                if (dirtHealth <= 0)
                {
                    HoofMinigame.instance.DirtPicked();
                    Destroy(gameObject);
                }
            }
        }
    }
}