using System;
using System.Collections.Generic;
using Section11_144.Entities;
using Section11_144.Entities.Exceptions;

namespace Section11_144
{
    class Program
    {
        static void Main(string[] args)
        {

            bool Rolling = true;                        
            int NReservs, ReservsSum = 0;  // NReservs could've been instanced WITHIN the big while scope

            List<Reservation> R1List = new List<Reservation>();  // I've first instanced a void Reservation class object outside the while scope to not make it get override every loop

            while (Rolling)
            {

                DateTime DCheckin;
                DateTime DCheckOut;
                bool FirstTry = true;
                bool SecondTry = true;
                // List<Reservation> R1List = new List<Reservation>();

                if (R1List.Count < 1)                                               // IT CAN CRASH HERE
                {
                    Console.WriteLine("\n\n   Éoq of reserv program ");
                    Console.Write("\n   How many reservs do you want to do? ");
                    NReservs = int.Parse(Console.ReadLine());
                }
                else
                {
                    Console.Write("\n\n   How many reservs do you want to add? ");
                    NReservs = int.Parse(Console.ReadLine());
                }

                while (FirstTry)
                {                    
                    try
                    {
                        for (int i = 0; i < NReservs; i++)
                        {
                            if (ReservsSum == 0)
                            {
                                Console.WriteLine($"\n\n   Enter the reserv #{i + 1} data ");
                            }
                            else
                            {
                                Console.WriteLine($"\n\n   Enter the reserv #{ReservsSum + 1 + i} data ");
                            }

                            Console.Write("\n   What is the room number you want to pick? ");
                            int RNumber = int.Parse(Console.ReadLine());

                            Console.Write("\n   Enter your checkin intended date: ");
                            DCheckin = DateTime.Parse(Console.ReadLine());

                            Console.Write("\n   Enter the checkout intended date: ");
                            DCheckOut = DateTime.Parse(Console.ReadLine());

                            R1List.Add(new Reservation(RNumber, DCheckin, DCheckOut));
                        }
                        FirstTry = false;    // The program will came here only IF there are no exception till now
                        break;              // EXIT the loop ONLY if it NOT TRIGGERS A DomainException
                    }
                    catch (DomainException e)
                    {
                        Console.WriteLine("\n   Application exception: " + e.Message);
                        ListCleaner(ReservsSum, R1List);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("\n   Format error: " + e.Message);
                        ListCleaner(ReservsSum, R1List);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\n   Unexpected error: " + e.Message);
                        ListCleaner(ReservsSum, R1List);
                    }
                }

                // TRY DIVISION

                while (SecondTry)
                {
                    try
                    {
                        Console.Write("\n\n   Do you want to update any other reservation dates? (y/n) ");
                        char Answer = char.Parse(Console.ReadLine());

                        while (Answer != 'y' && Answer != 'n')
                        {
                            Console.Write("\n\n   Invalid answer. Try it again! ");
                            Console.Write("\n   Do you want to update any other reservation dates? (y/n) ");
                            Answer = char.Parse(Console.ReadLine());
                        }

                        if (Answer == 'y')
                        {
                            Console.Write("\n   Enter the reservation index position that you want to have the dates updated: ");
                            int RIndex = int.Parse(Console.ReadLine());

                            // ENTERING THE UPDATES DATES BELOW

                            Console.Write("\n   Enter your checkin intended date: ");
                            DateTime UDCheckin = DateTime.Parse(Console.ReadLine());

                            Console.Write("\n   Enter the checkout intended date: ");
                            DateTime UDCheckOut = DateTime.Parse(Console.ReadLine());

                            // WRITING THE RESULTS                  THE PERSONAL EXCEPTIONS CLASS WE CREATED IS ALREADY VERIFYING WHETER IS POSSIBLE TO UPDATE THE CURRENT DATES OR NOT

                            Console.WriteLine();
                            R1List[RIndex].UpdateDates(UDCheckin, UDCheckOut);  // UPDATES THE R1List specif item content BESIDES IT HAS ALREADY BEEN UPDATED A LITTLE BEFORE                            
                            List<string> Conv = new List<string>();
                            foreach (Reservation thatReserv1 in R1List)
                            {
                                Console.WriteLine(R1List[R1List.IndexOf(thatReserv1)]);
                                Conv.Add(R1List[R1List.IndexOf(thatReserv1)].ToString());
                                Console.WriteLine();
                            }

                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"G:\CS TXT Files\Section 11_144 Last\Relatory " + R1List.Count + ".txt"))
                            foreach (string obj in Conv)
                            {
                                    file.WriteLine(obj);
                            }
                            SecondTry = false;  // EXIT THE SecondTry loop
                        }
                        else
                        {
                            Console.WriteLine();
                            List<string> Conv1 = new List<string>();
                            foreach (Reservation thatReserv2 in R1List)
                            {
                                Console.WriteLine(R1List[R1List.IndexOf(thatReserv2)]);
                                Conv1.Add(R1List[R1List.IndexOf(thatReserv2)].ToString());
                                Console.WriteLine();
                            }

                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"G:\CS TXT Files\Section 11_144 Last\Relatory " + R1List.Count + ".txt"))
                            foreach (string obj in Conv1)
                            {
                                file.WriteLine(obj);
                            }
                            SecondTry = false; // EXIT THE SecondTry loop
                        }
                    }
                    catch (DomainException e)
                    {
                        Console.WriteLine("\n   Application exception: " + e.Message);
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("\n   Format error: " + e.Message);
                    }
                    catch (Exception e)  // ESSA TEM QUE SER A ÚLTIMA, POIS *TODAS* AS EXECESSÕES CASARÃO COM ELA POR MEIO DE UPCASTING!!
                    {
                        Console.WriteLine("\n   Unexpected error: " + e.Message);
                    }
                }

                // FIRST QUESTION OF MAIN

                ReservsSum = R1List.Count; // Making myself awere of the List items number BEFORE I add new itens to it in a new loop in the big while

                Console.Write("\n   Do you want to add a new list of reservs? (y/n) ");  // IT CAN CRASH HERE
                char Answer2 = char.Parse(Console.ReadLine());

                while (Answer2 != 'y' && Answer2 != 'Y' && Answer2 != 'n' && Answer2 != 'N')
                {
                    Console.Write("\n   You've entered a invalid answer, man ");
                    Console.Write("\n   Do you want to add a new list of reservs? (y/n) ");
                    Answer2 = char.Parse(Console.ReadLine());                                   // GET OUT OF THIS LITTLE WHILE MEANS TO GET ONE MORE CYCLE OF THE BIG WHILE LOOP 
                }

                if (Answer2 == 'n' || Answer2 == 'N')
                {
                    Console.WriteLine("\n   End");
                    Rolling = false;  // Terminating the big while                    
                }
            }

            // SUPPORT LIST CLEANER FUNCTION BELOW
            // SUPPORT LIST CLEANER FUNCTION BELOW

            static void ListCleaner(int thatReservsSum, List<Reservation> thatR1List)  // CLEAN THE LIST IN THE DESIRED INDEX POSITIONS
            {
                for (int i = thatReservsSum; i < thatR1List.Count; i++)
                {
                    thatR1List.RemoveAt(i);
                }
            }
        }
    }
}


