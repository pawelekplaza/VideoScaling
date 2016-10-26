namespace VideoScaling
{
    public interface ISwitchable
    {
        void UtilizeState(object state);
        string Name { get; set; }
    }
}
