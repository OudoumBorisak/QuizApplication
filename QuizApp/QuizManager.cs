using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp
{
    public class QuizManager
    {
        private readonly QuizCategory HistoryCategory;
        private readonly QuizCategory BiologyCategory;
        private readonly QuizCategory GeographyCategory;
        private readonly QuizCategory EarthScienceCategory;


        private const int MaxAttempts = 3;
        private int attemptCount = 0;

        private List<User> users;
        private User currentUser;

        public enum Menu { START = 1, VIEW, MANAGE, DELETEALL, CHANGE, EXIT = 0 }
        public enum Log { LOGIN = 1, REGISTER, EXIT = 0 }
        public enum Stage { HISTORY = 1, BIOLOGY, GEOGRAPHY, EARTHSCIENCE, EXIT = 0 }
        public enum Option { ADD = 1, UPDATE, DELETE, BACK = 0 }
        public enum Choice { TEXT = 1, OPTION, ANSWER, EXIT = 0 }


        public QuizManager()
        {
            HistoryCategory = new QuizCategory();
            BiologyCategory = new QuizCategory();
            GeographyCategory = new QuizCategory();
            EarthScienceCategory = new QuizCategory();

            users = new List<User>();

            // Initialize with some example questions
            InitializeQuestions();
        }

        public void DisplayMainMenu()
        {
            if (!LoginOrRegister())
            {
                return;
            }

            Console.WriteLine($"Welcome to the Quiz Application, {currentUser.Username}!");

            while (true)
            {
                int input = IntegerInput("Menu:\n1. Start Quiz\n2. View Previous Results\n3. Manage Question(Add, Update, Delete)\n4. Delete All Question\n5. Change Setting\n0. Exit\nEnter your choice: ");
                Console.Clear();

                switch (input)
                {
                    case (int)Menu.START:
                        StartQuizMenu();
                        break;
                    case (int)Menu.VIEW:
                        ViewPreviousResultsMenu();
                        break;
                    case (int)Menu.MANAGE:
                        ManageQuestionsMenu();
                        break;
                    case (int)Menu.DELETEALL:
                        DeleteAllQuestionsMenu();
                        break;
                    case (int)Menu.CHANGE:
                        ChangeSettings();
                        break;
                    case (int)Menu.EXIT:
                        Console.WriteLine("You are Exiting quiz application...");
                        PressAnyKeyToContinue();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 6.");
                        PressAnyKeyToContinue();
                        Console.Clear();
                        break;
                }
            }
        }

        private bool LoginOrRegister()
        {
            int choice = IntegerInput("1. Login\n2. Register\n0. Exit\nEnter your choice: ");
            Console.Clear();
            switch (choice)
            {
                case (int)Log.LOGIN:
                    return Login();
                case (int)Log.REGISTER:
                    return Register();
                case (int)Log.EXIT:
                    Console.WriteLine("You are exiting.");
                    PressAnyKeyToContinue();
                    return false;
                default:
                    Console.WriteLine("Invalid input. Please try again! ");
                    PressAnyKeyToContinue();
                    return false;
            }
        }

        private bool Login()
        {
            while (attemptCount < MaxAttempts)
            {
                string inputUsername = InputString("Enter login username: ");
                int inputPassword = IntegerInput("Enter login password: ");
                Console.Clear();
                foreach (var user in users)
                {
                    if (user.Username == inputUsername && user.Password == inputPassword)
                    {
                        currentUser = user;
                        Console.WriteLine("Login successful!");
                        return true;
                    }
                    attemptCount++;
                    Console.WriteLine("Login failed. Invalid username or password.");
                }
            }
            return false;
        }

        private bool Register()
        {
            while (attemptCount < MaxAttempts)
            {
                string newUsername = InputString("Enter new username: ");
                int newPassword = IntegerInput("Enter new password: ");

                foreach (var user in users)
                {
                    if (user.Username == newUsername)
                    {
                        attemptCount++;
                        Console.WriteLine("Username already taken. Please try a different username.");
                        PressAnyKeyToContinue();
                        Console.Clear();
                        return LoginOrRegister();
                    }
                }
                users.Add(new User(newUsername, newPassword));
                Console.WriteLine("\nRegistration successful! Please login to continue.");
                PressAnyKeyToContinue();
                Console.Clear();
                return LoginOrRegister();
            }
            return false;
                
        }

        private void ChangeSettings()
        {
            while (attemptCount < MaxAttempts)
            {
                int inputPassword = IntegerInput("Enter current password: ");
                Console.Clear();
                if (inputPassword == currentUser.Password)
                {
                    string newUsername = InputString("Enter new username: ");
                    int newPassword = IntegerInput("Enter new password: ");

                    currentUser.Username = newUsername;
                    currentUser.Password = newPassword;
                    Console.WriteLine("Settings changed successfully!");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    break;
                }
                attemptCount++;
                Console.WriteLine("Current password is incorrect. Please try again");
            }
            return;
        }

        private void InitializeQuestions()
        {
            // Add some example questions to each category
            HistoryCategory.AddQuestion(new QuizQuestion("1. Who were the first two Presidents of the United States?\n", new List<string> { "George Washington", "Thomas Jefferson", "John Adams", "James Madison" }, new List<int> { 0, 2 }));
            HistoryCategory.AddQuestion(new QuizQuestion("2. In which years did World War I begin and end?\n", new List<string> { "1912", "1914", "1918", "1920" }, new List<int> { 1, 2 }));
            HistoryCategory.AddQuestion(new QuizQuestion("3. Which ancient civilizations built pyramids?\n", new List<string> { "Romans", "Egyptians", "Greek", "Mayans" }, new List<int> { 1, 3 }));
            HistoryCategory.AddQuestion(new QuizQuestion("4. Who sailed to America in 1492 and explored it before that?\n", new List<string> { "Marco Polo", "Ferdinand Magellan", "Christopher Columbus", "Leif Erikson" }, new List<int> { 2, 3 }));
            HistoryCategory.AddQuestion(new QuizQuestion("5. Which empires were known for their advanced road networks?\n", new List<string> { "Roman Empire", "Byzantine Empire", "Mongol Empire", "Inca Empire" }, new List<int> { 0, 3 }));
            HistoryCategory.AddQuestion(new QuizQuestion("6. Which events are commonly linked to the start of World Wars?\n", new List<string> { "The assassination of Archduke Franz Ferdinand", "The bombing of Pearl Harbor", "The invasion of Poland by Germany", "The sinking of the Lusitania" }, new List<int> { 0, 2 }));
            HistoryCategory.AddQuestion(new QuizQuestion("7. Who were influential leaders in the Indian independence movement?\n", new List<string> { "Jawaharlal Nehru", "Mahatma Gandi", "Indira Gandi", "Subhas Chandra Bose" }, new List<int> { 1, 2 }));
            HistoryCategory.AddQuestion(new QuizQuestion("8. Which countries were central to the Renaissance?\n", new List<string> { "Spain", "France", "Italy", "England" }, new List<int> { 2, 3 }));
            HistoryCategory.AddQuestion(new QuizQuestion("9. When did the Berlin Wall fall, and when did Germany reunify?\n", new List<string> { "1987", "1989", "1990", "1991" }, new List<int> { 1, 2 }));
            HistoryCategory.AddQuestion(new QuizQuestion("10. Which British Prime Ministers were in power during and after World War II?\n", new List<string> { "Neville Chamberlain", "Winston Churchill", "Clement Attlee", "Margaret Thatcher" }, new List<int> { 1, 2 }));

            BiologyCategory.AddQuestion(new QuizQuestion("1. Which of the following are types of cells found in humans?\n", new List<string> { "Eukaryotic cells", "Prokaryotic cells", "Red blood cells", "White blood cells" }, new List<int> { 0, 2 }));
            BiologyCategory.AddQuestion(new QuizQuestion("2. Which organelles are responsible for energy production in cells?\n", new List<string> { "Mitochondria", "Ribosomes", "Chloroplasts", "Nucleus" }, new List<int> { 0, 2 }));
            BiologyCategory.AddQuestion(new QuizQuestion("3. Which macromolecules are essential for genetic information storage and transmission?\n", new List<string> { "Proteins", "DNA", "Lipids", "RNA" }, new List<int> { 1, 3 }));
            BiologyCategory.AddQuestion(new QuizQuestion("4. Which of the following are types of blood vessels?\n", new List<string> { "Veins", "Alveoli", "Capillaries", "Nephrons" }, new List<int> { 0, 2 }));
            BiologyCategory.AddQuestion(new QuizQuestion("5. Which organisms can perform photosynthesis?\n", new List<string> { "Fungi", "Plants", "Bacteria", "Algae" }, new List<int> { 1, 3 }));
            BiologyCategory.AddQuestion(new QuizQuestion("6. Which of these structures are found in plant cells but not in animal cells?\n", new List<string> { "Cell membrane", "Cell wall", "Chloroplast", "Mitochondria" }, new List<int> { 1, 2 }));
            BiologyCategory.AddQuestion(new QuizQuestion("7. Which hormones are involved in regulating blood sugar levels?\n", new List<string> { "Insulin", "Estrogen", "Glucagon", "Thyroxine" }, new List<int> { 0, 2 }));
            BiologyCategory.AddQuestion(new QuizQuestion("8. Which processes are part of cellular respiration?\n", new List<string> { "Glycolysis", "Photosynthesis", "Krebs cycle", "Transcription" }, new List<int> { 0, 2 }));
            BiologyCategory.AddQuestion(new QuizQuestion("9. Which kingdoms include organisms that are unicellular?\n", new List<string> { "Plantae", "Fungi", "Protista", "Bacteria" }, new List<int> { 2, 3 }));
            BiologyCategory.AddQuestion(new QuizQuestion("10. Which of these structures are part of the human respiratory system?\n", new List<string> { "Trachea", "Esophagus", "Bronchi", "Diaphragm" }, new List<int> { 0, 2 }));

            GeographyCategory.AddQuestion(new QuizQuestion("1. Which of the following are continents?\n", new List<string> { "Asia", "Australia", "Greenland", "Antarctica" }, new List<int> { 0, 3 }));
            GeographyCategory.AddQuestion(new QuizQuestion("2. Which countries are located in South America?\n", new List<string> { "Brazil", "Mexico", "Argentina", "Spain" }, new List<int> { 0, 2 }));
            GeographyCategory.AddQuestion(new QuizQuestion("3. Which rivers are among the longest in the world?\n", new List<string> { "Amazon River", "Yangtze River", "Mississippi River", "Thames River" }, new List<int> { 0, 1 }));
            GeographyCategory.AddQuestion(new QuizQuestion("4. Which of the following are deserts?\n", new List<string> { "Sahara", "Gobi", "Himalayas", "Andes" }, new List<int> { 0, 1 }));
            GeographyCategory.AddQuestion(new QuizQuestion("5. Which oceans border Africa?\n", new List<string> { "Atlantic Ocean", "Indian Ocean", "Pacific Ocean", "Arctic Ocean" }, new List<int> { 0, 1 }));
            GeographyCategory.AddQuestion(new QuizQuestion("6. Which countries are landlocked?\n", new List<string> { "Switzerland", "Japan", "Austria", "New Zealand" }, new List<int> { 0, 2 }));
            GeographyCategory.AddQuestion(new QuizQuestion("7. Which are the world's largest countries by land area?\n", new List<string> { "Canada", "India", "Russia", "United Kingdom" }, new List<int> { 0, 2 }));
            GeographyCategory.AddQuestion(new QuizQuestion("8. Which of these mountain ranges are in Europe?\n", new List<string> { "Rockies", "Alps", "Andes", "Pyrenees" }, new List<int> { 1, 3 }));
            GeographyCategory.AddQuestion(new QuizQuestion("9. Which cities are the capitals of their respective countries?\n", new List<string> { "Tokyo", "Sydney", "Ottawa", "Cape Town" }, new List<int> { 0, 2 }));
            GeographyCategory.AddQuestion(new QuizQuestion("10. Which of these countries are part of the European Union (EU)?\n", new List<string> { "Germany", "Norway", "France", "Switzerland" }, new List<int> { 0, 2 }));

            EarthScienceCategory.AddQuestion(new QuizQuestion("1. Which of the following are layers of the Earth?\n", new List<string> { "Crust", "Troposphere", "Mantle", "Ionosphere" }, new List<int> { 0, 2 }));
            EarthScienceCategory.AddQuestion(new QuizQuestion("2. Which of the following are types of rocks?\n", new List<string> { "Igneous", "Sedimentary", "Foliated", "Metamorphic" }, new List<int> { 0, 3 }));
            EarthScienceCategory.AddQuestion(new QuizQuestion("3. Which gases are most abundant in Earth’s atmosphere?\n", new List<string> { "Oxygen", "Nitrogen", "Carbon Dioxide", "Hydrogen" }, new List<int> { 0, 1 }));
            EarthScienceCategory.AddQuestion(new QuizQuestion("4. Which natural disasters are caused by tectonic activity?\n", new List<string> { "Earthquakes", "Hurricanes", "Volcanoes", "Tornadoes" }, new List<int> { 0, 2 }));
            EarthScienceCategory.AddQuestion(new QuizQuestion("5. Which planets are considered gas giants?\n", new List<string> { "Mars", "Jupiter", "Neptune", "Venus" }, new List<int> { 1, 2 }));
            EarthScienceCategory.AddQuestion(new QuizQuestion("6. Which of the following are forms of precipitation?\n", new List<string> { "Hail", "Wind", "Rain", "Clouds" }, new List<int> { 0, 2 }));
            EarthScienceCategory.AddQuestion(new QuizQuestion("7. Which of these are renewable energy sources?\n", new List<string> { "Solar energy", "Coal", "Wind energy", "Natural gas" }, new List<int> { 0, 2 }));
            EarthScienceCategory.AddQuestion(new QuizQuestion("8. Which are major tectonic plates?\n", new List<string> { "Pacific Plate", "Arctic Plate", "Eurasian Plate", "Atlantic Plate" }, new List<int> { 0, 2 }));
            EarthScienceCategory.AddQuestion(new QuizQuestion("9. Which minerals are commonly found in Earth's crust?\n", new List<string> { "Quartz", "Halite", "Gold", "Feldspar" }, new List<int> { 0, 3 }));
            EarthScienceCategory.AddQuestion(new QuizQuestion("10. Which processes are part of the water cycle?\n", new List<string> { "Evaporation", "Sedimentation", "Condensation", "Photosynthesis" }, new List<int> { 0, 2 }));
        }
 
        private void StartQuizMenu()
        {
            int categoryChoice = IntegerInput("Choose a category to start quiz:\n1. History\n2. Biology\n3. Geography\n4. Earth Sciencen0. Exit\nEnter your choice: ");
            Console.Clear();

            QuizCategory selectedCategory = null;
            switch (categoryChoice)
            {
                case (int)Stage.HISTORY:
                    selectedCategory = HistoryCategory;
                    break;
                case (int)Stage.BIOLOGY:
                    selectedCategory = BiologyCategory;
                    break;
                case (int)Stage.GEOGRAPHY:
                    selectedCategory = GeographyCategory;
                    break;
                case (int)Stage.EARTHSCIENCE:
                    selectedCategory = EarthScienceCategory;
                    break;
                case (int)Stage.EXIT:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 0 to 4.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    return;
            }
            selectedCategory.StartQuiz();
        }

        private void ViewPreviousResultsMenu()
        {
            int categoryChoice = IntegerInput("Choose a category to view previous result:\n1. History\n2. Biology\n3. Geography\n4. Earth Science\n0. Exit\nEnter your choice: ");
            Console.Clear();

            QuizCategory selectedCategory = null;
            switch (categoryChoice)
            {
                case (int)Stage.HISTORY:
                    selectedCategory = HistoryCategory;
                    break;
                case (int)Stage.BIOLOGY:
                    selectedCategory = BiologyCategory;
                    break;
                case (int)Stage.GEOGRAPHY:
                    selectedCategory = GeographyCategory;
                    break;
                case (int)Stage.EARTHSCIENCE:
                    selectedCategory = EarthScienceCategory;
                    break;
                case (int)Stage.EXIT:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 0 to 4.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    return;
            }
            selectedCategory.ViewPreviousResults();
        }

        private void ManageQuestionsMenu()
        {
            int categoryChoice = IntegerInput("Choose a category to manage question:\n1. History\n2. Biology\n3. Geography\n4. Earth Science\n0. Exit\nEnter your choice: ");
            Console.Clear();

            QuizCategory selectedCategory = null;
            switch (categoryChoice)
            {
                case (int)Stage.HISTORY:
                    selectedCategory = HistoryCategory;
                    break;
                case (int)Stage.BIOLOGY:
                    selectedCategory = BiologyCategory;
                    break;
                case (int)Stage.GEOGRAPHY:
                    selectedCategory = GeographyCategory;
                    break;
                case (int)Stage.EARTHSCIENCE:
                    selectedCategory = EarthScienceCategory;
                    break;
                case (int)Stage.EXIT:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 0 to 4.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    return;
            }

            int operationChoice = IntegerInput("Choose an operation:\n1. Add Question\n2. Update Question\n3. Delete Question\n0. Back to Main Menu\nEnter your choice: ");
            Console.Clear();
            switch (operationChoice)
            {
                case (int)Option.ADD:
                    AddQuestion(selectedCategory);
                    break;
                case (int)Option.UPDATE:
                    UpdateQuestion(selectedCategory);
                    break;
                case (int)Option.DELETE:
                    DeleteQuestion(selectedCategory);
                    break;
                case (int)Option.BACK:
                    Console.WriteLine("Returning to main menu...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 0 to 4.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    break;
            }
        }

        private void AddQuestion(QuizCategory category)
        {
            do
            {
                string questionText = InputString("Enter the question test: ");
                List<string> options = new List<string>();
                Console.WriteLine("Enter options for player (enter 'done' to finish):");
                while (true)
                {
                    Console.Write("Option: ");
                    string option = Console.ReadLine();
                    if (option.ToLower() == "done")
                        break;
                    options.Add(option);
                }
                category.AddQuestion(CreateQuestion(questionText, options));
                Console.WriteLine("Question Successfully added!");

            } while (AskQuestion());
            Console.Clear();
        }

        private void UpdateQuestion(QuizCategory category)
        {
            do
            {
                Console.WriteLine("Current questions:");
                category.DisplayQuestions();

                Console.Write("Enter the index of the question to update: ");
                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    Console.Clear();
                    if (index >= 1 && index < category.Questions.Count)    
                    {
                        int partChoice = IntegerInput("Which part do you want to update?\n1. Question Text\n2. Options\n3. Correct Answers\n0. Exit\nEnter your choice: ");

                        switch (partChoice)
                        {
                            case (int)Choice.TEXT:
                                Console.Write("Enter the updated question text: ");
                                string questionText = Console.ReadLine();
                                category.Questions[index].QuestionText = questionText;  
                                break;
                            case (int)Choice.OPTION:
                                List<string> options = new List<string>();
                                Console.WriteLine("Enter updated options (enter 'done' to finish):");
                                while (true)
                                {
                                    Console.Write("Option: ");
                                    string option = Console.ReadLine();
                                    if (option.ToLower() == "done")
                                        break;
                                    options.Add(option);
                                }
                                category.Questions[index].Options = options;    
                                break;
                            case (int)Choice.ANSWER:
                                Console.WriteLine("Enter the index of correct answer(s) (comma-separated, starting from 1):");
                                string correctAnswerInput = Console.ReadLine();
                                string[] correctAnswerParts = correctAnswerInput.Split(',');

                                List<int> correctAnswers = new List<int>();
                                foreach (var part in correctAnswerParts)
                                {
                                    if (int.TryParse(part.Trim(), out int answerIndex))
                                    {
                                        correctAnswers.Add(answerIndex - 1); // Convert to zero-based index
                                    }
                                }
                                category.Questions[index].CorrectAnswers = correctAnswers;  
                                break;
                            case (int)Choice.EXIT:
                                Console.WriteLine("\nExiting");
                                break;
                            default:
                                Console.WriteLine("\nInvalid choice.");
                                PressAnyKeyToContinue();
                                Console.Clear();
                                return;
                        }

                        Console.WriteLine("\nQuestion updated successfully.");
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid question index.");
                        PressAnyKeyToContinue();
                        Console.Clear();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter a number.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    return;
                }
            } while (AskQuestion());
            Console.Clear();
        }


        private void DeleteQuestion(QuizCategory category)
        {
            do
            {
                Console.WriteLine("Current questions:");
                category.DisplayQuestions();

                Console.Write("Enter the index of the question to delete: ");
                if (int.TryParse(Console.ReadLine(), out int index))
                {
                    bool confirm = AskConfirmation("You are going to delete this question? (y/n): ");
                    if (confirm)
                    {
                        category.DeleteQuestion(index);
                        Console.WriteLine("\nQuestion deleted successfully.");
                    }
                    else
                    {
                        Console.WriteLine("\nDeletion cancelled.");
                        PressAnyKeyToContinue();
                        Console.Clear();
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            } while (AskQuestion());
            Console.Clear();
        }

        private void DeleteAllQuestionsMenu()
        {
            do
            {
                int categoryChoice = IntegerInput("Choose a category to delete entire question:\n1. History\n2. Biology\n3. Geography\n4. Earth Science\n0. Exit\nEnter your choice: ");

                QuizCategory selectedCategory = null;
                switch (categoryChoice)
                {
                    case (int)Stage.HISTORY:
                        selectedCategory = HistoryCategory;
                        break;
                    case (int)Stage.BIOLOGY:
                        selectedCategory = BiologyCategory;
                        break;
                    case (int)Stage.GEOGRAPHY:
                        selectedCategory = GeographyCategory;
                        break;
                    case (int)Stage.EARTHSCIENCE:
                        selectedCategory = EarthScienceCategory;
                        break;
                    case (int)Stage.EXIT:
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number from 0 to 4.");
                        PressAnyKeyToContinue();
                        Console.Clear();
                        return;
                }
                bool confirm = AskConfirmation("You are going to delete this category? (y/n): ");
                if (confirm)
                {
                    selectedCategory.DeleteAllQuestions();
                    Console.WriteLine("\nQuestion deleted successfully.");
                }
                else
                {
                    Console.WriteLine("\nDeletion cancelled.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                    return;
                }
            } while (AskQuestion());
            Console.Clear();
        }

        private QuizQuestion CreateQuestion(string questionText, List<string> options)
        {
            Console.Write("Enter two index of correct answer(s) with space: ");
            string correctAnswerInput = Console.ReadLine();
            Console.Clear();

            List<int> userAnswers = new List<int>();
            foreach (char c in correctAnswerInput)
            {
                if (int.TryParse(c.ToString(), out int answerIndex))
                {
                    userAnswers.Add(answerIndex - 1); // Convert to zero-based index
                }
            }

            return new QuizQuestion(questionText, options, userAnswers);

        }

        public static int IntegerInput(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                // Try to parse the input to an integer
                if (int.TryParse(input, out result))
                {
                    Console.Clear();
                    return result;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please only integer.");
                    PressAnyKeyToContinue();
                    Console.Clear();
                }
            }
        }

        public static string InputString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string userInput = Console.ReadLine();
                // Check if the input is an integer
                if (int.TryParse(userInput, out _))
                {
                    Console.WriteLine("\nInvalid input, Please enter only string");
                    PressAnyKeyToContinue();
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    return userInput;
                }
            }

        }

        public static void PressAnyKeyToContinue()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static bool AskConfirmation(string confirm)
        {
            while (true)
            {
                Console.Write(confirm);
                string input = Console.ReadLine().Trim().ToLower();

                if (input == "y")
                {
                    return true;
                }
                else if (input == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
        }

        public static bool AskQuestion()
        {
            while (true)
            {
                Console.Write("\nDo you want to make more? (y/n): ");
                string response = Console.ReadLine().Trim().ToLower();
                Console.Clear();

                if (response == "y")
                {
                    return true;
                }
                else if (response == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter 'y' or 'n'.");
                }
            }
        }
    }
}
