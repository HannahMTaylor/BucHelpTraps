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

        private static ITable GetQuestionTable()
        {
            IDatabaseDriver driver = Drivers.GetDefaultDriver();
            string name = "question";
            ITable table = driver.GetTableForName(name);
            if (table != null) return table;
            // table not present, so create it
            RowHeader rowHeader = new RowHeader(
                new Column("id", Column.Type.Integer),
                new Column("title",Column.Type.Text),
                new Column("description",Column.Type.Text),
                new Column("username",Column.Type.Text),
                new Column("answer",Column.Type.Text),
                new Column("created",Column.Type.Text),
                new Column("lastupdated",Column.Type.Text)
            );
            return driver.CreateTable(name, rowHeader);
        }

        // return question for ID
        public static Question GetForId(int qid)
        {
            
            IEnumerator<Row> enumerator = GetQuestionTable().Select(row => row.GetAsInt("id") == qid).GetEnumerator();
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
            IEnumerator<Row> enumerator = GetQuestionTable().SelectAll().GetEnumerator();
            List<Question> questions = new List<Question>();
            while (enumerator.MoveNext())
            {
                Row row = enumerator.Current;
                questions.Add(FromRow(row));
            }
            return questions;
        }

        // row -> obj conversion
        private static Question FromRow(Row row)
        {
            Question question = new Question();
            question.QuestionID = row.GetAsInt("id");
            question.Title = row.GetAsString("title");
            question.Description = row.GetAsString("description");
            question.UserName = row.GetAsString("username");
            question.Answer = row.GetAsString("answer");
            question.Created = DateTime.Parse(row.GetAsString("created"));
            question.LastUpdated = DateTime.Parse(row.GetAsString("lastupdated"));
            return question;
        }
    }
}
