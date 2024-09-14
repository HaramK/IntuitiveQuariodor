using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quaridor
{
    public class PlayerTokenCompo : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer playerSprite;
        [SerializeField] private float alpha = 1f;

        public void Set(int id, bool isValid)
        {
            var color = playerSprite.color;
            color = !isValid ? Color.red : id == 0 ? Color.white : Color.black;
            color = new Color(color.r, color.g, color.b, alpha);
            playerSprite.color = color;
        }
    }
}