using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class SoundImageHandler : MonoBehaviour
    {
        [SerializeField] private Image _sourceImage;
        [SerializeField] private Sprite _soundOnSprite;
        [SerializeField] private Sprite _soundOffSprite;

        public void ChangeImage()
        {
            if (_sourceImage.sprite == _soundOnSprite)
            {
                _sourceImage.sprite = _soundOffSprite;
            }
            else
            {
                _sourceImage.sprite = _soundOnSprite;
            }
        }
    }
}