namespace RequestProcessingPipeline
{
    public static class FromTwentyThToHundredThExtensions
    {
        public static IApplicationBuilder UseFromTwentyThToHundredTh(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromTwentyThToHundredTh>();
        }
    }
}
