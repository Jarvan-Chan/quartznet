using BenchmarkDotNet.Attributes;

namespace Quartz.Benchmark;

[MemoryDiagnoser]
public class CronExpressionBenchmark
{
    [ParamsSource(nameof(DateValues))]
    public DateTime Date { get; set; }

    public IEnumerable<DateTime> DateValues => new[]
    {
        new DateTime(2005, 6, 1, 22, 15, 0),
        new DateTime(2005, 12, 31, 23, 59, 59),
    };

    [ParamsSource(nameof(CronExpressionValues))]
    public string CronExpression { get; set; } = "";

    public IEnumerable<string> CronExpressionValues => new[]
    {
        "0 15 10 6,15 * ? *",
        "0 0/5 10 6,15 * ? *",
        "0 15 10 15 * ? *",
        "0 15 10 15,31 * ? *",
        "0 15 10 6,15,LW * ? *",
        "0 15 10 6,15,L * ? *",
        "0 15 10 15,L * ? *",
        "0 15 10 15,31 * ? *",
        "0 15 10 15,L-2 * ? *",
        "0 15 10 1,3,6,15,L * ? *",
        "0 15 10 1,2,3,4,5,6,7,8,9,10,15,L * ? *",
        "0 15 10 15,LW-2 * ? *",
        "0 15 10 ? * 6#3 *"
    };

    [Benchmark]
    public void CreateExpressionsAndCalculateFireTimeAfter()
    {
        var expression = new CronExpression(CronExpression);
        expression.GetNextValidTimeAfter(Date);
    }
}