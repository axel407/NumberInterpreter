namespace RequestProcessingPipeline
{
    public class FromTwentyToHundredMiddleware
    {
        private readonly RequestDelegate _next;

        public FromTwentyToHundredMiddleware(RequestDelegate next)
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
                if (number < 20)
                {
                    await _next.Invoke(context);
                }
                else if(number > 100)
                {
                    await context.Response.WriteAsync("Number greater than one hundred");
                }
                else if (number == 100)
                {
                    await context.Response.WriteAsync("Your number is one hundred");
                }
                else
                {
                    number /= 10;
                    string[] Numbers = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                    context.Session.SetString("number", Numbers[number - 2]);
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
