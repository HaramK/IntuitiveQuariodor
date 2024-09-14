using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quaridor
{
    public class WallTokenCompo : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer wallSprite;

        public void SetRotation(RotationType rotationType)
        {
            transform.rotation = Quaternion.Euler(0, 0, rotationType switch {
                RotationType.Vertical => 0,
                RotationType.Horizontal => 90,
                _ => 0
            });
        }
        
        public void Set(bool isValid)
        {
            if(!ColorUtility.TryParseHtmlString("#D1E55C", out var basicColor))
            {
                Debug.LogError("Invalid hexadecimal color");
                return;
            }
            wallSprite.color = isValid ? basicColor : Color.red;
        }
    }
}
