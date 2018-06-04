using UnityEngine;

public class QuestionRepoHandler : MonoBehaviour
{
    private QuestionHandler _handler;

    private void Start()
    {
        _handler = GetComponent<QuestionHandler>();
    }
    
    
}