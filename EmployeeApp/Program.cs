using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeApp
{
	internal class Program
	{
		public static List<Employee> employees;
		static void Main(string[] args)
		{
			/* another simple program, so i will once again lay out my thought process in comments
			 * we will have a base Employee class acting as a parent. we will then make a few children that are a little more specialized
			 * by default the Employee will have Name, Title, and Salary. more will be listed out as needed.
			 * time to put them all into a list and see what happens
			 */
			employees = new List<Employee>();

			employees.Add(new Manager("Fraser Tucker","Founder of Tucker Tech",5500));
			employees.Add(new Manager("Daniel Blake","Head of IT",4500));
			employees.Add(new Engineer("Beth Barr","Hardware Engineer",3300,289));
			employees.Add(new Engineer("Lottie Cunningham","IT",4000,127));
			employees.Add(new Engineer("Kelvin Marks","Fix-It-All",2500,1051));
			employees.Add(new Employee("Sebastian Carney","Janitor",1800));
			employees.Add(new Employee("Ruqayyah Mann","Janitor",1800));
			employees.Add(new Employee("Tori Kirby","One Two Oatmeal",1600));
			employees.Add(new Employee("Haider Michael","Office Worker",1800));
			employees.Add(new Employee("Ian Gentry","Janitor",1400));

			foreach (Employee employee in employees){
				employee.CalcAnnualSalary();
				Console.WriteLine(employee.Name+" | "+employee.Title);
				switch (employee){
					case Manager m:
						Console.WriteLine("Manager employee bonus (monthly): $"+(employees.Count*100).ToString());
					break;
					case Engineer e:
						Console.WriteLine("Repairs done this year: "+e.RepairsDone.ToString()+" | Total bonus this year: $"+(e.RepairsDone*20).ToString());
					break;
				}
				Console.WriteLine("Monthly / Annual Salary: $"+employee.MonthlySalary.ToString()+" / $"+employee.AnnualSalary.ToString());
				Console.WriteLine();
			}

			Console.ReadKey();
		}
		


		public class Employee{
			public Employee(string name,string title,int salary){
				Name = name;
				Title = title;
				MonthlySalary = salary;
			}
			public virtual void CalcAnnualSalary(){
				AnnualSalary = MonthlySalary*12;
			}
			public string Name {get;set;}
			public string Title {get;set;}
			public int MonthlySalary {get;set;}

			public int AnnualSalary {get;set;}
		}

		public class Manager : Employee{
			public Manager(string name, string title, int salary) : base(name,title,salary){
				Name = name;
				Title = title;
				MonthlySalary = salary;
			}
			public override void CalcAnnualSalary(){
				AnnualSalary = (MonthlySalary+(employees.Count*100))*12;
			}
		}

		public class Engineer : Employee{
			public int RepairsDone {get;set;}
			public Engineer(string name, string title, int salary, int repairs) : base(name,title,salary){
				Name = name;
				Title = title;
				MonthlySalary = salary;
				RepairsDone = repairs;
			}

			public override void CalcAnnualSalary(){
				AnnualSalary = ((MonthlySalary)*12)+(RepairsDone*20);
			}
		}
	}
}
