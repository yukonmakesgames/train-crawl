[System.Serializable]
public class Element
{
    public ElementController ElementController;
    public bool Closed = false;

    public Element(ElementController _elementController)
    {
        ElementController = _elementController;
    }
}