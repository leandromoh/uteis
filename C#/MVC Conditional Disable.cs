public static MvcHtmlString Disable(this MvcHtmlString helper, bool disabled)
{
    if (helper == null)
        throw new ArgumentNullException();

    if (disabled)
    {
        string html = helper.ToString();
        int startIndex = html.IndexOf('>');

        html = html.Insert(startIndex, " disabled=\"disabled\"");
        return MvcHtmlString.Create(html);
    }

    return helper;
}
