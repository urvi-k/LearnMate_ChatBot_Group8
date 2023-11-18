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

        public string Phone{
            get => _phone;
            set => _phone = value;
        }

        public string Size{
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
        public void Save(){
           using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        UPDATE orders
        SET size = $size
        WHERE phone = $phone
    ";
                commandUpdate.Parameters.AddWithValue("$size", Size);
                commandUpdate.Parameters.AddWithValue("$phone", Phone);
                int nRows = commandUpdate.ExecuteNonQuery();
                if(nRows == 0){
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(size, phone)
            VALUES($size, $phone)
        ";
                    commandInsert.Parameters.AddWithValue("$size", Size);
                    commandInsert.Parameters.AddWithValue("$phone", Phone);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}
