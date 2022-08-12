using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class PlayerLifeView : MonoBehaviour
    {
        [SerializeField] private Image[] _images;

        public void Display(int count)
        {
            if (count > _images.Length)
            {
                throw new Exception("invalid quantity to display");
            }

            for (int i = 0; i < _images.Length; i++)
            {
                if (i < count)
                {
                    _images[i].enabled = true;
                }
                else
                {
                    _images[i].enabled = false;
                }
            }
        }
    }
}
