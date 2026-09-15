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
    { 1.0/6, 1.0/6, 1.0/6, 1.0/6, 1.0/6, 1.0/6 },
    { 1, 0, 0, 0, 0, 0 },
    { 1.0/12, 1.0/4, 1.0/9, 2.0/9, 1.0/6, 1.0/6 }
};

int NextState(int currentState)
{
    double rand = Random.Shared.NextDouble();
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

int GetFirstState(int distribution)
{
    double rand = Random.Shared.NextDouble();
    double cumulativeProbability = 0.0;

    for (int i = 0; i < Lambda.GetLength(1); i++)
    {
        cumulativeProbability += Lambda[distribution, i];

        if (rand < cumulativeProbability)
        {
            return i;
        }
    }

    return Lambda.GetLength(1) - 1;
}

int numberOfRuns = 100;
int numberOfSteps = 1000;

for (int i = 0; i < 3; i++)
{
    double[,] rho = new double[numberOfSteps, 6];

    for (int j = 0; j < numberOfRuns; j++)
    {
        int[] counter = { 0, 0, 0, 0, 0, 0 };
        int currentState = GetFirstState(i);

        for (int k = 0; k < numberOfSteps; k++)
        {
            currentState = NextState(currentState);
            counter[currentState]++;

            for (int state = 0; state < 6; state++)
            {
                rho[k, state] += (double)counter[state] / (k + 1);
            }
        }
    }

    for (int k = 0; k < numberOfSteps; k++)
    {
        for (int state = 0; state < 6; state++)
        {
            rho[k, state] /= numberOfRuns;
        }
    }

    WriteResults(rho, $"rho_{i + 1}.txt");
}

void WriteResults(double[,] rho, string fileName)
{
    using StreamWriter writer = new StreamWriter(fileName);
    for (int m = 0; m < rho.GetLength(0); m++)
    {
        writer.Write($"{m + 1}");
        for (int state = 0; state < rho.GetLength(1); state++)
        {
            writer.Write($"\t{rho[m, state]:F6}");
        }
        writer.WriteLine();
    }
}