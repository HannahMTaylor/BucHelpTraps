using BucHelp.DatabaseServices;

namespace BucHelp.Data
{
    public class UserObjService
    {
        // return user for ID
        public static User GetForID(int qid)
        {

            IEnumerator<Row> enumerator = GetTable().Select(row => row.GetAsInt("id") == qid).GetEnumerator();
            if (enumerator.MoveNext())
            {
                Row row = enumerator.Current;
                return FromRow(row);
            }
            else
            {
                return null; // not found
            }
        }

        // return all
        public static List<User> GetAll()
        {
            IEnumerator<Row> enumerator = GetTable().SelectAll().GetEnumerator();
            List<User> questions = new List<User>();
            while (enumerator.MoveNext())
            {
                Row row = enumerator.Current;
                questions.Add(FromRow(row));
            }
            return questions;
        }

        // get next available ID, based on database
        public static int GenerateID()
        {
            IEnumerator<Row> enumerator = GetTable().SelectAll().GetEnumerator();
            int nextId = -1;
            while (enumerator.MoveNext())
            {
                Row row = enumerator.Current;
                nextId = Math.Max(nextId, row.GetAsInt("id"));
            }
            return nextId + 1;
        }

        // write row out
        public static void Write(User user)
        {
            // Remove the row if it is already there and reinsert
            // not as efficient but the update would be painful to write
            //GetTable().Delete(row => row.GetAsInt("id") == user.UserName);
            //GetTable().Insert(ToRow(user));
        }

        // private below here
        // null bits
        // a set bit indicates that field is null
        // 1 = null, 0 = present!
        private const int NULL_USERNAME = 0x01;
        private const int NULL_EMAIL = 0x02;
        private const int NULL_PASSWORD = 0x04;

        // get or create table
        private static ITable GetTable()
        {
            IDatabaseDriver driver = Drivers.GetDefaultDriver();
            string name = "user";
            ITable table = driver.GetTableForName(name);
            if (table != null) return table;
            // table not present, so create it
            RowHeader rowHeader = new RowHeader(
                new Column("id", Column.Type.Integer),
                new Column("nulls", Column.Type.Integer),
                // nullable
                new Column("username", Column.Type.Text),
                new Column("email", Column.Type.Text),
                new Column("password", Column.Type.Text)
            );
            table = driver.CreateTable(name, rowHeader);
            driver.Commit();
            return table;
        }

        // row -> obj conversion
        private static User FromRow(Row row)
        {
            User user = new User();
            int uid = row.GetAsInt("id");
            // get nulls
            int nulls = row.GetAsInt("nulls");
            // if null bit is zero, then field is present- get it
            if ((nulls & NULL_USERNAME) == 0) user.UserName = row.GetAsString("username");
            if ((nulls & NULL_EMAIL) == 0) user.Email = row.GetAsString("email");
            if ((nulls & NULL_PASSWORD) == 0) user.Password = row.GetAsString("password");
            return user;
        }

        // obj -> row conversion, updates do not use this
        private static Row ToRow(Question question)
        {
            Row row = new Row(GetTable().Header);
            //row.SetAsInt("id", question.QuestionID);
            //// build nulls
            //int nulls = 0;
            //SetAsStringNullable(row, "title", question.Title, NULL_TITLE, ref nulls);
            //SetAsStringNullable(row, "description", question.Description, NULL_DESCRIPTION, ref nulls);
            //SetAsStringNullable(row, "username", question.UserName, NULL_USERNAME, ref nulls);
            //SetAsStringNullable(row, "answer", question.Answer, NULL_ANSWER, ref nulls);
            //row.SetAsString("created", question.Created.ToString());
            //row.SetAsString("lastupdated", question.LastUpdated.ToString());
            //row.SetAsInt("nulls", nulls);
            return row;
        }

        // set row nullable string
        private static void SetAsStringNullable(Row row, string column, string value, int mask, ref int nulls)
        {
            if (value != null)
            {
                row.SetAsString(column, value);
            }
            else
            {
                // placeholder string- never read, but required anyway
                row.SetAsString(column, "<null>");
                nulls |= mask;
            }
        }
    }
}
