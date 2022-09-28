// See https://aka.ms/new-console-template for more information
int numberCount = 10000;
Console.Write("Введiть значення параметра лямбда: ");
double lambda = Convert.ToDouble(Console.ReadLine());
double sum = 0;
double avg, disp;

Random random = new ();
List<double> genNumbers = new ();
int[] intervalNumsCount = new int[]{0,0,0,0,0,0,0,0,0,0};
int[] intervalTheorNumsCount = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0,0 };

List<double> intervalEnds = new() {
    0.25/lambda, 0.5/lambda, 0.75/lambda, 1/lambda, 1.25/lambda, 1.5/lambda,
    1.75/lambda, 2/lambda, 2.25/lambda, double.PositiveInfinity
};


//numbers generating and counting frequency for intervals
for (int i = 0; i < numberCount; i++) 
{
    double genNumber=random.NextDouble();
    double x = (-1 / lambda) * Math.Log(genNumber);
    genNumbers.Add(x);
    sum += x;
    for (int j = 0; j < intervalEnds.Count; j++)
    {
        if (x < intervalEnds[j])
        {   //if number is set in selected interval
            intervalNumsCount[j]++;
            break;   //for not adding count to intervals with bigger interval end 
        }
    }
}

Console.WriteLine("Частоти потрапляння у iнтервали: ");
foreach (var item in intervalNumsCount)
{
    Console.WriteLine(item);
}

//count theoretical frequencies on intervals
for (int i = 0; i < intervalTheorNumsCount.Length; i++)
{
    if (i == 0)
    {
        intervalTheorNumsCount[i] = Convert.ToInt32(numberCount * (1 - Math.Pow(Math.E, -lambda * intervalEnds[i]) - 1 + Math.Pow(Math.E, -lambda * 0)));
    }
    else
    {
        intervalTheorNumsCount[i] = Convert.ToInt32(numberCount * (1 - Math.Pow(Math.E, -lambda * intervalEnds[i]) - 1 + Math.Pow(Math.E, -lambda * intervalEnds[i - 1])));
    }
}

Console.WriteLine("Теоретичнi частоти потрапляння у iнтервали: ");
foreach (var item in intervalTheorNumsCount)
{
    Console.WriteLine(item);
}



//count average and disperce
avg = sum / numberCount;
double dispSum = 0;
for (int i = 0; i < numberCount; i++)
{
    dispSum += Math.Pow(genNumbers[i] - avg, 2);
}
disp = dispSum / numberCount;


//counting Xi^2
double criter = 0;
for (int i = 0; i <intervalNumsCount.Length ; i++)
{
    criter += Math.Pow(intervalNumsCount[i] - intervalTheorNumsCount[i], 2) / (double)intervalTheorNumsCount[i];
}

Console.WriteLine($"Середнє значення елементiв вибiрки: {avg}");
Console.WriteLine($"Значення дисперсii елементiв вибiрки: {disp}");

Console.WriteLine($"Значення критерiю  -  {criter}");

//кількість ступенів свободи у нас 9-1=8, при альфа 0.05 критерій дорівнює 17,53

if (criter < 17.53)
{
    Console.WriteLine("З довiрчою ймовiрнiстю 0,95 наш розподiл вiдповiдає експоненцiйному розподiлу");
}
else {
    Console.WriteLine("З довiрчою ймовiрнiстю 0,95 наш розподiл не вiдповiдає експоненцiйному розподiлу");
}