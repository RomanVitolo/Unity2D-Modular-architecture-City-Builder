using UnityEngine;
using UnityEngine.UI;

namespace Gentleland.SteampunkUI.Scripts
{
    [RequireComponent(typeof(Selectable))]
    public class KeyFieldUnSelect : MonoBehaviour
    {
        [SerializeField]
        Selectable selectable;

        public void UnSelect()
        {
            if (selectable != null) selectable.Select();
        }
    }
}
