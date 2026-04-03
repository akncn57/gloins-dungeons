using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    private Vector2 initialPosition;

    protected override void Start()
    {
        base.Start();
        initialPosition = background.anchoredPosition;
        // background.gameObject.SetActive(false); // Kaldırıldı, artık hep görünecek
        
        // Prefab içerisinde kapalı kaydedilmiş olma ihtimaline karşı zorla açıyoruz:
        background.gameObject.SetActive(true);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        // background.gameObject.SetActive(true); // Artık hep açık
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        // Parmağını çektiğinde ilk merkezine geri dönsün
        background.anchoredPosition = initialPosition;
        // background.gameObject.SetActive(false); // Kaldırıldı
        base.OnPointerUp(eventData);
    }
}