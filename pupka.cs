using Microsoft.Maui.Controls;

namespace Kallculator2
{
public partial class MainPage : ContentPage
{
string currentNumber = "0";
string selectedOperator;
double firstNumber, secondNumber;
bool isFirstNumber = true;

public MainPage()
{
InitializeComponent();
}

void OnNumberButtonClicked(object sender, EventArgs e)
{
Button button = (Button)sender;
string pressed = button.Text;

if (currentNumber == "0" || isFirstNumber)
{
currentNumber = "";
isFirstNumber = false;
}

currentNumber += pressed;
resultLabel.Text = currentNumber;
}

void OnOperatorButtonClicked(object sender, EventArgs e)
{
Button button = (Button)sender;
selectedOperator = button.Text;

firstNumber = double.Parse(currentNumber);
isFirstNumber = true;
}

void OnEqualsButtonClicked(object sender, EventArgs e)
{
secondNumber = double.Parse(currentNumber);

double result = 0;
switch (selectedOperator)
{
case "+":
result = firstNumber + secondNumber;
break;
case "-":
result = firstNumber - secondNumber;
break;
case "*":
result = firstNumber * secondNumber;
break;
case "/":
if (secondNumber != 0)
{
result = firstNumber / secondNumber;
}
else
{
resultLabel.Text = "Error";
}
break;
}

resultLabel.Text = result.ToString();
currentNumber = result.ToString();
isFirstNumber = true;
}

void OnClearButtonClicked(object sender, EventArgs e)
{
currentNumber = "0";
selectedOperator = "";
firstNumber = 0;
secondNumber = 0;
isFirstNumber = true;
resultLabel.Text = "0";
}
}
}