using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalHierarchy
{
	internal class Program
	{
		static void Main(string[] args)
		{
			/* hey, i did this already! just with shapes... i'd love to explain again but it'd be a copy paste of week7. so instead i'll just give the gist:
			 * - you can inherit Class1 as Class2 by formatting your class like "Class2 : Class1"
			 * - all properties, variables, etc. are shared from the parent, and can be modified manually.
			 * - children can have their own properties/variables too
			 * - you can share functions, and those functions can be modified by having your parent use virtual and your child use override in the functions
			 * - children can act as their parent classes if cast to, and can be cast back as themselves if need be.
			 * - specific functions can be used only if you cast them properly. we will make an example out of this with our Bird class and using "is"
			 * might've missed something constructor wise, but this should be more than enough. let's not have another text wall (whoops)
			 */ 

			// add six animals to a new list. we'll do the alt method here since it will complain otherwise
			List<Animal> aniList = new List<Animal>{new Dog("Hunter",7),new Dog("Biscuit",5),new Cat("Loki",2),new Cat("Whiskers",5),new Bird("Tweet",4)};

			// now they will all list their loops
			foreach (Animal animal in aniList){
				Console.WriteLine(animal.name+" ("+animal.age.ToString()+") says "+animal.MakeSound());
				if (animal is Bird){
					var bird = (Bird)animal;
					Console.WriteLine(bird.name+" ("+bird.age.ToString()+") also says "+bird.CooSound());
				}
			}

			Console.ReadKey(); // wait for input
		}

		public class Animal{
			public string name {get;set;}
			public int age {get;set;}
			public Animal(string newName,int newAge){
				name = newName;
				age = newAge;
			}
			public virtual string MakeSound(){
				return "...........";
			}
		}

		public class Dog : Animal{
			public Dog(string newName,int newAge) : base(newName,newAge){
				name = newName;
				age = newAge;
			}
			public override string MakeSound(){
				return "woof";
			}
		}

		public class Cat : Animal{
			public Cat(string newName,int newAge) : base(newName,newAge){
				name = newName;
				age = newAge;
			}
			public override string MakeSound(){
				return "meow";
			}
		}

		public class Bird : Animal{
			public Bird(string newName,int newAge) : base(newName,newAge){
				name = newName;
				age = newAge;
			}
			public override string MakeSound(){
				return "chirp";
			}

			public string CooSound(){
				return "coo";
			}
		}
	}
}