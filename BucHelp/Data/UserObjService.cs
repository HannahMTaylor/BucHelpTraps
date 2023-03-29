using BucHelp.DatabaseServices;

namespace BucHelp.Data
{
    public class UserObjService
    {
        // return user for ID
        public static User GetForID(int uid)
        {

            IEnumerator<Row> enumerator = GetTable().Select(row => row.GetAsInt("id") == uid).GetEnumerator();
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

        private static List<User> GetUserList()
        {
            List<User> users = new List<User>();

            IEnumerator<Row> enumerator = GetTable().SelectAll().GetEnumerator();
            while (enumerator.MoveNext())
            {
                Row row = enumerator.Current;
                users.Add(FromRow(row));
            }

            return users;
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
            GetTable().Delete(row => row.GetAsInt("id") == user.UserId);
            GetTable().Insert(ToRow(user));
        }

        // private below here
        // null bits
        // a set bit indicates that field is null
        // 1 = null, 0 = present!
        private const int NULL_AFFILIATION = 0x01;

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
                // not null
                new Column("username", Column.Type.Text),
                new Column("email", Column.Type.Text),
                new Column("password", Column.Type.Text),
                // nullable
                new Column("affiliation", Column.Type.Text)
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
            if ((nulls & NULL_AFFILIATION) == 0) user.Affiliation = row.GetAsString("affiliation");
            user.UserName = row.GetAsString("username");
            user.Email = row.GetAsString("email");
            user.Password = row.GetAsString("password");
            return user;
        }

        // obj -> row conversion
        private static Row ToRow(User user)
        {
            Row row = new Row(GetTable().Header);
            row.SetAsInt("id", user.UserId);
            // build nulls
            int nulls = 0;
            row.SetAsString("username", user.UserName);
            row.SetAsString("email", user.Email);
            row.SetAsString("password", user.Password);
            SetAsStringNullable(row, "affiliation", user.Affiliation, NULL_AFFILIATION, ref nulls);
            row.SetAsInt("nulls", nulls);
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
