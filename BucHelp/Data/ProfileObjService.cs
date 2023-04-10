using BucHelp.DatabaseServices;

namespace BucHelp.Data
{
	public class ProfileObjService
	{
		// return profile for ID
		public static Profile GetForID(int qid)
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
		public static List<Profile> GetAll()
		{
			IEnumerator<Row> enumerator = GetTable().SelectAll().GetEnumerator();
			List<Profile> profiles = new List<Profile>();
			while (enumerator.MoveNext())
			{
				Row row = enumerator.Current;
				profiles.Add(FromRow(row));
			}
			return profiles;
		}

		// return all for given question ID
		public static List<Profile> GetAllForQuestionId(int qid)
		{
			IEnumerator<Row> enumerator = GetTable().Select(row => row.GetAsInt("questionid") == qid).GetEnumerator();
			List<Profile> profiles = new List<Profile>();
			while (enumerator.MoveNext())
			{
				Row row = enumerator.Current;
				profiles.Add(FromRow(row));
			}
			return profiles;
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
		public static void Write(Profile profile)
		{
			// Remove the row if it is already there and reinsert
			// not as efficient but the update would be painful to write
			GetTable().Delete(row => row.GetAsInt("id") == profile.ProfileId);
			GetTable().Insert(ToRow(profile));
		}

		// write a list of profiles for a question
		public static void WriteQuestionProfileList(int questionId, List<Profile> profiles)
		{
			// remove existing profiles for question (to be overwritten by list)
			ProfileObjService.GetTable().Delete(row => row.GetAsInt("questionid") == questionId);
			// write each profile in the list
			foreach (Profile a in profiles)
			{
				Write(a);
			}
		}

		// private below here
		// null bits
		// no null fields in this class

		// get or create table
		private static ITable GetTable()
		{
			IDatabaseDriver driver = Drivers.GetDefaultDriver();
			string name = "profile";
			ITable table = driver.GetTableForName(name);
			if (table != null) return table;
			// table not present, so create it
			RowHeader rowHeader = new RowHeader(
				new Column("id", Column.Type.Integer),
				new Column("nulls", Column.Type.Integer), // included anyway for expansion purposes
				new Column("userid", Column.Type.Integer),
				new Column("username", Column.Type.Text),
                new Column("name", Column.Type.Text),
                new Column("major", Column.Type.Text),
                new Column("email", Column.Type.Text),
                new Column("description", Column.Type.Text),
                new Column("phone", Column.Type.Text),
                new Column("socialmedia", Column.Type.Text),
                new Column("points", Column.Type.Integer),
                new Column("stars", Column.Type.Integer),
                new Column("reviews", Column.Type.Integer)
            );
			table = driver.CreateTable(name, rowHeader);
			driver.Commit();
			return table;
		}

		// row -> obj conversion
		private static Profile FromRow(Row row)
		{
			Profile profile = new Profile();
			profile.ProfileId = row.GetAsInt("id");
			// get nulls (they are not used now)
			int nulls = row.GetAsInt("nulls");
			// get nonnull fields
			profile.UserId = row.GetAsInt("userid");
			profile.Username = row.GetAsString("username");
			profile.Name = row.GetAsString("name");
			profile.Major = row.GetAsString("major");
			profile.Email = row.GetAsString("email");
			profile.Description = row.GetAsString("description");
			profile.Phone = row.GetAsString("phone");
			profile.SocialMedia = row.GetAsString("socialmedia");
			profile.Points = row.GetAsInt("points");
			profile.Stars = row.GetAsInt("stars");
			profile.Reviews = row.GetAsInt("reviews");
			return profile;
		}

		// obj -> row conversion
		private static Row ToRow(Profile Profile)
		{
			Row row = new Row(GetTable().Header);
			row.SetAsInt("id", Profile.ProfileId);
			// build nulls
			int nulls = 0;
			row.SetAsInt("userid", Profile.UserId);
			row.SetAsString("username", Profile.Username);
			row.SetAsString("major", Profile.Major);
			row.SetAsString("email", Profile.Email);
			row.SetAsString("description", Profile.Description);
			row.SetAsString("phone", Profile.Phone);
			row.SetAsString("socialmedia", Profile.SocialMedia);
			row.SetAsInt("points", Profile.Points);
            row.SetAsInt("stars", Profile.Stars);
            row.SetAsInt("reviews", Profile.Reviews);
            row.SetAsInt("nulls", nulls);
			return row;
		}
	}
}
