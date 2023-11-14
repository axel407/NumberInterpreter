namespace RequestProcessingPipeline
{
    public static class FromElevenThToNineteenThExtensions
    {
        public static IApplicationBuilder UseFromElevenThToNineteenTh(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromElevenThToNineteenThMiddleware>();
        }
    }
}
