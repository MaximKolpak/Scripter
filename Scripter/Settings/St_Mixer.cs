namespace Scripter.Settings
{
    public class St_Mixer
    {
        public string IpAdress { get; set; } //ipdaress
        public bool ePort { get; set; } //enable port
        public int Port { get; set; } //port
        public bool eInterval { get; set; } //true
        public int Interval { get; set; } //ms Connect
        public int CheckInterval { get; set; }//ms Connect Checked for Mixer
        public int ThreadSleepConnection { get; set; } //ms Sleep Thread For Check Connection Mixer
    }
}