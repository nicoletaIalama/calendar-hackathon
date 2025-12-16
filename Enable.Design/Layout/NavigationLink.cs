namespace Enable.Design.Layout
{
    public class NavigationLink(string title, string? href, bool hasSubMenu = false)
    {
        public string Title { get; } = title;
        public string? Href { get; } = href;
        public bool HasSubMenu { get; } = hasSubMenu;
        public bool SubMenuIsOpen;
        public SortedDictionary<string, NavigationSubItem>? MenuItems { get; init; }
    }

    public class NavigationSubItem
    {
        public string Href { get; init; } = default!;
        public bool OpenInNewTab { get; init; } = false;

        public NavigationSubItem(string href, bool openInNewTab = false)
        {
            Href = href;
            OpenInNewTab = openInNewTab;
        }
    }

    public class NavigationLinkComparer : IEqualityComparer<NavigationLink>
    {
        public bool Equals(NavigationLink? x, NavigationLink? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null || y is null) return false;
            return x.Title == y.Title && x.Href == y.Href;
        }

        public int GetHashCode(NavigationLink obj)
        {
            return HashCode.Combine(obj.Title, obj.Href);
        }
    }
}
