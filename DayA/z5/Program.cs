int number = 1234;

int first = number / 1000;
int second = (number / 100) % 10;
int third = (number / 10) % 10;
int fourth = number % 10;

int result = second * 1000 + first * 100 + fourth * 10 + third;

Console.WriteLine(result);