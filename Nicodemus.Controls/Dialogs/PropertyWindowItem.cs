namespace Nicodemus.Controls.Dialogs
{
    public class PropertyWindowItem
    {
        public string Title { get; set; }
        public string Text { get; set; }

        public PropertyWindowItem() { }

        public PropertyWindowItem(string title, string text)
        {
            Title = title;
            Text = text;
        }
    }
}
