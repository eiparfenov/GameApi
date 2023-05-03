using System.Reflection;

namespace GameApi.Shared;

public class PropertyProvider<T>
{
    private object _item;
    private PropertyInfo _prop;

    public PropertyProvider(object item, PropertyInfo prop)
    {
        _item = item;
        _prop = prop;
    }

    public T Value
    {
        get => (T) _prop.GetValue(_item);
        set => _prop.SetValue(_item, value);
    }

    public string Name => _prop.Name;
}