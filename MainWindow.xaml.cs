/*
 * @Author Abdoulaye Faye - Souahibou Mbouille Ndiaye - Djiby Dieng
 * Copiright DIC 3 INFORMATIQUE
 * 2018  -  2019
 * ESP SENEGAL DAKAR
 * 
 * */


using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MySql.Data.MySqlClient;

namespace SimulImpot
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySqlConnection conn;
        Impot impot = new Impot();
        private int enfants;
        private double salaire;
        private int nbreJours;
        private int conjoint;

        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                this.DateTimeLabel.Content = DateTime.Now.ToString();
            }, this.Dispatcher);
            conn = DBUtils.GetDBConnection();
            try
            {
                conn.Open();
                Console.WriteLine("Connexion réussie!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur: " + e.Message);
            }
        }

        private void CalculImpotBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!checkInputs()) return;

            double nbreParts = 0.5 * enfants;
            double bruteFiscalAnnuel = salaire * (360 / nbreJours);
            double abattement = bruteFiscalAnnuel * 0.3;
            Console.WriteLine(abattement);
               switch (conjoint)
                {
                    case 0:
                        {
                            nbreParts += 1.0;
                            break;
                        }
                    case 1:
                        {
                            nbreParts += 1.5;
                            break;
                        }
                    case 2:
                        {
                            nbreParts += 2.0;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                
                NbrePartsResult.Content = nbreParts;
                double brutFiscalAnAvant = bruteFiscalAnnuel;
                if (abattement > 900000)
                {
                    brutFiscalAnAvant -= 900000;
                }
                else
                {
                    brutFiscalAnAvant -= abattement;
                }

            Console.WriteLine(brutFiscalAnAvant);

                double irrpar = impot.calculIrppar(brutFiscalAnAvant);
            Console.WriteLine(irrpar);
                double reduction = impot.calculReduction(irrpar, nbreParts);
                double impots = (irrpar - reduction) / (360 / nbreJours);
          
            Employe emp = new Employe
            {
                nom = NomText.Text,
                prenom = PrenomText.Text,
                salaire = Convert.ToDouble(SalaireText.GetLineText(0)),
                nbreJours = Convert.ToInt32(NbreJoursText.GetLineText(0)),
                enfants = Convert.ToInt32(EnfantsText.GetLineText(0)),
                conjoint = Convert.ToInt32(ConjointText.Text),
                nbreParts = nbreParts,
                brutFiscaAnnuel = bruteFiscalAnnuel,
                brutFiscal = brutFiscalAnAvant,
                abattement = abattement,
                irpp = irrpar,
                reduction = reduction,
                impots = impots
            };

            NbrePartsResult.Content =  "Nombre de Parts:  " + nbreParts;
            BrutFiscalAnnuelResult.Content =  "Brut Fiscal Annuel:  " +  bruteFiscalAnnuel;
            BrutFiscalAbatlResult.Content = "Brut Fiscal après abattement:  " + brutFiscalAnAvant;
            AbattemmentResult.Content =  "Abattement:  " + abattement ;
            IrppResult.Content = "IRPP avant Reduction:  " +  irrpar;
            ReductionResult.Content ="Reduction:  " + reduction;
            ImpotsResult.Content ="Impots:  " +  impots;
            int rowCount = QueryData.InsertData(conn, emp);
            if (rowCount == 1) MessageBox.Show("Insertion réussie.");
            //emp.save();
            else MessageBox.Show("Problème lors de l'insertion des données dans la base de données");

            NomText.Text = String.Empty;
            PrenomText.Text = String.Empty;
            SalaireText.Text = String.Empty;
            NbreJoursText.Text = String.Empty;
            EnfantsText.Text = String.Empty;
            ConjointText.Text = String.Empty;




        }

        


            private bool checkInputs()
            {
                // Prenoms et Noms
                string vide = string.Empty;
                if (PrenomText.Text.Equals(vide))
                {
                    MessageBox.Show("Veuillez saisir un prénom");
                    return false;
                }
                if (NomText.Text.Equals(vide))
                {
                    MessageBox.Show("Veuillez saisir un nom");
                    return false;
                }
                // Salaire
                if (!double.TryParse(SalaireText.Text, out  salaire) || salaire < 0)
                {
                    MessageBox.Show("Veuillez un salaire valide");
                    return false;
                }
                // Nombre enfants
                if (!int.TryParse(EnfantsText.Text, out enfants) || enfants < 0)
                {
                    MessageBox.Show("Veuillez saisir un nombre d'enfants valide");
                    return false;
                }
                // Nombre jours
                if (!int.TryParse(NbreJoursText.Text, out nbreJours) || nbreJours <= 0 || nbreJours > 360)
                {
                    MessageBox.Show("Veuillez saisir un nombre de jours valide");
                    return false;
                }
                // Conjoint
                if (!int.TryParse(ConjointText.Text, out conjoint) || conjoint < 0 || conjoint > 2)
                {
                    MessageBox.Show("Veuillez saisir un statut de conjoint valide");
                    return false;
                }
                return true;
            }

        

        

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {

            NomText.Text = String.Empty;
            PrenomText.Text = String.Empty;
            SalaireText.Text = String.Empty;
            NbreJoursText.Text = String.Empty;
            EnfantsText.Text = String.Empty;
            ConjointText.Text = String.Empty;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
