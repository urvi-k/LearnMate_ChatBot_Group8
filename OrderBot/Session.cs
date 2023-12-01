using System;
using System.ComponentModel.Design;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, 
            STUDENT_NAME,
            STUDENT_ID,
            ASSISTANCE_OPTION, 
            SELECTED_OPTION,
            GAME_TYPE,
            GAME_NAME,
            COURSE_TYPE,
            COURSE_NAME,
            GAME_ASSISTANCE,
            COURSE_ASSISTANCE
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            Order.CreateDataTable();
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome to the LearnMate!");
                    aMessages.Add("Hello. Are you a Conestoga Student?");
                    this.nCur = State.STUDENT_NAME;
                    break;
                case State.STUDENT_NAME:
                    if (sInMessage.ToLower() == "yes")
                    {
                        this.nCur = State.STUDENT_ID;
                    }
                    else 
                    {
                        aMessages.Add("Hello. Are you a Conestoga Student?");
                        break;
                    }
                    aMessages.Add("Please enter your name.");
                    Console.Write(sInMessage);
                    //this.nCur = State.STUDENT_ID;
                    break;
                case State.STUDENT_ID:
                    aMessages.Add("Please enter your Student ID.");
                    this.nCur = State.ASSISTANCE_OPTION;
                    break;
                case State.ASSISTANCE_OPTION:
                    if (sInMessage.Length == 7)
                    {
                        if (int.TryParse(sInMessage, out int value))
                        {
                            aMessages.Add("Please choose the Assistance type: 1. Academic 2. Sports");
                            this.nCur = State.SELECTED_OPTION;
                        }
                        else
                        {
                            aMessages.Add("Please enter your Student ID.");
                            break;
                        }
                    }
                    else {
                        aMessages.Add("Please enter your Student ID.");
                        break;
                    }
                    break;
                case State.SELECTED_OPTION:
                    if (sInMessage.ToLower() == "academic")
                    {
                        aMessages.Add("Please enter the Course Type you need Assistance for: 1. Online 2. Offline.");
                        this.nCur = State.COURSE_NAME;
                        break;
                    }
                    else if (sInMessage.ToLower() == "sports")
                    {
                        aMessages.Add("Please enter the Game Type you need Assistance for: 1. Indoor 2. Outdoor.");
                        this.nCur = State.GAME_NAME;
                        break;
                    }
                    else {
                        aMessages.Add("Please choose the Assistance type: 1. Academic 2. Sports");
                    }
                    break;
                case State.GAME_TYPE:
                    aMessages.Add("Please enter the Game Type you need Assistance for: 1. Indoor 2. Outdoor.");
                    this.nCur = State.GAME_NAME;
                    break;
                case State.GAME_NAME:
                    if (sInMessage.ToLower() == "indoor")
                    {
                        aMessages.Add("Please enter the Game Name you need Assistance for: 1. Chess 2. Pool 3.Carrom ");
                    }
                    else if (sInMessage.ToLower() == "outdoor")
                    {
                        aMessages.Add("Please enter the Game Name you need Assistance for: 1. Soccer 2. Cricket 3.Bascket Ball ");
                    }
                    else {
                        aMessages.Add("Please enter the Game Type you need Assistance for: 1. Indoor 2. Outdoor.");
                        break;
                    }
                    this.nCur = State.GAME_ASSISTANCE;
                    break;
               
                case State.GAME_ASSISTANCE:
                        aMessages.Add("You will get assisted by Prof. Kumar Bhojanapalli, On 1/12/2023 in class number:" + oOrder.ClassNumber);
                        aMessages.Add("Thank You");
                    break;
                case State.COURSE_ASSISTANCE:
                    if (sInMessage.ToLower() == "lab" || sInMessage.ToLower() == "theory")
                    {
                        aMessages.Add("You will get assisted by Prof. Kumar Bhojanapalli, On 1/15/2023 in class number:" + oOrder.ClassNumber);
                        aMessages.Add("Thank You");
                    }
                    else {
                        aMessages.Add("Please enter the Course Type you need Assistance for: 1. Lab 2. Theory.");
                    }
                    break;
                case State.COURSE_TYPE:
                    aMessages.Add("Please enter the Course Type you need Assistance for: 1. Online 2. Offline.");
                    this.nCur = State.COURSE_NAME;
                    break;
                case State.COURSE_NAME:
                    if (sInMessage.ToLower() == "offline" || sInMessage.ToLower() == "online")
                    {
                        aMessages.Add("Please enter the Course Type you need Assistance for: 1. Lab 2. Theory.");
                    }
                    else {

                        aMessages.Add("Please enter the Course Type you need Assistance for: 1. Online 2. Offline.");
                        break;
                    }
                    this.nCur = State.COURSE_ASSISTANCE;
                    break;
            }
            //aMessages.ForEach(delegate (String sMessage)
            //{
            //    System.Diagnostics.Debug.WriteLine(sMessage);
            //});
            return aMessages;
        }
    }
}
