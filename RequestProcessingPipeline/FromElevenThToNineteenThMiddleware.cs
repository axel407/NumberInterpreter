namespace RequestProcessingPipeline
{
    public class FromElevenThToNineteenThMiddleware
    {
        private readonly RequestDelegate _next;

        public FromElevenThToNineteenThMiddleware(RequestDelegate next)
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
                if (number > 10000)
                {
                    int check = number / 1000;

                    if (check < 11 || check > 19)
                    {
                        await _next.Invoke(context);
                    }
                    else
                    {
                        string[] Numbers = { "eleven thousand", "twelve thousand", "thirteen thousand", "fourteen thousand", "fifteen thousand", "sixteen thousand", "seventeen thousand", "eighteen thousand", "nineteen thousand" };
                        context.Session.SetString("number", Numbers[check - 11]);
                        await _next.Invoke(context);
                    }
                }
                else
                    await _next.Invoke(context);
            }
            catch (Exception)
            {
                await context.Response.WriteAsync("Incorrect parameter");
            }
        }
    }
}
