﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class StateOfMenu : State
    {
        public override void Handle(Program program)
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1 - новая игра\n2 - список игр\n3 - подключиться к игре");//game menu output
            int enteredValue = Convert.ToInt32(Console.ReadLine()); //get value of person chose
            while (enteredValue > 3 || enteredValue < 1) //if value is incorrect
            {
                Console.WriteLine("Вы ввели некорректное значение"); //person mistake output
                Console.WriteLine("1 - новая игра\n2 - список игр\n3 - подключиться к игре");//game menu output
                enteredValue = Convert.ToInt32(Console.ReadLine());//get value of person chose
            }
            switch (enteredValue)
            {
                case 1: //if person want to create new game

                    break;
                case 2: //if person want to have list of games
                    
                    break;
                case 3: //if person want to connect to the game
                    
                    break;
            }
            
            program.Pars(Recive(sock), ref x); //recive and generate a response
        }
    }
}