    public static class StringViewModel
    {
        public static int CountCharacters(string text)
        {
            return text.Count(x => !char.IsWhiteSpace(x));
        }
    }
