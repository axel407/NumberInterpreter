namespace RequestProcessingPipeline
{
    public class FromTwentyThToHundredTh
    {
        private readonly RequestDelegate _next;

        public FromTwentyThToHundredTh(RequestDelegate next)
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
                if (number < 20000)
                {
                    await _next.Invoke(context);
                }
                else if(number == 100000)
                {
                    await context.Response.WriteAsync("Your number is one hundred thousand");
                }
                else
                {
                    number /= 10000;
                    string[] Numbers = { "twenty thousand", "thirty thousand", "fourty thousand", "fifty thousand", "sixty thousand", "seventy thousand", "eighty thousand", "ninety thousand"};
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
