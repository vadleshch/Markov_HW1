Console.WriteLine("Hello, World!");

double[,] A = new double[,]
{
    { 0.5, 0.5, 0, 0, 0, 0 },
    { 0.5, 0, 0.5, 0, 0, 0 },
    { 0, 0.1, 0.4, 0.3, 0.2, 0 },
    { 0, 0, 0.3, 0, 0, 0.7 },
    { 0, 0, 0.2, 0.8, 0, 0 },
    { 0, 0, 0, 0.7, 0.3, 0 }
};

double[,] Lambda = new double[,]
{
    {  1/6, 1/6, 1/6, 1/6, 1/6, 1/6 },
    {  1, 0, 0, 0, 0, 0 },
    { 1/12, 1/4, 1/9, 2/9, 1/6, 1/6 }
};

int[] counter = { 0, 0, 0, 0, 0, 0 };

int NextState(int currentState)
{
    double rand = new Random().NextDouble();
    double cumulativeProbability = 0.0;
    for (int i = 0; i < A.GetLength(1); i++)
    {
        cumulativeProbability += A[currentState, i];
        if (rand < cumulativeProbability)
        {
            return i;
        }
    }
    return currentState;
}

