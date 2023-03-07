using BucHelp.DatabaseServices;

namespace BucHelp.Data
{
    public class QuestionObjService
    {
        //temporary list of static questions
        List<Question> questions = new List<Question>();

        //hardcode attributes for the temp list
        Question q1 = new Question { QuestionID = 0, Title = "Question 1", Description = "Where is graduation normally held?", Answer = "In the Mini-Dome"};
        Question q2 = new Question { QuestionID = 1, Title = "Question 2", Description = "How do I apply to graduate?", Answer = "in Goldlink" };




        //method to retrieve posted questions -- should be able to access the persistent storage as is
        public Task<List<Question>> GetPostedQuestionsAsync()
        {
            questions.Add(q1);
            questions.Add(q2);
            return Task.FromResult(questions);
        }

        // return question for ID
        public static Question GetForID(int qid)
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
        public static List<Question> GetAll()
        {
            IEnumerator<Row> enumerator = GetTable().SelectAll().GetEnumerator();
            List<Question> questions = new List<Question>();
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
        public static void Write(Question question)
        {
            // Remove the row if it is already there and reinsert
            // not as efficient but the update would be painful to write
            GetTable().Delete(row => row.GetAsInt("id") == question.QuestionID);
            GetTable().Insert(ToRow(question));
        }

        // private below here
        // null bits
        // a set bit indicates that field is null
        // 1 = null, 0 = present!
        private const int NULL_TITLE = 0x01;
        private const int NULL_DESCRIPTION = 0x02;
        private const int NULL_USERNAME = 0x04;
        private const int NULL_ANSWER = 0x08;
		private const int NULL_CATEGORY = 0x10;

		// get or create table
		private static ITable GetTable()
        {
            IDatabaseDriver driver = Drivers.GetDefaultDriver();
            string name = "question";
            ITable table = driver.GetTableForName(name);
            if (table != null) return table;
            // table not present, so create it
            RowHeader rowHeader = new RowHeader(
                new Column("id", Column.Type.Integer),
                new Column("nulls", Column.Type.Integer),
                // nullable
                new Column("title", Column.Type.Text),
                new Column("description", Column.Type.Text),
                new Column("username", Column.Type.Text),
                new Column("answer", Column.Type.Text),
				new Column("category", Column.Type.Text),
				// nonnull
				new Column("created", Column.Type.Text),
                new Column("lastupdated", Column.Type.Text)
            );
            table = driver.CreateTable(name, rowHeader);
            driver.Commit();
            return table;
        }

        // row -> obj conversion
        private static Question FromRow(Row row)
        {
            Question question = new Question();
            question.QuestionID = row.GetAsInt("id");
            // get nulls
            int nulls = row.GetAsInt("nulls");
            // if null bit is zero, then field is present- get it
            if ((nulls & NULL_TITLE) == 0) question.Title = row.GetAsString("title");
            if ((nulls & NULL_DESCRIPTION) == 0) question.Description = row.GetAsString("description");
            if ((nulls & NULL_USERNAME) == 0) question.UserName = row.GetAsString("username");
            if ((nulls & NULL_ANSWER) == 0) question.Answer = row.GetAsString("answer");
			if ((nulls & NULL_CATEGORY) == 0) question.Category = row.GetAsString("category");
			// these are never null
			question.Created = DateTime.Parse(row.GetAsString("created"));
            question.LastUpdated = DateTime.Parse(row.GetAsString("lastupdated"));
            return question;
        }

        // obj -> row conversion, updates do not use this
        private static Row ToRow(Question question)
        {
            Row row = new Row(GetTable().Header);
            row.SetAsInt("id", question.QuestionID);
            // build nulls
            int nulls = 0;
            SetAsStringNullable(row, "title", question.Title, NULL_TITLE, ref nulls);
            SetAsStringNullable(row, "description", question.Description, NULL_DESCRIPTION, ref nulls);
            SetAsStringNullable(row, "username", question.UserName, NULL_USERNAME, ref nulls);
            SetAsStringNullable(row, "answer", question.Answer, NULL_ANSWER, ref nulls);
			SetAsStringNullable(row, "category", question.Category, NULL_CATEGORY, ref nulls);
			row.SetAsString("created", question.Created.ToString());
            row.SetAsString("lastupdated", question.LastUpdated.ToString());
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
