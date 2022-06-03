using Cotur.DataMining.Association; //Package Sources would be available on demand of Resource Person
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataMiningAssignment
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("_______Apriori Assignment_______\n");
            Console.WriteLine("Subject: Data Mining");
            Console.WriteLine("RP: Rabia Naseer Rao\n");
            Console.WriteLine("Submitted by: ");
            Console.WriteLine("Faseeh Ahmed [F2020052005]-MIT");
            Console.WriteLine("Zohaib Hussain [F2020052006]-MIT");
            Console.WriteLine("Iqra Azam [F2020052001]-MIT");
            Console.WriteLine("M Waqas Amin [F2020052016]-MIT");

            Apriori MyAp = LoadData(); //Function returns AprioriClass Type object by loading it trhough file

            //support value should be suitable according to transaction data, exception may occur
            ProcessAP(MyAp, 0.25f); //Processing Apriori and printing it to console, minimum support value 

            Console.ReadLine();// to make console stay after printing data
        }

        public static void ProcessAP(Apriori ap, float minSupp)
        {
            ap.CalculateCNodes(minSupp); //Calculation of Nodes (Pair subsets) according to min support

            int tbl = 1; //Table Counter

            foreach(var Levels in ap.EachLevelOfNodes) //Prints Each Level of Apriori
            {
                Console.WriteLine("\n\n______Table "+tbl+" _______", tbl++);//Table Header and incrementer

                Console.Write("\nFieldnames:"); //fetched data fileds
                for (int i = 0; i < ap.Data.FieldNames.Count; i++)
                {
                    Console.Write(ap.Data.FieldNames[i]);
                    if (i != ap.Data.FieldNames.Count - 1) Console.Write(",");
                    else Console.Write("\t");
                }

                foreach (CNode node in Levels)//Data fild names (the subset which have been approved by min supp) -Ref1
                {
                    Console.Write("\nElelent IDs: ");
                    for(int i=0; i< node.ElementIDs.Count; i++)
                    {
                        Console.Write(node.ElementIDs[i]);
                        if (i != node.ElementIDs.Count - 1) Console.Write(",");
                        else Console.Write("\n");
                    }
                    Console.Write("Support: " + node.Support+"\n");

                    Console.WriteLine("Transaction Heads: " + node.ToDetailedString(ap.Data)); // REF 1

                }
                    //Console.Write("\n" + "table ended");
                    Console.Write("\n_____________________");
            }

            Console.WriteLine("\n ______Rules_______");

            foreach (AssociationRule rules in ap.Rules) //Gives the details of the pairs which have been approved ultimately by min supp
                Console.WriteLine(rules.ToDetailedString(ap.Data));

        }

        public static Apriori LoadData() //Reading fileds from file and transaction data to return an Aproiri object
        {
            List<string> Fieldnames = null;
            List<bool[]> rows = new List<bool[]>();

            string line;
            bool header = true;

            System.IO.StreamReader file = new System.IO.StreamReader(@"E:\FaseehAhmed\DataMining\DataMiningAssignment\AssignmentData\GarmentsTransactionsData.txt");

            while ((line = file.ReadLine())!=null)
            {
                if(header)
                {
                    Fieldnames = line.Split(',').ToList();
                    header = false;
                    continue;
                }

                rows.Add(line.Split(',').Select(x => Convert.ToBoolean(int.Parse(x))).ToArray());
            }
            file.Close();

            //Console.WriteLine("Field Names Detected from File :");

            foreach(string name in Fieldnames)
                Console.WriteLine(name);

            Console.WriteLine("\n_____________\n\n");

            return new Apriori(new DataFields(Fieldnames, rows));
        }
    }
}