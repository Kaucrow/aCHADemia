namespace aCHADemia.Core.Classes
{
    public class NavigationContext
    {
        public object Parameter { get; }
        public bool Cancel { get; set; }

        public NavigationContext(object parameter)
        {
            Parameter = parameter;
        }
    }
}
