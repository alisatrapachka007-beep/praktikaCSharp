int number = 123;

int firstDigit = number / 100;
int lastDigit = number % 10;
int product = firstDigit * lastDigit;

Console.WriteLine(product);