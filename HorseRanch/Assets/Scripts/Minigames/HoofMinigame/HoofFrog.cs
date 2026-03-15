using UnityEngine;
using UnityEngine.EventSystems;

public class HoofFrog : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHovering = false;
    private float damageCooldown = 0f;

    public void OnPointerEnter(PointerEventData eventData) { isHovering = true; }
    public void OnPointerExit(PointerEventData eventData) { isHovering = false; }

    void Update()
    {
        if (damageCooldown > 0)
        {
            damageCooldown -= Time.unscaledDeltaTime;
        }

        if (isHovering && Input.GetMouseButton(0) && damageCooldown <= 0)
        {
            float mouseSpeed = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")).magnitude;

            if (mouseSpeed > 0.1f)
            {
                HoofMinigame.instance.FrogPoked();

                damageCooldown = 1.0f;
            }
        }
    }
}