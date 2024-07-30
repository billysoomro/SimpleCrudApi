namespace SimpleCrudApi.Utilities
{
    public static class EnvironmentChecker
    {
        public static bool IsRunningInLambda() => bool.TryParse(Environment.GetEnvironmentVariable("RUNNING_IN_LAMBDA_FUNCTION"), out _);
    }
}
