using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    public GameObject AnswerObject;
    public Transform AnswerParentTransform;

    private string _answer = "";

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        var pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var hitInfo = Physics2D.Raycast(_camera.ScreenToWorldPoint(pos), Vector2.zero);
        // RaycastHit2D can be either true or null, but has an implicit conversion to bool, so we can use it like this
        if (hitInfo)
        {
            HandleTouchOn(hitInfo.transform.gameObject);
            // Here you can check hitInfo to see which collider has been hit, and act appropriately.
        }
    }

    private void HandleTouchOn(GameObject transformGameObject)
    {
        CreateAnswerLine(transformGameObject.GetComponent<PuzzleObject>().Letter);
    }

    private void CreateAnswerLine(char c)
    {
        _answer += c;
        var go = Instantiate(AnswerObject, Vector3.zero, Quaternion.identity);
        go.GetComponent<PuzzleObject>().SetCharacter(c);
        go.transform.parent = AnswerParentTransform;
        go.transform.localPosition = Vector2.right * 0.8f * _answer.Length;
        go.transform.localScale = Vector3.one;
        AnswerParentTransform.position += Vector3.left * 0.4f;
    }
}