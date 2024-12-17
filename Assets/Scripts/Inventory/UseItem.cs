using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UseItem : MonoBehaviour
{
    [SerializeField] private HotBar hotBar;
    [SerializeField] private GameObject BookDisplay;
    
    public bool activated { get;private set; } = false;
    private int _currentPage = 0;
    private GameObject _currentPageDisplay;
    
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
        if (_context.ReadValue<float>() > 0 && _currentPage + 1 <  book.Texts.Length)
        {
            _currentPage ++;
            SetPage(book);
        }

        if (_context.ReadValue<float>() < 0 && _currentPage > 0)
        {
            _currentPage --;
            SetPage(book);
        }
    }

    private void SetPage(Book book)
    {
        Destroy(_currentPageDisplay);
        _currentPageDisplay = Instantiate(book.Texts[_currentPage], BookDisplay.transform);
    }
    
    private void book(Item item)
    {
        activated = !activated;
        if (activated)
        {
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
