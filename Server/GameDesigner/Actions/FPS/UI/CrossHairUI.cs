using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class CrossHairUI:MonoBehaviour
{
    [System.Serializable]
    public class CrossImage
    {
        public RectTransform rt;
        [Header("准心偏移方向")]
        public Vector2 moveDir;
    }
    public CrossImage[] crossHair = new CrossImage[4];
    public static CrossHairUI crossHairUI;
    public static CrossHairUI Instance {
        get {
            if (crossHairUI==null) {
                crossHairUI = FindObjectOfType<CrossHairUI>();
            }
            return crossHairUI;
        }
    }
    public int index = 0;
    public int indexMax = 5;

    private void Awake()
    {
        crossHairUI = this;
    }

    public void Update()
    {
        if ((Instance.index > 0 & Instance.index < Instance.indexMax) | (Instance.index > 0 & !InputControl.Instance.fire)) {
            if (GameDesigner.Timer.SpecifiedTime(10,0.1f)) {
                foreach (var image in Instance.crossHair) {
                    image.rt.anchoredPosition -= image.moveDir;
                }
                Instance.index--;
            }
        }
    }

    public static void SetCrossHair()
    {
        if (Instance.index >= Instance.indexMax) {
            return;
        }
        foreach (var image in Instance.crossHair) {
            image.rt.anchoredPosition += image.moveDir;
        }
        Instance.index++;
    }
}
