namespace Tiffi.Tools.GURPSSpace
{
    internal class DiceRoller(int seed)
    {
        public readonly Random random = new(seed);

        public int LastNaturalValue { get; private set; }

        public int Roll(int amountOfDice, int diceSides)
        {
            var rollResult = 0;
            for(var i = 0; i < amountOfDice; i++)
            {
                rollResult += Roll(diceSides);
            }
            LastNaturalValue = rollResult;
            return rollResult;
        }

        public int Roll(int diceSides) => random.Next(diceSides) + 1;

        public int Roll(int amountOfDice, int diceSides, int modifier) => Roll(amountOfDice, diceSides) + modifier;

        //from https://stackoverflow.com/a/1034605
        public int Roll(string diceFormula)
        {
            int t = 0;

            // Addition is lowest order of precedence
            var a = diceFormula.Split('+');

            // Add results of each group
            if (a.Length > 1)
            {
                foreach (var b in a)
                {
                    t += Roll(b);
                }
            }
            else
            {
                // Multiplication is next order of precedence
                var m = a[0].Split('*');

                // Multiply results of each group
                if (m.Length > 1)
                {
                    t = 1; // So that we don't zero-out our results...

                    foreach (var n in m)
                        t *= Roll(n);
                }
                else
                {
                    // Die definition is our highest order of precedence
                    var d = m[0].Split('d');

                    // This operand will be our die count, static digits, or else something we don't understand
                    if (!int.TryParse(d[0].Trim(), out t))
                        t = 0;


                    // Multiple definitions ("2d6d8") iterate through left-to-right: (2d6)d8
                    for (int i = 1; i < d.Length; i++)
                    {
                        // If we don't have a right side (face count), assume 6
                        if (!int.TryParse(d[i].Trim(), out int f))
                            f = 6;

                        int u = 0;

                        // If we don't have a die count, use 1
                        for (int j = 0; j < (t == 0 ? 1 : t); j++)
                            u += random.Next(1, f);

                        t += u;
                    }
                }
            }

            return t;
        }
    }
}
