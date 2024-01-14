using UnityEngine;

namespace SDK
{
    public class TextOptimizer : MonoBehaviour
    {
        [SerializeField] private int _maxLength;

        public string OptimizeText(string name)
        {
            string nameToLower = name.ToLower();
            char[] letters = nameToLower.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            string finalName = new (letters);

            if (finalName.Length > _maxLength)
                return finalName[.._maxLength];
            else
                return finalName;
        }
    }
}