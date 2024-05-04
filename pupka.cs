using Microsoft.Maui.Controls;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CalculatorApp
{
    public class CalculatorDbContext : DbContext
    {
        public DbSet<CalculationHistory> History { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=calculator.db");
        }
    }
}
{
    public class CalculationHistory
    {
        public int Id { get; set; }
        public string Calculation { get; set; }
        public double Result { get; set; }
    }
}
{
    public partial class MainPage : ContentPage
    {
        private string currentNumber = "0";
        private string selectedOperator;
        private double firstNumber, secondNumber;
        private bool isFirstNumber = true;
        
        public MainPage()
        {
            InitializeComponent();
            LoadCalculationHistory();
        }

        private void SaveCalculationHistory(string calculation, double result)
        {
            using (var db = new CalculatorDbContext())
            {
                db.History.Add(new CalculationHistory { Calculation = calculation, Result = result });
                db.SaveChanges();
            }
        }

        private void OnEqualsButtonClicked(object sender, EventArgs e)
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

            SaveCalculationHistory($"{firstNumber} {selectedOperator} {secondNumber} = {result}", result);
        }

        private void LoadCalculationHistory()
        {
            using (var db = new CalculatorDbContext())
            {
                var history = db.History.ToList();
            }
        }

    }
}