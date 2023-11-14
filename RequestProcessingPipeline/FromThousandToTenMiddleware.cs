namespace RequestProcessingPipeline
{
    public class FromThousandToTenMiddleware
    {
        private readonly RequestDelegate _next;

        public FromThousandToTenMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string? token = context.Request.Query["number"];
            try
            {
                int number = Convert.ToInt32(token);
                number = Math.Abs(number);
                if (number < 1000 || number > 10999)
                {
                    await _next.Invoke(context);
                }
                else
                {
                    number /= 1000;
                    string[] Numbers = { "one thousand", "two thousand", "three thousand", "four thousand", "five thousand", "six thousand", "seven thousand", "eight thousand", "nine thousand", "ten thousand" };
                    context.Session.SetString("number", Numbers[number - 1]);
                    await _next.Invoke(context);
                }

            }
            catch (Exception)
            {
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}
