using UnityEngine;
using UnityEngine.TextCore.Text;

[RequireComponent(typeof(Animator))]
public class CharacterAnimatorSelector : MonoBehaviour
{
    private Animator animator;

    private int character;
    public RuntimeAnimatorController controllerA;
    public RuntimeAnimatorController controllerB;

    private void Start()
    {
        animator = GetComponent<Animator>();

        character = UserDataLoader.LoadCharacter();
        if (character == 1)
        {
            animator.runtimeAnimatorController = controllerA;
        }
        else if (character == 2)
        {
            animator.runtimeAnimatorController = controllerB;
        }
    }
}
