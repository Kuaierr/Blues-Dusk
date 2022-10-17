using UnityEngine;
using UnityEngine.UI;
using GameKit.QuickCode;
namespace UnityGameKit.Runtime
{
    public class Switcher : MonoBehaviour
    {
        public bool Fade => gradienter.gameObject.activeSelf;
        public bool Loading => loading.gameObject.activeSelf;
        public bool Animating => animator.gameObject.activeSelf;
        public bool Swipe => swiper.gameObject.activeSelf;
        
        public Animator animator;
        public Transform swiper;
        public Image gradienter;

        [Space]
        public UI_Loading loading;
    }
}
