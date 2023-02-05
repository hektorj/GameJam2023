using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenInAndOut : MonoBehaviour
{
    public enum Transformaciones { Move, MoveLocal, Rotate, Scale, Rebote }
    [System.Serializable]
    public class Objetos
    {
        public GameObject objeto;
        public Transformaciones transformacion;
        public Vector3 initialPosition;
        public Vector3 target;
        public float modificador;

        [Header("IN Settings")]
        public float animationDurationIN;
        public float delayIN;
        public LeanTweenType introEaseType;

        [Header("OUT Settings")]
        public float animationDurationOUT;
        public float delayOUT;
        public LeanTweenType outroEaseType;
    }

    public Objetos[] objetos;

    private void Start()
    {
        foreach (Objetos obj in objetos)
        {
            if (obj.transformacion == Transformaciones.Move)
            {
                LeanTween.move(obj.objeto, obj.target, obj.animationDurationIN).setEase(obj.introEaseType).setDelay(obj.delayIN);
            }
            else if (obj.transformacion == Transformaciones.MoveLocal)
            {
                LeanTween.moveLocal(obj.objeto, obj.target, obj.animationDurationIN).setEase(obj.introEaseType).setDelay(obj.delayIN);
            }
            else if (obj.transformacion == Transformaciones.Rotate)
            {
                LeanTween.rotateLocal(obj.objeto, obj.target * obj.modificador, obj.animationDurationIN).setEase(obj.introEaseType).setDelay(obj.delayIN);
            }
            else if (obj.transformacion == Transformaciones.Scale)
            {
                LeanTween.scale(obj.objeto, obj.target, obj.animationDurationIN).setEase(obj.introEaseType).setDelay(obj.delayIN);
            }
            else if (obj.transformacion == Transformaciones.Rebote)
            {
                LeanTween.move(obj.objeto, obj.target, obj.animationDurationIN).setEase(obj.introEaseType).setLoopPingPong(1).setDelay(obj.delayIN);
            }
        }

    }


    public TweenInAndOut HideGameObject()
    {
        LeanTween.move(gameObject, objetos[0].initialPosition, objetos[0].animationDurationOUT).setEaseOutQuart();

        //GameManagerTiro.instance.introHided = true;

        return null;
    }
}