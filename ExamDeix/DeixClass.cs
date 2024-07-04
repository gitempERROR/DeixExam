namespace ExamDeix
{
    internal class DeixClass
    {
        public double[,] Matrix { get; set; }
        public double[] PointsPath { get; set; }
        public double[] Anwser { get; set; }

        public DeixClass(string filePath) 
        { 
            using var reader = new StreamReader(filePath);
            int startID = 0;
            while (reader.Peek() != -1)
            {
                string[] line = [.. reader.ReadLine().Split(" ")];
                Matrix ??= new double[line.Length, line.Length];
                for (int i = 0; i < line.Length; i++) Matrix[startID, i] = double.Parse(line[i]);
                startID++;
            }
            reader.Close();
        }

        public void FindMin(ref double value, ref int id)
        {
            double minValue = double.MaxValue;
            for (int i = 0; i < PointsPath.Length; i++)
            {
                if (PointsPath[i] < minValue && PointsPath[i] != -1)
                {
                    id = i;
                    minValue = PointsPath[i];
                }
            }
            value = minValue;
        }

        public void CalculateMinPathes()
        {
            PointsPath = new double[Matrix.GetLength(0)];
            for (int i = 1; i < PointsPath.Length; i++) PointsPath[i] = double.MaxValue;
            Anwser = (double[])PointsPath.Clone();
            for (int i = 0; i < PointsPath.Length; i++)
            {
                int CurrentID = 0;
                double Value = 0;
                FindMin(ref Value, ref CurrentID);
                for (int j = 0; j < PointsPath.Length; j++)
                {
                    if (Matrix[CurrentID, j] != 0 && PointsPath[j] > PointsPath[CurrentID] + Matrix[CurrentID, j])
                    {
                        PointsPath[j] = PointsPath[CurrentID] + Matrix[CurrentID, j];
                        Anwser[j] = PointsPath[CurrentID] + Matrix[CurrentID, j];
                    }
                }
                PointsPath[CurrentID] = -1;
            }
        }

        public void PrintToFile(string filePath) 
        {
            string output = "";
            using var writer = new StreamWriter(filePath);
            for (int i = 0; i < Anwser.Length; i++)
            {
                output += $"Путь в точку {i} равен {Anwser[i]}\n";
            }
            writer.Write(output);
            writer.Close();
        }

        public void FindMinPathes(string filePath)
        {
            CalculateMinPathes();
            PrintToFile(filePath);
        }
    }
}
