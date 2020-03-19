namespace Demo.Model
{
    public class TextValueModel
    {
        public TextValueModel()
        { }

        public TextValueModel(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public string Value { get; set; }
    }
}