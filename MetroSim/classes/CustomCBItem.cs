namespace MetroSim
{
    class CustomCBItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public CustomCBItem(string text, string value)
        {
            this.Text = text;
            this.Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
