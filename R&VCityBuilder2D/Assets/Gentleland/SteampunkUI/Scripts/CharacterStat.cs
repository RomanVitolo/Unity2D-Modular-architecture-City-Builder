using TMPro;
using UnityEngine;

namespace Gentleland.SteampunkUI.Scripts
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CharacterStat : MonoBehaviour
    {
        TextMeshProUGUI textMesh;
        int value;
        void Start()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
            value = int.Parse(textMesh.text);
        }

        public void Increment()
        {
            value += 1;
            textMesh.text = "" +value;
        }

        public void Decrement()
        {
            value -= 1;
            textMesh.text = ""+value;

        }
    }
}