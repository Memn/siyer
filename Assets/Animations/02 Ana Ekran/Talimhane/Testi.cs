using UnityEngine;

public class Testi : MonoBehaviour
{
    private Animator _animator;


    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ShakeAnimation()
    {
        GetComponent<AudioSource>().Play();
        _animator.SetTrigger("sallanma");
    }
    
}