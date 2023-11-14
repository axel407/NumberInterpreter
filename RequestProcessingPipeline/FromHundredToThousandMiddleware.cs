namespace RequestProcessingPipeline
{
    public class FromHundredToThousandMiddleware
    {
        private readonly RequestDelegate _next;
        //Так приятно снова вернутся на С#
        public FromHundredToThousandMiddleware(RequestDelegate next)
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
                if (number < 100 || number > 1000)
                {
                    await _next.Invoke(context);
                }
                else if(number == 1000)
                {
                    await context.Response.WriteAsync("Your number is one thousand");
                }
                else
                {
                    number /= 100;
                    string[] Numbers = { "one hundred", "two hundred", "three hundred", "four hundred", "five hundred", "six hundred", "seven hundred", "eight hundred", "nine hundred" };
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
