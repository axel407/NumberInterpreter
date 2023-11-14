namespace RequestProcessingPipeline
{
    public static class FromThousandToTenExtension
    {
        public static IApplicationBuilder UseFromThousandToTen(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FromThousandToTenMiddleware>();
        }
    }
}
