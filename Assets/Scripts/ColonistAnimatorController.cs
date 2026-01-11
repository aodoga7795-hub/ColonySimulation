using UnityEngine;
using UnityEngine.InputSystem;

public class ColonistAnimatorController : MonoBehaviour
{
    /// <summary>
    /// 住人のアニメーター
    /// </summary>
    public Animator ColonistAnimator;

    private void Update()
    {
        //デバック用

        if (Keyboard.current.digit0Key.wasPressedThisFrame)
            PlayIdleAnimation();

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
            PlayWalkingAnimation();

        if (Keyboard.current.digit2Key.wasPressedThisFrame)
            PlayMineAnimation();


        if (Keyboard.current.digit3Key.wasPressedThisFrame)
            PlaySleepingAnimation();

        if (Keyboard.current.digit4Key.wasPressedThisFrame)
            PlayDancingAnimation();


        if (Keyboard.current.digit1Key.wasPressedThisFrame
            && Keyboard.current.minusKey.wasPressedThisFrame)
            PlayDeathAnimation();
    }

    /// <summary>
    /// 待機アニメーションを再生
    /// </summary>
    public void PlayIdleAnimation()
    {
        ColonistAnimator.SetInteger("AnimationState", 0);
    }
    /// <summary>
    /// 歩くアニメーションを再生
    /// </summary>
    public void PlayWalkingAnimation()
    {
        ColonistAnimator.SetInteger("AnimationState",1);
    }
    /// <summary>
    /// 掘るアニメーションを再生
    /// </summary>
    public void PlayMineAnimation()
    {
        ColonistAnimator.SetInteger("AnimationState", 2);
    }

    /// <summary>
    /// 睡眠アニメーションを再生
    /// </summary>
    public void PlaySleepingAnimation()
    {
        ColonistAnimator.SetInteger("AnimationState", 3);
    }

    /// <summary>
    /// ダンスアニメーションを再生
    /// </summary>
    public void PlayDancingAnimation()
    {
        ColonistAnimator.SetInteger("AnimationState", 4);
    }

    /// <summary>
    /// 死ぬアニメーションを再生
    /// </summary>
    public void PlayDeathAnimation()
    {
        ColonistAnimator.SetInteger("AnimationState", -1);
        ColonistAnimator.SetTrigger("DeathTrigger");
    }

}
