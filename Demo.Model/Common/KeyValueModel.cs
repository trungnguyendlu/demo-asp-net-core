namespace Demo.Model
{
    public class KeyValueModel
    {
        public KeyValueModel()
        { }

        public KeyValueModel(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}