using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CreateDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            var listWithFoods = new StringBuilder();
            using(StreamReader reader=new StreamReader("listWithFoods.txt"))
            {
                int count=0;
                while (true)
                {
                //new Salad { Name = "Mixed Salad", Calories = 80, Carbohydrates = 40, Proteins = 40, Fats = 20, 
                //Portion_Size = 250, Calories_Per_Portions = 0, Ingredients = "vegetables"},
                    line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    string[] bufferLine = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string resultLine = String.Format("new {8} {{ Name = \"{0}\", Calories = {1}m, Carbohydrates = {2}m, Proteins = {3}m, Fats = {4}m, Portion_Size = {5}m, Calories_Per_Portions = {6}m, Ingredients = \"{7}\" }},"
                        , bufferLine[0], bufferLine[1], bufferLine[2], bufferLine[3], bufferLine[4], bufferLine[5], bufferLine[6], bufferLine[7],bufferLine[8]);
                    listWithFoods.AppendLine(resultLine);
                }
                Console.WriteLine(listWithFoods.ToString());

                using (StreamWriter writer =new StreamWriter("resultList.txt"))
                {
                    writer.Write(listWithFoods);
                }
            }
        }
    }
}
