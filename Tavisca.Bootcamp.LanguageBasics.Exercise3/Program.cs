using System;
using System.Linq;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
   public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            // Add your code here.
            int totalMeals=protein.Length; 
            int totalDietPlans=dietPlans.Length;
            int[] calories=new int[totalMeals];
            int[] result=new int[totalDietPlans];

             for(int i=0;i<totalMeals;i++){
                calories[i]= (protein[i]*5 + carbs[i]*5 + fat[i]*9);
            }
           
           List<int> shortlistedMeals=new List<int>();//will store index of meals selected by priority
           

           for(int i=0;i<totalDietPlans;i++){
               
               shortlistedMeals.Clear();
               
               for(int k=0;k<totalMeals;k++){//adding all meals initially
                     shortlistedMeals.Add(k);
                    }
               
               String currentDietPlan=dietPlans[i];
        
               for(int j=0;j<currentDietPlan.Length;j++){
                    char nutrient=currentDietPlan[j];

                    if(shortlistedMeals.Count==1)//condition if only one meal can be selected
                    break;

                     switch(nutrient)
                        {
                             case 'P': shortlistedMeals=Maximise(protein,shortlistedMeals);
                                        break;
                             case 'p': shortlistedMeals=Minimise(protein,shortlistedMeals);
                                        break;
                             case 'C': shortlistedMeals=Maximise(carbs,shortlistedMeals);
                                        break;
                             case 'c': shortlistedMeals=Minimise(carbs,shortlistedMeals);
                                        break;
                             case 'F': shortlistedMeals=Maximise(fat,shortlistedMeals);
                                        break;
                             case 'f': shortlistedMeals=Minimise(fat,shortlistedMeals);
                                        break;
                             case 'T': shortlistedMeals=Maximise(calories,shortlistedMeals);
                                        break;
                             case 't': shortlistedMeals=Minimise(calories,shortlistedMeals);
                                        break;                      
                         }

               }

            result[i]=shortlistedMeals[0];
           } 
          
          return result;
        }


      public static List<int> Maximise(int[] nutrient,List<int> shortlistedMeals){//Find maximum of given nutreint and add that meal to list
           int max=-1;
           List<int> toReturnMeals=new List<int>();

           for(int i=0;i<shortlistedMeals.Count;i++){
               if(nutrient[shortlistedMeals[i]]>max)
                   max=nutrient[shortlistedMeals[i]];

           }
           
           for(int i=0;i<shortlistedMeals.Count;i++){

               if(nutrient[shortlistedMeals[i]]==max)
                   {
                       toReturnMeals.Add(shortlistedMeals[i]);
                   }

           }
          return toReturnMeals;   
      }


  public static List<int> Minimise(int[] nutrient,List<int> shortlistedMeals){//Find minimum of given nutreint and add that meal to list
           int min=2000;
           List<int> toReturnMeals=new List<int>();
           for(int i=0;i<shortlistedMeals.Count;i++){

               if(nutrient[shortlistedMeals[i]]<min)
                   min=nutrient[shortlistedMeals[i]];

           }
           for(int i=0;i<shortlistedMeals.Count;i++){

               if(nutrient[shortlistedMeals[i]]==min)
                   {
                       toReturnMeals.Add(shortlistedMeals[i]);
                   }

           }

          return toReturnMeals;   
      }
    }
}
