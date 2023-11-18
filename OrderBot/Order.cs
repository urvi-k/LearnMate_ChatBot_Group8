using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _size = String.Empty;
        private string _phone = String.Empty;
        private string _assistance_type = String.Empty;
        private string _game_type = String.Empty;
        private string _course_type = String.Empty;
        private string _game_name = String.Empty;
        private string _course_name = String.Empty;
        private string _online_time = String.Empty;
        private string _class_number = String.Empty;
        private string _professor_name = String.Empty;

        public string Phone {
            get => _phone;
            set => _phone = value;
        }

        public string Size {
            get => _size;
            set => _size = value;
        }

        public string AssistanceType
        {
            get => _assistance_type;
            set => _assistance_type = value;
        }

        public string GameType
        {
            get => _game_type;
            set => _game_type = value;
        }

        public string CourseType
        {
            get => _course_type;
            set => _course_type = value;
        }

        public string GameName
        {
            get => _game_name;
            set => _game_name = value;
        }

        public string CourseName
        {
            get => _course_name;
            set => _course_name = value;
        }

        public string OnlineTime
        {
            get => _online_time;
            set => _online_time = value;
        }

        public string ClassNumber
        {
            get => _class_number;
            set => _class_number = value;
        }

        public string ProfessorName
        {
            get => _professor_name;
            set => _professor_name = value;
        }
        public void Save()
        {

            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
                    UPDATE Data
                    SET 
                        assistance_type = $assistance_type,
                        game_type = $game_type,
                        course_type = $course_type,
                        game_name = $game_name,
                        course_name = $course_name,
                        online_time = $online_time,
                        class_number = $class_number,
                        professor_name = $professor_name
                    WHERE phone = $phone
                ";
                commandUpdate.Parameters.AddWithValue("$assistance_type", AssistanceType);
                commandUpdate.Parameters.AddWithValue("$game_type", GameType);
                commandUpdate.Parameters.AddWithValue("$course_type", CourseType);
                commandUpdate.Parameters.AddWithValue("$game_name", GameName);
                commandUpdate.Parameters.AddWithValue("$course_name", CourseName);
                commandUpdate.Parameters.AddWithValue("$online_time", OnlineTime);
                commandUpdate.Parameters.AddWithValue("$class_number", ClassNumber);
                commandUpdate.Parameters.AddWithValue("$professor_name", ProfessorName);
                commandUpdate.Parameters.AddWithValue("$phone", Phone);
                int nRows = commandUpdate.ExecuteNonQuery();

                if (nRows == 0)
                {
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
                        INSERT INTO Data(assistance_type, game_type, course_type, game_name, course_name, online_time, class_number, professor_name, phone)
                        VALUES($assistance_type, $game_type, $course_type, $game_name, $course_name, $online_time, $class_number, $professor_name, $phone)
                    ";
                    commandInsert.Parameters.AddWithValue("$assistance_type", AssistanceType);
                    commandInsert.Parameters.AddWithValue("$game_type", GameType);
                    commandInsert.Parameters.AddWithValue("$course_type", CourseType);
                    commandInsert.Parameters.AddWithValue("$game_name", GameName);
                    commandInsert.Parameters.AddWithValue("$course_name", CourseName);
                    commandInsert.Parameters.AddWithValue("$online_time", OnlineTime);
                    commandInsert.Parameters.AddWithValue("$class_number", ClassNumber);
                    commandInsert.Parameters.AddWithValue("$professor_name", ProfessorName);
                    commandInsert.Parameters.AddWithValue("$phone", Phone);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();
                }
            }
        }



            public static void CreateDataTable()
            {
                using (var connection = new SqliteConnection(DB.GetConnectionString()))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Data (
                        assistance_type TEXT,
                        game_type TEXT,
                        course_type TEXT,
                        game_name TEXT,
                        course_name TEXT,
                        online_time TEXT,
                        class_number TEXT,
                        professor_name TEXT,
                        phone TEXT PRIMARY KEY
                    );
                ";

                    try
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("Data table created successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error creating Data table: {ex.Message}");
                    }
                }
            }
        }
    }



