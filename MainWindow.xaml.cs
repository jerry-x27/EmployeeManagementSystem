using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PracticeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Employee> employees = new List<Employee>();


        public MainWindow()
        {
            InitializeComponent();
            rb_hourlyPaid.IsChecked = true;
            UpdateLabels(EmployeeType.HourlyEmployee);
            SampleData();
        }
        private void SampleData()
        {
            employees.Add(new HourlyEmployee("Eshaarveer Singh", 25.3m, 45));  
            employees.Add(new HourlyEmployee("Harsimranjit Singh", 18.75m, 38)); 
            employees.Add(new CommissionEmployee("Jatin Garg", 5380, 0.1m)); 
            employees.Add(new SalariedEmployee("Divyanshu Verma", 2389)); 

            foreach (var emp in employees)
            {
                lb_employeeList.Items.Add(emp.EmployeeName);
            }
        }
        private void UpdateLabels(EmployeeType type)
        {
            tb_hoursWorked.Visibility = Visibility.Visible;
            tb_hourlyWage.Visibility = Visibility.Visible;
            lbl_hourWorked.Visibility = Visibility.Visible;
            lbl_hourlyWage.Visibility = Visibility.Visible;

            if (type == EmployeeType.CommissionEmployee)
            {
                lbl_hourWorked.Content = "Gross Sales:";
                lbl_hourlyWage.Content = "Commission Rate:";
            }
            else if (type == EmployeeType.SalariedEmployee)
            {
                lbl_hourWorked.Content = "Weekly Salary:";
                lbl_hourlyWage.Visibility = Visibility.Hidden;
                tb_hourlyWage.Visibility = Visibility.Hidden;
            }
            else
            {
                lbl_hourWorked.Content = "Hours Worked:";
                lbl_hourlyWage.Content = "Hourly Wage:";
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (rb_hourlyPaid.IsChecked == true)
            {
                UpdateLabels(EmployeeType.HourlyEmployee);
            }
            else if (rb_commissionBased.IsChecked == true)
            {
                UpdateLabels(EmployeeType.CommissionEmployee);
            }
            else
            {
                UpdateLabels(EmployeeType.SalariedEmployee);
            }
        }
        private bool validateHourlyEmployee(out decimal hoursWorked, out decimal hourlyWage)
        {
            hoursWorked = 0;
            hourlyWage = 0;

            if (tb_name.Text == "")
            {
                MessageBox.Show("Name Field Cannot Be Empty..!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!decimal.TryParse(tb_hoursWorked.Text, out hoursWorked) || hoursWorked < 0 || hoursWorked > 168)
            {
                MessageBox.Show("Invalid Hours Worked..!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!decimal.TryParse(tb_hourlyWage.Text, out hourlyWage) || hourlyWage < 0)
            {
                MessageBox.Show("Invalid Hourly Wage..!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool validateCommissionEmployee(out decimal grossSales, out decimal commRate)
        {
            grossSales = 0;
            commRate = 0;

            if (tb_name.Text == "")
            {
                MessageBox.Show("Name Field Cannot Be Empty..!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!decimal.TryParse(tb_hoursWorked.Text, out grossSales) || grossSales < 0)
            {
                MessageBox.Show("Invalid Gross Sales..!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!decimal.TryParse(tb_hourlyWage.Text, out commRate) || commRate < 0)
            {
                MessageBox.Show("Invalid Commission Rate..!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool validateSalaryEmployee(out decimal weeklySalary)
        {
            weeklySalary = 0;

            if (tb_name.Text == "")
            {
                MessageBox.Show("Name Field Cannot Be Empty..!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!decimal.TryParse(tb_hoursWorked.Text, out weeklySalary) || weeklySalary < 0)
            {
                MessageBox.Show("Invalid Weekly Salary..!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void btn_calculate_Click(object sender, RoutedEventArgs e)
        {
            Employee newEmployee = null;

            if (rb_hourlyPaid.IsChecked == true)
            {
                if (!validateHourlyEmployee(out decimal hoursWorked, out decimal hourlyWage))
                {
                    return;
                }

                newEmployee = new HourlyEmployee(tb_name.Text, hourlyWage, hoursWorked);
            }

            if (rb_commissionBased.IsChecked == true)
            {
                if (!validateCommissionEmployee(out decimal grossSales, out decimal commRate))
                {
                    return;
                }

                newEmployee = new CommissionEmployee(tb_name.Text, grossSales, commRate);
            }

            if (rb_weeklySalary.IsChecked == true)
            {
                if (!validateSalaryEmployee(out decimal weeklySalary))
                {
                    return;
                }

                newEmployee = new SalariedEmployee(tb_name.Text, weeklySalary);
            }

            employees.Add(newEmployee);
            lb_employeeList.Items.Add(newEmployee.EmployeeName);

            DisplayEmployeeDetails(newEmployee);

        }

        private void DisplayEmployeeDetails(Employee employee)
        {
            tb_grossEarnings.Text = employee.GrossEarnings.ToString("C");
            tb_tax.Text = employee.Tax.ToString("C");
            tb_netEarning.Text = employee.NetEarnings.ToString("C");
        }

        private void lb_employeeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_employeeList.SelectedItems.Count != null)
            {
                string selectedName = lb_employeeList.SelectedItem.ToString();

                Employee selectedEmployee = (from emp in employees
                                             where emp.EmployeeName == selectedName
                                             select emp).FirstOrDefault();

                if (selectedEmployee != null)
                {
                    tb_hoursWorked.Visibility = Visibility.Visible;
                    tb_hourlyWage.Visibility = Visibility.Visible;
                    lbl_hourWorked.Visibility = Visibility.Visible;
                    lbl_hourlyWage.Visibility = Visibility.Visible;

                    tb_name.Text = selectedEmployee.EmployeeName;

                    if (selectedEmployee is HourlyEmployee hourly)
                    {
                        lbl_hourWorked.Content = "Hours Worked:";
                        lbl_hourlyWage.Content = "Hourly Wage:";
                        rb_hourlyPaid.IsChecked = true;
                        tb_hoursWorked.Text = hourly.HoursWorked.ToString();
                        tb_hourlyWage.Text = hourly.HourlyWage.ToString();
                    }

                    else if (selectedEmployee is CommissionEmployee commission)
                    {
                        lbl_hourWorked.Content = "Gross Sales:";
                        lbl_hourlyWage.Content = "Commission Rate:";
                        rb_commissionBased.IsChecked = true;
                        tb_hoursWorked.Text = commission.GrossSales.ToString();
                        tb_hourlyWage.Text = commission.CommissionRate.ToString();
                    }

                    else if (selectedEmployee is SalariedEmployee salary)
                    {
                        lbl_hourWorked.Content = "Weekly Salary:";
                        lbl_hourlyWage.Visibility = Visibility.Hidden;
                        tb_hourlyWage.Visibility = Visibility.Hidden;
                        rb_weeklySalary.IsChecked = true;
                        tb_hoursWorked.Text = salary.WeeklySalary.ToString();
                    }

                    tb_grossEarnings.Text = selectedEmployee.GrossEarnings.ToString("C");
                    tb_tax.Text = selectedEmployee.Tax.ToString("C");
                    tb_netEarning.Text = selectedEmployee.NetEarnings.ToString("C");
                }
            }
        }
        private void btn_clear_Click(object sender, RoutedEventArgs e)
        {
            tb_name.Clear();
            tb_grossEarnings.Clear();
            tb_hourlyWage.Clear();
            tb_hoursWorked.Clear();
            tb_netEarning.Clear();
            tb_tax.Clear();
        }
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}