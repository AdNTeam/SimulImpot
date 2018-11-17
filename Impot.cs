using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulImpot
{
    class Impot
    {
        public double calculReduction(double irrpar, double nombreParts)
        {
            double reduction = 0.0;
            switch (nombreParts)
            {
                case 1:
                    {
                        reduction = 0;
                        break;
                    }
                case 1.5:
                    {
                        reduction = NormaliserReduction(irrpar, 0.1, 100000, 300000);
                        break;
                    }
                case 2:
                    {
                        reduction = NormaliserReduction(irrpar, 0.15, 200000, 650000);
                        break;
                    }
                case 2.5:
                    {
                        reduction = NormaliserReduction(irrpar, 0.2, 300000, 1100000);
                        break;
                    }
                case 3:
                    {
                        reduction = NormaliserReduction(irrpar, 0.25, 400000, 1650000);
                        break;
                    }
                case 3.5:
                    {
                        reduction = NormaliserReduction(irrpar, 0.3, 500000, 2030000);
                        break;
                    }
                case 4:
                    {
                        reduction = NormaliserReduction(irrpar, 0.35, 600000, 2490000);
                        break;
                    }
                case 4.5:
                    {
                        reduction = NormaliserReduction(irrpar, 0.4, 700000, 2755000);
                        break;
                    }
                case 5:
                    {
                        reduction = NormaliserReduction(irrpar, 0.45, 800000, 3180000);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            return reduction;
        }

        public double NormaliserReduction(double irrpar, double taux, double min, double max)
        {
            double resultat = 0.0;
            double temp = irrpar * taux;
            if (temp < min) resultat = min;
            else if (temp > max) resultat = max;
            else resultat = temp;
            return resultat;
        }

        public double calculIrppar(double bfaa)
        {
            double irppar;
            if (bfaa < 630000)
            {
                irppar = 0;
            }
            else if (bfaa > 630000 && bfaa <= 1500000)
            {
                irppar = (bfaa - 630000) * 0.2;
            }
            else if (bfaa > 1500000 && bfaa <= 4000000)
            {
                irppar = 174000 + (bfaa - 1500000) * 0.3;
            }
            else if (bfaa > 4000000 && bfaa <= 8000000)
            {
                irppar = 174000 + 750000 + (bfaa - 4000000) * 0.35;
            }
            else if (bfaa > 8000000 && bfaa <= 13500000)
            {
                irppar = 174000 + 750000 + 1400000 + (bfaa - 8000000) * 0.37;
            }
            else if (bfaa > 13500000 && bfaa <= 1000000000)
            {
                irppar = 174000 + 750000 + 1400000 + 2035000 + (bfaa - 13500000) * 0.4;
            }
            else
            {
                irppar = 398959000;
            }
            return irppar;
        }
    }
}
