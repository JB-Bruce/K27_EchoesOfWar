using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UseItem : MonoBehaviour
{
    [SerializeField] private HotBar hotBar;
    [SerializeField] private GameObject BookDisplay;
    [SerializeField] private TextMeshProUGUI LeftPage;
    [SerializeField] private TextMeshProUGUI RightPage;
    
    public bool activated { get;private set; } = false;
    private int _currentPage = 0;
    
    public void Use(Item item)
    {
        if (item._hideInventory)
        {
            hotBar.gameObject.SetActive(!hotBar.gameObject.activeSelf);
        }

        if (item is Book)
        {
            book(item);
        }
    }

    public void TurnPages(InputAction.CallbackContext  _context, Book book)
    {
        if (_context.ReadValue<float>() > 0 && _currentPage+2 <  book.Texts.Length)
        {
            _currentPage += 2;
            SetPage(book);
        }

        if (_context.ReadValue<float>() < 0 && _currentPage > 0)
        {
            _currentPage -= 2;
            SetPage(book);
        }
    }

    private void SetPage(Book book)
    {
        LeftPage.text = book.Texts[_currentPage].text;
        RightPage.text = _currentPage + 1 < book.Texts.Length ? book.Texts[_currentPage + 1].text : "";
    }
    
    private void book(Item item)
    {
        activated = !activated;
        if (activated)
        {
            gameObject.GetComponent<TextMeshProUGUI>().SetText("");
            BookDisplay.SetActive(true);
            SetPage(item as Book);
        }
        else
        {
            _currentPage = 0;
            BookDisplay.SetActive(false);
            gameObject.GetComponent<TextMeshProUGUI>().SetText(item._name);
        }
    }
}
