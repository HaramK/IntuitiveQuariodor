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
    }
}
