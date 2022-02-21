using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class SettingsToggle : MonoBehaviour
{
    public RectTransform btnFB, btnIG, btnInfo, btnSound, btnMusic;
    public float moveSound, moveMusic,moveFB, moveIG, moveInfo;
    public float defaultPosY, defaultPosX;
    public float speed;

    bool expanded;

    // Start is called before the first frame update
    void Start()
    {
        expanded = false;
    }

    public void Toggle()
    {
        if (!expanded)
        {
            btnSound.DOAnchorPosY(moveSound, speed, false);
            btnMusic.DOAnchorPosY(moveMusic, speed, false);

            btnFB.DOAnchorPosY(moveFB, speed, false);
            btnIG.DOAnchorPosY(moveIG, speed, false);
            btnInfo.DOAnchorPosY(moveInfo, speed, false);

            expanded = true;

        }
        else
        {
            btnSound.DOAnchorPosY(defaultPosY, speed, false);
            btnMusic.DOAnchorPosY(defaultPosY, speed, false);

            btnFB.DOAnchorPosY(defaultPosY, speed, false);
            btnIG.DOAnchorPosY(defaultPosY, speed, false);
            btnInfo.DOAnchorPosY(defaultPosY, speed, false);

            expanded = false;

        }
    }


}
