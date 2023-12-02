using Microsoft.Data.Sqlite;

namespace LearnMateBot
{
    public class Order : ISQLModel
    {
        private string _student_id = String.Empty;
        private string _assistance_type = String.Empty;
        private string _game_type = String.Empty;
        private string _course_type = String.Empty;
        private string _game_name = String.Empty;
        private string _course_name = String.Empty;

        public string StudentId {
            get => _student_id;
            set => _student_id = value;
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


    
        public void Save()
        {

            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                var command = connection.CreateCommand();
                commandUpdate.CommandText =
                @"SELECT * FROM Data";
               // commandUpdate.Parameters.AddWithValue("$id", id);

                using (var reader = commandUpdate.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);

                        Console.WriteLine($"Hello, {name}!");
                    }
                }



                commandUpdate.CommandText =
                @"
                    UPDATE Data
                    SET 
                        assistance_type = $assistance_type,
                        game_type = $game_type,
                        course_type = $course_type,
                        game_name = $game_name,
                        course_name = $course_name,
                    WHERE student_id = $student_id
                ";
                commandUpdate.Parameters.AddWithValue("$assistance_type", AssistanceType);
                commandUpdate.Parameters.AddWithValue("$game_type", GameType);
                commandUpdate.Parameters.AddWithValue("$course_type", CourseType);
                commandUpdate.Parameters.AddWithValue("$game_name", GameName);
                commandUpdate.Parameters.AddWithValue("$course_name", CourseName);
                commandUpdate.Parameters.AddWithValue("$student_id", StudentId);
                int nRows = commandUpdate.ExecuteNonQuery();

                if (nRows == 0)
                {
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
                        INSERT INTO Data(assistance_type, game_type, course_type, game_name, course_name, student_id)
                        VALUES($assistance_type, $game_type, $course_type, $game_name, $course_name, $student_id)
                    ";
                    commandInsert.Parameters.AddWithValue("$assistance_type", AssistanceType);
                    commandInsert.Parameters.AddWithValue("$game_type", GameType);
                    commandInsert.Parameters.AddWithValue("$course_type", CourseType);
                    commandInsert.Parameters.AddWithValue("$game_name", GameName);
                    commandInsert.Parameters.AddWithValue("$course_name", CourseName);
                    commandInsert.Parameters.AddWithValue("$student_id", StudentId);
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
                        student_id TEXT PRIMARY KEY
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



