using BucHelp.DatabaseServices;

namespace BucHelp.Data
{
	public class AnswerObjService
	{
		// return answer for ID
		public static Answer GetForID(int qid)
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
		public static List<Answer> GetAll()
		{
			IEnumerator<Row> enumerator = GetTable().SelectAll().GetEnumerator();
			List<Answer> answers = new List<Answer>();
			while (enumerator.MoveNext())
			{
				Row row = enumerator.Current;
				answers.Add(FromRow(row));
			}
			return answers;
		}

		// return all for given question ID
		public static List<Answer> GetAllForQuestionId(int qid)
		{
			IEnumerator<Row> enumerator = GetTable().Select(row => row.GetAsInt("questionid") == qid).GetEnumerator();
			List<Answer> answers = new List<Answer>();
			while (enumerator.MoveNext())
			{
				Row row = enumerator.Current;
				answers.Add(FromRow(row));
			}
			return answers;
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
		public static void Write(Answer answer)
		{
			// Remove the row if it is already there and reinsert
			// not as efficient but the update would be painful to write
			GetTable().Delete(row => row.GetAsInt("id") == answer.AnswerId);
			GetTable().Insert(ToRow(answer));
		}

		// private below here
		// null bits
		// no null fields in this class

		// get or create table
		private static ITable GetTable()
		{
			IDatabaseDriver driver = Drivers.GetDefaultDriver();
			string name = "answer";
			ITable table = driver.GetTableForName(name);
			if (table != null) return table;
			// table not present, so create it
			RowHeader rowHeader = new RowHeader(
				new Column("id", Column.Type.Integer),
				new Column("nulls", Column.Type.Integer), // included anyway for expansion purposes
				// nonnull
				new Column("questionid", Column.Type.Integer),
				new Column("userid", Column.Type.Integer),
				new Column("description", Column.Type.Text),
				new Column("votes", Column.Type.Integer),
				new Column("created", Column.Type.Text),
				new Column("lastupdated", Column.Type.Text)
			);
			table = driver.CreateTable(name, rowHeader);
			driver.Commit();
			return table;
		}

		// row -> obj conversion
		private static Answer FromRow(Row row)
		{
			Answer answer = new Answer();
			answer.AnswerId = row.GetAsInt("id");
			// get nulls (they are not used now)
			int nulls = row.GetAsInt("nulls");
			// get nonnull fields
			answer.QuestionId = row.GetAsInt("questionid");
			answer.UserId = row.GetAsInt("userid");
			answer.Description = row.GetAsString("description");
			answer.Votes = row.GetAsInt("votes");
			answer.Created = DateTime.Parse(row.GetAsString("created"));
			answer.LastUpdated = DateTime.Parse(row.GetAsString("lastupdated"));
			return answer;
		}

		// obj -> row conversion
		private static Row ToRow(Answer answer)
		{
			Row row = new Row(GetTable().Header);
			row.SetAsInt("id", answer.AnswerId);
			// build nulls
			int nulls = 0;
			row.SetAsInt("questionid", answer.QuestionId);
			row.SetAsInt("userid", answer.UserId);
			row.SetAsString("description", answer.Description);
			row.SetAsInt("votes", answer.Votes);
			row.SetAsString("created", answer.Created.ToString());
			row.SetAsString("lastupdated", answer.LastUpdated.ToString());
			row.SetAsInt("nulls", nulls);
			return row;
		}
	}
}
