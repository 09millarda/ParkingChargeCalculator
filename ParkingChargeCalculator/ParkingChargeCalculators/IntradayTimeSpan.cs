namespace ParkingChargeCalculator.ParkingChargeCalculators
{
    public class IntradayTimeSpan
    {
        public int Start { get; set; }
        public int End { get; set; }

        public IntradayTimeSpan(int start, int end)
        {
            Start = start;
            End = end;
        }
    }
}
