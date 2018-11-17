using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulImpot
{
    class QueryData
    {
        public static int InsertData(MySqlConnection conn, Employe emp)
        {
            string sql = "Insert into employe(prenom, nom, salaire, nbreJours, conjoint, enfants,brutFiscalAnnuel,brutFiscal,abattement,irpp,nbreParts,reduction,impots) " +
                            "values(@Prenom, @Nom, @Salaire, @NombreJours, @Conjoint, @Enfants,@BrutFiscalAnnuel,@BrutFiscalAnAvant,@Abattement,@Irpp,@NbreParts,@Reduction,@Impots)";

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;

            cmd.Parameters.Add("@Prenom", MySqlDbType.String).Value = emp.prenom;
            cmd.Parameters.Add("@Nom", MySqlDbType.String).Value = emp.nom;
            cmd.Parameters.Add("@Salaire", MySqlDbType.Decimal).Value = emp.salaire;
            cmd.Parameters.Add("@NombreJours", MySqlDbType.Int32).Value = emp.nbreJours;
            cmd.Parameters.Add("@Conjoint", MySqlDbType.String).Value = emp.conjoint;
            cmd.Parameters.Add("@Enfants", MySqlDbType.Int32).Value = emp.enfants;
            cmd.Parameters.Add("@BrutFiscalAnnuel", MySqlDbType.Int32).Value = emp.brutFiscaAnnuel;
            cmd.Parameters.Add("@BrutFiscalAnAvant", MySqlDbType.Int32).Value = emp.brutFiscal;
            cmd.Parameters.Add("@Abattement", MySqlDbType.Int32).Value = emp.abattement;
            cmd.Parameters.Add("@Irpp", MySqlDbType.Int32).Value = emp.irpp;
            cmd.Parameters.Add("@NbreParts", MySqlDbType.Int32).Value = emp.nbreParts;
            cmd.Parameters.Add("@Reduction", MySqlDbType.Int32).Value = emp.reduction;
            cmd.Parameters.Add("@Impots", MySqlDbType.Int32).Value = emp.impots;

            int rowCount = cmd.ExecuteNonQuery();
            return rowCount;
        }
    }
}
