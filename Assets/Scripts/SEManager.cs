using UnityEngine;

public class SEManager : MonoBehaviour
{
    /// <summary>
    /// SEManagerをどこからでも呼べるようにStatic変数を用意します
    /// Static修飾子をつけると、ゲーム実行時にどこからでも参照することができる
    /// </summary>
    public static SEManager Instance;

    /// <summary>
    /// AudioSourceは音を鳴らすスピーカーの役割をするコンポーネント
    /// </summary>
    private AudioSource SEAudioSource;

    /// <summary>
    /// Startが実行されるより前に実行されるメソッド
    /// 主に初期化などを行うときに使われる
    /// </summary>
    private void Awake()
    {
        Instance = this;
        if(SEAudioSource == null)
        {
            //AddComponentはこのクラスが追加されたGameObjectに
            //指定したコンポーネントを追加したいときに使います
            SEAudioSource = this.gameObject.AddComponent<AudioSource>();

        }
    }

    /// <summary>
    /// SEを再生するためのメソッド
    /// 引数のAudioClipの音源をAudioSourceに再生させる
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlaySE(AudioClip audioClip)
    {
        SEAudioSource.PlayOneShot(audioClip);
    }

    public void ChangeSEVolume(float value)
    {
        SEAudioSource.volume = value;
        
    }
}
