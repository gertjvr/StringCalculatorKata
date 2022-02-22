namespace StringCalculator.Xunit.SpecFor.UnitTests
{
    public abstract class CalculatorSpecFor : SpecFor<Calculator>
    {
        protected string Numbers { get; set; } = string.Empty;

        protected int Expected { get; set; }

        protected int Result { get; set; }
    }
}