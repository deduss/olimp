using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Olimp.Models;

public class CustomSelectList : IEnumerable<SelectListItem>
{
    private IEnumerable<SelectListItem> _enumerableImplementation;

    private CustomSelectList(IEnumerable<SelectListItem> enumerableImplementation)
    {
        _enumerableImplementation = enumerableImplementation;
    }

    public IEnumerator<SelectListItem> GetEnumerator()
    {
        return _enumerableImplementation.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_enumerableImplementation).GetEnumerator();
    }

    public static CustomSelectList Create<T>(IEnumerable<T> enumerable, Func<T, string> dataValueSelector,
        Func<T, string> dataTextSelector)
    {
        var result = enumerable
            .Select(arg => new SelectListItem(dataTextSelector(arg), dataValueSelector(arg)));

        return new CustomSelectList(result);
    }
}