using UnityEngine;

namespace Rubber
{

    public class ResetColor : MonoBehaviour
    {
        Animator animator;
        // Use this for initialization
        void Start()
        {
            LevelObjectives.MakeAllConditionsFalse();
            animator = GetComponent<Animator>();
        }

        void OnEnable()
        {
            Instantier.ColorReseted += InıtialColorState;
        }
        void OnDisable()
        {
            Instantier.ColorReseted -= InıtialColorState;
        }

        public void InıtialColorState()
        {
            LevelObjectives.MakeAllConditionsFalse();
            LevelObjectives.ColorInitializer(SceneDirector.activeScene.buildIndex, ref animator);
        }
    }
}
