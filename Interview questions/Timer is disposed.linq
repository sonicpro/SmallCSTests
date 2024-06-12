<Query Kind="Program">
  <Namespace>System.Timers</Namespace>
</Query>

void Main()
{
    using (var _timer = new System.Timers.Timer())
    {
        _timer.Interval = 12;
        _timer.Elapsed += _timer_Elapsed;
        _timer.Start();
        Thread.Sleep(100);
    }
    
}

// Nothing is output to the console. (We need not to dispose the timer until at least some Elapsed event fire.
private void _timer_Elapsed(object sender, ElapsedEventArgs e)
{
    // The line is not printing anything because the format string "D" is invalid for TimeSpan.TotalMilliseconds:double value.
    Console.WriteLine($"Bingo! Ms scheduled:{TimeSpan.FromHours(24).TotalMilliseconds:D}, max value: {double.MaxValue:G}.");
}