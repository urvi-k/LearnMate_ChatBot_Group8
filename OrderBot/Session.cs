using System;

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
                    aMessages.Add("Please enter your name.");
                    this.nCur = State.STUDENT_ID;
                    break;
                case State.STUDENT_ID:
                    aMessages.Add("Please enter your Student ID.");
                    this.nCur = State.ASSISTANCE_OPTION;
                    break;
                case State.ASSISTANCE_OPTION:
                    aMessages.Add("Please choose the Assistance subject: 1. Academic 2. Sports");
                    this.nCur = State.GAME_TYPE;
                    break;
                case State.GAME_TYPE:
                    aMessages.Add("Please enter the Game Type you need Assistance for: 1. Indoor 2. Outdoor.");
                    this.nCur = State.GAME_NAME;
                    break;
                case State.GAME_NAME:
                    aMessages.Add("Please enter the Game Name you need Assistance for: 1. Soccer 2. Cricket 3.Bascket Ball ");
                    this.nCur = State.GAME_ASSISTANCE;
                    break;

                case State.GAME_ASSISTANCE:
                    aMessages.Add("You will get assisted by Prof. Kumar Bhojanapalli, On 1/12/2023 in class number:" + oOrder.ClassNumber);
                    aMessages.Add("Thank You");
                    break;
                case State.COURSE_ASSISTANCE:
                    aMessages.Add("You will get assisted by Prof. Kumar Bhojanapalli, On 1/15/2023 in class number:" + oOrder.ClassNumber);
                    aMessages.Add("Thank You");
                    break;
                case State.COURSE_TYPE:
                    aMessages.Add("Please enter the Course Type you need Assistance for: 1. Online 2. Offline.");
                    this.nCur = State.COURSE_NAME;
                    break;
                case State.COURSE_NAME:
                    aMessages.Add("Please enter the Course Type you need Assistance for: 1. Lab 2. Theory.");
                    this.nCur = State.COURSE_ASSISTANCE;
                    break;
            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }
    }
}
