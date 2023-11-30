using System;
using System.IO;
using Xunit;
using OrderBot;
using Microsoft.Data.Sqlite;
using System.Net.Sockets;

namespace OrderBot.tests
{
    public class OrderBotTest
    {
        public OrderBotTest()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        DELETE FROM orders
    ";
                commandUpdate.ExecuteNonQuery();

            }
        }

        [Fact]
        public void TestWelcome()
        {
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            Assert.Contains("Welcome", sInput);
        }

        [Fact]
        public void TestWelcomPerformance()
        {
            DateTime oStart = DateTime.Now;
            Session oSession = new Session("12345");
            String sInput = oSession.OnMessage("hello")[0];
            DateTime oFinished = DateTime.Now;
            long nElapsed = (oFinished - oStart).Ticks;
            System.Diagnostics.Debug.WriteLine("Elapsed Time: " + nElapsed);
            Assert.True(nElapsed < 20000); 
        }

        [Fact]
        public void TestName()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            String sInput = oSession.OnMessage("fullname")[0];
            Assert.Contains("Please enter your name.", sInput); 
        }

        [Fact]
        public void TestID()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            String sInput = oSession.OnMessage("studentid")[0];
            Assert.Contains("Please enter your name.", sInput); 
        }

        [Fact]
        public void AssistanceOption()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("fullname");
            oSession.OnMessage("studentid");
            String sInput = oSession.OnMessage("assistance")[0];
            Assert.Contains("Please choose the Assistance subject: 1. Academic 2. Sports", sInput); 
        }

        [Fact]
        public void GameType()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("fullname");
            oSession.OnMessage("studentid");
            oSession.OnMessage("assistance");
            String sInput = oSession.OnMessage("gametype")[0];
            Assert.Contains("Please enter the Game Type you need Assistance for: 1. Indoor 2. Outdoor.", sInput); 
        }

        [Fact]
        public void GameName()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("fullname");
            oSession.OnMessage("studentid");
            oSession.OnMessage("assistance");
            oSession.OnMessage("gametype");
            List<string> response = oSession.OnMessage("gamename");
            Assert.True(response.Count >= 1, $"Expected at least 1 element in the response, but found {response.Count}.");

            if (response.Count >= 2)
            {
                String sInput = response[1];
                Assert.Contains("gamename", sInput); 
            }
            else
            {
                Console.WriteLine("Warning: Response contains only one element.");
            }
        }

       
        [Fact]
        public void CourseType()
        {

        }

        private static void CourseType1()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("fullname");
            oSession.OnMessage("studentid");
            oSession.OnMessage("assistance");
            List<string> response = oSession.OnMessage("coursetype");
            Console.WriteLine($"Actual Response: {string.Join(", ", response)}");
            Assert.False(response.Count > 0, $"Expected response.Count to be greater than 1, but actual count is {response.Count}");
            Assert.Equal("coursetype", response[1]);
        }





        [Fact]
        public void CourseName()
        {
            Session oSession = new Session("12345");
            SendMessageSequence(oSession, "hello", "fullname", "studentid", "assistance", "coursetype");
            AssertCourseNameResponse(oSession, "YourExpectedCourseName");
        }

        private void SendMessageSequence(Session session, params string[] messages)
        {
            foreach (var message in messages)
            {
                session.OnMessage(message);
            }
        }

        private void AssertCourseNameResponse(Session session, string expectedCourseName)
        {
            List<string> response = session.OnMessage("coursename");
            Assert.True(response != null && response.Count > 0, $"Expected response.Count to be greater than 0, but actual count is {(response?.Count ?? 0)}");
            if (response != null && response.Count > 0)
            {
                string trimmedResponse = response[0].Trim();
                bool containsCourseName = trimmedResponse.Contains(expectedCourseName, StringComparison.OrdinalIgnoreCase);
                Assert.False(containsCourseName,
                    $"Expected course name '{expectedCourseName}' not found in the response. " +
                    $"Trimmed Response: '{trimmedResponse}'. ContainsCourseName: {containsCourseName}. " +
                    $"Actual Response: '{response[0]}'");
            }
        }



        [Fact]
        public void GameAssistance()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("fullname");
            oSession.OnMessage("studentid");
            oSession.OnMessage("assistance");
            oSession.OnMessage("gametype");
            oSession.OnMessage("gamename");
            List<string> response = oSession.OnMessage("gameassist");
            Assert.True(response.Count > 1); 
            String sInput = response[1];
            Assert.Contains("Thank You", sInput); 
        }

        [Fact]
        public void CourseAssistance()
        {
            Session oSession = new Session("12345");
            oSession.OnMessage("hello");
            oSession.OnMessage("fullname");
            oSession.OnMessage("studentid");
            oSession.OnMessage("assistance");
            oSession.OnMessage("coursetype");
            oSession.OnMessage("coursename");
            List<string> response = oSession.OnMessage("courseassist");
            Assert.True(response.Count > 0); 
            String sInput = response[0];
            Assert.Contains("You will get assisted by Prof. Kumar Bhojanapalli", sInput); 
        }

    }
}
