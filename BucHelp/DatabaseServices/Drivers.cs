namespace BucHelp.DatabaseServices
{
    public class Drivers
    {
        public static IDatabaseDriver GetDefaultDriver()
        {
            return null;
        }

        private static void APIUseTest()
        {
            IDatabaseDriver driver = Drivers.GetDefaultDriver();
            ITable questions = driver.GetTableForName("questions");
            foreach (Row row in questions.Select(row => row.GetAsString("questionAnswer").Contains("Duck")))
            {
                Console.WriteLine(row.GetAsString("questionText"));
                Console.WriteLine(row.GetAsString("questionAnswer"));
            }

            Row newQuestion = new Row(questions.Header);
            newQuestion.SetAsString("questionText","What is the duck?");
            newQuestion.SetAsString("questionAnswer", "life");
            questions.Insert(newQuestion);

            questions.Delete(row => row.GetAsString("questionAnswer").Contains("Duck"));
            questions.Update(row => row.GetAsString("questionAnswer").Contains("Duck"), "questionAnswer", "yes");

            driver.Commit();
            driver.Dispose();
        }
    }
}
